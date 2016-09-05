using UnityEngine;
using UnityEngine.UI;

using System.Collections;

public class TextNamePlayer : Photon.MonoBehaviour
{

    public Text[] mArray_TextName;
    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < PhotonNetwork.playerList.Length; i++)
        {
            int indexID = PhotonNetwork.playerList[i].ID - 1;
            string name = PhotonNetwork.playerList[indexID].name;
            mArray_TextName[indexID].text = name;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        int indexID = newPlayer.ID - 1;
        string name = newPlayer.name;

        // Set to name to UI
        mArray_TextName[indexID].text = name;
    }

}
