using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedCircle : MonoBehaviour
{
	float circleSize;

	Red red;

	// Start is called before the first frame update
	void Start()
	{
		red = GameObject.Find("RedFire").GetComponent<Red>();
	}

	// Update is called once per frame
	void Update()
	{
		if(!red.GetCollect())
		{
			if(circleSize < red.maxSize)
			{
				circleSize += red.changeSize;
			}
			else if(circleSize >= red.maxSize)
			{
				circleSize = red.maxSize;
			}
		}
		else
		{
			//circleSize = 0;
		}

		var circleVec = new Vector3(circleSize, circleSize, circleSize);
		transform.localScale = circleVec;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "RedBlock")
		{
			RedBlock redBlock = collision.GetComponent<RedBlock>();
			redBlock.isLightHit = true;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "RedBlock")
		{
			RedBlock redBlock = collision.GetComponent<RedBlock>();
			redBlock.isLightHit = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "RedBlock")
		{
			RedBlock redBlock = collision.GetComponent<RedBlock>();
			redBlock.isLightHit = false;
		}
	}
}