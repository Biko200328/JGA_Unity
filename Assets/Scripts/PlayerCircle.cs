using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
	[Header("�~�̍ő咼�a")]
	public float maxCircleSize = 1;

	[Header("�~�̍ŏ����a")]
	public float minCircleSize = 0;

	[Header("�T�C�Y�������l")]
	[SerializeField] float changeSize;

	// ���݂̉~�͈̔�
	private float circleSize;

	PlayerMove playerMove;

	// Start is called before the first frame update
	void Start()
	{
		GameObject player = GameObject.Find("Player");
		playerMove = player.GetComponent<PlayerMove>();
	}

	// Update is called once per frame
	void Update()
	{
		if(playerMove.isLightOn)
		{
			//�ő�l���Ⴉ�����瑫��
			if(circleSize < maxCircleSize)
			{
				circleSize += changeSize;
			}

			//�ő�l���傫��������ő�l�ɍ��킹��
			if(circleSize > maxCircleSize)
			{
				circleSize = maxCircleSize;
			}
		}
		else
		{
			// �ŏ��l(0)���傫�����������
			if (circleSize > minCircleSize)
			{
				circleSize -= changeSize;
			}

			//�ŏ��l��菬����������ŏ��l�ɍ��킹��
			if (circleSize < minCircleSize)
			{
				circleSize = minCircleSize;
			}
		}

		var circleVec = new Vector3(circleSize, circleSize / 2, circleSize);
		transform.localScale = circleVec;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "block")
		{
			Rigidbody2D blockRb = collision.gameObject.GetComponent<Rigidbody2D>();
			blockRb.isKinematic = false;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "block")
		{
			Rigidbody2D blockRb = collision.gameObject.GetComponent<Rigidbody2D>();
			blockRb.isKinematic = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "block")
		{
			Rigidbody2D blockRb = collision.gameObject.GetComponent<Rigidbody2D>();
			blockRb.isKinematic = true;
			blockRb.velocity = Vector3.zero;
		}
	}
}
