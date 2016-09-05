using UnityEngine;
using System.Collections;

public class CameraMoveOnPath : MonoBehaviour {

	public Transform CenterPoint;
	public float Rotation_Speed = 5.0f;
	
	public float angle = 0.0f;
	public float MaximumAngle = 360.0f;
	bool flag = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

		//if (Input.touchCount > 0 ){

			Touch touch = Input.GetTouch (0);

		if ( touch.tapCount == 1) {

				flag = false;

			}

			if (flag == false) {

				transform.RotateAround (CenterPoint.position, new Vector3 (0.0f, -1.0f, 0.0f), Time.deltaTime * Rotation_Speed);

				angle = angle > 45.0f ? 45.0f : Mathf.Abs (MaximumAngle - transform.eulerAngles.y);

				if (angle == 45.0f) {

					//transform.eulerAngles = new Vector3(transform.eulerAngles.x,Mathf.Ceil(transform.eulerAngles.y),transform.eulerAngles.z);

					MaximumAngle = MaximumAngle == 0 ? MaximumAngle = 360.0f : MaximumAngle - angle;

					flag = true;

				}
			}

		}

	//}



}
