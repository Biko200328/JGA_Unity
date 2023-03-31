using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : MonoBehaviour
{
	private bool isCollect;

	[Header("円の最大値")]
	public float maxSize;

	[Header("サイズ増減数値")]
	public float changeSize;

	Rigidbody2D rb;

	// Start is called before the first frame update
	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update()
	{
		
	}

	private void FixedUpdate()
	{
		// グリッドに合うように
		if (!isCollect)
		{
			// 整数部分
			int intLampPosX = (int)rb.position.x;
			// 小数部分
			float fltLampPosX = rb.position.x - intLampPosX;
			// 数値代入
			var pos = rb.position;

			if (fltLampPosX < 1 && fltLampPosX > 0.5f)
			{
				intLampPosX += 1;
			}

			pos.x = (float)intLampPosX;
			rb.position = pos;
		}
	}

	/// <summary>
	/// コレクトモード変更
	/// </summary>
	/// <param name="a">
	/// true -> 回収
	/// false -> 設置
	/// </param>
	public void SetCollect(bool a)
	{
		isCollect = a;
	}

	public bool GetCollect()
	{
		return isCollect;
	}
}
