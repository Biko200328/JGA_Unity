using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Green : MonoBehaviour
{
	public bool isCollect;

	[Header("円の最大値")]
	public float maxSize;

	[Header("サイズ増減数値")]
	public float changeSize;

	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void Update()
	{

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
