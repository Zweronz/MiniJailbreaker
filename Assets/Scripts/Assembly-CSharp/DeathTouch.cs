using UnityEngine;

public class DeathTouch : MonoBehaviour
{
	private int c_nInterval = 98;

	private float c_fFall = 0.2244898f;

	private Vector3 c_v3Peak = new Vector3(-3f, -35f, -1f);

	private Transform[] m_alDeathList;

	private int m_nIndex;

	private float m_fMoveDistance;

	private Vector3[] m_v3Position;

	private void Start()
	{
		Transform transform = null;
		string text = null;
		TUIMeshSprite tUIMeshSprite = null;
		if (m_alDeathList == null)
		{
			m_alDeathList = new Transform[globalVal.Config.getDeathCount()];
		}
		for (int i = 0; i < m_alDeathList.Length; i++)
		{
			transform = null;
			text = null;
			transform = getPicInstance();
			if (!(null == transform))
			{
				m_alDeathList[i] = transform;
				transform.parent = base.transform;
			}
		}
		m_v3Position = new Vector3[4];
		m_v3Position[0] = new Vector3(0f, -35f, -1f);
		m_v3Position[1] = new Vector3(98f, -57f, -1f);
		m_v3Position[2] = new Vector3(196f, -79f, -1f);
		m_v3Position[3] = new Vector3(294f, -79f, -1f);
	}

	private void Update()
	{
		if (!globalVal.MoveEnd)
		{
			m_fMoveDistance += globalVal.MoveDistance;
			globalVal.MoveDistance = 0f;
		}
		else
		{
			if (-49f >= m_alDeathList[m_nIndex].localPosition.x)
			{
				m_nIndex = getNextIndex(m_nIndex, 1);
			}
			else if (49f < m_fMoveDistance)
			{
				m_nIndex = getNextIndex(m_nIndex, -1);
			}
			m_fMoveDistance = 0f;
		}
		int num = 1;
		if (0f > m_fMoveDistance)
		{
			num = -1;
		}
		m_fMoveDistance = Mathf.Abs(m_fMoveDistance);
		if ((float)c_nInterval < m_fMoveDistance)
		{
			m_nIndex = getNextIndex(m_nIndex, -num);
			m_fMoveDistance -= c_nInterval;
		}
		m_fMoveDistance *= num;
		DrawStatue();
	}

	private int getNextIndex(int nIndex, int nCorrect)
	{
		nIndex += nCorrect;
		if (0 > nIndex)
		{
			nIndex = m_alDeathList.Length + nIndex;
		}
		else if (m_alDeathList.Length <= nIndex)
		{
			nIndex -= m_alDeathList.Length;
		}
		return nIndex;
	}

	private void DrawStatue()
	{
		for (int i = -3; i < 4; i++)
		{
			int nextIndex = getNextIndex(m_nIndex, i);
			m_alDeathList[getNextIndex(m_nIndex, i)].localPosition = getOffset(i);
			bool flag = checkStatus(getNextIndex(m_nIndex, i));
			if (flag)
			{
				Debug.Log("nIndex = " + m_nIndex + " i =" + i);
			}
			setLight(i + 3, flag);
		}
	}

	private Transform getPicInstance()
	{
		Transform transform = base.transform.Find("death");
		if (null == transform)
		{
			return null;
		}
		GameObject gameObject = Object.Instantiate(transform.gameObject) as GameObject;
		if (null == gameObject)
		{
			return null;
		}
		return gameObject.transform;
	}

	private Vector3 getOffset(int nIndex)
	{
		Vector3 result = c_v3Peak;
		int num = -1;
		if (0 > nIndex)
		{
			num = 1;
		}
		if (nIndex == 0 && 0f > m_fMoveDistance)
		{
			num = 1;
		}
		else if (nIndex == 0 && 0f < m_fMoveDistance)
		{
			num = -1;
		}
		float num2 = (float)(nIndex * c_nInterval) + m_fMoveDistance;
		float num3 = num2 * c_fFall * (float)num;
		result.x += num2;
		result.y += num3;
		return result;
	}

	private bool checkStatus(int nIndex)
	{
		bool flag = false;
		Debug.Log("globalVal.Death[nIndex] = " + globalVal.Death[nIndex]);
		if (0 < globalVal.Death[nIndex])
		{
			flag = true;
		}
		string text = globalVal.Config.getDeathPic(nIndex + 1);
		if (text == null || 0 >= text.Length)
		{
			return false;
		}
		Transform transform = m_alDeathList[nIndex];
		if (null == transform)
		{
			return false;
		}
		TUIMeshSprite tUIMeshSprite = transform.GetComponent(typeof(TUIMeshSprite)) as TUIMeshSprite;
		if (null == tUIMeshSprite)
		{
			return false;
		}
		if (!flag)
		{
			text = "un" + text;
		}
		tUIMeshSprite.frameName = text;
		tUIMeshSprite.UpdateMesh();
		return flag;
	}

	private void setLight(int nIndex, bool bShow)
	{
		Debug.Log("nIndex = " + nIndex);
		if (1 <= nIndex && 5 >= nIndex)
		{
			Transform transform = base.transform.Find("light" + nIndex);
			if (!(null == transform))
			{
				transform.gameObject.SetActiveRecursively(bShow);
			}
		}
	}
}
