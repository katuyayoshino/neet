using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneChangeManager : MonoBehaviour{
	
	/// <summary>
	/// シーン名リスト（string）
	/// </summary>
	[SerializeField]
	static public List<string> SceneNames = new List<string>() {
		"TitleScene",
		"HomeScene",
		"MapScene",
		"BattleScene"
	};

	/// <summary>
	/// ロード画面
	/// </summary>
	public LoadPlane loadPlane = null;

	/// <summary>
	///	シーン名リスト
	/// </summary>
	public enum SCENE_NAME {
		TITLE,
		HOME,
		MAP,
		BATTELE
	}
	
	/// <summary>
	/// シーン名取得
	/// </summary>
	/// <param name="_sceneName"></param>
	/// <returns></returns>
	static public string getSceneName(SCENE_NAME _sceneName) {
		return SceneNames[(int)_sceneName];
	}


	public void SceneChange(SCENE_NAME _sceneName, Text _progressText = null, Image _progressImg = null) {
			StartCoroutine(LoadScene(_sceneName,_progressText,_progressImg));
	}

	public IEnumerator LoadScene(SCENE_NAME _sceneName, Text _progressText = null, Image _progressImg = null) {
		Debug.Log("LoadScene");

		var _loadPlane = Instantiate(loadPlane);

		_progressImg = _loadPlane._progressImg;
		_progressText = _loadPlane._progressText;

		var async = SceneManager.LoadSceneAsync(getSceneName(_sceneName));
		async.allowSceneActivation = false;

		while(async.progress < 0.9f) {
			Debug.Log(async.progress);
			if(_progressImg != null) _progressImg.fillAmount = async.progress;
			if (_progressText != null) _progressText.text = async.progress.ToString() + " %";

			yield return new WaitForEndOfFrame();
		}

		if (_progressImg != null) _progressImg.fillAmount = 1f;
		if (_progressText != null) _progressText.text = "100 %";
		yield return new WaitForSeconds(5);
		async.allowSceneActivation = true;

	}
}
