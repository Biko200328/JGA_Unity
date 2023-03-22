using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalGate : MonoBehaviour
{
	public bool isGoal;

	RespawnManager respawnManager;

	[Header("���X�|�[���ʒu�̐ݒ�")]
	[SerializeField] Vector3 RespawnPos;

	// Start is called before the first frame update
	void Start()
	{
		// ���X�|�[���}�l�[�W���[
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
				respawnManager.SetRespawnPos(transform.position + RespawnPos);
			}
		}
	}

	public void RespawnSet()
	{
		respawnManager.SetRespawnPos(transform.position + RespawnPos);
	}

}