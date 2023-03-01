using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMoveBlock : MonoBehaviour
{
	public bool isMove = false;

	public float moveSpeed;

	Rigidbody2D rb;
	// Start is called before the first frame update
	void Start()
	{
		rb = gameObject.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (isMove)
		{
			rb.velocity = new Vector2(moveSpeed,0);
		}
		else
		{
			rb.velocity = Vector2.zero;
		}
	}
}
