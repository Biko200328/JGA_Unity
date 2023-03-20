using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
	[Header("�~�̍ő咼�a")]
	public float inputCircleSize = 1;
	float maxCircleSize = 0;

    [Header("�~�̍ŏ����a")]
	public float minCircleSize = 0;

	[Header("�T�C�Y�������l")]
	[SerializeField] float changeSize;

	// ���݂̉~�͈̔�
	public float circleSize;

	PlayerMove playerMove;

	// Start is called before the first frame update
	void Start()
	{
		GameObject player = GameObject.Find("Player");
		playerMove = player.gameObject.GetComponent<PlayerMove>();
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
