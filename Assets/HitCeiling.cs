using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class HitCeiling : MonoBehaviour
{
	// 判定フラグ
	public bool isHit;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 当たったcollisionのtagがFloorなら接地とする
		if (collision.gameObject.tag == "Floor") isHit = true;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		// 当たったcollisionのtagがFloorなら接地とする
		if (collision.gameObject.tag == "Floor") isHit = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// 出たらしていない
		if (collision.gameObject.tag == "Floor") isHit = false;
	}
}
