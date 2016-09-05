using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NetworkPlayer : Photon.MonoBehaviour{


    private Vector3 realPosition = Vector3.zero;
    private Quaternion realRotation = Quaternion.identity;

    // Use this for initialization
    void Start () {

      //  PhotonNetwork.sendRate = 20;
      //  PhotonNetwork.sendRateOnSerialize = 20;

    }
    void FixedUpdate()
    {
        SmoothMovement();
    }

    void SmoothMovement()
    {
        if (photonView.isMine)
        {

        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, realPosition, Time.deltaTime * 5);
            transform.rotation = Quaternion.Lerp(transform.rotation, realRotation, Time.deltaTime * 5);
        }
    }

    void OnPhotonSerializeView(PhotonStream stream , PhotonMessageInfo info){

		if (stream.isWriting) {

			stream.SendNext (transform.position);
			stream.SendNext (transform.rotation);

		} else {

            realPosition = (Vector3) stream.ReceiveNext();
            realRotation = (Quaternion)stream.ReceiveNext();

		}


	}


}
