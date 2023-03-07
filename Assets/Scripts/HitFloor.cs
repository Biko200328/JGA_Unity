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

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 当たったcollisionのtagがFloorなら接地とする
		if (collision.gameObject.tag == "Floor") isHit = true;

		GimmickRide(collision);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor") isHit = true;

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
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "leftMoveBlock")
		{
			isHit = true;
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "downMoveBlock")
		{
			isHit = true;
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "upMoveBlock")
		{
			isHit = true;
			player.transform.SetParent(collision.transform);
		}

		// 通り抜ける足場
		if(collision.gameObject.tag == "platform")
		{
			isHit = true;
			player.layer = 3;
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
	}
}
