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
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void FixedUpdate()
	{
		if (rb != null) rb.velocity = new Vector2(0, rb.velocity.y);
	}
}
