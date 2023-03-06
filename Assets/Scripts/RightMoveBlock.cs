using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightMoveBlock : MonoBehaviour
{
	[Header("ˆÚ“®‘¬“x")]
	public float moveSpeed;

	[Header("“®‚­ó‘Ô‚©‚Ç‚¤‚©‚Ìƒtƒ‰ƒO")]
	public bool isMove = false;


	public FloorCheckRight floorCheckRight;

	Rigidbody2D rb;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		//‰~‚Ì’†‚©‚ÂÕ“Ë”»’è‚ª‚È‚¢ê‡‚É
		if (isMove && !floorCheckRight.isFloor)
		{
			//“®‚©‚·
			transform.position += new Vector3(moveSpeed, 0, 0);
		}
	}
}
