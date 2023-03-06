using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	// Rigidbody宣言
	Rigidbody2D rb;
	// rbを有効化できてるか見るフラグ
	public bool isRbOff;
	// Start is called before the first frame update
	void Start()
	{
		// コンポーネント受け取り
		rb = this.gameObject.GetComponent<Rigidbody2D>();
		// 物理演算を行わないように設定
		rb.isKinematic = true;
	}

	// Update is called once per frame
	void Update()
	{
		// 物理演算がOnかOffかを見る
		isRbOff = rb.isKinematic;
		// velocityを常にOnに
		rb.velocity = new Vector2(0,rb.velocity.y);
	}

	//円の中にいなくても動かしたいときはコメントアウト
	//private void OnCollisionEnter(Collision collision)
	//{
	//	//当たっている物のRigidbodyを受け取る
	//	Rigidbody2D collisionRb = collision.gameObject.GetComponent<Rigidbody2D>();
	//	//それがx軸移動していたら
	//	if(collisionRb.velocity.x != 0)
	//	{
	//		//その分のvelocityをこのブロックに反映させる
	//		rb.velocity = new Vector2(collisionRb.velocity.x, rb.velocity.y);
	//	}
	//}
}
