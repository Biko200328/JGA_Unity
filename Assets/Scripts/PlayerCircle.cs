using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
	[Header("円の最大直径")]
	public float maxCircleSize = 1;

	[Header("円の最小直径")]
	public float minCircleSize = 0;

	[Header("サイズ増減数値")]
	[SerializeField] float changeSize;

	// 現在の円の範囲
	private float circleSize;

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
		if(playerMove.isLightOn)
		{
			//最大値より低かったら足す
			if(circleSize < maxCircleSize)
			{
				circleSize += changeSize;
			}

			//最大値より大きかったら最大値に合わせる
			if(circleSize > maxCircleSize)
			{
				circleSize = maxCircleSize;
			}
		}
		else
		{
			// 最小値(0)より大きかったら引く
			if (circleSize > minCircleSize)
			{
				circleSize -= changeSize;
			}

			//最小値より小さかったら最小値に合わせる
			if (circleSize < minCircleSize)
			{
				circleSize = minCircleSize;
			}
		}

		var circleVec = new Vector3(circleSize, circleSize / 2, circleSize);
		transform.localScale = circleVec;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "block")
		{
			Rigidbody2D blockRb = collision.gameObject.GetComponent<Rigidbody2D>();
			blockRb.isKinematic = false;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "block")
		{
			Rigidbody2D blockRb = collision.gameObject.GetComponent<Rigidbody2D>();
			blockRb.isKinematic = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "block")
		{
			Rigidbody2D blockRb = collision.gameObject.GetComponent<Rigidbody2D>();
			blockRb.isKinematic = true;
			blockRb.velocity = Vector3.zero;
		}
	}
}
