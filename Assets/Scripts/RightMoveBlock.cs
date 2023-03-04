using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMoveBlock : MonoBehaviour
{
	[Header("移動速度")]
	public float moveSpeed;

	[Header("動く状態かどうかのフラグ")]
	public bool isMove = false;


	public FloorCheckRight floorCheckRight;

	Rigidbody2D rb;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		//円の中かつ衝突判定がない場合に
		if (isMove && !floorCheckRight.isFloor)
		{
			//動かす
			transform.position += new Vector3(moveSpeed, 0,0);
		}
	}
}
