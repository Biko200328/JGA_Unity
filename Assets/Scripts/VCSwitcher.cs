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
			virtualCamera.Priority = (int)(10000 - // �������v�Z���A�D��x��ύX
				Vector3.Distance(transform.position, player.transform.position) * 100);
		}
	}
}
