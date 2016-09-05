using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class NetworkManager : Photon.MonoBehaviour
{

    #region Public Attribute

    public GameObject mControlPanelPopUpOut;
	public GameObject mControlPanelPopQuitGame;
    public GameObject mStartBtn;
    public GameObject mControlPanelStartGame;
    public GameObject mControlPanelUI;
    public Text mTextFeedBack;
    public GameObject mPlayerPrefab;

    #endregion

    #region Private Attribute

    private bool instantiate = false;
    #endregion

    #region Methods call by MonoBehaviour Behaviour

    void Start()
    {
        Debug.Log(PhotonNetwork.connectionStateDetailed);

        // TODO : After Test completed need to uncommand is line

       // mControlPanelStartGame.SetActive(true);

    }

    void Update()
    {
		if (Input.GetKey (KeyCode.Escape)) {

			mControlPanelPopQuitGame.SetActive (true);
		}

        if (!PhotonNetwork.isMasterClient)
        {
            mStartBtn.SetActive(false);
            mTextFeedBack.text = "You are Client. Player in room have " + PhotonNetwork.room.playerCount + "";
        }
        if (instantiate == false)
        {
            int index = PhotonNetwork.player.ID;
            InstantiatePlayer(index);
            instantiate = true;
        }
    }

    void FixedUpdate()
    {
        if (PhotonNetwork.isMasterClient)
        {
            mStartBtn.SetActive(true);
            mTextFeedBack.text = "You are Master Client. Player in room have " + PhotonNetwork.room.playerCount + "";
        }
    }

    #endregion

    #region Photon Messages

    void OnPhotonPlayerDisconnected(PhotonPlayer other)
    {
        Debug.Log("OnPhotonPlayerDisconnected() " + other.name); // seen when other disconnects

        if (PhotonNetwork.isMasterClient)
        {
            Debug.Log("OnPhotonPlayerConnected isMasterClient " + PhotonNetwork.isMasterClient); // called before OnPhotonPlayerDisconnected
            Debug.Log("PhotonNetwork : Loading Level : " + PhotonNetwork.room.playerCount);
        }
    }
    /// <summary>
    /// Called when the local player left the room. We need to load the launcher scene.
    /// </summary>
    void OnLeftRoom()
    {
		LoadingScencManager.LoadScene (1);
    }

    #endregion

    #region public Methods

    public GameObject InstantiatePlayer(int index)
    {
        Transform[] mSpawnPoint = GameObject.FindGameObjectWithTag("SpawnPoint").GetComponentsInChildren<Transform>();

        switch (index)
        {
            case 1:
                {
                    GameObject playerObj = PhotonNetwork.Instantiate(mPlayerPrefab.name, mSpawnPoint[1].position, mSpawnPoint[1].rotation, 0);
                    playerObj.GetComponentInChildren<TextMesh>().text = PhotonNetwork.playerList[0].name;
                    return playerObj;
                }
            case 2:
                {
                    GameObject playerObj = PhotonNetwork.Instantiate(mPlayerPrefab.name, mSpawnPoint[2].position, mSpawnPoint[2].rotation, 0);
                    playerObj.GetComponentInChildren<TextMesh>().text = PhotonNetwork.playerList[0].name;
                    return playerObj;
                }
            case 3:
                {
                    GameObject playerObj = PhotonNetwork.Instantiate(mPlayerPrefab.name, mSpawnPoint[3].position, mSpawnPoint[3].rotation, 0);
                    playerObj.GetComponentInChildren<TextMesh>().text = PhotonNetwork.playerList[0].name;
                    return playerObj;
                }
            case 4:
                {
                    GameObject playerObj = PhotonNetwork.Instantiate(mPlayerPrefab.name, mSpawnPoint[4].position, mSpawnPoint[4].rotation, 0);
                    playerObj.GetComponentInChildren<TextMesh>().text = PhotonNetwork.playerList[0].name;
                    return playerObj;
                }
        }
        return null;
    }

    [PunRPC]
    void ClosePanelStartGameNetwork(bool flag)
    {
        mControlPanelStartGame.SetActive(!flag);
        mControlPanelUI.SetActive(flag);
        GameObject.Find("GameManager").GetComponent<GameManager>().StartTurn();
    }

    public void OpenPanelStartGame()
    {
        photonView.RPC("ClosePanelStartGameNetwork", PhotonTargets.AllBuffered,false);
    }
    public void ClosePanelStartGame()
    {
        if (PhotonNetwork.playerList.Length > 1)
        {
            photonView.RPC("ClosePanelStartGameNetwork", PhotonTargets.AllBuffered,true);
        }
    }

    public void OpenPanelUI()
    {
        mControlPanelUI.SetActive(true);
    }

    public void ClosePanelPopup()
    {
        mControlPanelPopUpOut.SetActive(false);
		mControlPanelPopQuitGame.SetActive (false);
    }

    public void ShowPanelPopup()
    {
        mControlPanelPopUpOut.SetActive(true);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }
	public void QuitGame(){
	
		Application.Quit ();
	}
    #endregion

}
