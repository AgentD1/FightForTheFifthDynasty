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

	public float letterTime;
	float remainingLetterTime;
	string currentText = "";

	[Header("UI Stuff")]
	public GameObject dialogueRootObject;
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
		Debug.Log("Dialogue starting");
		dialogueRootObject.SetActive(true);
		AdvanceDialogue();
		OnDialogueStart.Invoke();
	}

	public void EndDialogue() {
		Debug.Log("Dialogue ending");
		currentDialogue = null;
		dialogueRootObject.SetActive(false);
		OnDialogueEnd.Invoke();
	}

	public void Update() {
		if (currentDialogue == null) return;

		if (currentText.Length != currentDialoguePart.text.Length) {
			remainingLetterTime -= Time.deltaTime;
			if (remainingLetterTime < 0) {
				currentText = currentDialoguePart.text.Substring(0, currentText.Length + 1);
				dialogueText.text = currentText;
				remainingLetterTime = letterTime;
			}
			if (Input.GetKeyDown(KeyCode.Space)) {
				currentText = currentDialoguePart.text;
				dialogueText.text = currentText;
			}
		} else {
			if (Input.GetKeyDown(KeyCode.Space)) {
				AdvanceDialogue();
			}
		}


		/*if (Input.GetKeyDown(KeyCode.Space)) {
			if (finishedCurrentDialoguePart) {
				AdvanceDialogue();
			}
		}*/
	}

	public void AdvanceDialogue() {
		if (currentDialogue.MoveNext()) {
			remainingLetterTime = 0;
			currentDialoguePart = currentDialogue.Current;
			Debug.Log(currentDialoguePart.text);
			if (currentDialoguePart == null) {
				EndDialogue();
				return;
			}
			currentText = "";
			dialogueText.text = "";
			profileSpriteRenderer.gameObject.SetActive(currentDialoguePart.profilePicture != null);
			if (currentDialoguePart.profilePictureOnRight) {
				profileSpriteRenderer.transform.SetAsLastSibling();
			} else {
				profileSpriteRenderer.transform.SetAsFirstSibling();
			}
			profileSpriteRenderer.sprite = currentDialoguePart.profilePicture;
			finishedCurrentDialoguePart = false;
			OnDialogueProgress.Invoke();
		} else {
			EndDialogue();
		}
	}
}
