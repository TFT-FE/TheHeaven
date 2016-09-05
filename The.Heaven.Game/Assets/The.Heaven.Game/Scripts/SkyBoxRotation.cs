using UnityEngine;
using System.Collections;

public class SkyBoxRotation : MonoBehaviour {

	public float mSkyBoxRotation_Speed = 1;
	
	// Update is called once per frame
	void Update () {
	
		RenderSettings.skybox.SetFloat ("_Rotation", Time.time * mSkyBoxRotation_Speed);
	}
}
