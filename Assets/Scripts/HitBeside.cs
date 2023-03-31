using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitBeside : MonoBehaviour
{
	PlayerMove playerMove;
	Lamp lamp;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		// PlayerMoveを取得
		GameObject playerObj = GameObject.Find("Player");
		playerMove = playerObj.GetComponent<PlayerMove>();

		// Lampを取得
		// lamp読み込み
		GameObject lampObj = GameObject.Find("Lamp");
		// ランプのスクリプトを取得
		lamp = lampObj.GetComponent<Lamp>();
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (!playerMove.isLampTake && collision.gameObject.tag == "growBox")
		{
			lamp.isHitGrowBox = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (!playerMove.isLampTake && collision.gameObject.tag == "growBox")
		{
			lamp.isHitGrowBox = false;
		}
	}
}
