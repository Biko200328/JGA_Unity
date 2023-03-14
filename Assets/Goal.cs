using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	public bool isGoal;
	[SerializeField] GameObject gate;
	[SerializeField] GameObject circle;

	[Header("開いた時のスプライト")]
	[SerializeField] Sprite openTex;

	[Header("速くなるスピード")]
	[SerializeField] float addCircle;

	[Header("円の最大値")]
	[SerializeField] float maxCircle;

	SpriteRenderer gateRenderer;


	// Start is called before the first frame update
	void Start()
	{
		gateRenderer = gate.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if(isGoal)
		{
			// 円を大きくする
			if(circle.transform.localScale.x < maxCircle)
			{
				circle.transform.localScale += new Vector3(addCircle, addCircle);
			}
			else
			{
				circle.transform.localScale = new Vector3(maxCircle, maxCircle);
			}

			//ゲートを開ける
			//当たり判定を消す
			Destroy(gate.GetComponent<BoxCollider2D>());
			//スプライトの変更
			gateRenderer.sprite = openTex;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "lamp")
		{
			isGoal = true;
		}
	}
}
