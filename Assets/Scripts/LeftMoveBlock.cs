using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeftMoveBlock : MonoBehaviour
{
	public bool isMove = false;

	public float moveSpeed;

	Rigidbody2D rb;
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
		if (isMove)
		{
			rb.MovePosition(transform.position - transform.right * moveSpeed * Time.deltaTime);
		}
	}
}
