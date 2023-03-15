using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Growth : MonoBehaviour
{
	[Header("ˆÚ“®Œo˜H")] public GameObject[] movePoint;
	[Header("‘¬‚³")] public float speed = 1.0f;

	public bool isLightIn;

	public GameObject a;
	public GameObject secondBox;

	public float createTime;
	float createNowTime;
	bool isCreate;

	public bool isEnd = false;

	public bool isFirst;

	Lamp lamp;
	PlayerMove playerMove;
	private void Start()
	{
		isFirst = false;

		GameObject lampObj = GameObject.Find("Lamp");
		lamp = lampObj.GetComponent<Lamp>();

		GameObject player = GameObject.Find("Player");
		playerMove = player.GetComponent<PlayerMove>();
	}

	private void Update()
	{
		if(!lamp.isLampOn)
		{
			isLightIn = false;
			isEnd = false;
		}

		if (playerMove.isLampCollect)
		{
			isLightIn = false;
			isEnd = false;
		}

		if (playerMove.isPlace)
		{
			isLightIn = false;
			isEnd = false;
		}
	}

	private void FixedUpdate()
	{
		if(!isEnd && isLightIn)
		{
			if (isCreate == false)
			{
				createNowTime += Time.deltaTime;
				if (createNowTime >= createTime)
				{
					isCreate = true;
				}
			}
			else
			{
				if(isFirst == false)
				{
					GrowthMove growthMove = Instantiate(a, transform.position, Quaternion.identity).GetComponent<GrowthMove>();
					growthMove.movePoint = this.movePoint;
					growthMove.speed = this.speed;
					growthMove.growth = this.gameObject.GetComponent<Growth>();
					createNowTime = 0;
					isCreate = false;
					isFirst = true;
				}
				else
				{
					if(secondBox != null)
					{
						GrowthMove growthMove = Instantiate(secondBox, transform.position, Quaternion.identity).GetComponent<GrowthMove>();
						growthMove.movePoint = this.movePoint;
						growthMove.speed = this.speed;
						growthMove.growth = this.gameObject.GetComponent<Growth>();
						createNowTime = 0;
						isCreate = false;
					}
				}
			}
		}
	}
}
