using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMoveBlock : MonoBehaviour
{
	[Header("ˆÚ“®‘¬“x")]
	public float moveSpeed;

	[Header("“®‚­ó‘Ô‚©‚Ç‚¤‚©‚Ìƒtƒ‰ƒO")]
	public bool isMove = false;


	public FloorCheckLeft floorCheckLeft;

	Rigidbody2D rb;

	Lamp lamp;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		GameObject lampObj = GameObject.Find("Lamp");
		lamp = lampObj.GetComponent<Lamp>();
	}

	// Update is called once per frame
	void Update()
	{
		//‰~‚Ì’†‚©‚ÂÕ“Ë”»’è‚ª‚È‚¢ê‡‚É
		if (isMove && !floorCheckLeft.isFloor)
		{
			//“®‚©‚·
			transform.position += new Vector3(-moveSpeed, 0, 0);
		}

		if (!lamp.isLampOn)
		{
			isMove = false;
		}
	}
}
