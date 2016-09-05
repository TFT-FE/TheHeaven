using UnityEngine;
using System.Collections;

public class NetworkLauncher : Photon.MonoBehaviour {

    #region Private Attribute

	public GameObject controlPanel;
	public GameObject mControlPanelPopQuitGame;
	#endregion

    #region Private Attribute

	/// <summary>
	/// This client's version number. Users are separated from each other by gameversion (which allows you to make breaking changes).
	/// </summary>
	/// 
	const string mVERSION = "v0.1";


	byte maxPlayersPerRoom = 4;

	/// <summary>
	/// Keep track of the current process. Since connection is asynchronous and is based on several callbacks from Photon, 
	/// we need to keep track of this to properly adjust the behavior when we receive call back by Photon.
	/// Typically this is used for the OnConnectedToMaster() callback.
	/// </summary>
	bool isConnecting;

	#endregion

    #region Methods call by Unity

	void Awake(){

		// #NotImportant
		// Force LogLevel
		PhotonNetwork.logLevel = PhotonLogLevel.ErrorsOnly;

		// #Critical
		// we don't join the lobby. There is no need to join a lobby to get the list of rooms.
		PhotonNetwork.autoJoinLobby = false;

		// #Critical
		// this makes sure we can use PhotonNetwork.LoadLevel() on the master client and all clients in the same room sync their level automatically
		PhotonNetwork.automaticallySyncScene = true;

	}

	void Update(){

		if (Input.GetKey (KeyCode.Escape)) {

			mControlPanelPopQuitGame.SetActive (true);
		}
	}

	#endregion

    #region Methods call by PhotonNetwork Behaviour

	/// <summary>
	/// Called after the connection to the master is established and authenticated but only when PhotonNetwork.autoJoinLobby is false.
	/// </summary>
	void OnConnectedToMaster()
	{

		Debug.Log("Region:"+PhotonNetwork.networkingPeer.CloudRegion);

		// we don't want to do anything if we are not attempting to join a room. 
		// this case where isConnecting is false is typically when you lost or quit the game, when this level is loaded, OnConnectedToMaster will be called, in that case
		// we don't want to do anything.
		if (isConnecting)
		{
			Debug.Log("DemoAnimator/Launcher: OnConnectedToMaster() was called by PUN. Now this client is connected and could join a room.\n Calling: PhotonNetwork.JoinRandomRoom(); Operation will fail if no room found");

			// #Critical: The first we try to do is to join a potential existing room. If there is, good, else, we'll be called back with OnPhotonRandomJoinFailed()
			PhotonNetwork.JoinRandomRoom();
		}
	}


	//void OnJoinedLobby(){

	//	PhotonNetwork.JoinRandomRoom();

	//}

	void OnPhotonRandomJoinFailed(object[] codeAndMsg){

		Debug.Log("DemoAnimator/Launcher:OnPhotonRandomJoinFailed() was called by PUN. No random room available, so we create one.\nCalling: PhotonNetwork.CreateRoom(null, new RoomOptions() {maxPlayers = 4}, null);");

		// #Critical: we failed to join a random room, maybe none exists or they are all full. No worries, we create a new room.
		RoomOptions roomOpeionts = new RoomOptions (){ MaxPlayers = maxPlayersPerRoom };

		PhotonNetwork.CreateRoom(null, roomOpeionts, null);

	}

	void OnDisconnectedFromPhoton(){

		Debug.LogError("DemoAnimator/Launcher:Disconnected");

		// #Critical: we failed to connect or got disconnected. There is not much we can do. Typically, a UI system should be in place to let the user attemp to connect again.
		//loaderAnime.StopLoaderAnimation();

		// #Critical: we failed to connect or got disconnected. There is not much we can do. Typically, a UI system should be in place to let the user attemp to connect again.


		isConnecting = false;
		controlPanel.SetActive(true);
	}

	void OnJoinedRoom(){
      
		LoadingScencManager.LoadScene (2,true);

	}
	#endregion


	#region public Methods


	/// <summary>
	/// Start the connection process. 
	/// - If already connected, we attempt joining a random room
	/// - if not yet connected, Connect this application instance to Photon Cloud Network
	/// </summary>
	public void Connect()
	{
		// keep track of the will to join a room, because when we come back from the game we will get a callback that we are connected, so we need to know what to do then
		isConnecting = true;

		// hide the Play button for visual consistency
		controlPanel.SetActive(false);

		// start the loader animation for visual effect.

		GetComponent<Connecting> ().ShowLoadingConnecting ();

		// we check if we are connected or not, we join if we are , else we initiate the connection to the server.
		if (PhotonNetwork.connected)
		{
			// #Critical we need at this point to attempt joining a Random Room. If it fails, we'll get notified in OnPhotonRandomJoinFailed() and we'll create one.
			PhotonNetwork.JoinRandomRoom();

		}else{

			// #Critical, we must first and foremost connect to Photon Online Server.
			PhotonNetwork.ConnectUsingSettings(mVERSION);
		}
	}
		
	#endregion

	public void QuitGame(){

		Application.Quit ();
	}
	public void ClosePanelPopup()
	{
		mControlPanelPopQuitGame.SetActive (false);
	}
}
