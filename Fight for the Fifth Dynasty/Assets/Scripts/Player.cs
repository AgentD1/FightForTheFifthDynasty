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

	public float defaultWalkDelay = 0.2f;
	float walkDelay = 0.2f;
	public float walkDistance = 0.5f;

	Vector2 directionFacing;

	DialogueObject mostRecentDialogueObject;
	bool active = true;
	bool dialogueEndedThisFrame;

	public void Start() {
		DialogueManager.dialogueManager.OnDialogueStart.AddListener(() => active = false);
		DialogueManager.dialogueManager.OnDialogueEnd.AddListener(() =>
		{
			mostRecentDialogueObject.Reset();
			active = true;
			dialogueEndedThisFrame = true;
		});
	}

	public void Update() {
		walkDelay -= Time.deltaTime;
		if (!active) {
			return;
		}

		if (walkDelay <= 0) {
			if (Input.GetKey(KeyCode.W)) {
				spriteRenderer.sprite = facingUp;
				directionFacing = Vector2.up * walkDistance;
				TryMoveInDirectionFacing();
			}
			if (Input.GetKey(KeyCode.S)) {
				spriteRenderer.sprite = facingDown;
				directionFacing = Vector2.down * walkDistance;
				TryMoveInDirectionFacing();
			}
			if (Input.GetKey(KeyCode.A)) {
				spriteRenderer.sprite = facingLeft;
				directionFacing = Vector2.left * walkDistance;
				TryMoveInDirectionFacing();
			}
			if (Input.GetKey(KeyCode.D)) {
				spriteRenderer.sprite = facingRight;
				directionFacing = Vector2.right * walkDistance;
				TryMoveInDirectionFacing();
			}
		}

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

	void TryMoveInDirectionFacing() {
		if (!Physics2D.CircleCast((Vector2)transform.position + directionFacing, 0.25f, directionFacing, 0.25f)) {
			transform.Translate(directionFacing);
			walkDelay = defaultWalkDelay;
		}
	}
}
