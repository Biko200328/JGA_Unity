using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheckDown : MonoBehaviour
{
	// ���ɓ������Ă��邩�ǂ���
	public bool isFloor;

	public DownMoveBlock downMoveBlock;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//�����Ă��Ԃ����������I�u�W�F�N�g�����̎���
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "rightMoveBlock" || collision.gameObject.tag == "leftMoveBlock" ||
			collision.gameObject.tag == "block" || collision.gameObject.tag == "upMoveBlock" || collision.gameObject.tag == "downMoveBlock" ||
			collision.gameObject.tag == "growOriginal" || collision.gameObject.tag == "growBox")
		{
			// �Փ˔����True��
			if (downMoveBlock.isMove) isFloor = true;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		//�����Ă��Ԃ����������I�u�W�F�N�g�����̎���
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "rightMoveBlock" || collision.gameObject.tag == "leftMoveBlock" ||
			collision.gameObject.tag == "block" || collision.gameObject.tag == "upMoveBlock" || collision.gameObject.tag == "downMoveBlock" ||
			collision.gameObject.tag == "growOriginal" || collision.gameObject.tag == "growBox")
		{
			// �Փ˔����True��
			if (downMoveBlock.isMove) isFloor = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		//�����Ă��Ԃ����������I�u�W�F�N�g�����̎���
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "rightMoveBlock" || collision.gameObject.tag == "leftMoveBlock" ||
			collision.gameObject.tag == "block" || collision.gameObject.tag == "upMoveBlock" || collision.gameObject.tag == "downMoveBlock" ||
			collision.gameObject.tag == "growOriginal" || collision.gameObject.tag == "growBox")
		{
			// �Փ˔����True��
			if (downMoveBlock.isMove) isFloor = false;
		}
	}
}
