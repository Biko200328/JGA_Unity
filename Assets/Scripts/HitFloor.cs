using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFloor : MonoBehaviour
{
	// �ڒn����
	// PlayerMove�ɓn���̂�public�錾
	public bool isHit;

	// Start is called before the first frame update
	void Start()
	{

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
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// ���ꂽ������false�ɂ��Ă�����΋󒆂ɂ���Ƃ���false�ɂȂ�
		if (collision.gameObject.tag == "Floor") isHit = false;
	}
}
