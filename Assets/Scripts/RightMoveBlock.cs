using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMoveBlock : MonoBehaviour
{
	[Header("移動速度")]
	public float moveSpeed;

	[Header("動く状態かどうかのフラグ")]
	public bool isMove = false;
	[Header("壁との衝突判定")]
	public bool isFloor = false;

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
		if (isMove && !isFloor)
		{
			//動かす
			transform.position += new Vector3(moveSpeed, 0,0);
		}
	}

	private void OnCollisionEnter2D(Collision2D collision)
	{
		//動いてる状態かつ当たったオブジェクトが床の時に
		if (collision.gameObject.tag == "Floor")
		{
			// 衝突判定をTrueに
			if(isMove)isFloor = true;
		}
	}
}
