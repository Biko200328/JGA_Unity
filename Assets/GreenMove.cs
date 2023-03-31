using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenMove : MonoBehaviour
{
	[Header("移動スピード")]
	[SerializeField] float moveSpeed;
	bool isRightMove;
	bool isLeftMove;
	bool isUpMove;
	bool isDownMove;

	Rigidbody2D rb;

	GameManager gameManager;

	private SpriteRenderer render;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		render = GetComponent<SpriteRenderer>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
	}

	// Update is called once per frame
	void Update()
	{
		if (gameManager.GetIsMove())
		{
			MoveUpdate();
			render.sortingOrder = 1;
		}
		else
		{
			render.sortingOrder = 0;
		}
	}

	private void FixedUpdate()
	{
		if (gameManager.GetIsMove())
		{
			//MoveFixedUpdate();
		}
		else
		{
			rb.velocity = Vector2.zero;
		}
	}

	private void MoveUpdate()
	{
		// リセット
		isUpMove = false;
		isDownMove = false;
		isLeftMove = false;
		isRightMove = false;

		if (Input.GetKey(KeyCode.A))
		{
			isUpMove = false;
			isDownMove = false;
			isRightMove = true;
			isLeftMove = false;

			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
			transform.eulerAngles = new Vector3(0, 0, 0);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			isUpMove = false;
			isDownMove = false;
			isRightMove = false;
			isLeftMove = true;

			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
			transform.eulerAngles = new Vector3(0, 180, 0);
		}
		else
		{
			rb.velocity = new Vector2(0, rb.velocity.y);
		}

		if(Input.GetKey(KeyCode.W))
		{
			isUpMove = true;
			isDownMove = false;
			isRightMove = false;
			isLeftMove = false;

			rb.velocity = new Vector2(rb.velocity.x, moveSpeed);
		}
		else if (Input.GetKey(KeyCode.S))
		{
			isUpMove = false;
			isDownMove = true;
			isRightMove = false;
			isLeftMove = false;

			rb.velocity = new Vector2(rb.velocity.x, -moveSpeed);
		}
		else
		{
			rb.velocity = new Vector2(rb.velocity.x,0);
		}
	}

	//private void MoveFixedUpdate()
	//{
	//	var n = rb.velocity;

	//	if (isRightMove)
	//	{
	//		n = new Vector2(-moveSpeed, rb.velocity.y);
	//		transform.eulerAngles = new Vector3(0, 0, 0);
	//	}
	//	else if (isLeftMove)
	//	{
	//		n = new Vector2(moveSpeed, rb.velocity.y);
	//		transform.eulerAngles = new Vector3(0, 180, 0);
	//	}
	//	else
	//	{
	//		n = new Vector2(0, rb.velocity.y);
	//	}

	//	if (isUpMove)
	//	{
	//		n = new Vector2(rb.velocity.x, moveSpeed);
	//	}
	//	else if (isDownMove)
	//	{
	//		n = new Vector2(rb.velocity.x, -moveSpeed);
	//	}
	//	else
	//	{
	//		n = new Vector2(rb.velocity.x, -moveSpeed);
	//	}

	//	rb.velocity = n;
	//}
}
