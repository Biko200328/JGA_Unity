using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedMove : MonoBehaviour
{
	[Header("移動スピード")]
	[SerializeField] float moveSpeed;
	bool isRightMove;
	bool isLeftMove;

	[Header("ジャンプ力")]
	[SerializeField] float jumpPower;

	Rigidbody2D rb;
	GameManager gameManager;
	SpriteRenderer render;
	RedFoot redFoot;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		render = GetComponent<SpriteRenderer>();
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		redFoot = transform.Find("RedFoot").GetComponent<RedFoot>();
	}

	// Update is called once per frame
	void Update()
	{
		if(!gameManager.GetIsMove())
		{
			MoveUpdate();
			Jump();
			render.sortingOrder = 1;
		}
		else
		{
			render.sortingOrder = 0;
		}
	}

	private void FixedUpdate()
	{
		if (!gameManager.GetIsMove())
		{
			MoveFixedUpdate();
		}
		else
		{
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
	}

	private void MoveUpdate()
	{
		if (Input.GetKey(KeyCode.A))
		{
			isRightMove = true;
			isLeftMove = false;
			
		}
		else if (Input.GetKey(KeyCode.D))
		{
			isLeftMove = true;
			isRightMove = false;
		}
		else
		{
			isLeftMove = false;
			isRightMove = false;
		}
	}

	private void MoveFixedUpdate()
	{
		if (isRightMove)
		{
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
			transform.eulerAngles = new Vector3(0, 0, 0);
		}
		else if (isLeftMove)
		{
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
			transform.eulerAngles = new Vector3(0, 180, 0);
		}
		else
		{
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
	}

	private void Jump()
	{
		if(Input.GetKeyDown(KeyCode.Space) && redFoot.GetIsHit())
		{
			rb.velocity = new Vector2(rb.velocity.x, jumpPower);
		}
	}
}
