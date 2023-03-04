using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFloor : MonoBehaviour
{
	//親オブジェクト
	GameObject player;

	// 接地判定
	public bool isHit;

	public bool isOnMoveBlock;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.Find("Player");
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 当たったcollisionのtagがFloorなら接地とする
		if (collision.gameObject.tag == "Floor") isHit = true;

		if (collision.gameObject.tag == "rightMoveBlock")
		{
			isOnMoveBlock = true;
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "downMoveBlock")
		{
			isOnMoveBlock = true;
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "upMoveBlock")
		{
			isOnMoveBlock = true;
			player.transform.SetParent(collision.transform);
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor") isHit = true;

		if (collision.gameObject.tag == "rightMoveBlock")
		{
			isOnMoveBlock = true;
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "downMoveBlock")
		{
			isOnMoveBlock = true;
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "upMoveBlock")
		{
			isOnMoveBlock = true;
			player.transform.SetParent(collision.transform);
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// 離れた時だけfalseにしてあげれば空中にいるときはfalseになる
		if (collision.gameObject.tag == "Floor") isHit = false;

		if (collision.gameObject.tag == "rightMoveBlock")
		{
			isOnMoveBlock = false;
			player.transform.SetParent(null);
		}

		if (collision.gameObject.tag == "downMoveBlock")
		{
			isOnMoveBlock = false;
			player.transform.SetParent(null);
		}

		if (collision.gameObject.tag == "upMoveBlock")
		{
			isOnMoveBlock = false;
			player.transform.SetParent(null);
		}
	}
}
