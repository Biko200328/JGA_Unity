using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFloor : MonoBehaviour
{
	// �ڒn����
	public bool isHit;

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
		// ��������collision��tag��Floor�Ȃ�ڒn�Ƃ���
		if (collision.gameObject.tag == "Floor") isHit = true;
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
