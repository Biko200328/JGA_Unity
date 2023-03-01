using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.IK;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	// [[Header("hoge")]] は ScriptのInspectorビューに文字が表示できる
	[Header("移動スピード")]
	// [SerializeField] は privateでもInspectorビューで数値が変更できる
	[SerializeField] private float moveSpeed;

	// 動いてるかどうか
	bool isRightMove;
	bool isLeftMove;
	bool isControllerMove;
	bool isMove;

	// スピードと同じようにジャンプ力の変数も作る
	[Header("ジャンプ力")]
	[SerializeField] private float jumpPower;

	// コントローラーの入力数値
	float inputHorizontal;
	float inputVertical;

	// 移動制限
	float limitX = 15.5f;
	float limitY = 8.0f;

	// 火がついてるかどうかフラグ
	public bool isLightOn;

	// privateで宣言してStartで取得する
	// public RigidBody2D rb にしてInspectorビューで直接入れてもいい
	Rigidbody2D rb;

	// 上に同じ
	HitFloor hitFloor;

	// Start is called before the first frame update
	void Start()
	{
		// Rigidbodyを取得
		rb = gameObject.GetComponent<Rigidbody2D>();

		// 子オブジェクト読み込み
		// Find("hoge")の hoge の部分を完全一致させないと読み取らないので注意
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
}
