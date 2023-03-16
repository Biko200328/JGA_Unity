using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	public bool isGoal;
	[SerializeField] GameObject gate;
	[SerializeField] GameObject circle;

	[Header("�J�������̃X�v���C�g")]
	[SerializeField] Sprite openTex;

	[Header("�����Ȃ�X�s�[�h")]
	[SerializeField] float addCircle;

	[Header("�~�̍ő�l")]
	[SerializeField] float maxCircle;

	SpriteRenderer gateRenderer;


	// Start is called before the first frame update
	void Start()
	{
		gateRenderer = gate.GetComponent<SpriteRenderer>();
	}

	// Update is called once per frame
	void Update()
	{
		if(isGoal)
		{
			// �~��傫������
			if(circle.transform.localScale.x < maxCircle)
			{
				circle.transform.localScale += new Vector3(addCircle, addCircle);
			}
			else
			{
				circle.transform.localScale = new Vector3(maxCircle, maxCircle);
			}
		}
	}

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if(collision.gameObject.tag == "lamp" || collision.gameObject.tag == "NotPlatformLamp")
		{
			isGoal = true;
		}
	}

	public void GoalGateOpen()
	{
		GoalGate goalgate = gate.GetComponent<GoalGate>();
		goalgate.isGoal = true;
		//�Q�[�g���J����
		//�����蔻����g���K�[��
		gate.GetComponent<BoxCollider2D>().isTrigger = true;
		//�X�v���C�g�̕ύX
		gateRenderer.sprite = openTex;
	}
}
