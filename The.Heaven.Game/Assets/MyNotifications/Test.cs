using UnityEngine;
using System.Collections;

public class Test : MonoBehaviour {

	void Awake(){
		StartCoroutine("moja");
	}
	IEnumerator moja(){
		yield return new WaitForSeconds(1);
		
		MyNotifications.CallNotification("Hello World!", 3);
		MyNotifications.CallNotification("MyNotifications", 4);
		MyNotifications.CallNotification("Goodbye World!", 3);
	}
}
