using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFloor : MonoBehaviour
{
	// 接地判定
	// PlayerMoveに渡すのでpublic宣言
	public bool isHit;

	Rigidbody2D playerRb;

	// Start is called before the first frame update
	void Start()
	{
		GameObject player = GameObject.Find("Player");
		playerRb = player.gameObject.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	// OnTriggerEnter     当たった(触れた)瞬間
	// OnTriggerStay      当たっている(触れている)間
	// OnTriggerExit      離れた瞬間
	// Trigger以外にもOncollision~~がある
	// 読み込んでいるcolliderがTrigger(すり抜ける)かどうかの違い
	// TriggerにするかどうかはcolliderのInspectorビューにチェックボックスがあるよ

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// 当たったcollisionのtagがFloorなら接地とする
		// そうしないとどのObjに当たった時でもJumpできてしまう
		if (collision.gameObject.tag == "Floor") isHit = true;

		// tagはInspectorビューのObjの名前の下で変えるところがある
		// tagの新規追加もそこでできるからFloorを作ってtilemapに設定する
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor") isHit = true;

		if(collision.gameObject.tag != "Floor")
		{
			Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
			if(rb.velocity.x != 0)
			{
				playerRb.velocity = new Vector2(rb.velocity.x, playerRb.velocity.y);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// 離れた時だけfalseにしてあげれば空中にいるときはfalseになる
		if (collision.gameObject.tag == "Floor") isHit = false;
	}
}
