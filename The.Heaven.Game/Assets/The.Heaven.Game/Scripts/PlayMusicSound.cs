using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

[RequireComponent(typeof (AudioSource))]
public class PlayMusicSound : MonoBehaviour {

	public AudioSource mAudio;
	public AudioClip mAudioClip;
	public bool mMuteAudio;
	// Use this for initialization
	void Start () {

		SceneManager.LoadScene (1, LoadSceneMode.Additive);
		mAudio.clip = mAudioClip;
		mAudio.loop = true;
		mAudio.Play ();
		mMuteAudio = mAudio.isPlaying;
		DontDestroyOnLoad (this.gameObject);
	}
		
}
