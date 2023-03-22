using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class Growth : MonoBehaviour
{
	[Header("移動経路")] public GameObject[] movePoint;
	[Header("速さ")] public float speed = 1.0f;

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

	JumpHitLeft jumpHitLeft;
	JumpHitLeft jumpHitLeft2;
	JumpHitRight jumpHitRight;
	JumpHitRight jumpHitRight2;
	private void Start()
	{
		isFirst = false;

		GameObject lampObj = GameObject.Find("Lamp");
		lamp = lampObj.GetComponent<Lamp>();

		GameObject player = GameObject.Find("Player");
		playerMove = player.GetComponent<PlayerMove>();

		// 子オブジェクト読み込み
		GameObject childJumpR = player.transform.Find("JumpHitRight").gameObject;
		// コンポーネント読み込み
		jumpHitRight = childJumpR.GetComponent<JumpHitRight>();
		// 子オブジェクト読み込み
		GameObject childJumpR2 = player.transform.Find("JumpHitRight2").gameObject;
		// コンポーネント読み込み
		jumpHitRight2 = childJumpR2.GetComponent<JumpHitRight>();

		// 子オブジェクト読み込み
		GameObject childJumpL = player.transform.Find("JumpHitLeft").gameObject;
		// コンポーネント読み込み
		jumpHitLeft = childJumpL.GetComponent<JumpHitLeft>();
		// 子オブジェクト読み込み
		GameObject childJumpL2 = player.transform.Find("JumpHitLeft2").gameObject;
		// コンポーネント読み込み
		jumpHitLeft2 = childJumpL2.GetComponent<JumpHitLeft>();
	}

	private void Update()
	{
		if(!lamp.isLampOn)
		{
			isLightIn = false;
			isEnd = false;
			lamp.isHitGrowBox = false;
		}

		if (playerMove.isLampCollect)
		{
			isLightIn = false;
			isEnd = false;
			lamp.isHitGrowBox = false;
			jumpHitLeft.isHit = false;
			jumpHitLeft2.isHit = false;
			jumpHitRight.isHit = false;
			jumpHitRight2.isHit = false;
		}

		if (playerMove.isPlace)
		{
			isLightIn = false;
			isEnd = false;
			lamp.isHitGrowBox = false;
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
