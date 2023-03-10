using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DownMoveBlock : MonoBehaviour
{
	[Header("移動速度")]
	public float moveSpeed;

	[Header("動く状態かどうかのフラグ")]
	public bool isMove = false;

	public FloorCheckDown floorCheckDown;

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
		//円の中かつ衝突判定がない場合に
		if (isMove && !floorCheckDown.isFloor)
		{
			//動かす
			transform.position += new Vector3(0, -moveSpeed, 0);
		}

		if (!lamp.isLampOn)
		{
			isMove = false;
		}

		if (playerMove.isLampCollect)
		{
			isMove = false;
		}
	}
}
