using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheckDown : MonoBehaviour
{// 床に当たっているかどうか
	public bool isFloor;

	public DownMoveBlock downMoveBlock;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//動いてる状態かつ当たったオブジェクトが床の時に
		if (collision.gameObject.tag == "Floor")
		{
			// 衝突判定をTrueに
			if (downMoveBlock.isMove) isFloor = true;
		}
	}
}
