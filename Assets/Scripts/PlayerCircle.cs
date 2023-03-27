using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
	[Header("円の最大直径")]
	public float inputCircleSize = 9;
	float maxCircleSize = 0;

    [Header("円の最小直径")]
	public float minCircleSize = 0;

	[Header("サイズ増減数値")]
	[SerializeField] float changeSize;

	// 現在の円の範囲
	public float circleSize;

	PlayerMove playerMove;
	Lamp lamp;

	JumpHitLeft jumpHitLeft;
	JumpHitLeft jumpHitLeft2;
	JumpHitRight jumpHitRight;
	JumpHitRight jumpHitRight2;

	// Start is called before the first frame update
	void Start()
	{
		GameObject player = GameObject.Find("Player");
		playerMove = player.gameObject.GetComponent<PlayerMove>();

		GameObject lampObj = GameObject.Find("Lamp");
		lamp = lampObj.GetComponent<Lamp>();

		// 子オブジェクト読み込み
		GameObject childJumpR = player.transform.Find("JumpHitRight").gameObject;
		// コンポーネント読み込み
		jumpHitRight = childJumpR.GetComponent<JumpHitRight>();
		// 子オブジェクト読み込み
		GameObject childJumpR2 = player.transform.Find("JumpHitRight2").gameObject;
		// コンポーネント読み込み
		jumpHitRight2 = childJumpR2.GetComponent<JumpHitRight>();

		// 子オブジェクト読み込み
		GameObject childJumpL = player.transform.Find("JumpHitLeft").gameObject;
		// コンポーネント読み込み
		jumpHitLeft = childJumpL.GetComponent<JumpHitLeft>();
		// 子オブジェクト読み込み
		GameObject childJumpL2 = player.transform.Find("JumpHitLeft2").gameObject;
		// コンポーネント読み込み
		jumpHitLeft2 = childJumpL2.GetComponent<JumpHitLeft>();
	}

	// Update is called once per frame
	void Update()
	{
		maxCircleSize = inputCircleSize;

		//光の範囲が変わる処理
        if (playerMove.lightSizeChange)
		{
			if (playerMove.isLampTake)
			{
				maxCircleSize = inputCircleSize;
			}
			else
			{
				//置いているときは光の範囲を+3する
				maxCircleSize = inputCircleSize + 3;
			}
        }

		// 置いてる時は円をなくす
		if(playerMove.isPlace)
		{
			circleSize = 0;
		}

		if(playerMove.isLampCollect)
		{
			circleSize = 0;
		}
		else
		{
			if (playerMove.isLightOn)
			{
				//最大値より低かったら足す
				if (circleSize < maxCircleSize)
				{
					circleSize += changeSize;
				}

				//最大値より大きかったら最大値に合わせる
				if (circleSize > maxCircleSize)
				{
					circleSize = maxCircleSize;
				}
			}
			else
			{
				// 最小値(0)より大きかったら引く
				if (circleSize > minCircleSize)
				{
					circleSize -= changeSize;
				}

				//最小値より小さかったら最小値に合わせる
				if (circleSize < minCircleSize)
				{
					circleSize = minCircleSize;
				}
			}
		}
		
		var circleVec = new Vector3(circleSize, circleSize, circleSize);

		transform.localScale = circleVec;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//ギミックのフラグ管理
		GimmicksOn(collision);

		// プレイヤー
		if (collision.gameObject.tag == "Player")
		{
			playerMove.isLightIn = true;
		}

		// 蛇ブロック
		if (collision.gameObject.tag == "growOriginal")
		{
			Growth growth = collision.GetComponent<Growth>();
			growth.isLightIn = true;
		}

		// 円の中だと出るブロック
		if(collision.gameObject.tag == "LightBlock")
		{
			LightBlock lightBlock = collision.GetComponent<LightBlock>();
			lightBlock.isLightHit = true;
			lightBlock.time = 0;
			lightBlock.isAlphaZero = false;
			SpriteRenderer render = collision.GetComponent<SpriteRenderer>();
			Color color = render.color;
			color.a = 100;
			render.color = color;
			// 当たり判定をつけなおす
			BoxCollider2D boxCollider2D = collision.GetComponent<BoxCollider2D>();
			boxCollider2D.isTrigger = false;
		}

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		//ギミックのフラグ管理
		GimmicksOn(collision);

		// プレイヤー
		if (collision.gameObject.tag == "Player")
		{
			playerMove.isLightIn = true;
		}

		// 蛇ブロック
		if (collision.gameObject.tag == "growOriginal")
		{
			Growth growth = collision.GetComponent<Growth>();
			growth.isLightIn = true;
		}

		// 円の中だと出るブロック
		if (collision.gameObject.tag == "LightBlock")
		{
			LightBlock lightBlock = collision.GetComponent<LightBlock>();
			lightBlock.isLightHit = true;
			lightBlock.time = 0;
			lightBlock.isAlphaZero = false;
			SpriteRenderer render = collision.GetComponent<SpriteRenderer>();
			Color color = render.color;
			color.a = 100;
			render.color = color;
			// 当たり判定をつけなおす
			CompositeCollider2D boxCollider2D = collision.GetComponent<CompositeCollider2D>();
			boxCollider2D.isTrigger = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//ギミックのフラグ管理
		GimmicksOff(collision);

		// プレイヤー
		if (collision.gameObject.tag == "Player")
		{
			playerMove.isLightIn = false;
		}

		// 蛇ブロック
		if (collision.gameObject.tag == "growOriginal")
		{
			Growth growth = collision.GetComponent<Growth>();
			growth.isLightIn = false;
			growth.isFirst = false;
			growth.isEnd = false;
			lamp.isHitGrowBox = false;
			jumpHitLeft.isHit = false;
			jumpHitLeft2.isHit = false;
			jumpHitRight.isHit = false;
			jumpHitRight2.isHit = false;
		}

		// 円の中だと出るブロック
		if (collision.gameObject.tag == "LightBlock")
		{
			LightBlock lightBlock = collision.GetComponent<LightBlock>();
			lightBlock.isLightHit = false;
		}
	}

	private void GimmicksOn(Collider2D collision)
	{
		// 普通のブロック
		if (collision.gameObject.tag == "block")
		{
			Rigidbody2D blockRb = collision.gameObject.GetComponent<Rigidbody2D>();
			blockRb.isKinematic = false;
			Block block = collision.gameObject.GetComponent<Block>();
			block.isLightIn = true;
		}

		// 左に移動するブロック
		if (collision.gameObject.tag == "leftMoveBlock")
		{
			LeftMoveBlock leftMoveBlock = collision.gameObject.GetComponent<LeftMoveBlock>();
			leftMoveBlock.isMove = true;
		}

		// 右に移動するブロック
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			RightMoveBlock rightMoveBlock = collision.gameObject.GetComponent<RightMoveBlock>();
			rightMoveBlock.isMove = true;
		}

		// 上に移動するブロック
		if (collision.gameObject.tag == "upMoveBlock")
		{
			UpMoveBlock upMoveBlock = collision.gameObject.GetComponent<UpMoveBlock>();
			upMoveBlock.isMove = true;
		}

		// 下に移動するブロック
		if (collision.gameObject.tag == "downMoveBlock")
		{
			DownMoveBlock downMoveBlock = collision.gameObject.GetComponent<DownMoveBlock>();
			downMoveBlock.isMove = true;
		}
	}

	private void GimmicksOff(Collider2D collision)
	{
		// 普通のブロック
		if (collision.gameObject.tag == "block")
		{
			Rigidbody2D blockRb = collision.gameObject.GetComponent<Rigidbody2D>();
			blockRb.isKinematic = true;
			blockRb.velocity = Vector3.zero;
			Block block = collision.gameObject.GetComponent<Block>();
			block.isLightIn = false;
		}

		// 左に移動するブロック
		if (collision.gameObject.tag == "leftMoveBlock")
		{
			LeftMoveBlock leftMoveBlock = collision.gameObject.GetComponent<LeftMoveBlock>();
			leftMoveBlock.isMove = false;
		}

		// 右に移動するブロック
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			RightMoveBlock rightMoveBlock = collision.gameObject.GetComponent<RightMoveBlock>();
			rightMoveBlock.isMove = false;
		}

		// 上に移動するブロック
		if (collision.gameObject.tag == "upMoveBlock")
		{
			UpMoveBlock upMoveBlock = collision.gameObject.GetComponent<UpMoveBlock>();
			upMoveBlock.isMove = false;
		}

		// 下に移動するブロック
		if (collision.gameObject.tag == "downMoveBlock")
		{
			DownMoveBlock downMoveBlock = collision.gameObject.GetComponent<DownMoveBlock>();
			downMoveBlock.isMove = false;
		}
	}
}
