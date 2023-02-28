using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
	// [[Header("hoge")]] �� Script��Inspector�r���[�ɕ������\���ł���
	[Header("�ړ��X�s�[�h")]
	// [SerializeField] �� private�ł�Inspector�r���[�Ő��l���ύX�ł���
	[SerializeField] private float moveSpeed;

	// �����Ă邩�ǂ���
	bool isMove;

	// �X�s�[�h�Ɠ����悤�ɃW�����v�͂̕ϐ������
	[Header("�W�����v��")]
	[SerializeField] private float jumpPower;

	float inputHorizontal;
	float inputVertical;

	public bool isLightOn;

	// private�Ő錾����Start�Ŏ擾����
	// public RigidBody2D rb �ɂ���Inspector�r���[�Œ��ړ���Ă�����
	Rigidbody2D rb;

	// ��ɓ���
	HitFloor hitFloor;

	// Start is called before the first frame update
	void Start()
	{
		// Rigidbody���擾
		rb = gameObject.GetComponent<Rigidbody2D>();

		// �q�I�u�W�F�N�g�ǂݍ���
		// Find("hoge")�� hoge �̕��������S��v�����Ȃ��Ɠǂݎ��Ȃ��̂Œ���
		GameObject child = transform.Find("HitFloor").gameObject;
		// class�ǂݍ���
		hitFloor = child.GetComponent<HitFloor>();
	}

	// Update is called once per frame
	void Update()
	{
		//�R���g���[���[
		inputHorizontal = Input.GetAxis("cHorizontalL");

		//�����Ă邩�ǂ������f
		if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || inputHorizontal != 0)
		{
			isMove = true;
		}
		else
		{
			isMove = false;
		}


		TakeLight();
	}

	// ���Ԋu�ŌĂ�update
	private void FixedUpdate()
	{
		// �����Ă��邩�̃t���O���I���Ȃ�
		if(isMove)Move();
	}

	private void Move()
	{
		// var�Ƃ�
		// �����l�̓��e����ϐ��̌^��C#�R���p�C���[���������Ď����I�ɐݒ肵�Ă���܂�(google�搶���)
		var pos = rb.position;

		// �����ł�rb(RigidBody)��position��pos�Ƃ��Ĉꎞ�ۊǂ��܂�
		// ���R�Ƃ��Ă�position��x,y,z �X�𒼐ڂ�����Ȃ��̂�position�Ƃ��đS���󂯎��A���̂܂ܑS���Ԃ��܂�
		// ���{����


		// GetKey             �����Ă����
		// GetKeyDown         �������u��
		// GetKeyUp           �������u��
		// �L�[�{�[�h
		if (Input.GetKey(KeyCode.A))
		{
			pos.x -= moveSpeed;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			pos.x += moveSpeed;
		}

		pos.x += inputHorizontal * moveSpeed;

		//�󂯎���Đ��l�ύX����pos��rb�ɕԂ��܂�
		rb.position = pos;
	}

	//private void Jump()
	//{
	//	//�ڒn���Ă���Ƃ���Space(Jump�{�^��)����������
	//	if (hitFloor.isHit == true)
	//	{
	//		if (Input.GetKeyDown(KeyCode.Space) || Input.GetButtonDown("buttonA"))
	//		{
	//			rb.velocity += new Vector2(0, jumpPower);
	//		}
	//	}
	//}

	private void TakeLight()
	{
		// �I���I�t�؂�ւ�
		if(Input.GetButtonDown("buttonRB"))
		{
			isLightOn = !isLightOn;
		}
	}
}
