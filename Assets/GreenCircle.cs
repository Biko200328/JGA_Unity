using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenCircle : MonoBehaviour
{
	float circleSize;

	Green green;
	// Start is called before the first frame update
	void Start()
	{
		green = GameObject.Find("GreenFire").GetComponent<Green>();
	}

	// Update is called once per frame
	void Update()
	{
		if (!green.GetCollect())
		{
			if (circleSize < green.maxSize)
			{
				circleSize += green.changeSize;
			}
			else if (circleSize >= green.maxSize)
			{
				circleSize = green.maxSize;
			}
		}
		else
		{
			circleSize = 0;
		}


		var circleVec = new Vector3(circleSize, circleSize, circleSize);
		transform.localScale = circleVec;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "GreenBlock")
		{
			GreenBlock greenBlock = collision.GetComponent<GreenBlock>();
			greenBlock.isLightHit = true;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "GreenBlock")
		{
			GreenBlock greenBlock = collision.GetComponent<GreenBlock>();
			greenBlock.isLightHit = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "GreenBlock")
		{
			GreenBlock greenBlock = collision.GetComponent<GreenBlock>();
			greenBlock.isLightHit = false;
		}
	}
}
