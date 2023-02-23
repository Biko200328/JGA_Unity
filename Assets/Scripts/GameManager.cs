using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		// Screen.SetResolution(横幅,縦幅,フルスクリーンにするか)
		// とりあえず今はフルスクリーンモードにする
		Screen.SetResolution(1920, 1080, true);

		// fpsの固定
		Application.targetFrameRate = 60;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
