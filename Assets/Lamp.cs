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
			}

			transform.position = QuintOut(throwNowTime, throwTime, startPos, startPos + new Vector2(0, maxY));
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

	public void LampThrow()
	{
		//Rigidbody‚Â‚¯‚é
		rb = gameObject.AddComponent<Rigidbody2D>();
		//FreezeRotation‚ðƒIƒ“‚É‚·‚é
		rb.freezeRotation = true;
	}

	public void RbLost()
	{
		Destroy(rb);
	}
}
