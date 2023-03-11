using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMoveBlock : MonoBehaviour
{
	[Header("�ړ����x")]
	public float moveSpeed;

	[Header("������Ԃ��ǂ����̃t���O")]
	public bool isMove = false;

	public FloorCheckDown floorCheckDown;

	Rigidbody2D rb;
	// Start is called before the first frame update 
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		//�~�̒����Փ˔��肪�Ȃ��ꍇ��
		if (isMove && !floorCheckDown.isFloor)
		{
			//������
			transform.position += new Vector3(0, -moveSpeed, 0);
		}
	}
}
