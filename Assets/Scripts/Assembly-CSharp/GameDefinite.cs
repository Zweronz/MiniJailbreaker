using UnityEngine;

public class GameDefinite : MonoBehaviour
{
	public Transform SubList;

	public Transform EnemyList;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void Load(int nIndex)
	{
		if (null != SubList)
		{
			LoadSub(nIndex);
		}
		if (null != EnemyList)
		{
			LoadEnemy(nIndex);
		}
	}

	private void LoadSub(int nIndex)
	{
		Transform transform = null;
		Transform transform2 = null;
		for (int i = 0; i < SubList.childCount; i++)
		{
			transform = SubList.GetChild(i);
		}
	}

	private void LoadEnemy(int nIndex)
	{
		Transform transform = null;
		Transform transform2 = null;
		for (int i = 0; i < EnemyList.childCount; i++)
		{
			transform = EnemyList.GetChild(i);
		}
	}
}
