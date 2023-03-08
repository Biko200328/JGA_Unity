using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LampHitFloor : MonoBehaviour
{
	public bool isHit;
	GameObject lamp;

	PlayerMove playerMove;

	// Start is called before the first frame update
	void Start()
	{
		lamp = GameObject.Find("Lamp");

		GameObject player = GameObject.Find("Player");
		playerMove = player.GetComponent<PlayerMove>();

		isHit = false;
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//// 当たったcollisionのtagがFloorなら接地とする
		//if (collision.gameObject.tag == "Floor") isHit = true;

		if (!playerMove.isLampTake)
		{
			if(collision.gameObject.tag == "Floor")
			{
				isHit = true;
			}

			if (collision.gameObject.tag == "platform")
			{
				isHit = true;
				lamp.layer = 7;
			}
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		//// 当たったcollisionのtagがFloorなら接地とする
		//if (collision.gameObject.tag == "Floor") isHit = true;

		if (!playerMove.isLampTake)
		{
			if (collision.gameObject.tag == "Floor")
			{
				isHit = true;
			}

			if (collision.gameObject.tag == "platform")
			{
				isHit = true;
				lamp.layer = 7;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//if (collision.gameObject.tag == "Floor") isHit = false;

		if (!playerMove.isLampTake)
		{
			if (collision.gameObject.tag == "Floor")
			{
				isHit = false;
			}

			if (collision.gameObject.tag == "platform")
			{
				isHit = false;
				lamp.layer = 10;
			}
		}
	}

	private void GimmickRide(Collider2D collision)
	{
		// 各移動ブロック
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			isHit = true;
			lamp.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "leftMoveBlock")
		{
			isHit = true;
			lamp.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "downMoveBlock")
		{
			isHit = true;
			lamp.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "upMoveBlock")
		{
			isHit = true;
			lamp.transform.SetParent(collision.transform);
		}
	}
}
