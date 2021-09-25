using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject : MonoBehaviour {
	public List<DialoguePart> dialogueParts;

	public IEnumerator<DialoguePart> dialoguePartEnumerator;

	public void Start() {
		Reset();
	}

	public void Reset() {
		dialoguePartEnumerator = ((IEnumerable<DialoguePart>)dialogueParts).GetEnumerator();
	}
}
