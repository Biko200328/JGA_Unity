using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
	Rigidbody2D rb;
	public bool a;
	// Start is called before the first frame update
	void Start()
	{
		rb = this.gameObject.GetComponent<Rigidbody2D>();
		rb.isKinematic = true;
	}

	// Update is called once per frame
	void Update()
	{
		a = rb.isKinematic;
		rb.velocity = new Vector2(0,rb.velocity.y);
	}

	private void OnCollisionEnter(Collision collision)
	{
		Rigidbody2D collisionRb = collision.gameObject.GetComponent<Rigidbody2D>();
		if(collisionRb.velocity.x != 0)
		{
			rb.velocity = new Vector2(collisionRb.velocity.x, rb.velocity.y);
		}
	}
}