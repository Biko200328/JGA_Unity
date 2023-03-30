using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlock : MonoBehaviour
{
	// フェードアウトするまでの時間(0.5sec)
	public float fadeTime = 0.5f;
	[System.NonSerialized] public float time;
	private SpriteRenderer render;

	[System.NonSerialized]  CompositeCollider2D collider;

	public bool isLightHit = false;

	[System.NonSerialized] public bool isAlphaZero;
	void Start()
	{
		render = GetComponent<SpriteRenderer>();
		Color color = render.color;
		color.a = 0;
		render.color = color;

		collider = GetComponent<CompositeCollider2D>();
	}

	void Update()
	{
		// ライトに当たってないとき
		if (!isLightHit)
		{
			time += Time.deltaTime;
			// 徐々に薄くしていく
			if (time < fadeTime)
			{
				float alpha = 1.0f - time / fadeTime;
				Color color = render.color;
				color.a = alpha;
				render.color = color;
			}
			// 時間を超えた時に完全に消す
			else if (time >= fadeTime)
			{
				Color color = render.color;
				color.a = 0;
				render.color = color;
				// 当たり判定を消す
				collider.isTrigger = true;
				// αフラグを0に
				isAlphaZero = true;
			}
		}
		else
		{
			collider.isTrigger = false;
			time = 0;
			isAlphaZero = false;
			Color color = render.color;
			color.a = 100;
			render.color = color;
		}
	}
}
