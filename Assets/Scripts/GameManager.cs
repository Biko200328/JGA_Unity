using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
	// �t�F�[�h�p�̃V�[���R���g���[���[
	SceneController sceneController;

	// Start is called before the first frame update
	void Start()
	{
		// Screen.SetResolution(����,�c��,�t���X�N���[���ɂ��邩)
		// �Ƃ肠�������̓t���X�N���[�����[�h�ɂ���
		Screen.SetResolution(1920, 1080, true);

		// fps�̌Œ�
		Application.targetFrameRate = 60;

		GameObject camera = GameObject.Find("Main Camera");
		sceneController = camera.GetComponent<SceneController>();
	}

	// Update is called once per frame
	void Update()
	{
		//if(Input.GetKeyDown(KeyCode.R))
		//{
		//	// ���݂̃V�[����ǂݒ���
		//	sceneController.sceneChange(SceneManager.GetActiveScene().name);
		//}
	}
}
