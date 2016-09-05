using UnityEngine;
using System.Collections;

public class Connecting : MonoBehaviour {

	public GameObject[] gameobjects;

	void Start(){

		foreach (GameObject obj in gameobjects) {

			obj.SetActive (false);

		}
	}

	public void ShowLoadingConnecting(){

		foreach (GameObject obj in gameobjects) {

			obj.SetActive (true);

		}
			
	}

}
