using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFoot : MonoBehaviour
{
	[SerializeField] bool isHit;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

	}

	public bool GetIsHit()
	{
		return isHit;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "RedBlock" || collision.gameObject.tag == "GreenBlock")
		{
			isHit = true;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "RedBlock" || collision.gameObject.tag == "GreenBlock")
		{
			isHit = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "RedBlock" || collision.gameObject.tag == "GreenBlock")
		{
			isHit = false;
		}
	}
}
