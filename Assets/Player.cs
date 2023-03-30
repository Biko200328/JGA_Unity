using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
	[Header("移動スピード")]
	[SerializeField] float moveSpeed;

	Rigidbody2D rb;
	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		if (Input.GetKey(KeyCode.A))
		{
			rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
			transform.eulerAngles = new Vector3(0, 0, 0);
		}
		else if (Input.GetKey(KeyCode.D))
		{
			rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
			transform.eulerAngles = new Vector3(0, 180, 0);
		}
		else
		{
			rb.velocity = new Vector2(0, rb.velocity.y);
		}
	}
}
