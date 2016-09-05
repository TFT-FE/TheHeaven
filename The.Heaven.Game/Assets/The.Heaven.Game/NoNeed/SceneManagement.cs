using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneManagement : MonoBehaviour {

	public GameObject mLoadingPanel;
	public Image mImagePercentage;
	public Text mTextPercentage;
	public GameObject mButton;
	private bool isSceneChange = false;
	AsyncOperation mAsync = null;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {

		if (isSceneChange){

			StartCoroutine (SceneLoading());
		}

	}
		

	public void ChangeScene(int index){

		isSceneChange = true;
		mLoadingPanel.SetActive(true);
		mButton.SetActive (false);
		mAsync = SceneManager.LoadSceneAsync(index);
		mAsync.allowSceneActivation = false;
	}

	IEnumerator SceneLoading(){

		float percentage = 0;
		while (!mAsync.isDone) {

			if (mAsync.progress < 0.9f) {

				mImagePercentage.fillAmount = mAsync.progress / 0.9f;
				percentage = mAsync.progress * 100;
				mTextPercentage.text = (int)percentage + "%";
			} else {

				mImagePercentage.fillAmount = mAsync.progress / 0.9f;
				percentage = (mAsync.progress/ 0.9f) * 100;
				mTextPercentage.text = (int)percentage + "%";
				mAsync.allowSceneActivation = true;
			}

			yield return null;
		}

		yield return mAsync;
	}

}
