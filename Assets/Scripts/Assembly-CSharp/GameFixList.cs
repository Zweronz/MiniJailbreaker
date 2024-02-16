using System.Collections;
using UnityEngine;

public class GameFixList
{
	private static GameFixList m_cmInstance;

	private ArrayList m_alFixList;

	public static GameFixList GetInstance()
	{
		if (m_cmInstance == null)
		{
			m_cmInstance = new GameFixList();
			m_cmInstance.Initialize();
		}
		return m_cmInstance;
	}

	private void Initialize()
	{
		if (m_alFixList == null)
		{
			m_alFixList = new ArrayList();
		}
	}

	public void Reset()
	{
		if (m_alFixList != null)
		{
			m_alFixList.Clear();
		}
		m_alFixList = new ArrayList();
	}

	public void Destroy()
	{
		if (m_alFixList != null)
		{
			m_alFixList.Clear();
		}
		m_alFixList = null;
		m_cmInstance = null;
	}

	public void addTransform(Transform cmTransform)
	{
		if (m_alFixList == null)
		{
			m_alFixList = new ArrayList();
		}
		m_alFixList.Add(cmTransform);
	}

	public void addFixNode(string strCode, TerrainNode cmNode, float fRunWay, Vector3 v3Position, Vector3 v3Forward, Vector3 v3EulerAngles, bool bIsMonster)
	{
		FixNode fixNode = default(FixNode);
		fixNode.Code = strCode;
		fixNode.Node = cmNode;
		fixNode.RunWay = fRunWay;
		fixNode.Position = v3Position;
		fixNode.Forward = v3Forward;
		fixNode.EulerAngles = v3EulerAngles;
		fixNode.IsMonster = bIsMonster;
		m_alFixList.Add(fixNode);
	}

	public bool fix(Transform cmSub, bool bIsMonster)
	{
		Vector3 position = cmSub.position;
		cmSub.position = Vector3.zero;
		position.y += 2f;
		RaycastHit hitInfo;
		if (Physics.Raycast(position, Vector3.down, out hitInfo, 10f, 1 << LayerMask.NameToLayer("Default")))
		{
			cmSub.position = hitInfo.point;
			return true;
		}
		position.y -= 2f;
		cmSub.position = position;
		globalVal.Subassembly.recycleSubassembly(cmSub);
		return false;
	}

	public void Run()
	{
		if (m_alFixList == null || 0 >= m_alFixList.Count)
		{
			return;
		}
		int index = 0;
		FixNode fixNode = (FixNode)m_alFixList[index];
		float num = 0f;
		m_alFixList.RemoveAt(index);
		Transform transform = null;
		transform = globalVal.Subassembly.getSubassembly(fixNode.Code);
		if (null == transform)
		{
			return;
		}
		Vector3 position = fixNode.Position;
		position.y = 0f;
		transform.position = position;
		transform.eulerAngles = fixNode.EulerAngles;
		fixNode.Node.addSub(transform);
		Transform transform2 = transform.Find("Item");
		if (!(null == transform2))
		{
			for (int i = 0; i < transform2.GetChildCount(); i++)
			{
				addFixNode(transform2.GetChild(i).name, fixNode.Node, 0f, transform2.GetChild(i).position, fixNode.Forward, Vector3.zero, fixNode.IsMonster);
			}
		}
	}
}
