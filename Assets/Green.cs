using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green : MonoBehaviour
{
	public bool isCollect;

	[Header("�~�̍ő�l")]
	public float maxSize;

	[Header("�T�C�Y�������l")]
	public float changeSize;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

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
