using System.Collections;
using UnityEngine;

public class GoldEffectScript : MonoBehaviour
{
	private float canMoveTime;

	private UIMoveData startData;

	private UIMoveData endData;

	private float startTime;

	private bool canMove;

	private Transform camTrans;

	private int m_value;

	private GameObject UIs;

	private TUIMeshText label_gold;

	private TUIMeshText gameover_gold;

	private bool m_call_key;

	private string m_call_name = string.Empty;

	private Transform m_call_trans;

	private int m_call_value;

	private void Start()
	{
		UIs = GameObject.Find("TUI/TUIControl");
		label_gold = UIs.transform.Find("title/label_glod").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
		gameover_gold = UIs.transform.Find("gameover_page/title/label_glod").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
	}

	private void Update()
	{
		if (!canMove)
		{
			return;
		}
		canMoveTime += Time.deltaTime;
		float num = canMoveTime / startTime;
		if (num >= 1f)
		{
			num = 1f;
			canMove = false;
			RecoverGoldFly();
			if (globalVal.UIState == UILayer.GAMEOVER)
			{
				AddTaskGold(m_value);
			}
			else
			{
				AddPlayerGold(m_value);
			}
			if (m_call_key)
			{
				m_call_trans.SendMessage(m_call_name, m_call_value);
				m_call_key = false;
			}
		}
		base.transform.position = Vector3.Lerp(startData.pos, endData.pos, num);
		base.transform.localScale = Vector3.Lerp(startData.size, endData.size, num);
	}

	private void StartMove(UIMoveData startdata, UIMoveData enddata, float time)
	{
		startData = startdata;
		endData = enddata;
		startTime = time;
		canMoveTime = 0f;
		canMove = true;
	}

	private void RecoverGoldFly()
	{
		ArrayList effGold = EffectManagerClass.body.effGold;
		base.gameObject.active = false;
		effGold.Add(base.gameObject);
	}

	private void AddPlayerGold(int value)
	{
		if ((int)globalVal.g_item_once_count[5] == 1)
		{
			value *= 2;
		}
		globalVal.g_gold += value;
		label_gold.text = string.Empty + globalVal.g_gold;
	}

	private void AddTaskGold(int value)
	{
		globalVal.g_gold += value;
		gameover_gold.text = string.Empty + globalVal.g_gold;
	}

	public void GoldFly(Vector3 viewPoint, int value)
	{
		UIMoveData uIMoveData = new UIMoveData();
		UIMoveData uIMoveData2 = new UIMoveData();
		camTrans = GameObject.Find("TUI/TUICamera").transform;
		m_value = value;
		Vector3 vector = camTrans.position + new Vector3(-240f, -160f, 0f);
		vector.x += viewPoint.x * 480f;
		vector.y += viewPoint.y * 320f;
		vector.z = 0f;
		Vector3 vector2 = camTrans.position + new Vector3(-240f, -160f, 0f);
		vector2.x += 465f;
		vector2.y += 302f;
		uIMoveData.pos = new Vector3(vector.x, vector.y, 0f);
		uIMoveData2.pos = new Vector3(vector2.x, vector2.y, 0f);
		uIMoveData.size = new Vector3(25f, 25f, 25f);
		uIMoveData2.size = new Vector3(18f, 18f, 18f);
		StartMove(uIMoveData, uIMoveData2, 0.5f);
	}

	public void GoldFly_big(Vector3 viewPoint, int value)
	{
		UIMoveData uIMoveData = new UIMoveData();
		UIMoveData uIMoveData2 = new UIMoveData();
		camTrans = GameObject.Find("TUI/TUICamera").transform;
		m_value = value;
		Vector3 vector = camTrans.position + new Vector3(-240f, -160f, 0f);
		vector.x += viewPoint.x * 480f;
		vector.y += viewPoint.y * 320f;
		vector.z = 0f;
		Vector3 vector2 = camTrans.position + new Vector3(-240f, -160f, 0f);
		vector2.x += 465f;
		vector2.y += 302f;
		uIMoveData.pos = new Vector3(vector.x, vector.y, 0f);
		uIMoveData2.pos = new Vector3(vector2.x, vector2.y, 0f);
		uIMoveData.size = new Vector3(65f, 65f, 65f);
		uIMoveData2.size = new Vector3(18f, 18f, 18f);
		StartMove(uIMoveData, uIMoveData2, 0.5f);
	}

	public void GoldFly_UI(Vector3 viewPoint, int value)
	{
		UIMoveData uIMoveData = new UIMoveData();
		UIMoveData uIMoveData2 = new UIMoveData();
		camTrans = GameObject.Find("TUI/TUICamera").transform;
		m_value = value;
		Vector3 vector = camTrans.position + new Vector3(-240f, -160f, 0f);
		vector.x += viewPoint.x * 480f;
		vector.y += viewPoint.y * 320f;
		vector.z = 0f;
		Vector3 vector2 = camTrans.position + new Vector3(-240f, -160f, 0f);
		vector2.x += 465f;
		vector2.y += 302f;
		uIMoveData.pos = new Vector3(vector.x, vector.y, -9f);
		uIMoveData2.pos = new Vector3(vector2.x, vector2.y, -9f);
		uIMoveData.size = new Vector3(18f, 18f, 18f);
		uIMoveData2.size = new Vector3(18f, 18f, 18f);
		StartMove(uIMoveData, uIMoveData2, 1f);
	}

	public void SetCallBack(Transform parent, string callback, int value)
	{
		m_call_key = true;
		m_call_name = callback;
		m_call_trans = parent;
		m_call_value = value;
	}
}
