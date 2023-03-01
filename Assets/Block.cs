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
	}
}
