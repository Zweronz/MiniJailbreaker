using System.Collections;
using UnityEngine;

public class GameChannelManage : GamePool
{
	private const string c_strInstancePath = "Prefabs/AutoTerrain/";

	private const string c_strPoolName = "Terrain";

	private static GameChannelManage m_cmInstance;

	private static Hashtable m_cmIndexList;

	private GameChannelManage()
	{
	}

	public static GameChannelManage GetInstance()
	{
		if (m_cmInstance == null)
		{
			m_cmInstance = new GameChannelManage();
			m_cmInstance.Initialize("Prefabs/AutoTerrain/", "Terrain");
			m_cmIndexList = new Hashtable();
		}
		return m_cmInstance;
	}

	public new void Destroy()
	{
		if (m_cmInstance != null)
		{
			m_cmInstance = null;
		}
		base.Destroy();
	}

	public Transform getChannel(string strCode)
	{
		if (strCode == null || 0 >= strCode.Length)
		{
			return null;
		}
		Transform transform = getObjectInstance(strCode);
		if (null == transform)
		{
			transform = createObjectInstance(strCode);
		}
		if (null != transform)
		{
			transform.gameObject.SetActiveRecursively(true);
			Transform transform2 = transform.Find("Animation");
			if (null != transform2 && null != transform2.GetComponent<Animation>()["still"])
			{
				transform2.GetComponent<Animation>().Play("still");
			}
		}
		return transform;
	}

	public void recycleChannel(Transform cmObject)
	{
		if (!(null == cmObject))
		{
			cmObject.localEulerAngles = Vector3.zero;
			cmObject.gameObject.SetActiveRecursively(false);
			recycleObject(cmObject);
		}
	}

	public Transform getWayPointbyIndex(Transform cmChannel, int nIndex)
	{
		if (null == cmChannel)
		{
			return null;
		}
		Transform transform = cmChannel.Find("WayPoint");
		if (null == transform)
		{
			return null;
		}
		return transform.Find(nIndex.ToString());
	}

	public Transform getLastWayPoint(Transform cmChannel)
	{
		if (null == cmChannel)
		{
			return null;
		}
		Transform transform = cmChannel.Find("WayPoint");
		if (null == transform)
		{
			return null;
		}
		return transform.Find((transform.GetChildCount() - 1).ToString());
	}

	public int getWayPointCount(Transform cmChannel)
	{
		if (null == cmChannel)
		{
			return 0;
		}
		Transform transform = cmChannel.Find("WayPoint");
		if (null == transform)
		{
			return 0;
		}
		return transform.childCount;
	}

	public bool linkChannel(Transform cmPre, Transform cmCur)
	{
		if (null == cmCur)
		{
			return false;
		}
		cmCur.position = Vector3.zero;
		Transform lastWayPoint = getLastWayPoint(cmPre);
		Transform wayPointbyIndex = getWayPointbyIndex(cmCur, 0);
		if (null == wayPointbyIndex)
		{
			Debug.Log("linkChannel is null " + cmCur.name);
			return false;
		}
		float num = 0f;
		if (null != lastWayPoint)
		{
			num = cmPre.localEulerAngles.y + lastWayPoint.localEulerAngles.y;
		}
		num += 360f - wayPointbyIndex.localEulerAngles.y;
		cmCur.Rotate(Vector3.up, num);
		if (null == lastWayPoint)
		{
			cmCur.position = -wayPointbyIndex.position;
		}
		else
		{
			cmCur.position = lastWayPoint.position - wayPointbyIndex.position;
		}
		if (null != cmPre)
		{
		}
		return true;
	}

	public void setChannelIndex(Transform cmChannel, int nIndex)
	{
		if (!(null == cmChannel))
		{
			if (m_cmIndexList == null)
			{
				m_cmIndexList = new Hashtable();
			}
			if (m_cmIndexList[cmChannel.GetInstanceID()] == null)
			{
				m_cmIndexList.Add(cmChannel.GetInstanceID(), nIndex);
			}
			else
			{
				m_cmIndexList[cmChannel.GetInstanceID()] = nIndex;
			}
		}
	}

	public int getChannelIndex(Transform cmChannel)
	{
		if (null == cmChannel)
		{
			return -1;
		}
		if (m_cmIndexList[cmChannel.GetInstanceID()] == null)
		{
			return -1;
		}
		return (int)m_cmIndexList[cmChannel.GetInstanceID()];
	}

	public float getChannelDistance(Transform cmChannel)
	{
		if (null == cmChannel)
		{
			return 0f;
		}
		Transform transform = cmChannel.Find("WayPoint");
		if (null == transform)
		{
			return 0f;
		}
		float num = 0f;
		Transform transform2 = null;
		Transform transform3 = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			transform2 = transform3;
			transform3 = transform.Find(i.ToString());
			if (!(null == transform2))
			{
				num += Vector3.Distance(transform2.position, transform3.position);
			}
		}
		return num;
	}
}
