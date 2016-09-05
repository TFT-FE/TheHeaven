using UnityEngine;
using System.Collections;

public class SwipeTest : MonoBehaviour {

	public float maxTime;
	public float minSwipeDist;
	public float mRotate_Speed  = 5;

	float startTime;
	float endTime;

	Vector3 startPos;
	Vector3 endPos;

	float swipeDistance;
	float swipeTime;

	public CameraControl mCameraControl;

	// Use this for initialization
	void Start () {
	
		mCameraControl = GameObject.Find ("CameraControl").GetComponent<CameraControl>();

	}
	
	// Update is called once per frame
	void FixedUpdate () {

		if (Input.touchCount > 0) {

			Touch touch = Input.GetTouch (0);

			if (touch.phase == TouchPhase.Began) {

				startTime = Time.time;
				startPos = touch.position;

			} else if (touch.phase == TouchPhase.Moved){

				endTime = Time.time;
				endPos = touch.position;

				swipeDistance = (endPos - startPos).magnitude;
				swipeTime = endTime - startTime;

				if (swipeTime < maxTime && swipeDistance > minSwipeDist) {

					swipe (mRotate_Speed);
				}

			}
			else if (touch.phase == TouchPhase.Ended) {
				
				startPos = new Vector3 ();
			}
		}

	}

	public void swipe(float mRotate_Speed){

		Vector2 dicstance = endPos - startPos;

		if (Mathf.Abs (dicstance.x) > Mathf.Abs (dicstance.y)) {

			Debug.Log ("Horizontal Swipe");

			if (dicstance.x > 0) {
				Debug.Log ("Swipe Right");
				mCameraControl.RotateCameraOnAxis_Y (new Vector3 (0f, 1.0f, 0f),mRotate_Speed);
			}
			if (dicstance.x < 0) {
				Debug.Log ("Swipe left");
				mCameraControl.RotateCameraOnAxis_Y (new Vector3 (0f, -1.0f, 0f),mRotate_Speed);
			}

		} else if (Mathf.Abs (dicstance.x) < Mathf.Abs (dicstance.y)) {

			Debug.Log ("Vertical Swipe");

			if (dicstance.y > 0) {
				Debug.Log ("Swipe Up");
			}
			if (dicstance.y < 0) {
				Debug.Log ("Swipe Down");
			}
		}

	}

}
