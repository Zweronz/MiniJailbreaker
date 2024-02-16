using UnityEngine;

public class GameSubassemblyManage : GamePool
{
	private const string c_strInstancePath = "Prefabs/Subassembly/";

	private const string c_strPoolName = "Subassembly";

	private static GameSubassemblyManage m_cmInstance;

	private GameSubassemblyManage()
	{
	}

	public static GameSubassemblyManage GetInstance()
	{
		if (m_cmInstance == null)
		{
			m_cmInstance = new GameSubassemblyManage();
			m_cmInstance.Initialize("Prefabs/Subassembly/", "Subassembly");
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

	public static string randomItemCode(Transform cmSubassembly)
	{
		if (null == cmSubassembly)
		{
			return null;
		}
		for (int i = 0; i < cmSubassembly.GetChildCount(); i++)
		{
			Transform child = cmSubassembly.GetChild(i);
			if ("Item" == child.name)
			{
				int index = Random.Range(1, 100) % child.GetChildCount();
				return child.GetChild(index).name;
			}
		}
		return null;
	}

	public static void fixItem(Transform cmSubassembly)
	{
		RaycastHit hitInfo;
		if (!(null == cmSubassembly) && Physics.Raycast(cmSubassembly.position, Vector3.down, out hitInfo, 10f, 1))
		{
			cmSubassembly.position = hitInfo.point;
		}
	}

	public static void fixItem(Transform cmSubassembly, Vector3 v3Forward)
	{
		if (null == cmSubassembly)
		{
			return;
		}
		cmSubassembly.forward = v3Forward;
		float num = 0f;
		RaycastHit hitInfo;
		if (Physics.Raycast(cmSubassembly.position, Vector3.down, out hitInfo, 10f, 1))
		{
			cmSubassembly.position = hitInfo.point;
			num = Vector3.Angle(hitInfo.normal, Vector3.up);
			if (0f > hitInfo.normal.z)
			{
				num *= -1f;
			}
		}
		Vector3 localEulerAngles = cmSubassembly.localEulerAngles;
		localEulerAngles.x += num;
		cmSubassembly.localEulerAngles = localEulerAngles;
	}

	public static Transform fixItem(string strCode, Vector3 v3Position, Vector3 v3Forward)
	{
		if (strCode == null || 0 >= strCode.Length)
		{
			return null;
		}
		Transform subassembly = m_cmInstance.getSubassembly(strCode);
		subassembly.position = v3Position;
		subassembly.forward = v3Forward;
		float num = 0f;
		RaycastHit hitInfo;
		if (Physics.Raycast(subassembly.position, Vector3.down, out hitInfo, 10f, 1))
		{
			subassembly.position = hitInfo.point;
			num = Vector3.Angle(hitInfo.normal, Vector3.up);
			if (0f > hitInfo.normal.z)
			{
				num *= -1f;
			}
		}
		else
		{
			m_cmInstance.recycleSubassembly(subassembly);
		}
		Vector3 localEulerAngles = subassembly.localEulerAngles;
		localEulerAngles.x += num;
		subassembly.localEulerAngles = localEulerAngles;
		return subassembly;
	}

	public Transform getSubassembly(string strCode)
	{
		Transform transform = getObjectInstance(strCode);
		if (null == transform)
		{
			transform = createObjectInstance(strCode);
		}
		if (null == transform)
		{
			Debug.Log("Not Found = " + strCode);
			return null;
		}
		transform.gameObject.SetActiveRecursively(true);
		Transform transform2 = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			transform2 = transform.GetChild(i);
			if (null == transform2)
			{
				continue;
			}
			for (int j = 0; j < transform2.childCount; j++)
			{
				if (null != transform2.GetChild(j) && null != transform2.GetChild(j).GetComponent<Animation>() && null != transform2.GetChild(j).GetComponent<Animation>()["001"])
				{
					transform2.GetChild(j).GetComponent<Animation>().Play("001");
				}
			}
			break;
		}
		return transform;
	}

	public void recycleSubassembly(Transform cmSubassembly)
	{
		if (!(null == cmSubassembly))
		{
			cmSubassembly.gameObject.SetActiveRecursively(false);
			recycleObject(cmSubassembly);
		}
	}
}
