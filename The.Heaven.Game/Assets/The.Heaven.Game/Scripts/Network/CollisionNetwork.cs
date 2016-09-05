using UnityEngine;
using System.Collections;

public class CollisionNetwork : Photon.MonoBehaviour
{

    private NetworkManager mNetworkManager;
    public int ID;

    // Use this for initialization
    void Start()
    {
        mNetworkManager = GameObject.Find("NetworkManager").GetComponent<NetworkManager>();
    }

    void Awake()
    {
        if (photonView.isMine)
            photonView.RPC("ChangeMyName", PhotonTargets.AllBuffered, PhotonNetwork.playerList.Length.ToString());
    }

    [PunRPC]
    void ChangeMyName(string myNewName)
    {
        gameObject.transform.name = myNewName;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "PlayerCollision")
        {
            if (photonView.isMine)
            {
                RespawnPlayer();
            }
        }
    }
    public void RespawnPlayer()
    {
        int index = GetComponent<PhotonView>().owner.ID;
        if (GetComponent<PhotonView>().instantiationId.Equals(0))
        {
            Destroy(gameObject);
        }
        else
        {
            if (GetComponent<PhotonView>().isMine)
            {
                PhotonNetwork.Destroy(gameObject);
                mNetworkManager.InstantiatePlayer(index);
            }
        }
    }

}
