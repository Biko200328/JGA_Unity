using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFloor : MonoBehaviour
{
	//親オブジェクト
	GameObject player;
	GameObject Lamp;

	PlayerMove playerMove;

	// 接地判定
	public bool isHit;

	// 透明化
	public bool isGoast;
	// 透明化の時間
	public float goastTime;
	private float time;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.Find("Player");
		playerMove = player.GetComponent<PlayerMove>();
		Lamp = GameObject.Find("Lamp");
	}

	// Update is called once per frame
	void Update()
	{
		if(isGoast)
		{
			time += Time.deltaTime;
			if(time >= goastTime)
			{
				isGoast = false;
			}

			player.layer = 9;
			if (playerMove.isLampTake) Lamp.layer = 10;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 当たったcollisionのtagがFloorなら接地とする
		if (collision.gameObject.tag == "Floor")
		{
			isHit = true;
			playerMove.isJump = false;
		}

		GimmickRide(collision);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor")
		{
			isHit = true;
			playerMove.isJump = false;
		}

		GimmickRide(collision);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// 離れた時だけfalseにしてあげれば空中にいるときはfalseになる
		if (collision.gameObject.tag == "Floor") isHit = false;

		GimmickRideOff(collision);
	}

	private void GimmickRide(Collider2D collision)
	{
		// 各移動ブロック
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			isHit = true;
			playerMove.isJump = false;
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "leftMoveBlock")
		{
			isHit = true;
			playerMove.isJump = false;
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "downMoveBlock")
		{
			isHit = true;
			playerMove.isJump = false;
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "upMoveBlock")
		{
			isHit = true;
			playerMove.isJump = false;
			player.transform.SetParent(collision.transform);
		}

		// 通り抜ける足場
		if(collision.gameObject.tag == "platform")
		{
			isHit = true;
			playerMove.isJump = false;
			player.layer = 3;
			if (Input.GetKeyDown(KeyCode.S))
			{
				isGoast = true;
				time = 0;
			}
		}

		// 蛇ブロック
		if (collision.gameObject.tag == "growOriginal")
		{
			isHit = true;
			playerMove.isJump = false;
		}

		if (collision.gameObject.tag == "growBox")
		{
			isHit = true;
			playerMove.isJump = false;
		}
	}

	private void GimmickRideOff(Collider2D collision)
	{
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			player.transform.SetParent(null);
			isHit = false;
		}

		if (collision.gameObject.tag == "leftMoveBlock")
		{
			player.transform.SetParent(null);
			isHit = false;
		}

		if (collision.gameObject.tag == "downMoveBlock")
		{
			player.transform.SetParent(null);
			isHit = false;
		}

		if (collision.gameObject.tag == "upMoveBlock")
		{
			player.transform.SetParent(null);
			isHit = false;
		}

		// 通り抜ける足場
		if (collision.gameObject.tag == "platform")
		{
			isHit = false;
			player.layer = 9;
			if(playerMove.isLampTake)Lamp.layer = 10;
		}

		// 蛇ブロック
		if (collision.gameObject.tag == "growOriginal")
		{
			isHit = false;
		}

		if (collision.gameObject.tag == "growBox")
		{
			isHit = false;
		}
	}
}
