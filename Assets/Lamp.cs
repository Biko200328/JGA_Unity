using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lamp : MonoBehaviour
{
	Rigidbody2D rb;

	PlayerMove playerMove;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();

		GameObject player = GameObject.Find("Player");
		playerMove = player.GetComponent<PlayerMove>();
	}

	// Update is called once per frame
	void Update()
	{
		if(playerMove.isLampTake)
		{
			Destroy(rb);
		}
		else
		{
			if (rb == null)
			{
				rb = gameObject.AddComponent<Rigidbody2D>();
				//FreezeRotation‚ðƒIƒ“‚É‚·‚é
				rb.freezeRotation = true;
			}
		}
	}

	private void FixedUpdate()
	{
		if (rb != null) rb.velocity = new Vector2(0, rb.velocity.y);
	}
}
