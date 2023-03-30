using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("移動スピード")]
	[SerializeField] float moveSpeed;
	bool isRightMove;
	bool isLeftMove;

	[Header("プレイヤー状態")]
	[SerializeField]int state = 0;

	// 赤
	GameObject redObj;
	Red red;

	// 緑
	GameObject greenObj;
	Green green;

	Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		redObj = GameObject.Find("RedFire");
		red = redObj.gameObject.GetComponent<Red>();

		greenObj = GameObject.Find("GreenFire");
		green = greenObj.gameObject.GetComponent<Green>();
	}

	// Update is called once per frame
	void Update()
	{
		MoveUpdate();

		// 状態変化
		if (Input.GetKeyDown(KeyCode.Space))
		{
			state++;

			// ループさせる
			if (state > 3)
			{
				state = 0;
			}

			//赤回収
			if (state == 1)
			{
				// プレイヤーに親子付けする
				redObj.transform.SetParent(transform);
				// ポジションをプレイヤーと同じに
				redObj.transform.position = transform.position;
			}
			// 赤設置
			else if(state == 2)
			{
				// 親子付けを解除する
				redObj.transform.SetParent(null);
			}
			// 緑回収
			else if(state == 3)
			{
				// プレイヤーに親子付けする
				greenObj.transform.SetParent(transform);
				// ポジションをプレイヤーと同じに
				greenObj.transform.position = transform.position;
			}
			//緑設置
			else if (state == 0)
			{
				// 親子付けを解除する
				greenObj.transform.SetParent(null);
			}
		}

		switch (state)
		{
			case 0:
				green.SetCollect(false);
				break;
			case 1:
				red.SetCollect(true);
				break;
			case 2:
				red.SetCollect(false);
				break;
			case 3:
				green.SetCollect(true);
				break;
		}
	}

	private void FixedUpdate()
	{
		MoveFixedUpdate();
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
}
