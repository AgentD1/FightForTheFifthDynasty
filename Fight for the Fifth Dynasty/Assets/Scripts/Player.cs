using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Player : MonoBehaviour {
	public SpriteRenderer Spriterender;
	public Sprite FacingLeft;
	public Sprite FacingRight;
	public Sprite FacingUp;
	public Sprite FacingDown;

	static float t = 0.2f;

	Vector2 directionFacing;

	DialogueObject mostRecentDialogueObject;
	bool active = true;
	bool dialogueEndedThisFrame = false;

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

		t -= 0.01f - Time.deltaTime;

		if (t <= 0) { 
		if (!active) {
			return;
		}

		if (Input.GetKey(KeyCode.W)) {
			Spriterender.sprite = FacingUp;
			directionFacing = Vector2.up / 2;
			TryMoveInDirectionFacing();
		}
		if (Input.GetKey(KeyCode.S)) {
			Spriterender.sprite = FacingDown;
			directionFacing = Vector2.down / 2;
			TryMoveInDirectionFacing();
		}
		if (Input.GetKey(KeyCode.A)) {
			Spriterender.sprite = FacingLeft;
			directionFacing = Vector2.left / 2;
			TryMoveInDirectionFacing();
		}
		if (Input.GetKey(KeyCode.D)) {
				Spriterender.sprite = FacingRight;
				directionFacing = Vector2.right / 2;
				TryMoveInDirectionFacing();
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
		t = 0.2f;
	}

	void TryMoveInDirectionFacing()
		{
			if (!Physics2D.CircleCast((Vector2)transform.position + directionFacing, 0.25f, directionFacing, 0.25f))
			{
				transform.Translate(directionFacing);
			}
		}

	}
}
