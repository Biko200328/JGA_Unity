using System.Collections;
using System.Collections.Generic;
using UnityEditor.PackageManager.UI;
using UnityEditor.U2D.IK;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	// �ړ��X�s�[�h
	[Header("�ړ��X�s�[�h")]
	[SerializeField] private float moveSpeed;

	// �R���g���[���[�̓��͐��l
	float inputHorizontal;
	float inputVertical;

	//// �ړ�����
	//float limitX = 15.5f;
	//float limitY = 8.0f;

	// �����Ă邩�ǂ���
	bool isRightMove;
	bool isLeftMove;
	bool isControllerMove;

	// �X�s�[�h�Ɠ����悤�ɃW�����v�͂̕ϐ������
	[Header("�W�����v��")]
	[SerializeField] private float jumpPower;
	[SerializeField] private float lampJumpPower;

	[Header("�t���O")]
	// ���􂵂Ă��邩�ǂ���
	public bool isLampTake;
	// ����̒��ɂ��邩�ǂ���
	public bool isLightIn;
	// �΂����Ă邩�ǂ����t���O
	public bool isLightOn;
	// �ׂɈ�}�X�̃u���b�N����������
	public bool isNextBlockL = false;
	public bool isNextBlockR = false;

	Rigidbody2D rb;
	HitFloor hitFloor;
	HitCeiling hitCeiling;
	GameObject lampObj;
	Rigidbody2D lampRb;
	// Start is called before the first frame update
	void Start()
	{
		// Rigidbody���擾
		rb = gameObject.GetComponent<Rigidbody2D>();

		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childFloor = transform.Find("HitFloor").gameObject;
		// �R���|�[�l���g�ǂݍ���
		hitFloor = childFloor.GetComponent<HitFloor>();

		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childCeiling = transform.Find("HitCeiling").gameObject;
		// �R���|�[�l���g�ǂݍ���
		hitCeiling = childCeiling.GetComponent<HitCeiling>();

		// lamp�ǂݍ���
		lampObj = GameObject.Find("Lamp");
		// �����v��Rigidbody���擾
		lampRb = lampObj.GetComponent<Rigidbody2D>();

		//�����v������
		isLightOn = true;
	}

	// Update is called once per frame
	void Update()
	{
		Move();

		//TakeLight();

		AutoJump();

		TakeLamp();
	}

	//�ړ�
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

	//�W�����v
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

	private void AutoJump()
	{

		if (isNextBlockL && hitFloor.isHit)
		{
			if (Input.GetKey(KeyCode.A))
			{
				rb.velocity = new Vector2(0, jumpPower);
			}
		}

		if (isNextBlockR && hitFloor.isHit)
		{
			if (Input.GetKey(KeyCode.D))
			{
				rb.velocity = new Vector2(0, jumpPower);
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

	private void TakeLamp()
	{
		// ���C�g�̒��ɂ��Ă��v���C���[�̏�Ƀu���b�N���Ȃ��Ƃ��Ƀ����v���Ă�
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// �����v�������Ă���Ƃ�
			// �n�ʂɂ��Ă���Ƃ�
			if (isLampTake && hitFloor.isHit)
			{
				isLampTake = false;
				//Rigidbody����
				lampRb = lampObj.AddComponent<Rigidbody2D>();
				//FreezeRotation���I���ɂ���
				lampRb.freezeRotation = true;
				//��ɔ�΂�
				lampRb.velocity += new Vector2(0, lampJumpPower);
			}
			// �����v�������Ă��Ȃ��Ƃ�
			// ���C�g�̒��ɂ��ď�Ƀu���b�N���Ȃ��Ƃ�
			else if(!isLampTake && isLightIn && !hitCeiling.isHit)
			{
				isLampTake = true;
				Destroy(lampRb);
			}
		}

		if (isLampTake)
		{
			// �e�q�t��
			lampObj.transform.SetParent(this.transform);
			// �v���C���[�̏�Ɉړ�
			lampObj.transform.position = new Vector2(this.transform.position.x, this.transform.position.y + 1);
		}
		else
		{
			// �e�q�t������
			lampObj.transform.SetParent(null);
		}
	}
}
