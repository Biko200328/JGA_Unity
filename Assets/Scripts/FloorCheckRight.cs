using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class FloorCheckRight : MonoBehaviour
{
	// ���ɓ������Ă��邩�ǂ���
	public bool isFloor;

	public RightMoveBlock rightMoveBlock;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		//�����Ă��Ԃ����������I�u�W�F�N�g�����̎���
		if (collision.gameObject.tag == "Floor")
		{
			// �Փ˔����True��
			if (rightMoveBlock.isMove) isFloor = true;
		}
	}
}