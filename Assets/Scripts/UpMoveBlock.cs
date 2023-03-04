using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpMoveBlock : MonoBehaviour
{
	public bool isMove = false;

	public float moveSpeed;
	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{
		if (isMove)
		{
			transform.position += new Vector3(0, moveSpeed,0);
		}
	}
}
