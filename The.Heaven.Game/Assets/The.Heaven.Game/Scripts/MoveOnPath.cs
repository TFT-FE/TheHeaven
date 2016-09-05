using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MoveOnPath : Photon.MonoBehaviour
{

    #region Public Attribute

    public PathPoints[] mMoveOnPath;
    public int[] mCurrentWayPointID = new int[3];
    public float mSpeed = 0.5f;
    public float mRotateSpeed = 5.0f;
    public string mPathName;
    public int[] mMoveNum = new int[3];
    #endregion

    #region Private Attribute

    // private float reachDistance = 0.01f;
    private int[] mPathHolderID = new int[3];
    // private List<GameObject> mPath_ObjsList = new List<GameObject>();
    private GameObject[] mArray_Objs;
    private GameManager mGameManager;
    private Vector3 mCurrentPosition;
    #endregion

    #region Callback Mehtods In Unity
    // Use this for initialization
    void Start()
    {
        mGameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        GetPathPoints();

        for (int i = 0; i < mPathHolderID.Length; i++)
        {
            mPathHolderID[i] = 0;
            mMoveNum[i] = -1;
        }

    }

    // Update is called once per frame
    void Update()
    {
        if (photonView.isMine)
        {
            int index = PhotonNetwork.player.ID - 1;
            PlayerMoveByIndex(index);
        }

    }
    #endregion

    #region Private Custom Methods
    void GetPathPoints()
    {
        mArray_Objs = GameObject.FindGameObjectsWithTag("Path");

        mMoveOnPath[0] = mArray_Objs[1].GetComponent<PathPoints>();
        mMoveOnPath[1] = mArray_Objs[2].GetComponent<PathPoints>();
        mMoveOnPath[2] = mArray_Objs[0].GetComponent<PathPoints>();

    }

    private void PlayerMoveByIndex(int index)
    {
        //TODO : When selected answer True or False
        //if (mgamemanager.mplayerselected_brain && mgamemanager != null)
        //{
        //    if (mmovenum[index] != -1)
        //    {
        //        mcurrentwaypointid[index]++;
        //    }
        //    mmovenum[index]++;

        //}

        if (mPathHolderID[index] < 3 && mMoveNum[index] > -1)
        {
            // TODO : Not yet to test
            DifferentMoveForEachPlayer(index);

            PathPoints pathPoint = mMoveOnPath[mPathHolderID[index]];
            Transform pathPointPosition = pathPoint.mPath_Objs[mCurrentWayPointID[index]];

            // float distance = Vector3.Distance(pathPointPosition.position, transform.position);
            transform.position = Vector3.MoveTowards(transform.position, pathPointPosition.position, Time.deltaTime * mSpeed);

            var rotation = Quaternion.LookRotation(pathPointPosition.position - transform.position);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, Time.deltaTime * mRotateSpeed);


        }
    }
    private void DifferentMoveForEachPlayer(int index)
    {
        switch (index)
        {
            case 0:
                {
                    if (mCurrentWayPointID[index] >= mMoveOnPath[mPathHolderID[index]].mPath_Objs.Count)
                    {
                        mCurrentWayPointID[index] = 0;
                        mPathHolderID[index]++;
                        mMoveNum[index] = 0;
                    }
                }
                break;
            case 1:
                {
                    PartOfDifferentMove(index, 4);
                }
                break;
            case 2:
                {
                    PartOfDifferentMove(index, 6);
                }
                break;
            case 3:
                {
                    PartOfDifferentMove(index, 2);
                }
                break;
        }
    }

    private void PartOfDifferentMove(int index, int value)
    {
        if (mCurrentWayPointID[index] >= mMoveOnPath[mPathHolderID[index]].mPath_Objs.Count)
        {
            mCurrentWayPointID[index] = 1;
        }

        if (mMoveNum[index] == 9)
        {
            mCurrentWayPointID[index] = value;
            mPathHolderID[index]++;

            if (mPathHolderID[index] == 2)
            {
                mCurrentWayPointID[index] = 0;
            }

            mMoveNum[index] = 0;
        }
    }
    #endregion
}
