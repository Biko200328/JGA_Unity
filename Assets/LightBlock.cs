using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightBlock : MonoBehaviour
{
	// �t�F�[�h�A�E�g����܂ł̎���(0.5sec)
	public float fadeTime = 0.5f;
	[System.NonSerialized] public float time;
	private SpriteRenderer render;

	[System.NonSerialized] public CompositeCollider2D collider2D;

	public bool isLightHit;

	Lamp lamp;
	PlayerMove playerMove;

	[System.NonSerialized] public bool isAlphaZero;


	void Start()
	{
		render = GetComponent<SpriteRenderer>();
		Color color = render.color;
		color.a = 0;
		render.color = color;

		collider2D = GetComponent<CompositeCollider2D>();

		GameObject lampObj = GameObject.Find("Lamp");
		lamp = lampObj.GetComponent<Lamp>();

		GameObject playerObj = GameObject.Find("Player");
		playerMove = playerObj.GetComponent<PlayerMove>();
	}

	void Update()
	{
		// ���C�g�ɓ������ĂȂ��Ƃ�
		if(!isLightHit)
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
			else if(time >= fadeTime)
			{
				Color color = render.color;
				color.a = 0;
				render.color = color;
				// �����蔻�������
				collider2D.isTrigger = true;
				// ���t���O��0��
				isAlphaZero = true;
			}
		}

		if (!lamp.isLampOn)
		{
			isLightHit = false;
			isAlphaZero = false;
		}

		if (playerMove.isLampCollect)
		{
			isLightHit = false;
			isAlphaZero = false;
		}

		if (playerMove.isPlace)
		{
			isLightHit = false;
			isAlphaZero = false;
		}
	}
}
