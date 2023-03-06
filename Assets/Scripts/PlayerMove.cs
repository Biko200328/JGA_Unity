using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEditor.U2D.IK;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	// 移動スピード
	[Header("移動スピード")]
	[SerializeField] private float moveSpeed;

	// コントローラーの入力数値
	float inputHorizontal;
	float inputVertical;

	//// 移動制限
	//float limitX = 15.5f;
	//float limitY = 8.0f;

	// 動いてるかどうか
	bool isRightMove;
	bool isLeftMove;
	bool isControllerMove;

	// スピードと同じようにジャンプ力の変数も作る
	[Header("ジャンプ力")]
	[SerializeField] private float jumpPower;
	[SerializeField] private float lampJumpPower;

	[Header("フラグ")]
	// 分裂しているかどうか
	public bool isLampTake;
	// 灯りの中にいるかどうか
	public bool isLightIn;
	// 火がついてるかどうかフラグ
	public bool isLightOn;
	// 隣に一マスのブロックがあったら
	public bool isNextBlockL = false;
	public bool isNextBlockR = false;

	Rigidbody2D rb;
	HitFloor hitFloor;
	HitCeiling hitCeiling;
	GameObject lampObj;
	Rigidbody2D lampRb;
	// Start is called before the first frame update
	void Start()
	{
		// Rigidbodyを取得
		rb = gameObject.GetComponent<Rigidbody2D>();

		// 子オブジェクト読み込み
		GameObject childFloor = transform.Find("HitFloor").gameObject;
		// コンポーネント読み込み
		hitFloor = childFloor.GetComponent<HitFloor>();

		// 子オブジェクト読み込み
		GameObject childCeiling = transform.Find("HitCeiling").gameObject;
		// コンポーネント読み込み
		hitCeiling = childCeiling.GetComponent<HitCeiling>();

		// lamp読み込み
		lampObj = GameObject.Find("Lamp");
		// ランプのRigidbodyを取得
		lampRb = lampObj.GetComponent<Rigidbody2D>();

		//ランプをつける
		isLightOn = true;
	}

	// Update is called once per frame
	void Update()
	{
		Move();

		//TakeLight();

		AutoJump();

		TakeLamp();
	}

	//移動
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

	//ジャンプ
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

	private void AutoJump()
	{

		if (isNextBlockL && hitFloor.isHit)
		{
			if (Input.GetKey(KeyCode.A))
			{
				rb.velocity = new Vector2(0, jumpPower);
			}
		}

		if (isNextBlockR && hitFloor.isHit)
		{
			if (Input.GetKey(KeyCode.D))
			{
				rb.velocity = new Vector2(0, jumpPower);
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

	private void TakeLamp()
	{
		// ライトの中にいてかつプレイヤーの上にブロックがないときにランプを呼ぶ
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// ランプを持っているとき
			// 地面についているとき
			if (isLampTake && hitFloor.isHit)
			{
				isLampTake = false;
				//Rigidbodyつける
				lampRb = lampObj.AddComponent<Rigidbody2D>();
				//FreezeRotationをオンにする
				lampRb.freezeRotation = true;
				//上に飛ばす
				lampRb.velocity += new Vector2(0, lampJumpPower);
			}
			// ランプを持っていないとき
			// ライトの中にいて上にブロックがないとき
			else if(!isLampTake && isLightIn && !hitCeiling.isHit)
			{
				isLampTake = true;
				Destroy(lampRb);
			}
		}

		if (isLampTake)
		{
			// 親子付け
			lampObj.transform.SetParent(this.transform);
			// プレイヤーの上に移動
			lampObj.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);
		}
		else
		{
			// 親子付け解除
			lampObj.transform.SetParent(null);
		}
	}
}
