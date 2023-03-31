using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : MonoBehaviour
{
	private bool isCollect;

	[Header("�~�̍ő�l")]
	public float maxSize;

	[Header("�T�C�Y�������l")]
	public float changeSize;

	Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void FixedUpdate()
	{
		// �O���b�h�ɍ����悤��
		if (!isCollect)
		{
			// ��������
			int intLampPosX = (int)rb.position.x;
			// ��������
			float fltLampPosX = rb.position.x - intLampPosX;
			// ���l���
			var pos = rb.position;

			if (fltLampPosX < 1 && fltLampPosX > 0.5f)
			{
				intLampPosX += 1;
			}

			pos.x = (float)intLampPosX;
			rb.position = pos;
		}
	}

	/// <summary>
	/// �R���N�g���[�h�ύX
	/// </summary>
	/// <param name="a">
	/// true -> ���
	/// false -> �ݒu
	/// </param>
	public void SetCollect(bool a)
	{
		isCollect = a;
	}

	public bool GetCollect()
	{
		return isCollect;
	}
}
