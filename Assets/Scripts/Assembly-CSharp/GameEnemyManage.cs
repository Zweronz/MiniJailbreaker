using UnityEngine;

public class GameEnemyManage : GamePool
{
	private const string c_strInstancePath = "Prefabs/Monster/";

	private const string c_strPoolName = "Monster";

	private static GameEnemyManage m_cmInstance;

	private Transform m_cmPlayer;

	public Transform Player
	{
		set
		{
			m_cmPlayer = value;
		}
	}

	private GameEnemyManage()
	{
	}

	public static GameEnemyManage GetInstance()
	{
		if (m_cmInstance == null)
		{
			m_cmInstance = new GameEnemyManage();
			m_cmInstance.Initialize("Prefabs/Monster/", "Monster");
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

	public Transform getMonster(string strCode)
	{
		if (strCode == null || 0 >= strCode.Length)
		{
			return null;
		}
		MonsterCfg monsterCfg = globalVal.Config.getMonsterCfg(strCode);
		if (monsterCfg == null)
		{
			return null;
		}
		Transform transform = getObjectInstance(monsterCfg.Instance);
		if (null == transform)
		{
			transform = createObjectInstance(monsterCfg.Instance);
		}
		if (null == transform)
		{
			return null;
		}
		GameEnemy gameEnemy = transform.GetComponent(typeof(GameEnemy)) as GameEnemy;
		if (null != gameEnemy)
		{
			gameEnemy.gameObject.SetActiveRecursively(true);
			gameEnemy.setTarget(m_cmPlayer);
			gameEnemy.Init(strCode);
		}
		globalVal.MonsterCount++;
		return transform;
	}

	public void recycleMonster(Transform cmMonster)
	{
		if (!(null == cmMonster))
		{
			GameEnemy gameEnemy = cmMonster.GetComponent(typeof(GameEnemy)) as GameEnemy;
			if (null != gameEnemy)
			{
				gameEnemy.Destroy();
			}
			cmMonster.gameObject.SetActiveRecursively(false);
			recycleObject(cmMonster);
			globalVal.MonsterCount--;
		}
	}
}
