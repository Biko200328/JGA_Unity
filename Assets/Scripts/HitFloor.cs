using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFloor : MonoBehaviour
{
	//�e�I�u�W�F�N�g
	GameObject player;
	GameObject Lamp;

	PlayerMove playerMove;

	// �ڒn����
	public bool isHit;

	// ������
	public bool isGoast;
	// �������̎���
	public float goastTime;
	private float time;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.Find("Player");
		playerMove = player.GetComponent<PlayerMove>();
		Lamp = GameObject.Find("Lamp");
	}

	// Update is called once per frame
	void Update()
	{
		if(isGoast)
		{
			time += Time.deltaTime;
			if(time >= goastTime)
			{
				isGoast = false;
			}

			player.layer = 9;
			if (playerMove.isLampTake) Lamp.layer = 10;
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ��������collision��tag��Floor�Ȃ�ڒn�Ƃ���
		if (collision.gameObject.tag == "Floor")
		{
			isHit = true;
			playerMove.isJump = false;
		}

		GimmickRide(collision);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor")
		{
			isHit = true;
			playerMove.isJump = false;
		}

		GimmickRide(collision);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// ���ꂽ������false�ɂ��Ă�����΋󒆂ɂ���Ƃ���false�ɂȂ�
		if (collision.gameObject.tag == "Floor") isHit = false;

		GimmickRideOff(collision);
	}

	private void GimmickRide(Collider2D collision)
	{
		// �e�ړ��u���b�N
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			isHit = true;
			playerMove.isJump = false;
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "leftMoveBlock")
		{
			isHit = true;
			playerMove.isJump = false;
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "downMoveBlock")
		{
			isHit = true;
			playerMove.isJump = false;
			player.transform.SetParent(collision.transform);
		}

		if (collision.gameObject.tag == "upMoveBlock")
		{
			isHit = true;
			playerMove.isJump = false;
			player.transform.SetParent(collision.transform);
		}

		// �ʂ蔲���鑫��
		if(collision.gameObject.tag == "platform")
		{
			isHit = true;
			playerMove.isJump = false;
			player.layer = 3;
			if (Input.GetKeyDown(KeyCode.S))
			{
				isGoast = true;
				time = 0;
			}
		}

		// �փu���b�N
		if (collision.gameObject.tag == "growOriginal")
		{
			isHit = true;
			playerMove.isJump = false;
		}

		if (collision.gameObject.tag == "growBox")
		{
			isHit = true;
			playerMove.isJump = false;
		}
	}

	private void GimmickRideOff(Collider2D collision)
	{
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			player.transform.SetParent(null);
			isHit = false;
		}

		if (collision.gameObject.tag == "leftMoveBlock")
		{
			player.transform.SetParent(null);
			isHit = false;
		}

		if (collision.gameObject.tag == "downMoveBlock")
		{
			player.transform.SetParent(null);
			isHit = false;
		}

		if (collision.gameObject.tag == "upMoveBlock")
		{
			player.transform.SetParent(null);
			isHit = false;
		}

		// �ʂ蔲���鑫��
		if (collision.gameObject.tag == "platform")
		{
			isHit = false;
			player.layer = 9;
			if(playerMove.isLampTake)Lamp.layer = 10;
		}

		// �փu���b�N
		if (collision.gameObject.tag == "growOriginal")
		{
			isHit = false;
		}

		if (collision.gameObject.tag == "growBox")
		{
			isHit = false;
		}
	}
}
