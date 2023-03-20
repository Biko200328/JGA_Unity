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

	public Vector3 respawnPos;

	// スピードと同じようにジャンプ力の変数も作る
	[Header("ジャンプ力")]
	[SerializeField] private float jumpPower;

	[Header("ランプの回収時間")]
	[SerializeField] private float lampCollectTime;
	private float collectNowTime;

	[Header("上に投げるのか")]
	public bool throwMode;

    [Header("ランプが範囲外でも回収できるか")]
    public bool collectNoLimit = false;

    [Header("ジャンプができるか")]
    public bool jumpNoLimit = false;

    [Header("光の範囲が置いたときに変わるか")]
    public bool lightSizeChange = false;

	[Header("ランプがプレイヤーのX軸の動きを真似するか")]
	public bool lightSynchro = false;

    // 分裂しているかどうか
    [System.NonSerialized] public bool isLampTake;
	// 灯りの中にいるかどうか
	[System.NonSerialized] public bool isLightIn;
	// 火がついてるかどうかフラグ
	[System.NonSerialized] public bool isLightOn;
	// ランプが回収中か
	[System.NonSerialized] public bool isLampCollect;
	// 置くときのフラグ
	/*[System.NonSerialized] */public bool isPlace;
	[SerializeField] PlayerCircle playerCircle;

	public Rigidbody2D rb;
	HitFloor hitFloor;
	HitCeiling hitCeiling;
	GameObject lampObj;
	Lamp lampSqr;

	JumpHitLeft jumpHitLeft;
	JumpHitLeft jumpHitLeft2;

	JumpHitRight jumpHitRight;
	JumpHitRight jumpHitRight2;

	GameObject colliderObj;

	RespawnManager respawnManager;
	GameObject particle;

	public bool isJump;

	// Start is called before the first frame update
	void Start()
	{
		// リスポーンマネージャー
		GameObject respawnManagerObj = GameObject.Find("RespawnManager");
		respawnManager = respawnManagerObj.GetComponent<RespawnManager>();
		transform.position = respawnManager.GetRespawnPos();

		// Rigidbodyを取得
		rb = gameObject.GetComponent<Rigidbody2D>();

		//colliderOvj読み込み
		colliderObj = transform.Find("SetCollider").gameObject;
		colliderObj.SetActive(false);

		// パーティクル読み込み
		particle = transform.Find("Particle").gameObject;

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

		if(jumpNoLimit)
		{
            if (Input.GetKeyDown(KeyCode.C) && isJump == false)
            {
                rb.velocity = new Vector2(0, 10);
				isJump = true;
            }
        }

		if(isLightIn && !hitCeiling.isHit)
		{
			particle.SetActive(true);
		}
		else
		{
			particle.SetActive(false);
		}
	}

	//移動
	private void Move()
	{
		// コントローラーの左右入力数値を受け取る
		inputHorizontal = Input.GetAxis("cHorizontalL");

		//イドウ
		if(isJump)
		{
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
		else
		{
			if (hitFloor.isHit)
			{
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
			else
            {
				if (Input.GetKey(KeyCode.A))
				{
					rb.velocity = new Vector2(-moveSpeed * 0.05f, rb.velocity.y);
				}
				else if (Input.GetKey(KeyCode.D))
				{
					rb.velocity = new Vector2(moveSpeed * 0.05f, rb.velocity.y);
				}
				else if (inputHorizontal != 0)
				{
					rb.velocity = new Vector2(inputHorizontal * moveSpeed * 0.05f, rb.velocity.y);
				}
				else
				{
					rb.velocity = new Vector2(0, rb.velocity.y);
				}
			}
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
				transform.SetParent(null);
				rb.velocity = new Vector2(0, jumpPower);
				isJump = true;
				jumpHitLeft.isHit = false;
				//gameObject.layer = 9;
			}
		}

		if (jumpHitRight.isHit && !jumpHitRight2.isHit && hitFloor.isHit)
		{
			if (Input.GetKey(KeyCode.D))
			{
				transform.SetParent(null);
				rb.velocity = new Vector2(0, jumpPower);
				isJump = true;
				jumpHitRight.isHit = false;
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
		//光の中にいなくても回収できるように
		if(collectNoLimit)
		{
			isLightIn = true;
        }

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
					lampSqr.GetLampRb();
					if (throwMode)
					{
						lampSqr.LampThrow(transform.position);
						isLightOn = false;
					}
					else
					{
						isPlace = true;
					}
					// 親子付け解除
					lampObj.transform.SetParent(null);
					// 生成したコライダーを捨てる
					colliderObj.SetActive(false);
					// 個々のコライダーをつけなおす
					gameObject.AddComponent<BoxCollider2D>();
					lampObj.AddComponent<BoxCollider2D>();
					// レイヤーをプレイヤーに変更
					gameObject.layer = 9;
				}
			}
			// ランプを持っていないとき
			// ライトの中にいて上にブロックがないとき
			else if (!isLampTake && isLightIn && !hitCeiling.isHit && !isPlace)
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
				// レイヤーをランプに変更
				gameObject.layer = 10;
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
