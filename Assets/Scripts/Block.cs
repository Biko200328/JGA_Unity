using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Block : MonoBehaviour
{
	// Rigidbody�錾
	Rigidbody2D rb;
	// rb��L�����ł��Ă邩����t���O
	public bool isLightIn;

	Lamp lamp;

	// Start is called before the first frame update
	void Start()
	{
		// �R���|�[�l���g�󂯎��
		rb = this.gameObject.GetComponent<Rigidbody2D>();
		// �������Z���s��Ȃ��悤�ɐݒ�
		rb.isKinematic = true;

		GameObject lampObj = GameObject.Find("Lamp");
		lamp = lampObj.GetComponent<Lamp>();
	}

	// Update is called once per frame
	void Update()
	{
		// velocity�����On��
		rb.velocity = new Vector2(0,rb.velocity.y);

		if(!isLightIn)
		{
			transform.SetParent(null);
		}

		if (!lamp.isLampOn)
		{
			isLightIn = false;
		}
	}

	//�~�̒��ɂ��Ȃ��Ă������������Ƃ��̓R�����g�A�E�g
	//private void OnCollisionEnter(Collision collision)
	//{
	//	//�������Ă��镨��Rigidbody���󂯎��
	//	Rigidbody2D collisionRb = collision.gameObject.GetComponent<Rigidbody2D>();
	//	//���ꂪx���ړ����Ă�����
	//	if(collisionRb.velocity.x != 0)
	//	{
	//		//���̕���velocity�����̃u���b�N�ɔ��f������
	//		rb.velocity = new Vector2(collisionRb.velocity.x, rb.velocity.y);
	//	}
	//}
}
