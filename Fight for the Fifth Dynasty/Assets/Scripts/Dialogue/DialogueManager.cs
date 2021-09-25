using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SearchService;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

public class DialogueManager : MonoBehaviour {
	public static DialogueManager dialogueManager { get; private set; }

	public UnityEvent OnDialogueStart, OnDialogueEnd, OnDialogueProgress;

	IEnumerator<DialoguePart> currentDialogue;
	DialoguePart currentDialoguePart;
	bool finishedCurrentDialoguePart;

	[Header("UI Stuff")]
	public Transform dialogueRootObject;
	public TextMeshProUGUI dialogueText;
	public Image profileSpriteRenderer;

	void Awake() {
		if (dialogueManager != null) {
			Debug.LogError("Dialogue Manager being reinitialized!");
			return;
		}
		dialogueManager = this;
	}

	public void SetDialogue(IEnumerator<DialoguePart> enumerator) {
		currentDialogue = enumerator;
	}

	public void StartDialogue() {
		dialogueRootObject.gameObject.SetActive(true);
		AdvanceDialogue();
		OnDialogueStart.Invoke();
	}

	public void EndDialogue() {
		currentDialogue = null;
		dialogueRootObject.gameObject.SetActive(false);
		OnDialogueEnd.Invoke();
	}

	public void Update() {
		if (currentDialogue == null) return;
		finishedCurrentDialoguePart = true;

		if (Input.GetKeyDown(KeyCode.Space)) {
			if (finishedCurrentDialoguePart) {
				AdvanceDialogue();
			}
		}
	}

	public void AdvanceDialogue() {
		if (currentDialogue.MoveNext()) {
			currentDialoguePart = currentDialogue.Current;
			Debug.Log(currentDialoguePart.text);
			if (currentDialoguePart == null) {
				EndDialogue();
				return;
			}
			dialogueText.text = currentDialoguePart.text;
			profileSpriteRenderer.sprite = currentDialoguePart.profilePicture;
			finishedCurrentDialoguePart = false;
			OnDialogueProgress.Invoke();
		} else {
			EndDialogue();
		}
	}
}
