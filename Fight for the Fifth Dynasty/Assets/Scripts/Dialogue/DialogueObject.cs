using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueObject : MonoBehaviour {
	public List<DialoguePart> dialogueParts;

	public IEnumerator<DialoguePart> dialoguePartEnumerator;

	public void Start() {
		dialoguePartEnumerator = ((IEnumerable<DialoguePart>)dialogueParts).GetEnumerator();
	}
}
