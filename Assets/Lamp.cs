using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
	[SerializeField]Rigidbody2D rb;

	public bool isThrow;
	public float throwNowTime;
	public float throwTime;

	public Vector2 startPos;
	[SerializeField] private float maxY;

	public Vector2 pos;

	public bool isFall;
	private float fallNowTime;
	private Vector2 fallStartPos;
	private Vector2 endPos;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if(isThrow)
		{
			throwNowTime += Time.deltaTime;
			rb.velocity = Vector2.zero;
			if (throwNowTime >= throwTime)
			{
				isThrow = false;
				throwNowTime = throwTime;
				isFall = true;
				fallNowTime = 0;
				fallStartPos = transform.position;
			}

			transform.position = QuintOut(throwNowTime, throwTime, startPos, startPos + new Vector2(0, maxY));
		}

		if(isFall)
		{
			fallNowTime += Time.deltaTime;
			if (rb != null) rb.velocity = Vector2.zero;
			if (fallNowTime >= throwTime)
			{
				isFall = false;
				fallNowTime = throwTime;
			}

		transform.position = QuintIn(fallNowTime, throwTime, fallStartPos, fallStartPos + new Vector2(0, -maxY - 1));
		}
	}

	private void FixedUpdate()
	{
		if (rb != null)
		{
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
	}

	public static Vector2 QuintOut(float t, float totaltime, Vector2 min, Vector2 max)
	{
		max -= min;
		t = t / totaltime - 1;
		return max * (t * t * t * t * t + 1) + min;
	}

	public static Vector2 QuintIn(float t, float totaltime, Vector2 min, Vector2 max)
	{
		max -= min;
		t /= totaltime;
		return max * t * t * t * t * t + min;
	}

	public void LampThrow(Vector3 pos)
	{
		//Rigidbodyつける
		rb = gameObject.AddComponent<Rigidbody2D>();
		//FreezeRotationをオンにする
		rb.freezeRotation = true;

		// 投げたフラグをtrue
		isThrow = true;
		// タイムを0に
		throwNowTime = 0;
		// スタートポジションを現在のposに変更
		startPos = new Vector2(pos.x, pos.y + 1);
	}

	public void RbLost()
	{
		Destroy(rb);
	}
}
