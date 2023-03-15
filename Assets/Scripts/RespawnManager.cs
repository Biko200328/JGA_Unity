using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnManager : MonoBehaviour
{
	[SerializeField] public static Vector3 respawnPos = new Vector3(0,0,0);

	// Start is called before the first frame update
	void Start()
	{
		if(respawnPos == Vector3.zero)
		{
			respawnPos = GameObject.Find("Player").transform.position;
		}
	}

	// Update is called once per frame
	void Update()
	{

	}
	
	// ゲッター
	public Vector3 GetRespawnPos()
	{
		return respawnPos;
	}

	// セッター
	public void SetRespawnPos(Vector3 pos)
	{
		respawnPos = pos;
	}
}
