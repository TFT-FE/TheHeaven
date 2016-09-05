using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MusicControll : MonoBehaviour {

	public GameObject mAudio;
	public PlayMusicSound mPlayMusicSound; 

	void Awake () {
		
		mAudio = GameObject.FindGameObjectWithTag("Music");
		mPlayMusicSound = mAudio.GetComponent<PlayMusicSound> ();

	}
				
	public void StopAndPlayAudio(){

		mPlayMusicSound.mMuteAudio = !mPlayMusicSound.mMuteAudio;

		if (mPlayMusicSound.mMuteAudio) {
			
			mPlayMusicSound.mAudio.Play ();
	
		} else {
			
			mPlayMusicSound.mAudio.Pause ();
		}
	}


}
