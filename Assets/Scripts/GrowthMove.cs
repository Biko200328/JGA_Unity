using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthMove : MonoBehaviour
{
	[Header("移動経路")] public GameObject[] movePoint;
	[Header("速さ")] public float speed = 1.0f;

	public Growth growth;

	private Rigidbody2D rb;
	private int nowPoint = 0;
	private bool returnPoint = false;

	public bool isStop = false;

	private void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		if (movePoint != null && movePoint.Length > 0 && rb != null)
		{
			rb.position = movePoint[0].transform.position;
		}
	}

	private void Update()
	{
		if (!growth.isLightIn)
		{
			Destroy(this.gameObject);
		}

		if(isStop)
		{
			growth.isEnd = true;
		}
	}

	private void FixedUpdate()
	{
		if (movePoint != null && movePoint.Length > 1 && rb != null && !growth.isEnd && !isStop)
		{
			//通常進行
			if (!returnPoint)
			{
				int nextPoint = nowPoint + 1;

				//目標ポイントとの誤差がわずかになるまで移動
				if (Vector3.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
				{
					//現在地から次のポイントへのベクトルを作成
					Vector3 toVector = Vector3.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);
					// z軸回転
					var n2n = movePoint[nextPoint].transform.position - movePoint[nowPoint].transform.position;
					// transformを取得
					Transform myTransform = this.transform;

					// ローカル座標を基準に、回転を取得
					Vector3 localAngle = myTransform.localEulerAngles;
					if (n2n.x >= 1)
					{
						localAngle.z = -90.0f; // ローカル座標を基準に、z軸を軸にした回転を-90度に変更
					}
					else if (n2n.x <= -1)
					{
						localAngle.z = 90.0f; // ローカル座標を基準に、z軸を軸にした回転を90度に変更
					}
					else if (n2n.y >= 1)
					{
						localAngle.z = 0.0f; // ローカル座標を基準に、z軸を軸にした回転を0度に変更
					}
					else if (n2n.y <= -1)
					{
						localAngle.z = 180.0f; // ローカル座標を基準に、z軸を軸にした回転を180度に変更
					}
					myTransform.localEulerAngles = localAngle; // 回転角度を設定
					//次のポイントへ移動
					rb.MovePosition(toVector);
				}
				//次のポイントを１つ進める
				else
				{
					rb.MovePosition(movePoint[nextPoint].transform.position);
					++nowPoint;

					//現在地が配列の最後だった場合
					if (nowPoint + 1 >= movePoint.Length)
					{
						growth.isEnd = true;
						movePoint = null;
					}
				}
			}
			//折返し進行
			//else
			//{
			//	int nextPoint = nowPoint - 1;

			//	//目標ポイントとの誤差がわずかになるまで移動
			//	if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
			//	{
			//		//現在地から次のポイントへのベクトルを作成
			//		Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

			//		//次のポイントへ移動
			//		rb.MovePosition(toVector);
			//	}
			//	//次のポイントを１つ戻す
			//	else
			//	{
			//		rb.MovePosition(movePoint[nextPoint].transform.position);
			//		--nowPoint;

			//		//現在地が配列の最初だった場合
			//		if (nowPoint <= 0)
			//		{
			//			returnPoint = false;
			//		}
			//	}
			//}
		}
	}
}
