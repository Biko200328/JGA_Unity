using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenBlock : MonoBehaviour
{
	// �t�F�[�h�A�E�g����܂ł̎���(0.5sec)
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
		// ���C�g�ɓ������ĂȂ��Ƃ�
		if (!isLightHit)
		{
			time += Time.deltaTime;
			// ���X�ɔ������Ă���
			if (time < fadeTime)
			{
				float alpha = 1.0f - time / fadeTime;
				Color color = render.color;
				color.a = alpha;
				render.color = color;
			}
			// ���Ԃ𒴂������Ɋ��S�ɏ���
			else if (time >= fadeTime)
			{
				Color color = render.color;
				color.a = 0;
				render.color = color;
				// �����蔻�������
				collider.isTrigger = true;
				// ���t���O��0��
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
