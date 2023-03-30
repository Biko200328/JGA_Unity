using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Red : MonoBehaviour
{
	public bool isCollect;

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
}
