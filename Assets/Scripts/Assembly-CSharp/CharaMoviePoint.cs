using System.Collections;
using UnityEngine;

[ExecuteInEditMode]
public class CharaMoviePoint : MonoBehaviour
{
	public CharaAction[] ActionList;

	private ArrayList m_cmPointList;

	public Transform EndPoint;

	private WayPointNode m_cmEndPoint;

	public Transform MyTransform
	{
		get
		{
			return base.transform;
		}
	}

	public ArrayList MyWayPoints
	{
		get
		{
			if (m_cmPointList == null)
			{
				m_cmPointList = new ArrayList();
			}
			return m_cmPointList;
		}
	}

	private void Awake()
	{
	}

	private void Start()
	{
	}

	private void Update()
	{
		if (m_cmEndPoint != null)
		{
		}
	}

	public CharaAction getNextPoint(int nIndex)
	{
		if (nIndex >= ActionList.Length)
		{
			return null;
		}
		return ActionList[nIndex];
	}

	public void OnDestroy()
	{
	}
}
