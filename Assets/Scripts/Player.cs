using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("�ړ��X�s�[�h")]
	[SerializeField] float moveSpeed;
	bool isRightMove;
	bool isLeftMove;

	[Header("����p�^�[��")]
	[SerializeField] bool isCollectPattern = false;

	[Header("�v���C���[���")]
	[SerializeField]int[] state = {0,0};

	// ��
	GameObject redObj;
	Red red;

	// ��
	GameObject greenObj;
	Green green;

	Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		redObj = GameObject.Find("RedFire");
		red = redObj.gameObject.GetComponent<Red>();

		greenObj = GameObject.Find("GreenFire");
		green = greenObj.gameObject.GetComponent<Green>();
	}

	// Update is called once per frame
	void Update()
	{
		MoveUpdate();


		// ��ԕω�
		if (Input.GetKeyDown(KeyCode.Space))
		{
			if(!isCollectPattern)
			{
				state[0]++;

				// ���[�v������
				if (state[0] > 3)
				{
					state[0] = 0;
				}

				//�ԉ��
				if (state[0] == 1)
				{
					// �v���C���[�ɐe�q�t������
					redObj.transform.SetParent(transform);
					// �|�W�V�������v���C���[�Ɠ�����
					redObj.transform.position = transform.position;
				}
				// �Ԑݒu
				else if (state[0] == 2)
				{
					// �e�q�t������������
					redObj.transform.SetParent(null);
				}
				// �Ή��
				else if (state[0] == 3)
				{
					// �v���C���[�ɐe�q�t������
					greenObj.transform.SetParent(transform);
					// �|�W�V�������v���C���[�Ɠ�����
					greenObj.transform.position = transform.position;
				}
				//�ΐݒu
				else if (state[0] == 0)
				{
					// �e�q�t������������
					greenObj.transform.SetParent(null);
				}
			}
			else
			{
				state[1]++;
				// ���[�v������
				if (state[1] > 1)
				{
					state[1] = 0;
				}

				// �ԃe���|�[�g
				if (state[1] == 1)
				{
					// �|�W�V�������v���C���[�Ɠ�����
					redObj.transform.position = transform.position;
				}
				// �΃e���|�[�g
				else
				{
					// �|�W�V�������v���C���[�Ɠ�����
					greenObj.transform.position = transform.position;
				}
			}
		}

		if(!isCollectPattern)
		{
			switch (state[0])
			{
				case 0:
					green.SetCollect(false);
					break;
				case 1:
					red.SetCollect(true);
					break;
				case 2:
					red.SetCollect(false);
					break;
				case 3:
					green.SetCollect(true);
					break;
			}
		}
		
	}

	private void FixedUpdate()
	{
		MoveFixedUpdate();
	}

	private void MoveUpdate()
	{
		if (Input.GetKey(KeyCode.A))
		{
			isRightMove = true;
			isLeftMove = false;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			isLeftMove = true;
			isRightMove = false;
		}
		else
		{
			isLeftMove = false;
			isRightMove = false;
		}
	}

	private void MoveFixedUpdate()
	{
		if (isRightMove)
		{
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
			transform.eulerAngles = new Vector3(0, 0, 0);
		}
		else if (isLeftMove)
		{
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
			transform.eulerAngles = new Vector3(0, 180, 0);
		}
		else
		{
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
	}
}
