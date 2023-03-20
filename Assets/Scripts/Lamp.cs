using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
	[SerializeField] Rigidbody2D rb;

	// ������t���O
	private bool isThrow;
	// ���݂̎���
	private float throwNowTime;
	[Header("�����鎞��")]
	[SerializeField] private float throwTime;

	[Header("�����v��������")]
	[SerializeField] private float lampTime;

	// �n�_
	private Vector2 startPos;
	[Header("�ǂ��܂Ŕ�Ԃ�")]
	[SerializeField] private float maxY;

	// ������t���O
	private bool isFall;
	// ���݂̎���
	private float fallNowTime;
	// �����I�������Ɏn�_�ɂ���悤��vec2
	private Vector2 fallStartPos;

	[Header("�p���[�ŏd�͂�����")]
	[SerializeField] private float fallSpeed;

	public bool isLampOn;

	PlayerMove playerMove;
	LampHitFloor lampHitFloor;

	public SpriteRenderer spriteRenderer;
	public Sprite sprite;

	RespawnManager respawnManager;
	PlayerCircle playerCircle;

	

	// Start is called before the first frame update
	void Start()
	{
		// ���X�|�[���}�l�[�W���[
		GameObject respawnManagerObj = GameObject.Find("RespawnManager");
		respawnManager = respawnManagerObj.GetComponent<RespawnManager>();
		transform.position = respawnManager.GetRespawnPos();

		// Rigidbody���擾
		rb = GetComponent<Rigidbody2D>();

		// PlayerMove���擾
		GameObject playerObj = GameObject.Find("Player");
		playerMove = playerObj.GetComponent<PlayerMove>();

		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childObj = transform.Find("HitFloor").gameObject;
		// �R���|�[�l���g�ǂݍ���
		lampHitFloor = childObj.GetComponent<LampHitFloor>();

		//�~
		GameObject circleObj = transform.Find("CircleObj").gameObject;
		playerCircle = circleObj.GetComponent<PlayerCircle>();

		// ���C���[��ύX
		gameObject.layer = 10;

		isLampOn = true;
	}

	// Update is called once per frame
	void Update()
	{
		if (playerMove.isLampCollect)
		{
			spriteRenderer.sprite = null;
		}
		else
		{
			if (playerMove.isPlaceMode) spriteRenderer.sprite = sprite;
		}

		// �����v�������Ă��Ȃ��Ƃ������C�[�W���O�͊|���Ȃ�
		// �����v�����ɂ��Ă��Ȃ��Ƃ����ǉ�
		if (!playerMove.isLampTake)
		{
			// �㏸���̎�
			if (isThrow)
			{
				// ���Ԃ�i�܂���
				throwNowTime += Time.deltaTime;
				// �d�͂��󂯂Ȃ��悤��
				if (rb != null) rb.velocity = new Vector2(rb.velocity.x, 0);

				// �����蔻�背�C���[��ύX
				if (throwNowTime <= lampTime)
				{
					gameObject.layer = 10;
				}

				// �����v������
				if (throwNowTime >= lampTime)
				{
					// �����v������
					playerMove.isLightOn = true;
					isLampOn = true;
				}

				// �g�[�^���̎��Ԃ𒴂����ꍇ
				if (throwNowTime >= throwTime)
				{
					// �t���O���I�t��
					isThrow = false;
					// ���݂̎��Ԃ��g�[�^���̎��Ԃɐݒ�
					throwNowTime = throwTime;
					// ������C�[�W���O�J�n �ϐ�������
					isFall = true;
					fallNowTime = 0;
					fallStartPos = transform.position;
				}

				// position��ύX
				rb.MovePosition(ExpOut(throwNowTime, throwTime, startPos, startPos + new Vector2(0, maxY)));
			}

			// ������C�[�W���O
			if (isFall)
			{
				fallNowTime += Time.deltaTime;
				//�d�͂��󂯂Ȃ��悤��
				if (rb != null) rb.velocity = new Vector2(rb.velocity.x, 0);
				if (fallNowTime >= throwTime)
				{
					isFall = false;
					fallNowTime = throwTime;
					//�����ŗ������x�𒲐߂���I�I�I�I�I�I�I�I
					rb.velocity = new Vector2(rb.velocity.x, fallSpeed);
				}

				rb.MovePosition(ExpIn(fallNowTime, throwTime, fallStartPos, fallStartPos + new Vector2(0, -maxY - 1)));
			}
		}
	}

	private void FixedUpdate()
	{
		// null�`�F�b�N
		if (rb != null)
		{
			// ���Ɋ���Ȃ��悤��
			rb.velocity = new Vector2(0, rb.velocity.y);
		}

		//���C�g���V���N�����邩(���Ɋ���悤�ɂȂ��Ă��܂�����)
		if (!playerMove.isLampTake && playerMove.lightSynchro)
		{
			if (!isThrow && !isFall)
			{
				rb.velocity = new Vector2(playerMove.rb.velocity.x, rb.velocity.y);
			}
		}
	}

	// �C�[�Y�A�E�g
	public static Vector2 QuintOut(float t, float totaltime, Vector2 min, Vector2 max)
	{
		max -= min;
		t = t / totaltime - 1;
		return max * (t * t * t * t * t + 1) + min;
	}

	public static Vector2 ExpOut(float t, float totaltime, Vector2 min, Vector2 max)
	{
		max -= min;
		return t == totaltime ? max + min : max * (-Mathf.Pow(2, -10 * t / totaltime) + 1) + min;
	}

	// �C�[�Y�C��
	public static Vector2 QuintIn(float t, float totaltime, Vector2 min, Vector2 max)
	{
		max -= min;
		t /= totaltime;
		return max * t * t * t * t * t * t * t * t * t * t + min;
	}

	public static Vector2 ExpIn(float t, float totaltime, Vector2 min, Vector2 max)
	{
		max -= min;
		return t == 0.0 ? min : max * Mathf.Pow(2, 10 * (t / totaltime - 1)) + min;
	}

	public void GetLampRb()
	{
		//Rigidbody����
		rb = gameObject.AddComponent<Rigidbody2D>();
		//FreezeRotation���I���ɂ���
		rb.freezeRotation = true;
		// collisionDetection��ύX
		rb.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
		//// Interpolate��ύX
		//rb.interpolation = RigidbodyInterpolation2D.Interpolate;
	}

	// PlayerMove�ɓn�����߂̊֐�
	// �ϐ��̏�����
	public void LampThrow(Vector3 pos)
	{
		// �������t���O��true
		isThrow = true;
		// �^�C����0��
		throwNowTime = 0;
		// �X�^�[�g�|�W�V���������݂�pos�ɕύX
		startPos = new Vector2(pos.x, pos.y + 1);
		// ������t���O�I�t��
		isFall = false;
		// ������J�E���g��0��
		fallNowTime = 0;
	}

	// rb�������֐�
	// �������PlayerMove�ɓn���p
	public void RbLost()
	{
		Destroy(rb);
		// ���Ƃ̓����蔻����I�t��
		lampHitFloor.isHit = false;
		// ���背�C���[��ύX
		gameObject.layer = 10;
	}

	//private void OnCollisionEnter2D(Collision2D collision)
	//{
	//	if(lampHitFloor.isHit && playerMove.isPlace)
	//	{
	//		if(collision.gameObject.tag == "Floor")
	//		{
	//			playerMove.isPlace = false;
	//		}
	//	}
	//}
}
