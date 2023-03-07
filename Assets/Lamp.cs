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

	PlayerMove playerMove;

	// Start is called before the first frame update
	void Start()
	{
		// Rigidbodyを取得
		rb = GetComponent<Rigidbody2D>();

		// PlayerMoveを取得
		GameObject playerObj = GameObject.Find("Player");
		playerMove = playerObj.GetComponent<PlayerMove>();
	}

	// Update is called once per frame
	void Update()
	{
		// ランプを持っていないときしかイージングは掛けない
		if(!playerMove.isLampTake)
		{
			// 上昇中の時
			if (isThrow)
			{
				// 時間を進ませる
				throwNowTime += Time.deltaTime;
				// 重力がかからいないように設定
				rb.velocity = Vector2.zero;
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
				transform.position = QuintOut(throwNowTime, throwTime, startPos, startPos + new Vector2(0, maxY));
			}

			// 落ちるイージング
			//if (isFall)
			//{
			//	fallNowTime += Time.deltaTime;
			//	if (rb != null) rb.velocity = Vector2.zero;
			//	if (fallNowTime >= throwTime)
			//	{
			//		isFall = false;
			//		fallNowTime = throwTime;
			//	}

			//	transform.position = QuintIn(fallNowTime, throwTime, fallStartPos, fallStartPos + new Vector2(0, -maxY - 1));
			//}
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

	// イーズイン
	public static Vector2 QuintIn(float t, float totaltime, Vector2 min, Vector2 max)
	{
		max -= min;
		t /= totaltime;
		return max * t * t * t * t * t + min;
	}

	// PlayerMoveに渡すための関数
	// 変数の初期化
	public void LampThrow(Vector3 pos)
	{
		//Rigidbodyつける
		rb = gameObject.AddComponent<Rigidbody2D>();
		//FreezeRotationをオンにする
		rb.freezeRotation = true;

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
