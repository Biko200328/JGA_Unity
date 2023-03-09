using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheckLeft : MonoBehaviour
{
	// 床に当たっているかどうか
	public bool isFloor;

	public LeftMoveBlock leftMoveBlock;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//動いてる状態かつ当たったオブジェクトが床の時に
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "rightMoveBlock" || collision.gameObject.tag == "leftMoveBlock" ||
			collision.gameObject.tag == "block" || collision.gameObject.tag == "upMoveBlock" || collision.gameObject.tag == "downMoveBlock")
		{
			// 衝突判定をTrueに
			if (leftMoveBlock.isMove) isFloor = true;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		//動いてる状態かつ当たったオブジェクトが床の時に
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "rightMoveBlock" || collision.gameObject.tag == "leftMoveBlock" ||
			collision.gameObject.tag == "block" || collision.gameObject.tag == "upMoveBlock" || collision.gameObject.tag == "downMoveBlock")
		{
			// 衝突判定をTrueに
			if (leftMoveBlock.isMove) isFloor = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//動いてる状態かつ当たったオブジェクトが床の時に
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "rightMoveBlock" || collision.gameObject.tag == "leftMoveBlock" ||
			collision.gameObject.tag == "block" || collision.gameObject.tag == "upMoveBlock" || collision.gameObject.tag == "downMoveBlock")
		{
			// 衝突判定をTrueに
			if (leftMoveBlock.isMove) isFloor = false;
		}
	}
}
