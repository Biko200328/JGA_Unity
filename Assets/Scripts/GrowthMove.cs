using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowthMove : MonoBehaviour
{
	[Header("�ړ��o�H")] public GameObject[] movePoint;
	[Header("����")] public float speed = 1.0f;

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
			//�ʏ�i�s
			if (!returnPoint)
			{
				int nextPoint = nowPoint + 1;

				//�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
				if (Vector3.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
				{
					//���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
					Vector3 toVector = Vector3.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);
					// z����]
					var n2n = movePoint[nextPoint].transform.position - movePoint[nowPoint].transform.position;
					// transform���擾
					Transform myTransform = this.transform;

					// ���[�J�����W����ɁA��]���擾
					Vector3 localAngle = myTransform.localEulerAngles;
					if (n2n.x >= 1)
					{
						localAngle.z = -90.0f; // ���[�J�����W����ɁAz�������ɂ�����]��-90�x�ɕύX
					}
					else if (n2n.x <= -1)
					{
						localAngle.z = 90.0f; // ���[�J�����W����ɁAz�������ɂ�����]��90�x�ɕύX
					}
					else if (n2n.y >= 1)
					{
						localAngle.z = 0.0f; // ���[�J�����W����ɁAz�������ɂ�����]��0�x�ɕύX
					}
					else if (n2n.y <= -1)
					{
						localAngle.z = 180.0f; // ���[�J�����W����ɁAz�������ɂ�����]��180�x�ɕύX
					}
					myTransform.localEulerAngles = localAngle; // ��]�p�x��ݒ�
					//���̃|�C���g�ֈړ�
					rb.MovePosition(toVector);
				}
				//���̃|�C���g���P�i�߂�
				else
				{
					rb.MovePosition(movePoint[nextPoint].transform.position);
					++nowPoint;

					//���ݒn���z��̍Ōゾ�����ꍇ
					if (nowPoint + 1 >= movePoint.Length)
					{
						growth.isEnd = true;
						movePoint = null;
					}
				}
			}
			//�ܕԂ��i�s
			//else
			//{
			//	int nextPoint = nowPoint - 1;

			//	//�ڕW�|�C���g�Ƃ̌덷���킸���ɂȂ�܂ňړ�
			//	if (Vector2.Distance(transform.position, movePoint[nextPoint].transform.position) > 0.1f)
			//	{
			//		//���ݒn���玟�̃|�C���g�ւ̃x�N�g�����쐬
			//		Vector2 toVector = Vector2.MoveTowards(transform.position, movePoint[nextPoint].transform.position, speed * Time.deltaTime);

			//		//���̃|�C���g�ֈړ�
			//		rb.MovePosition(toVector);
			//	}
			//	//���̃|�C���g���P�߂�
			//	else
			//	{
			//		rb.MovePosition(movePoint[nextPoint].transform.position);
			//		--nowPoint;

			//		//���ݒn���z��̍ŏ��������ꍇ
			//		if (nowPoint <= 0)
			//		{
			//			returnPoint = false;
			//		}
			//	}
			//}
		}
	}
}
