using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.IK;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	// 移動スピード
	[Header("移動スピード")]
	[SerializeField] private float moveSpeed;

	// 動いてるかどうか
	bool isRightMove;
	bool isLeftMove;
	bool isControllerMove;

	// スピードと同じようにジャンプ力の変数も作る
	[Header("ジャンプ力")]
	[SerializeField] private float jumpPower;

	// コントローラーの入力数値
	float inputHorizontal;
	float inputVertical;

	//// 移動制限
	//float limitX = 15.5f;
	//float limitY = 8.0f;

	// 火がついてるかどうかフラグ
	[Header("フラグ")]
	public bool isLightOn;

	public bool isOnMoveBlock;


	Rigidbody2D rb;

	HitFloor hitFloor;
	// Start is called before the first frame update
	void Start()
	{
		// Rigidbodyを取得
		rb = gameObject.GetComponent<Rigidbody2D>();

		// 子オブジェクト読み込み
		GameObject child = transform.Find("HitFloor").gameObject;
		// class読み込み
		hitFloor = child.GetComponent<HitFloor>();
	}

	// Update is called once per frame
	void Update()
	{
		Move();

		TakeLight();
	}

	//動いてるかどうかのフラグ管理
	// Updateに入れる
	private void Move()
	{
		// コントローラーの左右入力数値を受け取る
		inputHorizontal = Input.GetAxis("cHorizontalL");

		//動いてるかどうか判断
		if (Input.GetKey(KeyCode.A))
		{
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
		}
		else if (inputHorizontal != 0)
		{
			rb.velocity = new Vector2(inputHorizontal * moveSpeed, rb.velocity.y);
		}
		else
		{
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
	}

	private void Jump()
	{
		//接地しているときにSpace(Jumpボタン)を押した時
		if (hitFloor.isHit == true)
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("buttonA"))
			{
				rb.velocity += new Vector2(0, jumpPower);
			}
		}
	}

	private void TakeLight()
	{
		// オンオフ切り替え
		if (Input.GetButtonDown("buttonRB") || Input.GetKeyDown(KeyCode.Space))
		{
			isLightOn = !isLightOn;
		}
	}

	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if(collision.gameObject.tag == "rightMoveBlock")
	//	{
	//		isOnMoveBlock = true;
	//		transform.SetParent(collision.transform);
	//	}

	//	if (collision.gameObject.tag == "downMoveBlock")
	//	{
	//		isOnMoveBlock = true;
	//		transform.SetParent(collision.transform);
	//	}
	//}

	//private void OnCollisionExit2D(Collision2D collision)
	//{
	//	if (collision.gameObject.tag == "rightMoveBlock")
	//	{
	//		isOnMoveBlock = false;
	//		transform.SetParent(null);
	//	}

	//	if (collision.gameObject.tag == "downMoveBlock")
	//	{
	//		isOnMoveBlock = false;
	//		transform.SetParent(null);
	//	}
	//}
}
