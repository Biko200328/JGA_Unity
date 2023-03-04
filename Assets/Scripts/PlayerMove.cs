using System.Collections;
using System.Collections.Generic;
using UnityEditor.U2D.IK;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	// �ړ��X�s�[�h
	[Header("�ړ��X�s�[�h")]
	[SerializeField] private float moveSpeed;

	// �����Ă邩�ǂ���
	bool isRightMove;
	bool isLeftMove;
	bool isControllerMove;

	// �X�s�[�h�Ɠ����悤�ɃW�����v�͂̕ϐ������
	[Header("�W�����v��")]
	[SerializeField] private float jumpPower;

	// �R���g���[���[�̓��͐��l
	float inputHorizontal;
	float inputVertical;

	//// �ړ�����
	//float limitX = 15.5f;
	//float limitY = 8.0f;

	// �΂����Ă邩�ǂ����t���O
	[Header("�t���O")]
	public bool isLightOn;

	public bool isOnMoveBlock;


	Rigidbody2D rb;

	HitFloor hitFloor;
	// Start is called before the first frame update
	void Start()
	{
		// Rigidbody���擾
		rb = gameObject.GetComponent<Rigidbody2D>();

		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject child = transform.Find("HitFloor").gameObject;
		// class�ǂݍ���
		hitFloor = child.GetComponent<HitFloor>();
	}

	// Update is called once per frame
	void Update()
	{
		Move();

		TakeLight();
	}

	//�����Ă邩�ǂ����̃t���O�Ǘ�
	// Update�ɓ����
	private void Move()
	{
		// �R���g���[���[�̍��E���͐��l���󂯎��
		inputHorizontal = Input.GetAxis("cHorizontalL");

		//�����Ă邩�ǂ������f
		if (Input.GetKey(KeyCode.A))
		{
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
		}
		else if (inputHorizontal != 0)
		{
			rb.velocity = new Vector2(inputHorizontal * moveSpeed, rb.velocity.y);
		}
		else
		{
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
	}

	private void Jump()
	{
		//�ڒn���Ă���Ƃ���Space(Jump�{�^��)����������
		if (hitFloor.isHit == true)
		{
			if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("buttonA"))
			{
				rb.velocity += new Vector2(0, jumpPower);
			}
		}
	}

	private void TakeLight()
	{
		// �I���I�t�؂�ւ�
		if (Input.GetButtonDown("buttonRB") || Input.GetKeyDown(KeyCode.Space))
		{
			isLightOn = !isLightOn;
		}
	}

	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if(collision.gameObject.tag == "rightMoveBlock")
	//	{
	//		isOnMoveBlock = true;
	//		transform.SetParent(collision.transform);
	//	}

	//	if (collision.gameObject.tag == "downMoveBlock")
	//	{
	//		isOnMoveBlock = true;
	//		transform.SetParent(collision.transform);
	//	}
	//}

	//private void OnCollisionExit2D(Collision2D collision)
	//{
	//	if (collision.gameObject.tag == "rightMoveBlock")
	//	{
	//		isOnMoveBlock = false;
	//		transform.SetParent(null);
	//	}

	//	if (collision.gameObject.tag == "downMoveBlock")
	//	{
	//		isOnMoveBlock = false;
	//		transform.SetParent(null);
	//	}
	//}
}
