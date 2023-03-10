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

	[Header("ランプの回収時間")]
	[SerializeField] private float lampCollectTime;
	private float collectNowTime;

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
	// ランプが回収中か
	public bool isLampCollect;
	[SerializeField] PlayerCircle playerCircle;

	Rigidbody2D rb;
	HitFloor hitFloor;
	HitCeiling hitCeiling;
	GameObject lampObj;
	Lamp lampSqr;

	JumpHitLeft jumpHitLeft;
	JumpHitLeft jumpHitLeft2;

	JumpHitRight jumpHitRight;
	JumpHitRight jumpHitRight2;

	GameObject colliderObj;

	// Start is called before the first frame update
	void Start()
	{
		// Rigidbodyを取得
		rb = gameObject.GetComponent<Rigidbody2D>();

		//colliderOvj読み込み
		colliderObj = transform.Find("SetCollider").gameObject;
		colliderObj.SetActive(false);

		// 子オブジェクト読み込み
		GameObject childFloor = transform.Find("HitFloor").gameObject;
		// コンポーネント読み込み
		hitFloor = childFloor.GetComponent<HitFloor>();

		// 子オブジェクト読み込み
		GameObject childCeiling = transform.Find("HitCeiling").gameObject;
		// コンポーネント読み込み
		hitCeiling = childCeiling.GetComponent<HitCeiling>();

		// 子オブジェクト読み込み
		GameObject childJumpR = transform.Find("JumpHitRight").gameObject;
		// コンポーネント読み込み
		jumpHitRight = childJumpR.GetComponent<JumpHitRight>();
		// 子オブジェクト読み込み
		GameObject childJumpR2 = transform.Find("JumpHitRight2").gameObject;
		// コンポーネント読み込み
		jumpHitRight2 = childJumpR2.GetComponent<JumpHitRight>();

		// 子オブジェクト読み込み
		GameObject childJumpL = transform.Find("JumpHitLeft").gameObject;
		// コンポーネント読み込み
		jumpHitLeft = childJumpL.GetComponent<JumpHitLeft>();
		// 子オブジェクト読み込み
		GameObject childJumpL2 = transform.Find("JumpHitLeft2").gameObject;
		// コンポーネント読み込み
		jumpHitLeft2 = childJumpL2.GetComponent<JumpHitLeft>();

		// lamp読み込み
		lampObj = GameObject.Find("Lamp");
		// ランプのスクリプトを取得
		lampSqr = lampObj.GetComponent<Lamp>();
		//ランプをつける
		isLightOn = true;

		gameObject.layer = 9;
	}

	// Update is called once per frame
	void Update()
	{
		Move();

		//TakeLight();

		AutoJump();

		TakeLamp();

		LampCollect();
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
		if (jumpHitLeft.isHit && !jumpHitLeft2.isHit && hitFloor.isHit)
		{
			if (Input.GetKey(KeyCode.A))
			{
				rb.velocity = new Vector2(0, jumpPower);
				//gameObject.layer = 9;
			}
		}

		if (jumpHitRight.isHit && !jumpHitRight2.isHit && hitFloor.isHit)
		{
			if (Input.GetKey(KeyCode.D))
			{
				rb.velocity = new Vector2(0, jumpPower);
				//gameObject.layer = 9;
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
			if (isLampTake)
			{
				// 地面についているとき
				if (hitFloor.isHit && !isLampCollect)
				{
					isLampTake = false;
					lampSqr.LampThrow(transform.position);
					isLightOn = false;
					// 親子付け解除
					lampObj.transform.SetParent(null);
					//// 生成したコライダーを捨てる
					colliderObj.SetActive(false);
					// 個々のコライダーをつけなおす
					gameObject.AddComponent<BoxCollider2D>();
					lampObj.AddComponent<BoxCollider2D>();
				}
			}
			// ランプを持っていないとき
			// ライトの中にいて上にブロックがないとき
			else if(!isLampTake && isLightIn && !hitCeiling.isHit)
			{
				isLampTake = true;
				lampSqr.RbLost();
				//// いったんランプの親子関係を無しに
				// lampObj.transform.SetParent(null);
				// lampをオフに
				isLightOn = false;
				//回収中のフラグをオン
				isLampCollect = true;
				collectNowTime = 0;
				// 親子付け
				lampObj.transform.SetParent(this.transform);
				// 既存のコライダーをなくす
				Destroy(gameObject.GetComponent<BoxCollider2D>());
				Destroy(lampObj.GetComponent<BoxCollider2D>());
				// 二つ用のコライダーを生成
				colliderObj.SetActive(true);
			}
		}
	}

	private void LampCollect()
	{
		if (isLampCollect)
		{
			collectNowTime += Time.deltaTime;
			if (collectNowTime >= lampCollectTime)
			{
				lampObj.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f);
				isLampCollect = false;
				// lampをオンに
				isLightOn = true;
			}
		}
	}
}
