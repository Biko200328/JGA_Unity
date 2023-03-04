using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFloor : MonoBehaviour
{
	// 接地判定
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
		if (collision.gameObject.tag == "Floor") isHit = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// 離れた時だけfalseにしてあげれば空中にいるときはfalseになる
		if (collision.gameObject.tag == "Floor") isHit = false;
	}
}
