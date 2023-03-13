using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Block : MonoBehaviour
{
	// Rigidbody宣言
	Rigidbody2D rb;
	// rbを有効化できてるか見るフラグ
	public bool isLightIn;

	Lamp lamp;

	// Start is called before the first frame update
	void Start()
	{
		// コンポーネント受け取り
		rb = this.gameObject.GetComponent<Rigidbody2D>();
		// 物理演算を行わないように設定
		rb.isKinematic = true;

		GameObject lampObj = GameObject.Find("Lamp");
		lamp = lampObj.GetComponent<Lamp>();
	}

	// Update is called once per frame
	void Update()
	{
		// velocityを常にOnに
		rb.velocity = new Vector2(0,rb.velocity.y);

		if(!isLightIn)
		{
			transform.SetParent(null);
		}

		if (!lamp.isLampOn)
		{
			isLightIn = false;
		}
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
