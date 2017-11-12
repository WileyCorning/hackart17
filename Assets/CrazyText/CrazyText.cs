using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrazyText : MonoBehaviour {


	public string displayText; //it'll break up the text by space
	public float wordDuration = 0.5f; //time for each word



	public GameObject[] textList;

	// Use this for initialization
	void Start () {
		StartCoroutine(Display());	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void DisplayText(){
		StartCoroutine(Display());
	}
		
	IEnumerator Display(){
		//determine the word list
		string[] wordList = displayText.Split(' ');




		for (int i = 0; i < wordList.Length; i++) {


			gameObject.GetComponent<AudioSource>().PlayOneShot(gameObject.GetComponent<AudioSource>().clip,1.0f);
			//loop through all the text objects
			for (int g = 0; g < textList.Length; g++) {
				//loop through children, update text....
				foreach (Transform t in textList[g].transform) {
					t.gameObject.GetComponent<TextMesh> ().text = wordList [i];
				}
			}
			yield return new WaitForSeconds(wordDuration);
		}


		//display a blank text now....
		//loop through all the text objects
		for (int g = 0; g < textList.Length; g++) {
			//loop through children, update text....
			foreach (Transform t in textList[g].transform) {
				t.gameObject.GetComponent<TextMesh> ().text = "";
			}
		}
			
	}

}
