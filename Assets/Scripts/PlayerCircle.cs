using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
	PlayerMove playerMove;


	// Start is called before the first frame update
	void Start()
	{
		GameObject player = GameObject.Find("Player");
		playerMove = player.GetComponent<PlayerMove>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//ギミックのフラグ管理
		GimmicksOn(collision);

		// プレイヤー
		if (collision.gameObject.tag == "Player")
		{
			playerMove.isLightIn = true;
		}

		// 蛇ブロック
		if(collision.gameObject.tag == "growOriginal")
		{
			Growth growth = collision.GetComponent<Growth>();
			growth.isLightIn = true;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		//ギミックのフラグ管理
		GimmicksOn(collision);

		// プレイヤー
		if (collision.gameObject.tag == "Player")
		{
			playerMove.isLightIn = true;
		}

		// 蛇ブロック
		if (collision.gameObject.tag == "growOriginal")
		{
			Growth growth = collision.GetComponent<Growth>();
			growth.isLightIn = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//ギミックのフラグ管理
		GimmicksOff(collision);

		// プレイヤー
		if (collision.gameObject.tag == "Player")
		{
			playerMove.isLightIn = false;
		}

		// 蛇ブロック
		if (collision.gameObject.tag == "growOriginal")
		{
			Growth growth = collision.GetComponent<Growth>();
			growth.isLightIn = false;
			growth.isEnd = false;
		}
	}

	private void GimmicksOn(Collider2D collision)
	{
		// 普通のブロック
		if (collision.gameObject.tag == "block")
		{
			Rigidbody2D blockRb = collision.gameObject.GetComponent<Rigidbody2D>();
			blockRb.isKinematic = false;
			Block block = collision.gameObject.GetComponent<Block>();
			block.isLightIn = true;
		}

		// 左に移動するブロック
		if (collision.gameObject.tag == "leftMoveBlock")
		{
			LeftMoveBlock leftMoveBlock = collision.gameObject.GetComponent<LeftMoveBlock>();
			leftMoveBlock.isMove = true;
		}

		// 右に移動するブロック
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			RightMoveBlock rightMoveBlock = collision.gameObject.GetComponent<RightMoveBlock>();
			rightMoveBlock.isMove = true;
		}

		// 上に移動するブロック
		if (collision.gameObject.tag == "upMoveBlock")
		{
			UpMoveBlock upMoveBlock = collision.gameObject.GetComponent<UpMoveBlock>();
			upMoveBlock.isMove = true;
		}

		// 下に移動するブロック
		if (collision.gameObject.tag == "downMoveBlock")
		{
			DownMoveBlock downMoveBlock = collision.gameObject.GetComponent<DownMoveBlock>();
			downMoveBlock.isMove = true;
		}
	}

	private void GimmicksOff(Collider2D collision)
	{
		// 普通のブロック
		if (collision.gameObject.tag == "block")
		{
			Rigidbody2D blockRb = collision.gameObject.GetComponent<Rigidbody2D>();
			blockRb.isKinematic = true;
			blockRb.velocity = Vector3.zero;
			Block block = collision.gameObject.GetComponent<Block>();
			block.isLightIn = false;
		}

		// 左に移動するブロック
		if (collision.gameObject.tag == "leftMoveBlock")
		{
			LeftMoveBlock leftMoveBlock = collision.gameObject.GetComponent<LeftMoveBlock>();
			leftMoveBlock.isMove = false;
		}

		// 右に移動するブロック
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			RightMoveBlock rightMoveBlock = collision.gameObject.GetComponent<RightMoveBlock>();
			rightMoveBlock.isMove = false;
		}

		// 上に移動するブロック
		if (collision.gameObject.tag == "upMoveBlock")
		{
			UpMoveBlock upMoveBlock = collision.gameObject.GetComponent<UpMoveBlock>();
			upMoveBlock.isMove = false;
		}

		// 下に移動するブロック
		if (collision.gameObject.tag == "downMoveBlock")
		{
			DownMoveBlock downMoveBlock = collision.gameObject.GetComponent<DownMoveBlock>();
			downMoveBlock.isMove = false;
		}
	}
}
