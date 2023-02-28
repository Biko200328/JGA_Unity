using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCircle : MonoBehaviour
{
	public float circleSize = 1;
	private Vector3 circleVec;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{
		circleVec = new Vector3(circleSize, circleSize,circleSize);
		transform.localScale = circleVec;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		
	}
}
