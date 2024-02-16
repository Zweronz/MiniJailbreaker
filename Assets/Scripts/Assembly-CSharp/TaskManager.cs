using System.Collections;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
	private bool taskPop;

	private GameObject UIs;

	private TUIMeshText label_gold;

	private TAudioController audios;

	private float c_fWaitTime = 5f;

	private float[] m_fCompleteTime = new float[3];

	private float m_fCheckTime;

	private void Start()
	{
		GameObject gameObject = GameObject.Find("TAudioController");
		if (gameObject == null)
		{
			gameObject = new GameObject("TAudioController");
			gameObject.AddComponent(typeof(TAudioController));
			Object.DontDestroyOnLoad(gameObject);
		}
		audios = gameObject.GetComponent(typeof(TAudioController)) as TAudioController;
	}

	private void Update()
	{
		if (globalVal.UIState == UILayer.GAMEOVER)
		{
			bool buttonActive = true;
			for (int i = 0; i < 3; i++)
			{
				if (m_fCompleteTime[i] != -1f && c_fWaitTime <= Time.realtimeSinceStartup - m_fCompleteTime[i])
				{
					ChangeTaskInfo(i + 1);
					m_fCompleteTime[i] = -1f;
				}
				if (m_fCompleteTime[i] != -1f)
				{
					buttonActive = false;
				}
			}
			setButtonActive(buttonActive);
		}
		if (globalVal.UIState != UILayer.GAME || Time.realtimeSinceStartup - m_fCheckTime <= 3f)
		{
			return;
		}
		taskInfo taskInfo2 = null;
		globalVal.AccomplishmentScore = 0;
		globalVal.AccomplishmentGold = 0;
		for (int j = 0; j < globalVal.Accomplishment.Length / 2; j++)
		{
			if (!globalVal.Accomplishment[j * 2])
			{
				taskInfo2 = ItemManagerClass.body.hashTask[j] as taskInfo;
				if (taskInfo2 != null && checkTaskComplete(taskInfo2.type, taskInfo2.value1, taskInfo2.value2))
				{
					globalVal.Accomplishment[j * 2] = true;
					globalVal.Accomplishment[j * 2 + 1] = false;
					submitAccomplishment(taskInfo2);
					globalVal.AccomplishmentScore += taskInfo2.score;
					globalVal.AccomplishmentGold += taskInfo2.golds;
					return;
				}
			}
		}
		m_fCheckTime = Time.realtimeSinceStartup;
	}

	public void submitAccomplishment(taskInfo cmTaskInfo)
	{
		if (cmTaskInfo == null)
		{
			return;
		}
		GameObject gameObject = GameObject.Find("TUI");
		if (!(null == gameObject))
		{
			GameCenterManager gameCenterManager = gameObject.GetComponent(typeof(GameCenterManager)) as GameCenterManager;
			if (!(null == gameCenterManager))
			{
				gameCenterManager.submitAccomplishment(int.Parse(cmTaskInfo.id.Trim()));
			}
		}
	}

	public void InitTaskList()
	{
		taskPop = false;
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform;
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		Debug.Log("globalVal.GameStatus = " + globalVal.GameStatus);
		if (globalVal.UIState == UILayer.GAMEOVER)
		{
			uIMoveControl.SetEndPos(-180f, -150f, 0f);
		}
		else
		{
			uIMoveControl.SetEndPos(-180f, -50f, 0f);
		}
		uIMoveControl.LeftToRight(0.2f, transform);
		if (globalVal.UIState == UILayer.GAMEOVER)
		{
		}
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			int num = 0;
			switch (child.name)
			{
			case "task_list1":
				num = 0;
				SetTaskInfo(num, child);
				break;
			case "task_list2":
				num = 1;
				SetTaskInfo(num, child);
				break;
			case "task_list3":
				num = 2;
				SetTaskInfo(num, child);
				break;
			}
		}
	}

	public void BackTask(int index)
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		string text = "task_list" + index;
		transform = base.transform.Find(text);
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		uIMoveControl.SetEndPos(0f, -45 * (index - 1), 0f);
		uIMoveControl.RightToLeft_taskback(0f, transform);
	}

	public void ChangeTaskInfo(int index)
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		string text = "task_list" + index;
		transform = base.transform.Find(text);
		globalVal.cur_task_id[index - 1]++;
		ArrayList arrayList = ItemManagerClass.body.taskListArray[index - 1] as ArrayList;
		if (globalVal.cur_task_id[index - 1] > arrayList.Count - 1)
		{
			globalVal.cur_task_id[index - 1] = 0;
		}
		string key = arrayList[globalVal.cur_task_id[index - 1]] as string;
		taskInfo taskInfo2 = ItemManagerClass.body.hashTask[key] as taskInfo;
		Transform transform2 = transform.Find("label_task_text");
		if (transform2 != null)
		{
			TUIMeshText tUIMeshText = transform2.GetComponent(typeof(TUIMeshText)) as TUIMeshText;
			tUIMeshText.text = taskInfo2.info;
		}
		transform2 = transform.Find("label_glod");
		if (transform2 != null)
		{
			TUIMeshText tUIMeshText2 = transform2.GetComponent(typeof(TUIMeshText)) as TUIMeshText;
			tUIMeshText2.text = string.Empty + taskInfo2.golds;
		}
		transform2 = transform.Find("pic_complete");
		if (transform2 != null)
		{
			UIMoveControl uIMoveControl2 = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
			uIMoveControl2.SetPos(-1000f, -1000f, 0f);
		}
		transform = base.transform.Find(text);
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		uIMoveControl.SetEndPos(0f, -45 * (index - 1), 0f);
		uIMoveControl.RightToLeft_taskback_back(0f, transform);
	}

	public void TaskComplete(int index)
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		string text = "task_list" + index;
		transform = base.transform.Find(text);
		Transform transform2 = transform.Find("pic_complete");
		uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		uIMoveControl.SetEndPos(-96f, 45f, -7.7f);
		uIMoveControl.ScaleRotate(0.3f, transform2);
		GoldFly(index);
		m_fCompleteTime[index - 1] = Time.realtimeSinceStartup;
	}

	public void GoldFly(int index)
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		string text = "task_list" + index;
		transform = base.transform.Find(text);
		Transform trans = transform.Find("pic_gold");
		int num = 0;
		Transform transform2 = transform.Find("label_glod");
		if (transform2 != null)
		{
			TUIMeshText tUIMeshText = transform2.GetComponent(typeof(TUIMeshText)) as TUIMeshText;
			num = int.Parse(tUIMeshText.text);
		}
		for (int i = 0; i < 5; i++)
		{
			int num2 = 0;
			num2 = ((i == 4) ? num : (i + 1));
			StartCoroutine(GoldFlyOntime((float)i * 0.1f, trans, num2, index));
			num -= i + 1;
		}
	}

	private IEnumerator GoldFlyOntime(float waitTime, Transform trans, int value, int index)
	{
		yield return new WaitForSeconds(waitTime);
		if (null != EffectManagerClass.body)
		{
			GameObject t = EffectManagerClass.body.PlayGoldFlyEffect_UI(trans, value);
			if (value > 5)
			{
				GoldEffectScript ge = t.GetComponent(typeof(GoldEffectScript)) as GoldEffectScript;
			}
		}
	}

	public void CallbackBackTask(int index)
	{
		BackTask(index);
	}

	public bool haveComplete()
	{
		taskInfo taskInfo2 = null;
		for (int i = 0; i < globalVal.cur_task_id.Length; i++)
		{
			taskInfo2 = getTaskInfo(i);
			if (taskInfo2 != null && checkTaskComplete(taskInfo2.type, taskInfo2.value1, taskInfo2.value2))
			{
				return true;
			}
		}
		return false;
	}

	public void setButtonActive(bool bActive)
	{
		if (globalVal.UIState != UILayer.GAMEOVER)
		{
			return;
		}
		Debug.Log("setButtonActive = " + bActive);
		GameObject gameObject = GameObject.Find("TUI/TUIControl/account");
		if (null == gameObject)
		{
			return;
		}
		TUIButtonClick tUIButtonClick = null;
		for (int i = 0; i < gameObject.transform.childCount; i++)
		{
			if ("menu" == gameObject.transform.GetChild(i).name || "retry" == gameObject.transform.GetChild(i).name || "thestash" == gameObject.transform.GetChild(i).name)
			{
				tUIButtonClick = gameObject.transform.GetChild(i).GetComponent(typeof(TUIButtonClick)) as TUIButtonClick;
				if (null != tUIButtonClick)
				{
					tUIButtonClick.disabled = !bActive;
				}
			}
		}
	}

	public void PopTaskList()
	{
		if (taskPop)
		{
			PopBackTaskList();
			taskPop = !taskPop;
			audios.PlayAudio("UImission_back");
			setButtonActive(true);
			return;
		}
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform;
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		if (globalVal.UIState == UILayer.GAMEOVER)
		{
			uIMoveControl.SetEndPos(80f, -150f, 0f);
		}
		else
		{
			uIMoveControl.SetEndPos(80f, -50f, 0f);
		}
		uIMoveControl.LeftToRight_taskpop(0f, transform);
		if (globalVal.UIState == UILayer.GAMEOVER)
		{
			taskInfo taskInfo2 = null;
			bool flag = false;
			for (int i = 0; i < globalVal.cur_task_id.Length; i++)
			{
				taskInfo2 = getTaskInfo(i);
				if (taskInfo2 != null)
				{
					flag = checkTaskComplete(taskInfo2.type, taskInfo2.value1, taskInfo2.value2);
					if (flag)
					{
						TaskComplete(i + 1);
					}
				}
			}
			setButtonActive(!flag);
		}
		taskPop = !taskPop;
	}

	public void PopBackTaskList()
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform;
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		if (globalVal.UIState == UILayer.GAMEOVER)
		{
			uIMoveControl.SetEndPos(80f, -150f, 0f);
		}
		else
		{
			uIMoveControl.SetEndPos(80f, -50f, 0f);
		}
		uIMoveControl.LeftToRight_taskpop_back(0f, transform);
	}

	private void SetTaskInfo(int listId, Transform childs)
	{
		ArrayList arrayList = ItemManagerClass.body.taskListArray[listId] as ArrayList;
		string key = arrayList[globalVal.cur_task_id[listId]] as string;
		taskInfo taskInfo2 = ItemManagerClass.body.hashTask[key] as taskInfo;
		if (taskInfo2 != null)
		{
			Transform transform = childs.Find("label_task_text");
			if (transform != null)
			{
				TUIMeshText tUIMeshText = transform.GetComponent(typeof(TUIMeshText)) as TUIMeshText;
				tUIMeshText.text = taskInfo2.info;
			}
			transform = childs.Find("label_glod");
			if (transform != null)
			{
				TUIMeshText tUIMeshText2 = transform.GetComponent(typeof(TUIMeshText)) as TUIMeshText;
				tUIMeshText2.text = string.Empty + taskInfo2.golds;
			}
			transform = childs.Find("pic_complete");
			if (transform != null)
			{
				UIMoveControl uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetPos(-1000f, -1000f, 0f);
			}
		}
	}

	public taskInfo getTaskInfo(int nIndex)
	{
		ArrayList arrayList = ItemManagerClass.body.taskListArray[nIndex] as ArrayList;
		if (globalVal.cur_task_id[nIndex] > arrayList.Count - 1)
		{
			globalVal.cur_task_id[nIndex] = 0;
		}
		string text = arrayList[globalVal.cur_task_id[nIndex]] as string;
		if (text == null || ItemManagerClass.body.hashTask[text] == null)
		{
			return null;
		}
		return ItemManagerClass.body.hashTask[text] as taskInfo;
	}

	public bool checkMeters(int nType, float fValue)
	{
		if (fValue <= globalVal.Distance[nType])
		{
			return true;
		}
		return false;
	}

	public bool checkGold(int nType, float fValue)
	{
		if (fValue <= (float)globalVal.Gold[nType])
		{
			return true;
		}
		return false;
	}

	public bool checkMonster(int nType, float fValue)
	{
		if (fValue <= (float)globalVal.Monster[nType])
		{
			return true;
		}
		return false;
	}

	public bool checkUseItem(int nType, int nItemID, float fValue)
	{
		if (12 <= nItemID)
		{
			return false;
		}
		if (fValue <= (float)globalVal.UseItemList[nItemID * 2 + nType])
		{
			return true;
		}
		return false;
	}

	public bool checkTreasureBox(int nType, int nItemID, float fValue)
	{
		if (12 <= nItemID)
		{
			return false;
		}
		if (fValue <= (float)globalVal.TreasureBox[nItemID * 2 + nType])
		{
			return true;
		}
		return false;
	}

	public bool checkDeathType(int nCount)
	{
		int num = 0;
		for (int i = 0; i < globalVal.Death.Length; i++)
		{
			if (0 < globalVal.Death[i])
			{
				num++;
			}
		}
		if (nCount <= num)
		{
			return true;
		}
		return false;
	}

	public bool checkTaskComplete(ENUM_TASK_TYPE nType, float fValue1, float fValue2)
	{
		switch (nType)
		{
		case ENUM_TASK_TYPE.TASK_METERS:
			return checkMeters(6, fValue1);
		case ENUM_TASK_TYPE.TASK_GOLD:
			return checkGold(6, fValue1);
		case ENUM_TASK_TYPE.TASK_MONSTER:
			return checkMonster(6, fValue1);
		case ENUM_TASK_TYPE.TASK_AREA_METERS:
			if (6 <= (int)fValue1)
			{
				return false;
			}
			return checkMeters((int)fValue1, fValue2);
		case ENUM_TASK_TYPE.TASK_AREA_GOLD:
			if (6 <= (int)fValue1)
			{
				return false;
			}
			return checkGold((int)fValue1, fValue2);
		case ENUM_TASK_TYPE.TASK_AREA_MONSTER:
			if (6 <= (int)fValue1)
			{
				return false;
			}
			return checkMonster((int)fValue1, fValue2);
		case ENUM_TASK_TYPE.TASK_ALL_METERS:
			return checkMeters(7, fValue1);
		case ENUM_TASK_TYPE.TASK_ALL_GOLD:
			return checkGold(7, fValue1);
		case ENUM_TASK_TYPE.TASK_ALL_MONSTER:
			return checkMonster(7, fValue1);
		case ENUM_TASK_TYPE.TASK_USEITEM:
			return checkUseItem(0, (int)fValue1, fValue2);
		case ENUM_TASK_TYPE.TASK_ALL_USEITEM:
			return checkUseItem(1, (int)fValue1, fValue2);
		case ENUM_TASK_TYPE.TASK_TREASUREBOX:
			return checkTreasureBox(0, (int)fValue1, fValue2);
		case ENUM_TASK_TYPE.TASK_ALL_TREASUREBOX:
			return checkTreasureBox(1, (int)fValue1, fValue2);
		case ENUM_TASK_TYPE.TASK_DEATH:
			return (int)fValue1 == globalVal.CurDeath;
		case ENUM_TASK_TYPE.TASK_DEATH_COUNT:
			return (int)fValue1 == globalVal.AllDeathCount;
		case ENUM_TASK_TYPE.TASK_DEATH_TYPE_COUNT:
			return (int)fValue2 == globalVal.Death[(int)fValue1];
		case ENUM_TASK_TYPE.TASK_UNDEFAULT_METERS:
			if (globalVal.g_avatar_id == 0)
			{
				return false;
			}
			return checkMeters(6, fValue1);
		case ENUM_TASK_TYPE.TASK_UNDEFAULT_GOLD:
			if (globalVal.g_avatar_id == 0)
			{
				return false;
			}
			return checkGold(6, fValue1);
		case ENUM_TASK_TYPE.TASK_UNDEFAULT_MONSTER:
			if (globalVal.g_avatar_id == 0)
			{
				return false;
			}
			return checkMonster(6, fValue1);
		case ENUM_TASK_TYPE.TASK_DEATH_TYPE:
			return checkDeathType((int)fValue1);
		default:
			return false;
		}
	}
}
