using UnityEngine;
using System.Collections;

public class ObjectRotate : MonoBehaviour {


	public float m_Rotate_Speed  = 0;
	public bool mIsRotation = false;
	public Transform m_transform;

	// Use this for initialization
	void Start () {

    }
	
	// Update is called once per frame
	void Update () {

		if (mIsRotation) {

			m_ObjectRotate ();
		
		}

	}
		
	public void Set_Rotate(){
		mIsRotation = !mIsRotation;
		Debug.Log ("click");
	}
    private void m_ObjectRotate()
    {
		transform.RotateAround(m_transform.position,new Vector3(0.0f,-1.0f,0.0f),Time.deltaTime * m_Rotate_Speed);

    }
}
