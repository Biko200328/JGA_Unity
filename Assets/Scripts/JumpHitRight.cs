using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHitRight : MonoBehaviour
{
	public bool isHit;

	Vector3 def;

	PlayerMove playerMove;
	// Start is called before the first frame update
	void Start()
	{
		def = transform.localRotation.eulerAngles;

		GameObject playerObj = GameObject.Find("Player");
		playerMove = playerObj.GetComponent<PlayerMove>();
	}

	// Update is called once per frame
	void Update()
	{
		Vector3 _parent = transform.parent.transform.localRotation.eulerAngles;

		//�C���ӏ�
		transform.localRotation = Quaternion.Euler(def - _parent);
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "rightMoveBlock" || collision.gameObject.tag == "leftMoveBlock" ||
			collision.gameObject.tag == "block" || collision.gameObject.tag == "upMoveBlock" || collision.gameObject.tag == "downMoveBlock" ||
			collision.gameObject.tag == "growOriginal" || collision.gameObject.tag == "growBox")
		{
			isHit = true;
		}

		if (collision.gameObject.tag == "LightBlock")
		{
			LightBlock lightblock = collision.GetComponent<LightBlock>();
			if (lightblock.isLightHit)
			{
				isHit = true;
			}
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "rightMoveBlock" || collision.gameObject.tag == "leftMoveBlock" ||
			collision.gameObject.tag == "block" || collision.gameObject.tag == "upMoveBlock" || collision.gameObject.tag == "downMoveBlock" ||
			collision.gameObject.tag == "growOriginal" || collision.gameObject.tag == "growBox")
		{
			isHit = false;
			playerMove.isJump = false;
		}

		if (collision.gameObject.tag == "LightBlock")
		{
			LightBlock lightblock = collision.GetComponent<LightBlock>();
			if (lightblock.isLightHit)
			{
				isHit = false;
				playerMove.isJump = false;
			}
		}
	}
}
