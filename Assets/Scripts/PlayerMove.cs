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

	// �R���g���[���[�̓��͐��l
	float inputHorizontal;
	float inputVertical;

    // �ړ�����
    float limitX = 15.5f;
    float limitY = 8.0f;

    // �΂����Ă邩�ǂ����t���O
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
		// �R���g���[���[�̍��E���͐��l���󂯎��
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
			//���Ɉړ�
			pos.x -= moveSpeed;
		}
		else if (Input.GetKey(KeyCode.D))
		{
			//�E�Ɉړ�
			pos.x += moveSpeed;
		}

		// �R���g���[���[�̐��l�𑫂�
		pos.x += inputHorizontal * moveSpeed;

        // ���݂̃|�W�V������ێ�����
        Vector3 currentPos = pos;

        // �͈͂𒴂��Ă�����͈͓��̒l��������
        currentPos.x = Mathf.Clamp(currentPos.x, -limitX, limitX);
        currentPos.y = Mathf.Clamp(currentPos.y, -limitY, limitY);

        // �󂯎���Đ��l�ύX����pos��rb�ɕԂ��܂�
        rb.position = currentPos;
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
		if(Input.GetButtonDown("buttonRB") || Input.GetKeyDown(KeyCode.Space))
		{
			isLightOn = !isLightOn;
		}
	}
}
