using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
	[SerializeField] Rigidbody2D rb;

	// 投げるフラグ
	private bool isThrow;
	// 現在の時間
	private float throwNowTime;
	[Header("投げる時間")]
	[SerializeField] private float throwTime;

	[Header("ランプがつく時間")]
	[SerializeField] private float lampTime;

	// 始点
	private Vector2 startPos;
	[Header("どこまで飛ぶか")]
	[SerializeField] private float maxY;

	// 落ちるフラグ
	private bool isFall;
	// 現在の時間
	private float fallNowTime;
	// 投げ終わった後に始点にするようのvec2
	private Vector2 fallStartPos;

	[Header("パワーで重力をつける")]
	[SerializeField] private float fallSpeed;

	public bool isLampOn;

	PlayerMove playerMove;
	LampHitFloor lampHitFloor;

	public SpriteRenderer spriteRenderer;
	public Sprite sprite;

	RespawnManager respawnManager;
	PlayerCircle playerCircle;

	public bool isHitGrowBox;

	

	// Start is called before the first frame update
	void Start()
	{
		// リスポーンマネージャー
		GameObject respawnManagerObj = GameObject.Find("RespawnManager");
		respawnManager = respawnManagerObj.GetComponent<RespawnManager>();
		transform.position = respawnManager.GetRespawnPos();

		// Rigidbodyを取得
		rb = GetComponent<Rigidbody2D>();

		// PlayerMoveを取得
		GameObject playerObj = GameObject.Find("Player");
		playerMove = playerObj.GetComponent<PlayerMove>();

		// 子オブジェクト読み込み
		GameObject childObj = transform.Find("HitFloor").gameObject;
		// コンポーネント読み込み
		lampHitFloor = childObj.GetComponent<LampHitFloor>();

		//円
		GameObject circleObj = transform.Find("CircleObj").gameObject;
		playerCircle = circleObj.GetComponent<PlayerCircle>();

		// レイヤーを変更
		gameObject.layer = 10;

		isLampOn = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (playerMove.isLampCollect)
		{
			spriteRenderer.sprite = null;
		}
		else
		{
			if (playerMove.isPlaceMode) spriteRenderer.sprite = sprite;
		}

		// ランプを持っていないときしかイージングは掛けない
		// ランプが床についていないときも追加
		if (!playerMove.isLampTake)
		{
			// 上昇中の時
			if (isThrow)
			{
				// 時間を進ませる
				throwNowTime += Time.deltaTime;
				// 重力を受けないように
				if (rb != null) rb.velocity = new Vector2(rb.velocity.x, 0);

				// 当たり判定レイヤーを変更
				if (throwNowTime <= lampTime)
				{
					gameObject.layer = 10;
				}

				// ランプをつける
				if (throwNowTime >= lampTime)
				{
					// ランプをつける
					playerMove.isLightOn = true;
					isLampOn = true;
				}

				// トータルの時間を超えた場合
				if (throwNowTime >= throwTime)
				{
					// フラグをオフに
					isThrow = false;
					// 現在の時間をトータルの時間に設定
					throwNowTime = throwTime;
					// 落ちるイージング開始 変数初期化
					isFall = true;
					fallNowTime = 0;
					fallStartPos = transform.position;
				}

				// positionを変更
				rb.MovePosition(ExpOut(throwNowTime, throwTime, startPos, startPos + new Vector2(0, maxY)));
			}

			// 落ちるイージング
			if (isFall)
			{
				fallNowTime += Time.deltaTime;
				//重力を受けないように
				if (rb != null) rb.velocity = new Vector2(rb.velocity.x, 0);
				if (fallNowTime >= throwTime)
				{
					isFall = false;
					fallNowTime = throwTime;
					//自分で落下速度を調節しろ！！！！！！！！
					rb.velocity = new Vector2(rb.velocity.x, fallSpeed);
				}

				rb.MovePosition(ExpIn(fallNowTime, throwTime, fallStartPos, fallStartPos + new Vector2(0, -maxY - 1)));
			}
		}
	}

	private void FixedUpdate()
	{
		// nullチェック
		if (rb != null)
		{
			// 横に滑らないように
			rb.velocity = new Vector2(0, rb.velocity.y);
		}

		//ライトがシンクロするか(横に滑るようになってしまうかも)
		if (!playerMove.isLampTake && playerMove.lightSynchro)
		{
			if (!isThrow && !isFall)
			{
				rb.velocity = new Vector2(playerMove.rb.velocity.x, rb.velocity.y);
			}
		}

		// ランプをグリッドに合うように
		if (!playerMove.isLampTake && !isHitGrowBox)
		{
			// 整数部分
			int intLampPosX = (int)rb.position.x;
			// 小数部分
			float fltLampPosX = rb.position.x - intLampPosX;
			// 数値代入
			var pos = rb.position;

			if (fltLampPosX < 0.1f)
			{
				intLampPosX -= 1;
			}
			else if (fltLampPosX > 0.9f)
			{
				intLampPosX += 1;
			}

			pos.x = (float)intLampPosX + 0.5f;
			rb.position = pos;
		}
	}

	// イーズアウト
	public static Vector2 QuintOut(float t, float totaltime, Vector2 min, Vector2 max)
	{
		max -= min;
		t = t / totaltime - 1;
		return max * (t * t * t * t * t + 1) + min;
	}

	public static Vector2 ExpOut(float t, float totaltime, Vector2 min, Vector2 max)
	{
		max -= min;
		return t == totaltime ? max + min : max * (-Mathf.Pow(2, -10 * t / totaltime) + 1) + min;
	}

	// イーズイン
	public static Vector2 QuintIn(float t, float totaltime, Vector2 min, Vector2 max)
	{
		max -= min;
		t /= totaltime;
		return max * t * t * t * t * t * t * t * t * t * t + min;
	}

	public static Vector2 ExpIn(float t, float totaltime, Vector2 min, Vector2 max)
	{
		max -= min;
		return t == 0.0 ? min : max * Mathf.Pow(2, 10 * (t / totaltime - 1)) + min;
	}

	public void GetLampRb()
	{
		//Rigidbodyつける
		rb = gameObject.AddComponent<Rigidbody2D>();
		//FreezeRotationをオンにする
		rb.freezeRotation = true;
		// collisionDetectionを変更
		rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		//// Interpolateを変更
		//rb.interpolation = RigidbodyInterpolation2D.Interpolate;
	}

	// PlayerMoveに渡すための関数
	// 変数の初期化
	public void LampThrow(Vector3 pos)
	{
		// 投げたフラグをtrue
		isThrow = true;
		// タイムを0に
		throwNowTime = 0;
		// スタートポジションを現在のposに変更
		startPos = new Vector2(pos.x, pos.y + 1);
		// 落ちるフラグオフに
		isFall = false;
		// 落ちるカウントを0に
		fallNowTime = 0;
	}

	// rbを消す関数
	// こちらもPlayerMoveに渡す用
	public void RbLost()
	{
		Destroy(rb);
		// 床との当たり判定をオフに
		lampHitFloor.isHit = false;
		// 判定レイヤーを変更
		gameObject.layer = 10;
	}
	
}
