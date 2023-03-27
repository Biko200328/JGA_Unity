using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
	[Header("�~�̍ő咼�a")]
	public float inputCircleSize = 9;
	float maxCircleSize = 0;

    [Header("�~�̍ŏ����a")]
	public float minCircleSize = 0;

	[Header("�T�C�Y�������l")]
	[SerializeField] float changeSize;

	// ���݂̉~�͈̔�
	public float circleSize;

	PlayerMove playerMove;
	Lamp lamp;

	JumpHitLeft jumpHitLeft;
	JumpHitLeft jumpHitLeft2;
	JumpHitRight jumpHitRight;
	JumpHitRight jumpHitRight2;

	// Start is called before the first frame update
	void Start()
	{
		GameObject player = GameObject.Find("Player");
		playerMove = player.gameObject.GetComponent<PlayerMove>();

		GameObject lampObj = GameObject.Find("Lamp");
		lamp = lampObj.GetComponent<Lamp>();

		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childJumpR = player.transform.Find("JumpHitRight").gameObject;
		// �R���|�[�l���g�ǂݍ���
		jumpHitRight = childJumpR.GetComponent<JumpHitRight>();
		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childJumpR2 = player.transform.Find("JumpHitRight2").gameObject;
		// �R���|�[�l���g�ǂݍ���
		jumpHitRight2 = childJumpR2.GetComponent<JumpHitRight>();

		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childJumpL = player.transform.Find("JumpHitLeft").gameObject;
		// �R���|�[�l���g�ǂݍ���
		jumpHitLeft = childJumpL.GetComponent<JumpHitLeft>();
		// �q�I�u�W�F�N�g�ǂݍ���
		GameObject childJumpL2 = player.transform.Find("JumpHitLeft2").gameObject;
		// �R���|�[�l���g�ǂݍ���
		jumpHitLeft2 = childJumpL2.GetComponent<JumpHitLeft>();
	}

	// Update is called once per frame
	void Update()
	{
		maxCircleSize = inputCircleSize;

		//���͈̔͂��ς�鏈��
        if (playerMove.lightSizeChange)
		{
			if (playerMove.isLampTake)
			{
				maxCircleSize = inputCircleSize;
			}
			else
			{
				//�u���Ă���Ƃ��͌��͈̔͂�+3����
				maxCircleSize = inputCircleSize + 3;
			}
        }

		// �u���Ă鎞�͉~���Ȃ���
		if(playerMove.isPlace)
		{
			circleSize = 0;
		}

		if(playerMove.isLampCollect)
		{
			circleSize = 0;
		}
		else
		{
			if (playerMove.isLightOn)
			{
				//�ő�l���Ⴉ�����瑫��
				if (circleSize < maxCircleSize)
				{
					circleSize += changeSize;
				}

				//�ő�l���傫��������ő�l�ɍ��킹��
				if (circleSize > maxCircleSize)
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
		}
		
		var circleVec = new Vector3(circleSize, circleSize, circleSize);

		transform.localScale = circleVec;
	}
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//�M�~�b�N�̃t���O�Ǘ�
		GimmicksOn(collision);

		// �v���C���[
		if (collision.gameObject.tag == "Player")
		{
			playerMove.isLightIn = true;
		}

		// �փu���b�N
		if (collision.gameObject.tag == "growOriginal")
		{
			Growth growth = collision.GetComponent<Growth>();
			growth.isLightIn = true;
		}

		// �~�̒����Əo��u���b�N
		if(collision.gameObject.tag == "LightBlock")
		{
			LightBlock lightBlock = collision.GetComponent<LightBlock>();
			lightBlock.isLightHit = true;
			lightBlock.time = 0;
			lightBlock.isAlphaZero = false;
			SpriteRenderer render = collision.GetComponent<SpriteRenderer>();
			Color color = render.color;
			color.a = 100;
			render.color = color;
			// �����蔻������Ȃ���
			BoxCollider2D boxCollider2D = collision.GetComponent<BoxCollider2D>();
			boxCollider2D.isTrigger = false;
		}

	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		//�M�~�b�N�̃t���O�Ǘ�
		GimmicksOn(collision);

		// �v���C���[
		if (collision.gameObject.tag == "Player")
		{
			playerMove.isLightIn = true;
		}

		// �փu���b�N
		if (collision.gameObject.tag == "growOriginal")
		{
			Growth growth = collision.GetComponent<Growth>();
			growth.isLightIn = true;
		}

		// �~�̒����Əo��u���b�N
		if (collision.gameObject.tag == "LightBlock")
		{
			LightBlock lightBlock = collision.GetComponent<LightBlock>();
			lightBlock.isLightHit = true;
			lightBlock.time = 0;
			lightBlock.isAlphaZero = false;
			SpriteRenderer render = collision.GetComponent<SpriteRenderer>();
			Color color = render.color;
			color.a = 100;
			render.color = color;
			// �����蔻������Ȃ���
			CompositeCollider2D boxCollider2D = collision.GetComponent<CompositeCollider2D>();
			boxCollider2D.isTrigger = false;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//�M�~�b�N�̃t���O�Ǘ�
		GimmicksOff(collision);

		// �v���C���[
		if (collision.gameObject.tag == "Player")
		{
			playerMove.isLightIn = false;
		}

		// �փu���b�N
		if (collision.gameObject.tag == "growOriginal")
		{
			Growth growth = collision.GetComponent<Growth>();
			growth.isLightIn = false;
			growth.isFirst = false;
			growth.isEnd = false;
			lamp.isHitGrowBox = false;
			jumpHitLeft.isHit = false;
			jumpHitLeft2.isHit = false;
			jumpHitRight.isHit = false;
			jumpHitRight2.isHit = false;
		}

		// �~�̒����Əo��u���b�N
		if (collision.gameObject.tag == "LightBlock")
		{
			LightBlock lightBlock = collision.GetComponent<LightBlock>();
			lightBlock.isLightHit = false;
		}
	}

	private void GimmicksOn(Collider2D collision)
	{
		// ���ʂ̃u���b�N
		if (collision.gameObject.tag == "block")
		{
			Rigidbody2D blockRb = collision.gameObject.GetComponent<Rigidbody2D>();
			blockRb.isKinematic = false;
			Block block = collision.gameObject.GetComponent<Block>();
			block.isLightIn = true;
		}

		// ���Ɉړ�����u���b�N
		if (collision.gameObject.tag == "leftMoveBlock")
		{
			LeftMoveBlock leftMoveBlock = collision.gameObject.GetComponent<LeftMoveBlock>();
			leftMoveBlock.isMove = true;
		}

		// �E�Ɉړ�����u���b�N
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			RightMoveBlock rightMoveBlock = collision.gameObject.GetComponent<RightMoveBlock>();
			rightMoveBlock.isMove = true;
		}

		// ��Ɉړ�����u���b�N
		if (collision.gameObject.tag == "upMoveBlock")
		{
			UpMoveBlock upMoveBlock = collision.gameObject.GetComponent<UpMoveBlock>();
			upMoveBlock.isMove = true;
		}

		// ���Ɉړ�����u���b�N
		if (collision.gameObject.tag == "downMoveBlock")
		{
			DownMoveBlock downMoveBlock = collision.gameObject.GetComponent<DownMoveBlock>();
			downMoveBlock.isMove = true;
		}
	}

	private void GimmicksOff(Collider2D collision)
	{
		// ���ʂ̃u���b�N
		if (collision.gameObject.tag == "block")
		{
			Rigidbody2D blockRb = collision.gameObject.GetComponent<Rigidbody2D>();
			blockRb.isKinematic = true;
			blockRb.velocity = Vector3.zero;
			Block block = collision.gameObject.GetComponent<Block>();
			block.isLightIn = false;
		}

		// ���Ɉړ�����u���b�N
		if (collision.gameObject.tag == "leftMoveBlock")
		{
			LeftMoveBlock leftMoveBlock = collision.gameObject.GetComponent<LeftMoveBlock>();
			leftMoveBlock.isMove = false;
		}

		// �E�Ɉړ�����u���b�N
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			RightMoveBlock rightMoveBlock = collision.gameObject.GetComponent<RightMoveBlock>();
			rightMoveBlock.isMove = false;
		}

		// ��Ɉړ�����u���b�N
		if (collision.gameObject.tag == "upMoveBlock")
		{
			UpMoveBlock upMoveBlock = collision.gameObject.GetComponent<UpMoveBlock>();
			upMoveBlock.isMove = false;
		}

		// ���Ɉړ�����u���b�N
		if (collision.gameObject.tag == "downMoveBlock")
		{
			DownMoveBlock downMoveBlock = collision.gameObject.GetComponent<DownMoveBlock>();
			downMoveBlock.isMove = false;
		}
	}
}
