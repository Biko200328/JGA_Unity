using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenFrame : MonoBehaviour
{
	[SerializeField] GreenBlock greenblock;
	private SpriteRenderer render;
	// Start is called before the first frame update
	void Start()
	{
		render = GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if (greenblock.GetIsLightHit())
		{
			render.sortingLayerName = "Player";
		}
		else
		{
			render.sortingLayerName = "Default";
		}
	}
}
