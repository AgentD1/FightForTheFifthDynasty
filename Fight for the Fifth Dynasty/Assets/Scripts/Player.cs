using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Player : MonoBehaviour {
	public SpriteRenderer spriteRenderer;

	public Sprite facingLeft;
	public Sprite facingRight;
	public Sprite facingUp;
	public Sprite facingDown;

	public AudioSource drink;

	public float walkSpeed = 2.5f;

	public float health = 3;
	public float maxHealth = 5;

	public static Player instance;

	Rigidbody2D rigidbody2D;

	[SerializeField] private UI_Inventory uiInventory;
	private Inventory inventory;

	Vector2 directionFacing;

	DialogueObject mostRecentDialogueObject;
	bool active = true;
	bool dialogueEndedThisFrame;

	public void Awake() {
		if (instance != null) {
			Debug.LogError("There's 2 player objects. Screw off.");
			return;
		}
		instance = this;

		inventory = new Inventory(UseItem);
		uiInventory.SetPlayer(this);
		uiInventory.SetInventory(inventory);

		ItemWorld.SpawnItemWorld(new Vector3(2,2), new Item {itemType = Item.ItemType.Potion, amount = 1});
	}

	public void Start() {
		rigidbody2D = GetComponent<Rigidbody2D>();

		DialogueManager.dialogueManager.OnDialogueStart.AddListener(() => active = false);
		DialogueManager.dialogueManager.OnDialogueEnd.AddListener(() => {
			mostRecentDialogueObject.Reset();
			active = true;
			dialogueEndedThisFrame = true;
		});
	}

	public void FixedUpdate() {
		Vector2 desiredVelocity = Vector2.zero;

		if (Input.GetKey(KeyCode.W)) {
			spriteRenderer.sprite = facingUp;
			directionFacing = Vector2.up;
			desiredVelocity += Vector2.up;
		}
		if (Input.GetKey(KeyCode.S)) {
			spriteRenderer.sprite = facingDown;
			directionFacing = Vector2.down;
			desiredVelocity += Vector2.down;
		}
		if (Input.GetKey(KeyCode.A)) {
			spriteRenderer.sprite = facingLeft;
			directionFacing = Vector2.left;
			desiredVelocity += Vector2.left;
		}
		if (Input.GetKey(KeyCode.D)) {
			spriteRenderer.sprite = facingRight;
			directionFacing = Vector2.right;
			desiredVelocity += Vector2.right;
		}

		if (desiredVelocity != Vector2.zero) {
			desiredVelocity.Normalize();
		}

		rigidbody2D.velocity = desiredVelocity * (walkSpeed * Time.fixedDeltaTime);
	}

	public void Update() {
		if (!dialogueEndedThisFrame && Input.GetKeyDown(KeyCode.Space)) {
			Collider2D[] hit = Physics2D.OverlapCircleAll((Vector2)transform.position + directionFacing, 0.25f);
			DialogueObject dialogue = (from h in hit
			                           where h.GetComponent<DialogueObject>() != null
			                           select h.GetComponent<DialogueObject>()).FirstOrDefault();

			if (dialogue != null) {
				mostRecentDialogueObject = dialogue;
				DialogueManager.dialogueManager.SetDialogue(dialogue.dialoguePartEnumerator);
				DialogueManager.dialogueManager.StartDialogue();
			}
		}
		dialogueEndedThisFrame = false;
	}

	private void OnTriggerEnter2D(Collider2D collider){
		ItemWorld itemWorld = collider.GetComponent<ItemWorld>();
		if (itemWorld != null){
			//Touchng Item
			inventory.AddItem(itemWorld.GetItem());
			itemWorld.DestroySelf();
		}
	}

	private void UseItem(Item item){
		switch(item.itemType){
		case Item.ItemType.Potion:
			if (health < 5){
				health = health + 1;
				inventory.RemoveItem(new Item { itemType = Item.ItemType.Potion, amount = 1});
				drink.Play();
			}
			break;
		case Item.ItemType.Sword:
			break;
		}
	}
}
