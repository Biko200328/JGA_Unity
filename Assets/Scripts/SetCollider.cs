using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCollider : MonoBehaviour
{
	GameObject player;

	// Start is called before the first frame update
	void Start()
	{
		player = GameObject.Find("Player");
	}

	// Update is called once per frame
	void Update()
	{
		gameObject.layer = player.layer;
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Goal")
		{
			Goal goal = collision.gameObject.GetComponent<Goal>();
			goal.isGoal = true;
		}
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Goal")
		{
			Goal goal = collision.gameObject.GetComponent<Goal>();
			goal.isGoal = true;
		}
	}
}
