using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGate : MonoBehaviour
{
	public bool isGoal;

	RespawnManager respawnManager;

	// Start is called before the first frame update
	void Start()
	{
		// リスポーンマネージャー
		GameObject respawnManagerObj = GameObject.Find("RespawnManager");
		respawnManager = respawnManagerObj.GetComponent<RespawnManager>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "Player" || collision.gameObject.tag == "lamp")
		{
			if(isGoal)
			{
				respawnManager.SetRespawnPos(transform.position);
			}
		}
	}

	public void RespawnSet()
	{
		respawnManager.SetRespawnPos(new Vector3(transform.position.x + 1,transform.position.y,transform.position.z));
	}

}
