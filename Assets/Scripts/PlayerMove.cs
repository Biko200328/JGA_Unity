using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	// [[Header("hoge")]] は ScriptのInspectorビューに文字が表示できる
	[Header("移動スピード")]
	// [SerializeField] は privateでもInspectorビューで数値が変更できる
	[SerializeField] private float moveSpeed;

	// スピードと同じようにジャンプ力の変数も作る
	[Header("ジャンプ力")]
	[SerializeField] private float jumpPower;

	float inputHorizontal;
	float inputVertical;

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
		TakeLight();
	}

	private void FixedUpdate()
	{
		Move();
		Jump();
	}

	private void Move()
	{
		// varとは
		// 初期値の内容から変数の型をC#コンパイラーが推測して自動的に設定してくれます(google先生より)
		var pos = rb.position;

		// ここではrb(RigidBody)のpositionをposとして一時保管します
		// 理由としてはpositionのx,y,z 個々を直接いじれないのでpositionとして全部受け取り、そのまま全部返します
		// 日本語難しい


		// GetKey             押している間
		// GetKeyDown         押した瞬間
		// GetKeyUp           離した瞬間
		// キーボード
		if (Input.GetKey(KeyCode.A))
		{
			pos.x -= moveSpeed;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			pos.x += moveSpeed;
		}

		//コントローラー
		inputHorizontal = Input.GetAxis("cHorizontalL");
		//inputVertical = Input.GetAxis("cVerticalL");

		pos.x += inputHorizontal * moveSpeed;

		//受け取って数値変更したposをrbに返します
		rb.position = pos;
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
		if(Input.GetButtonDown("buttonRB"))
		{
			isLightOn = !isLightOn;
		}
	}
}
