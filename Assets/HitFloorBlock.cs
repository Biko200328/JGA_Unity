using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using UnityEngine;

public class HitFloorBlock : MonoBehaviour
{
	// 親
	[SerializeField] GameObject blockObj;
	Block block;
	public bool isHit;

	// Start is called before the first frame update
	void Start()
	{
		block = blockObj.GetComponent<Block>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		GimmickRide(collision);
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		GimmickRide(collision);
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		GimmickRideOff(collision);
	}

	private void GimmickRide(Collider2D collision)
	{
		if (block.isLightIn)
		{
			// 各移動ブロック
			if (collision.gameObject.tag == "rightMoveBlock")
			{
				isHit = true;
				blockObj.transform.SetParent(collision.transform);
			}

			if (collision.gameObject.tag == "leftMoveBlock")
			{
				isHit = true;
				blockObj.transform.SetParent(collision.transform);
			}

			if (collision.gameObject.tag == "downMoveBlock")
			{
				isHit = true;
				blockObj.transform.SetParent(collision.transform);
			}

			if (collision.gameObject.tag == "upMoveBlock")
			{
				isHit = true;
				blockObj.transform.SetParent(collision.transform);
			}
		}
	}

	private void GimmickRideOff(Collider2D collision)
	{
		if (collision.gameObject.tag == "rightMoveBlock")
		{
			blockObj.transform.SetParent(null);
			isHit = false;
		}

		if (collision.gameObject.tag == "leftMoveBlock")
		{
			blockObj.transform.SetParent(null);
			isHit = false;
		}

		if (collision.gameObject.tag == "downMoveBlock")
		{
			blockObj.transform.SetParent(null);
			isHit = false;
		}

		if (collision.gameObject.tag == "upMoveBlock")
		{
			blockObj.transform.SetParent(null);
			isHit = false;
		}
	}
}
