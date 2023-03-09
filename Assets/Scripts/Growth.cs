using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Growth : MonoBehaviour
{
	[Header("ˆÚ“®Œo˜H")] public GameObject[] movePoint;
	[Header("‘¬‚³")] public float speed = 1.0f;

	public bool isLightIn;

	public GameObject a;

	public float createTime;
	float createNowTime;
	bool isCreate;

	public bool isEnd = false;

	private void Start()
	{
		
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
				GrowthMove growthMove = Instantiate(a, transform.position, Quaternion.identity).GetComponent<GrowthMove>();
				growthMove.movePoint = this.movePoint;
				growthMove.speed = this.speed;
				growthMove.growth = this.gameObject.GetComponent<Growth>();
				createNowTime = 0;
				isCreate = false;
			}
		}
	}
}
