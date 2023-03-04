using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitFloor : MonoBehaviour
{
	// Ú’n”»’è
	public bool isHit;

	// Start is called before the first frame update
	void Start()
	{
		
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		// “–‚½‚Á‚½collision‚Ìtag‚ªFloor‚È‚çÚ’n‚Æ‚·‚é
		if (collision.gameObject.tag == "Floor") isHit = true;
	}

	private void OnTriggerStay2D(Collider2D collision)
	{
		if (collision.gameObject.tag == "Floor") isHit = true;
	}

	private void OnTriggerExit2D(Collider2D collision)
	{
		// —£‚ê‚½‚¾‚¯false‚É‚µ‚Ä‚ ‚°‚ê‚Î‹ó’†‚É‚¢‚é‚Æ‚«‚Ífalse‚É‚È‚é
		if (collision.gameObject.tag == "Floor") isHit = false;
	}
}
