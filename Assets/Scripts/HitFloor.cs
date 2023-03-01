using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFloor : MonoBehaviour
{
	// �ڒn����
	// PlayerMove�ɓn���̂�public�錾
	public bool isHit;

	Rigidbody2D playerRb;

	// Start is called before the first frame update
	void Start()
	{
		GameObject player = GameObject.Find("Player");
		playerRb = player.gameObject.GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	// OnTriggerEnter     ��������(�G�ꂽ)�u��
	// OnTriggerStay      �������Ă���(�G��Ă���)��
	// OnTriggerExit      ���ꂽ�u��
	// Trigger�ȊO�ɂ�Oncollision~~������
	// �ǂݍ���ł���collider��Trigger(���蔲����)���ǂ����̈Ⴂ
	// Trigger�ɂ��邩�ǂ�����collider��Inspector�r���[�Ƀ`�F�b�N�{�b�N�X�������

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// ��������collision��tag��Floor�Ȃ�ڒn�Ƃ���
		// �������Ȃ��Ƃǂ�Obj�ɓ����������ł�Jump�ł��Ă��܂�
		if (collision.gameObject.tag == "Floor") isHit = true;

		// tag��Inspector�r���[��Obj�̖��O�̉��ŕς���Ƃ��낪����
		// tag�̐V�K�ǉ��������łł��邩��Floor�������tilemap�ɐݒ肷��
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor") isHit = true;

		if(collision.gameObject.tag != "Floor")
		{
			Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
			if(rb.velocity.x != 0)
			{
				playerRb.velocity = new Vector2(rb.velocity.x, playerRb.velocity.y);
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// ���ꂽ������false�ɂ��Ă�����΋󒆂ɂ���Ƃ���false�ɂȂ�
		if (collision.gameObject.tag == "Floor") isHit = false;
	}
}
