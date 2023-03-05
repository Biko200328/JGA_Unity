using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VCSwitcher : MonoBehaviour
{
	private CinemachineVirtualCamera virtualCamera;
	private GameObject player;

	// Start is called before the first frame update
	void Start()
	{
		virtualCamera = GetComponent<CinemachineVirtualCamera>();
		player = GameObject.Find("Player");
	}

	// Update is called once per frame
	void Update()
	{
		if(player != null)
		{
			virtualCamera.Priority = (int)(10000 - // 距離を計算し、優先度を変更
				Vector3.Distance(transform.position, player.transform.position) * 100);
		}
	}
}
