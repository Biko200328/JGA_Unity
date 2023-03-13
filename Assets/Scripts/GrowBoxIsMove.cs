using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrowBoxIsMove : MonoBehaviour
{
	[SerializeField] GameObject growthObj;
	GrowthMove growthMove;

	// Start is called before the first frame update
	void Start()
	{
		growthMove = growthObj.gameObject.GetComponent<GrowthMove>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "growBox")
		{
			if(growthMove != null) growthMove.isStop = true;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "growBox")
		{
            if (growthMove != null) growthMove.isStop = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "growBox")
		{
            if (growthMove != null) growthMove.isStop = false;
		}
	}
}
