using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
	// Start is called before the first frame update
	void Start()
	{
		// Screen.SetResolution(����,�c��,�t���X�N���[���ɂ��邩)
		// �Ƃ肠�������̓t���X�N���[�����[�h�ɂ���
		Screen.SetResolution(1920, 1080, true);

		// fps�̌Œ�
		Application.targetFrameRate = 60;
	}

	// Update is called once per frame
	void Update()
	{

	}
}
