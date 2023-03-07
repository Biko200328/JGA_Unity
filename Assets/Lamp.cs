using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
	[SerializeField]Rigidbody2D rb;

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

	PlayerMove playerMove;
	LampHitFloor lampHitFloor;

	// Start is called before the first frame update
	void Start()
	{
		// Rigidbodyを取得
		rb = GetComponent<Rigidbody2D>();

		// PlayerMoveを取得
		GameObject playerObj = GameObject.Find("Player");
		playerMove = playerObj.GetComponent<PlayerMove>();

		// 子オブジェクト読み込み
		GameObject childObj = transform.Find("HitFloor").gameObject;
		// コンポーネント読み込み
		lampHitFloor = childObj.GetComponent<LampHitFloor>();
	}

	// Update is called once per frame
	void Update()
	{
		// ランプを持っていないときしかイージングは掛けない
		// ランプが床についていないときも追加
		if(!playerMove.isLampTake)
		{
			// 上昇中の時
			if (isThrow)
			{
				// 時間を進ませる
				throwNowTime += Time.deltaTime;
				//重力を受けないように
				if (rb != null) rb.velocity = new Vector2(rb.velocity.x, 0);

				// ランプをつける
				if(throwNowTime >= lampTime)
				{
					// ランプをつける
					playerMove.isLightOn = true;
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
				transform.position = ExpOut(throwNowTime, throwTime, startPos, startPos + new Vector2(0, maxY));
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

				transform.position = ExpIn(fallNowTime, throwTime, fallStartPos, fallStartPos + new Vector2(0, -maxY - 1));
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

	// PlayerMoveに渡すための関数
	// 変数の初期化
	public void LampThrow(Vector3 pos)
	{
		//Rigidbodyつける
		rb = gameObject.AddComponent<Rigidbody2D>();
		//FreezeRotationをオンにする
		rb.freezeRotation = true;
		// collisionDetectionを変更
		rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		// Interpolateを変更
		rb.interpolation = RigidbodyInterpolation2D.Interpolate;

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
	}
}
