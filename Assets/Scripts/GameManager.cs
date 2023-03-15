using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// フェード用のシーンコントローラー
	SceneController sceneController;

	// Start is called before the first frame update
	void Start()
	{
		// Screen.SetResolution(横幅,縦幅,フルスクリーンにするか)
		// とりあえず今はフルスクリーンモードにする
		Screen.SetResolution(1920, 1080, true);

		// fpsの固定
		Application.targetFrameRate = 60;

		GameObject camera = GameObject.Find("Main Camera");
		sceneController = camera.GetComponent<SceneController>();
	}

	// Update is called once per frame
	void Update()
	{
		//if(Input.GetKeyDown(KeyCode.R))
		//{
		//	// 現在のシーンを読み直す
		//	sceneController.sceneChange(SceneManager.GetActiveScene().name);
		//}
	}
}
