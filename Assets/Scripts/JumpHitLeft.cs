using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpHitLeft : MonoBehaviour
{
	PlayerMove playerMove;
	// Start is called before the first frame update
	void Start()
	{
		GameObject player = GameObject.Find("Player");
		playerMove = player.GetComponent<PlayerMove>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Floor")
		{
			playerMove.isNextBlockL = true;
		}
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor")
		{
			playerMove.isNextBlockL = false;
		}
	}
}
