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
			if (collision.gameObject.tag == "Floor")
			{
				isHit = true;
				if (!playerMove.throwMode) playerMove.isPlace = false;
			}

			if (collision.gameObject.tag == "platform")
			{
				isHit = true;
				if (!playerMove.throwMode) playerMove.isPlace = false;
				lamp.layer = 7;
			}

			GimmickRide(collision);
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
				if (!playerMove.throwMode) playerMove.isPlace = false;
			}

			if (collision.gameObject.tag == "platform")
			{
				isHit = true;
				if (!playerMove.throwMode) playerMove.isPlace = false;
				lamp.layer = 7;
			}

			GimmickRide(collision);
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

			GimmickRideOff(collision);
		}
	}

	private void GimmickRide(Collider2D collision)
	{
		// 各移動ブロック
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			isHit = true;
			lamp.transform.SetParent(collision.transform);
			if (!playerMove.throwMode && playerMove.isPlaceMode) playerMove.isPlace = false;
		}

		if (collision.gameObject.tag == "leftMoveBlock")
		{
			isHit = true;
			lamp.transform.SetParent(collision.transform);
			if (!playerMove.throwMode && playerMove.isPlaceMode) playerMove.isPlace = false;
		}

		if (collision.gameObject.tag == "downMoveBlock")
		{
			isHit = true;
			lamp.transform.SetParent(collision.transform);
			if (!playerMove.throwMode && playerMove.isPlaceMode) playerMove.isPlace = false;
		}

		if (collision.gameObject.tag == "upMoveBlock")
		{
			isHit = true;
			lamp.transform.SetParent(collision.transform);
			if (!playerMove.throwMode && playerMove.isPlaceMode) playerMove.isPlace = false;
		}

		// 蛇ブロック
		if (collision.gameObject.tag == "growOriginal")
		{
			isHit = true;
			if (!playerMove.throwMode && playerMove.isPlaceMode) playerMove.isPlace = false;
		}

		if (collision.gameObject.tag == "growBox")
		{
			isHit = true;
			if (!playerMove.throwMode && playerMove.isPlaceMode) playerMove.isPlace = false;
		}
	}

	private void GimmickRideOff(Collider2D collision)
	{
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			lamp.transform.SetParent(null);
			isHit = false;
		}

		if (collision.gameObject.tag == "leftMoveBlock")
		{
			lamp.transform.SetParent(null);
			isHit = false;
		}

		if (collision.gameObject.tag == "downMoveBlock")
		{
			lamp.transform.SetParent(null);
			isHit = false;
		}

		if (collision.gameObject.tag == "upMoveBlock")
		{
			lamp.transform.SetParent(null);
			isHit = false;
		}
	}
}
