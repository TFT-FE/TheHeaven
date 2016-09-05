using UnityEngine;
using System.Collections;

public class CameraControl : Photon.MonoBehaviour {

    #region Public Attribute

	//public bool mIsRotation = false;
	//public Vector3 mAxisToRoatate;
	public Transform mCenterTransform;
	public Camera mMainCamera;
    public MoveOnPath mMoveOnPath;
    #endregion

    #region Private Attribute

    //private Transform[] mSpawnPosition;
    public float mAngle = 0;
	#endregion

    #region Methods call by Unity Behaviour

	void Start (){
        //	mSpawnPosition = GameObject.FindGameObjectWithTag ("SpawnPoint").GetComponentsInChildren<Transform> ();
        mMainCamera = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();
        mCenterTransform = GameObject.FindGameObjectWithTag("Land").GetComponent<Transform>();
        RatateToObject();
    }

	void Update(){

       // if (photonView.isMine)
       // {
        //    int index = PhotonNetwork.player.ID - 1;
           // RotateWhenObjectMove(index);

      //  }
    }
	#endregion

    // TODO : Ratate Camera while player move 
    public void RotateWhenObjectMove(int index)
    {

        switch (index)
        {
            case 0:
                {
                    if (mMoveOnPath.mMoveNum[index] >= 2 && mMoveOnPath.mMoveNum[index]  < 3 )
                    {
                       
                        mMainCamera.transform.RotateAround(mCenterTransform.position, new Vector3(0.0f, -1.0f, 0.0f), Time.deltaTime * 15);
                        mAngle++;

                        if (mAngle == 235)
                        {
                            mAngle = 0;
                        }
                    
                    }
                    if (mMoveOnPath.mMoveNum[index] >= 4 && mMoveOnPath.mMoveNum[index] < 5)
                    {
                        if (mAngle < 235)
                        {
                            mMainCamera.transform.RotateAround(mCenterTransform.position, new Vector3(0.0f, -1.0f, 0.0f), Time.deltaTime * 15);
                            mAngle++;
                        }
                        else
                        {
                            mAngle = 0;
                        }
                    }
                }
                break;
        }

    }

	public void RotateCameraOnAxis_Y(Vector3 axis , float rotSpeed){

		mMainCamera.transform.RotateAround (mCenterTransform.position, axis, Time.deltaTime * rotSpeed );
	}

	public void RatateToObject(){

		if (PhotonNetwork.player.ID.Equals(2)) {		
			mMainCamera.transform.RotateAround (mCenterTransform.position, new Vector3 (0.0f, -180.0f, 0.0f), 180);
		}

		if (PhotonNetwork.player.ID.Equals(3)) {
			mMainCamera.transform.RotateAround (mCenterTransform.position, new Vector3 (0.0f, 45.0f, 0.0f), 90);
		}

		if (PhotonNetwork.player.ID.Equals(4)) {
			mMainCamera.transform.RotateAround (mCenterTransform.position, new Vector3 (0.0f, -45.0f, 0.0f), 90);
		}
	}
}
