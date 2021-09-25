using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Player : MonoBehaviour {
	public SpriteRenderer Spriterender;
	public Sprite FacingLeft;
	public Sprite FacingRight;

	Vector2 directionFacing;

	bool active = true;

	public void Start() {
		DialogueManager.dialogueManager.OnDialogueStart.AddListener(() => active = false);
		DialogueManager.dialogueManager.OnDialogueEnd.AddListener(() => active = true);
	}

	public void Update() {
		if (!active) {
			return;
		}

		if (Input.GetKeyDown(KeyCode.W)) {
			directionFacing = Vector2.up;
			TryMoveInDirectionFacing();
		}
		if (Input.GetKeyDown(KeyCode.S)) {
			directionFacing = Vector2.down;
			TryMoveInDirectionFacing();
		}
		if (Input.GetKeyDown(KeyCode.A)) {
			Spriterender.sprite = FacingLeft;
			directionFacing = Vector2.left;
			TryMoveInDirectionFacing();
		}
		if (Input.GetKeyDown(KeyCode.D)) {
			Spriterender.sprite = FacingRight;
			directionFacing = Vector2.right;
			TryMoveInDirectionFacing();
		}

		if (Input.GetKeyDown(KeyCode.Space)) {
			Collider2D[] hit = Physics2D.OverlapCircleAll((Vector2)transform.position + directionFacing, 0.25f);
			DialogueObject dialogue = (from h in hit
			                           where h.GetComponent<DialogueObject>() != null
			                           select h.GetComponent<DialogueObject>()).FirstOrDefault();
			
			Debug.Log((Vector2)transform.position + directionFacing);
			Debug.Log(dialogue);
			if (dialogue != null) {
				DialogueManager.dialogueManager.SetDialogue(dialogue.dialoguePartEnumerator);
				DialogueManager.dialogueManager.StartDialogue();
			}
		}
	}

	void TryMoveInDirectionFacing() {
		if (!Physics2D.CircleCast((Vector2)transform.position + directionFacing, 0.25f, directionFacing, 0.25f)) {
			transform.Translate(directionFacing);
		}
	}
}
