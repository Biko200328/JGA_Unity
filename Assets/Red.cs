using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : MonoBehaviour
{
	public bool isCollect;

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
}
