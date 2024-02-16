using UnityEngine;

public class WayPointNode
{
	private Transform m_cmWayPoint;

	private string m_strName;

	private ENUM_AREA_TYPE m_nType;

	private WayPointNode m_cmNextPoint;

	private WayPointNode m_cmPrePoint;

	private ENUM_SHOW_SEARCHLIGHT m_nSearch;

	public Transform WayPoint
	{
		get
		{
			return m_cmWayPoint;
		}
		set
		{
			m_cmWayPoint = value;
		}
	}

	public string Name
	{
		get
		{
			return m_strName;
		}
		set
		{
			m_strName = value;
		}
	}

	public ENUM_AREA_TYPE Area
	{
		get
		{
			return m_nType;
		}
		set
		{
			m_nType = value;
		}
	}

	public WayPointNode NextPoint
	{
		get
		{
			return m_cmNextPoint;
		}
		set
		{
			m_cmNextPoint = value;
		}
	}

	public WayPointNode PrePoint
	{
		get
		{
			return m_cmPrePoint;
		}
		set
		{
			m_cmPrePoint = value;
		}
	}

	public ENUM_SHOW_SEARCHLIGHT SearchLight
	{
		get
		{
			return m_nSearch;
		}
		set
		{
			m_nSearch = value;
		}
	}

	public void del()
	{
		if (m_cmPrePoint != null)
		{
			m_cmPrePoint.NextPoint = m_cmNextPoint.NextPoint;
		}
		if (m_cmNextPoint != null)
		{
			m_cmNextPoint.PrePoint = m_cmPrePoint.PrePoint;
		}
		m_cmWayPoint = null;
		m_strName = null;
		m_cmPrePoint = null;
		m_cmNextPoint = null;
	}
}
