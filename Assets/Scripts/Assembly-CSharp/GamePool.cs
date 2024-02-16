using System.Collections;
using UnityEngine;

public class GamePool
{
	protected string m_strObjectPath;

	protected string m_strObjectPool;

	private Hashtable m_htResourcesPool;

	private Hashtable m_htInstancePool;

	private ArrayList m_alUseList;

	public void Initialize(string strObjectPath, string strObjectPool)
	{
		m_strObjectPath = strObjectPath;
		m_strObjectPool = strObjectPool;
		if (m_htResourcesPool == null)
		{
			m_htResourcesPool = new Hashtable();
		}
		if (m_htInstancePool == null)
		{
			m_htInstancePool = new Hashtable();
		}
		if (m_alUseList == null)
		{
			m_alUseList = new ArrayList();
		}
	}

	public void Destroy()
	{
		m_htResourcesPool.Clear();
		m_htResourcesPool = null;
		m_htInstancePool.Clear();
		m_htResourcesPool = null;
		m_alUseList.Clear();
		m_alUseList = null;
	}

	public Transform getObjectInstance(string strCode)
	{
		Transform transform = null;
		if (null == transform)
		{
			transform = getFromPool(strCode);
		}
		m_alUseList.Add(transform);
		return transform;
	}

	public GameObject getFromScene(string strCode)
	{
		if (strCode == null || 0 >= strCode.Length)
		{
			return null;
		}
		GameObject gameObject = GameObject.Find(m_strObjectPool);
		if (null == gameObject)
		{
			Debug.Log("Pool getFromScene cmPath is null " + m_strObjectPool);
			return null;
		}
		Transform transform = gameObject.transform.Find(strCode);
		if (null == transform)
		{
			Debug.Log("Pool getFromScene strCode is null " + strCode + " PoolName = " + gameObject.name);
			return null;
		}
		return transform.gameObject;
	}

	public Transform getFromPool(string strCode)
	{
		if (strCode == null || 0 >= strCode.Length || m_htInstancePool == null || m_htInstancePool[strCode] == null)
		{
			return null;
		}
		ArrayList arrayList = m_htInstancePool[strCode] as ArrayList;
		if (0 >= arrayList.Count)
		{
			return null;
		}
		Transform transform = null;
		while (0 < arrayList.Count && null == transform)
		{
			transform = arrayList[0] as Transform;
			arrayList.RemoveAt(0);
		}
		return transform;
	}

	public Transform createObjectInstance(string strCode)
	{
		GameObject fromScene = getFromScene(strCode);
		if (null == fromScene)
		{
			return null;
		}
		fromScene = Object.Instantiate(fromScene, Vector3.zero, Quaternion.identity) as GameObject;
		fromScene.transform.name = strCode;
		m_alUseList.Add(fromScene.transform);
		return fromScene.transform;
	}

	public GameObject LoadFromInstance(string strCode)
	{
		if (strCode == null || 0 >= strCode.Length)
		{
			return null;
		}
		GameObject gameObject = null;
		Transform transform = null;
		for (int i = 0; i < m_alUseList.Count; i++)
		{
			transform = m_alUseList[i] as Transform;
			if (null != transform && strCode == transform.name)
			{
				return transform.gameObject;
			}
		}
		gameObject = GameObject.Find(m_strObjectPool);
		if (null != gameObject)
		{
			for (int j = 0; j < gameObject.transform.childCount; j++)
			{
				transform = gameObject.transform.GetChild(j);
				if (null != transform && strCode == transform.name)
				{
					return transform.gameObject;
				}
			}
		}
		return null;
	}

	public GameObject LoadFromResources(string strCode)
	{
		if (strCode == null || 0 >= strCode.Length)
		{
			return null;
		}
		if (m_htResourcesPool == null)
		{
			m_htResourcesPool = new Hashtable();
		}
		GameObject gameObject = (GameObject)Resources.Load(m_strObjectPath + strCode);
		if (null != gameObject)
		{
			m_htResourcesPool.Add(strCode, gameObject);
		}
		return gameObject;
	}

	public bool recycleObject(Transform cmObject)
	{
		if (null == cmObject)
		{
			return false;
		}
		if (m_htInstancePool == null)
		{
			m_htInstancePool = new Hashtable();
		}
		ArrayList arrayList = null;
		if (m_htInstancePool[cmObject.name] == null)
		{
			arrayList = new ArrayList(4);
			m_htInstancePool.Add(cmObject.name, arrayList);
		}
		if (null != cmObject.transform.parent)
		{
			cmObject.transform.parent = null;
		}
		arrayList = m_htInstancePool[cmObject.name] as ArrayList;
		arrayList.Add(cmObject);
		m_htInstancePool[cmObject.name] = arrayList;
		m_alUseList.Remove(cmObject);
		return true;
	}

	public Transform citeObject(string strCode)
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
		if (null == transform)
		{
			return null;
		}
		recycleObject(transform);
		return transform;
	}
}
