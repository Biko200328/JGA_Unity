using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorCheckLeft : MonoBehaviour
{
	// ���ɓ������Ă��邩�ǂ���
	public bool isFloor;

	public LeftMoveBlock leftMoveBlock;
	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	private void OnTriggerEnter2D(Collider2D collision)
	{
		//�����Ă��Ԃ����������I�u�W�F�N�g�����̎���
		if (collision.gameObject.tag == "Floor")
		{
			// �Փ˔����True��
			if (leftMoveBlock.isMove) isFloor = true;
		}
	}
}
