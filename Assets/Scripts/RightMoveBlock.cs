using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMoveBlock : MonoBehaviour
{
	[Header("�ړ����x")]
	public float moveSpeed;

	[Header("������Ԃ��ǂ����̃t���O")]
	public bool isMove = false;


	public FloorCheckRight floorCheckRight;

	Rigidbody2D rb;

	Lamp lamp;
	PlayerMove playerMove;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		GameObject lampObj = GameObject.Find("Lamp");
		lamp = lampObj.GetComponent<Lamp>();

		GameObject player = GameObject.Find("Player");
		playerMove = player.GetComponent<PlayerMove>();
	}

	// Update is called once per frame
	void Update()
	{
		//�~�̒����Փ˔��肪�Ȃ��ꍇ��
		if (isMove && !floorCheckRight.isFloor)
		{
			//������
			transform.position += new Vector3(moveSpeed, 0, 0);
		}

		if (!lamp.isLampOn)
		{
			isMove = false;
		}

		if (playerMove.isLampCollect)
		{
			isMove = false;
		}

		if (playerMove.isPlace)
		{
			isMove = false;
		}
	}
}
