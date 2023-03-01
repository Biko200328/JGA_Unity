using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	// [[Header("hoge")]] は ScriptのInspectorビューに文字が表示できる
	[Header("移動スピード")]
	// [SerializeField] は privateでもInspectorビューで数値が変更できる
	[SerializeField] private float moveSpeed;

	// 動いてるかどうか
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
		// コントローラーの左右入力数値を受け取る
		inputHorizontal = Input.GetAxis("cHorizontalL");

		//動いてるかどうか判断
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || inputHorizontal != 0)
		{
			isMove = true;
		}
		else
		{
			isMove = false;
		}


		TakeLight();
	}

	// 一定間隔で呼ぶupdate
	private void FixedUpdate()
	{
		// 動いているかのフラグがオンなら
		if(isMove)Move();
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
			//左に移動
			pos.x -= moveSpeed;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			//右に移動
			pos.x += moveSpeed;
		}

		// コントローラーの数値を足す
		pos.x += inputHorizontal * moveSpeed;

        // 現在のポジションを保持する
        Vector3 currentPos = pos;

        // 範囲を超えていたら範囲内の値を代入する
        currentPos.x = Mathf.Clamp(currentPos.x, -limitX, limitX);
        currentPos.y = Mathf.Clamp(currentPos.y, -limitY, limitY);

        // 受け取って数値変更したposをrbに返します
        rb.position = currentPos;
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
		if(Input.GetButtonDown("buttonRB") || Input.GetKeyDown(KeyCode.Space))
		{
			isLightOn = !isLightOn;
		}
	}
}
