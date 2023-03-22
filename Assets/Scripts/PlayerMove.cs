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

	public Vector3 respawnPos;

	// �X�s�[�h�Ɠ����悤�ɃW�����v�͂̕ϐ������
	[Header("�W�����v��")]
	[SerializeField] private float jumpPower;

	[Header("�����v�̉������")]
	[SerializeField] private float lampCollectTime;
	private float collectNowTime;

	[Header("��ɓ�����̂�")]
	public bool throwMode;

	[Header("�����v���͈͊O�ł�����ł��邩")]
	public bool collectNoLimit = false;

	[Header("�W�����v���ł��邩")]
	public bool jumpNoLimit = false;

	[Header("���͈̔͂��u�����Ƃ��ɕς�邩")]
	public bool lightSizeChange = false;

	[Header("�����v���v���C���[��X���̓�����^�����邩")]
	public bool lightSynchro = false;

	// ���􂵂Ă��邩�ǂ���
	[System.NonSerialized] public bool isLampTake;
	// ����̒��ɂ��邩�ǂ���
	[System.NonSerialized] public bool isLightIn;
	// �΂����Ă邩�ǂ����t���O
	[System.NonSerialized] public bool isLightOn;
	// �����v���������
	 public bool isLampCollect;
	// �u���Ƃ��̃t���O
	/*[System.NonSerialized] */
	public bool isPlace;
	[SerializeField] PlayerCircle playerCircle;

	public Rigidbody2D rb;
	HitFloor hitFloor;
	HitCeiling hitCeiling;
	GameObject lampObj;
	Lamp lampSqr;

	JumpHitLeft jumpHitLeft;
	JumpHitLeft jumpHitLeft2;

	JumpHitRight jumpHitRight;
	JumpHitRight jumpHitRight2;

	GameObject colliderObj;
	GameObject colliderObj2;

	RespawnManager respawnManager;
	GameObject particle;

	public bool isJump;

	[Header("2�}�X�ɂ��邩�ǂ���")]
	public bool isPlaceMode;

	[Header("1�}�X�̎��ɂǂꂭ�炢�����邩")]
	public float lightOffTime;
	[SerializeField] float lightOffNowTime;

	SpriteRenderer spriteRendererer;
	[SerializeField] Sprite lightTex;
	[SerializeField] Sprite normal;
	// Start is called before the first frame update
	void Start()
	{
		// ���X�|�[���}�l�[�W���[
		GameObject respawnManagerObj = GameObject.Find("RespawnManager");
		respawnManager = respawnManagerObj.GetComponent<RespawnManager>();
		transform.position = respawnManager.GetRespawnPos();

		// Rigidbody���擾
		rb = gameObject.GetComponent<Rigidbody2D>();

		//colliderObj�ǂݍ���
		colliderObj = transform.Find("SetCollider").gameObject;
		colliderObj.SetActive(false);

		colliderObj2 = transform.Find("SetCollider2").gameObject;
		colliderObj2.SetActive(false);

		// �p�[�e�B�N���ǂݍ���
		particle = transform.Find("Particle").gameObject;

		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childFloor = transform.Find("HitFloor").gameObject;
		// �R���|�[�l���g�ǂݍ���
		hitFloor = childFloor.GetComponent<HitFloor>();

		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childCeiling = transform.Find("HitCeiling").gameObject;
		// �R���|�[�l���g�ǂݍ���
		hitCeiling = childCeiling.GetComponent<HitCeiling>();

		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childJumpR = transform.Find("JumpHitRight").gameObject;
		// �R���|�[�l���g�ǂݍ���
		jumpHitRight = childJumpR.GetComponent<JumpHitRight>();
		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childJumpR2 = transform.Find("JumpHitRight2").gameObject;
		// �R���|�[�l���g�ǂݍ���
		jumpHitRight2 = childJumpR2.GetComponent<JumpHitRight>();

		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childJumpL = transform.Find("JumpHitLeft").gameObject;
		// �R���|�[�l���g�ǂݍ���
		jumpHitLeft = childJumpL.GetComponent<JumpHitLeft>();
		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childJumpL2 = transform.Find("JumpHitLeft2").gameObject;
		// �R���|�[�l���g�ǂݍ���
		jumpHitLeft2 = childJumpL2.GetComponent<JumpHitLeft>();

		spriteRendererer = gameObject.GetComponent<SpriteRenderer>();

		// lamp�ǂݍ���
		lampObj = GameObject.Find("Lamp");
		// �����v�̃X�N���v�g���擾
		lampSqr = lampObj.GetComponent<Lamp>();
		//�����v������
		isLightOn = true;

		gameObject.layer = 9;
	}

	// Update is called once per frame
	void Update()
	{
		Move();

		//TakeLight();

		AutoJump();

		TakeLamp();

		LampCollect();

		if (jumpNoLimit)
		{
			if (Input.GetKeyDown(KeyCode.C) && isJump == false)
			{
				rb.velocity = new Vector2(0, 10);
				isJump = true;
			}
		}

		if (isPlaceMode)
		{
			if (isLightIn && !hitCeiling.isHit)
			{
				if (!isPlace) particle.SetActive(true);
			}
			else
			{
				if (!isPlace) particle.SetActive(false);
			}
		}
		else
		{
			if (isLightIn)
			{
				if (!isPlace) particle.SetActive(true);
			}
			else
			{
				if (!isPlace) particle.SetActive(false);
			}
		}

		PlaceMode();
	}

	//�ړ�
	private void Move()
	{
		// �R���g���[���[�̍��E���͐��l���󂯎��
		inputHorizontal = Input.GetAxis("cHorizontalL");

		//�C�h�E
		if (isJump)
		{
			if (Input.GetKey(KeyCode.A))
			{
				rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
				transform.eulerAngles = new Vector3(0, 0, 0);
			}
			else if (Input.GetKey(KeyCode.D))
			{
				rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
				transform.eulerAngles = new Vector3(0, 180, 0);
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
		else
		{
			if (hitFloor.isHit)
			{
				if (Input.GetKey(KeyCode.A))
				{
					rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
					transform.eulerAngles = new Vector3(0, 0, 0);
				}
				else if (Input.GetKey(KeyCode.D))
				{
					rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
					transform.eulerAngles = new Vector3(0, 180, 0);
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
			else
			{
				if (Input.GetKey(KeyCode.A))
				{
					rb.velocity = new Vector2(-moveSpeed * 0.05f, rb.velocity.y);
					transform.eulerAngles = new Vector3(0, 0, 0);
				}
				else if (Input.GetKey(KeyCode.D))
				{
					rb.velocity = new Vector2(moveSpeed * 0.05f, rb.velocity.y);
					transform.eulerAngles = new Vector3(0, 180, 0);
				}
				else if (inputHorizontal != 0)
				{
					rb.velocity = new Vector2(inputHorizontal * moveSpeed * 0.05f, rb.velocity.y);
				}
				else
				{
					rb.velocity = new Vector2(0, rb.velocity.y);
				}
			}
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
		if (jumpHitLeft.isHit && !jumpHitLeft2.isHit/* && hitFloor.isHit*/)
		{
			if (Input.GetKey(KeyCode.A))
			{
				transform.SetParent(null);
				rb.velocity = new Vector2(0, jumpPower);
				isJump = true;
				//jumpHitLeft.isHit = false;
				//gameObject.layer = 9;
			}
		}

		if (jumpHitRight.isHit && !jumpHitRight2.isHit/* && hitFloor.isHit*/)
		{
			if (Input.GetKey(KeyCode.D))
			{
				transform.SetParent(null);
				rb.velocity = new Vector2(0, jumpPower);
				isJump = true;
				//jumpHitRight.isHit = false;
				//gameObject.layer = 9;
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
		//���̒��ɂ��Ȃ��Ă�����ł���悤��
		if (collectNoLimit)
		{
			isLightIn = true;
		}

		// ���C�g�̒��ɂ��Ă��v���C���[�̏�Ƀu���b�N���Ȃ��Ƃ��Ƀ����v���Ă�
		if (Input.GetKeyDown(KeyCode.Space))
		{
			// �����v�������Ă���Ƃ�
			if (isLampTake)
			{
				// �n�ʂɂ��Ă���Ƃ�
				if (hitFloor.isHit && !isLampCollect)
				{
					isLampTake = false;
					lampSqr.GetLampRb();
					if (throwMode)
					{
						lampSqr.LampThrow(transform.position);
						isLightOn = false;
					}
					else
					{
						isPlace = true;
					}
					// �e�q�t������
					lampObj.transform.SetParent(null);
					//�j�}�X���[�h�̎�
					if (isPlaceMode)
					{
						// ���������R���C�_�[���̂Ă�
						colliderObj.SetActive(false);
					}
					// ��}�X���[�h�̎�
					else
					{
						// ���������R���C�_�[���̂Ă�
						colliderObj2.SetActive(false);
						// �����v�̃X�v���C�g������
						lampSqr.spriteRenderer.sprite = lampSqr.sprite;
						// �v���C���[�̃X�v���C�g�����ɖ߂�
						spriteRendererer.sprite = normal;
						// ���Ԃ����Z�b�g
						lightOffNowTime = 0;
					}
					// �X�̃R���C�_�[�����Ȃ���
					gameObject.AddComponent<BoxCollider2D>();
					lampObj.AddComponent<BoxCollider2D>();
					// ���C���[���v���C���[�ɕύX
					gameObject.layer = 9;
				}
			}
			// �����v�������Ă��Ȃ��Ƃ�
			// ���C�g�̒��ɂ���
			else if (!isLampTake && isLightIn && !isPlace)
			{
				// 2�}�X�̎��͏�ɕ����Ȃ��Ƃ�
				if (isPlaceMode && !hitCeiling.isHit)
				{
					isLampTake = true;
					lampSqr.RbLost();
					//// �������񃉃��v�̐e�q�֌W�𖳂���
					// lampObj.transform.SetParent(null);
					// lamp���I�t��
					isLightOn = false;
					//������̃t���O���I��
					isLampCollect = true;
					collectNowTime = 0;
					// �e�q�t��
					lampObj.transform.SetParent(this.transform);
					// �����̃R���C�_�[���Ȃ���
					Destroy(gameObject.GetComponent<BoxCollider2D>());
					Destroy(lampObj.GetComponent<BoxCollider2D>());
					// ��p�̃R���C�_�[�𐶐�
					colliderObj.SetActive(true);
					// ���C���[�������v�ɕύX
					gameObject.layer = 10;
				}
				else if (!isPlaceMode)
				{
					isLampTake = true;
					lampSqr.RbLost();
					//// �������񃉃��v�̐e�q�֌W�𖳂���
					// lampObj.transform.SetParent(null);
					// lamp���I�t��
					isLightOn = false;
					//������̃t���O���I��
					isLampCollect = true;
					collectNowTime = 0;
					// �e�q�t��
					lampObj.transform.SetParent(this.transform);
					// �����̃R���C�_�[���Ȃ���
					Destroy(gameObject.GetComponent<BoxCollider2D>());
					Destroy(lampObj.GetComponent<BoxCollider2D>());
					// �����v�̃X�v���C�g������
					lampSqr.spriteRenderer.sprite = lampSqr.sprite;
					// �v���C���[�̃X�v���C�g��ύX
					spriteRendererer.sprite = lightTex;
					// ��p�̃R���C�_�[�𐶐�
					colliderObj2.SetActive(true);
					// ���C���[�������v�ɕύX
					gameObject.layer = 10;
				}

			}
		}
	}

	private void LampCollect()
	{
		if (isLampCollect)
		{
			collectNowTime += Time.deltaTime;
			if (collectNowTime >= lampCollectTime)
			{
				if (isPlaceMode)
				{
					//��ɒu��
					lampObj.transform.position = new Vector3(transform.position.x, transform.position.y + 1.0f);
				}
				else
				{
					// ���̏�
					lampObj.transform.position = new Vector3(transform.position.x, transform.position.y);
				}

				isLampCollect = false;
				// lamp���I����
				isLightOn = true;
			}
		}
	}

	public void PlaceMode()
	{
		if (!isPlaceMode)
		{
			if (isPlace)
			{
				lightOffNowTime += Time.deltaTime;
				if (lightOffNowTime >= lightOffTime)
				{
					isPlace = false;
					lightOffNowTime = 0;
				}
			}
		}
	}
}
