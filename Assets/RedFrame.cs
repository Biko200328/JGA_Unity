using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedFrame : MonoBehaviour
{
	[SerializeField] RedBlock redblock;
	private SpriteRenderer render;
	// Start is called before the first frame update
	void Start()
	{
		render = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if(redblock.GetIsLightHit())
		{
			render.sortingLayerName = "Player";
		}
		else
		{
			render.sortingLayerName = "Default";
		}
	}
}
