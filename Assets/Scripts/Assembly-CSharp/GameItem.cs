using System.Collections;
using UnityEngine;

public class GameItem
{
	private static GameItem m_cmInstance;

	private Transform m_cmPlayer;

	private ArrayList m_alItem;

	private float m_fTime;

	private GameItem()
	{
	}

	public static GameItem GetInstance()
	{
		if (m_cmInstance == null)
		{
			m_cmInstance = new GameItem();
			m_cmInstance.Initialize();
		}
		return m_cmInstance;
	}

	public void Initialize()
	{
		m_alItem = new ArrayList(8);
	}

	public void setPlayer(Transform cmPlayer)
	{
		m_cmPlayer = cmPlayer;
	}

	public void UseItem(string strItemName)
	{
		ItemCfg itemCfg = GameInfo.GetInstance().getItemCfg(strItemName);
		if (itemCfg == null)
		{
			Debug.Log("Item config is null = " + strItemName);
			return;
		}
		switch (itemCfg.ItemType)
		{
		case ENUM_ITEM_TYPE.ITEM_GOLD_POWER:
			goldPower();
			break;
		case ENUM_ITEM_TYPE.ITEM_SHOCK_WAVE:
			shockWave();
			break;
		case ENUM_ITEM_TYPE.ITEM_SHIELD:
			shield();
			break;
		case ENUM_ITEM_TYPE.ITEM_RESURRECTION:
			resurrection();
			break;
		case ENUM_ITEM_TYPE.ITEM_START_FROM_KM:
			startFromKM();
			break;
		case ENUM_ITEM_TYPE.ITEM_GHOST:
			ghost();
			break;
		case ENUM_ITEM_TYPE.ITEM_ENERGY:
			energy();
			break;
		case ENUM_ITEM_TYPE.ITEM_MAGNET:
			magent();
			break;
		case ENUM_ITEM_TYPE.ITEM_EXCHANGE:
			exchange();
			break;
		case ENUM_ITEM_TYPE.ITEM_TREASUREBOX:
			treasureBox();
			break;
		}
		PlayEffect(itemCfg.OnceEffect);
		setBuffer(itemCfg);
	}

	public void targetItem(Transform cmItem)
	{
		if (m_alItem == null)
		{
			m_alItem = new ArrayList(8);
		}
		if (m_alItem.IndexOf(cmItem) != -1)
		{
			return;
		}
		ItemCfg itemCfg = GameInfo.GetInstance().getItemCfg(cmItem.name);
		if (itemCfg == null)
		{
			Debug.Log("Item config is null = " + cmItem.name);
			return;
		}
		switch (itemCfg.ItemType)
		{
		case ENUM_ITEM_TYPE.ITEM_GOLD:
			gold(cmItem);
			return;
		case ENUM_ITEM_TYPE.ITEM_TREASUREBOX:
			treasureBox();
			break;
		}
		PlayEffect(itemCfg.OnceEffect);
		m_alItem.Add(cmItem);
	}

	public void Run()
	{
		m_fTime = Time.realtimeSinceStartup;
		if (0 >= m_alItem.Count)
		{
			return;
		}
		Transform transform = (Transform)m_alItem[0];
		if (null == transform)
		{
			return;
		}
		m_alItem.RemoveAt(0);
		ENUM_ITEM_TYPE itemType = GameInfo.GetInstance().getItemType(transform.name);
		if (itemType == ENUM_ITEM_TYPE.ITEM_NONE)
		{
			Debug.Log("Item Type is none = " + transform.name);
			return;
		}
		ItemCfg itemCfg = GameInfo.GetInstance().getItemCfg(transform.name);
		if (itemCfg == null)
		{
			Debug.Log("Item config is null = " + transform.name);
			return;
		}
		setBuffer(itemCfg);
		switch (itemType)
		{
		case ENUM_ITEM_TYPE.ITEM_GOLD:
			break;
		case ENUM_ITEM_TYPE.ITEM_GOLD_POWER:
			goldPower();
			break;
		case ENUM_ITEM_TYPE.ITEM_SHOCK_WAVE:
			shockWave();
			break;
		case ENUM_ITEM_TYPE.ITEM_SHIELD:
			shield();
			break;
		case ENUM_ITEM_TYPE.ITEM_RESURRECTION:
			resurrection();
			break;
		case ENUM_ITEM_TYPE.ITEM_START_FROM_KM:
			startFromKM();
			break;
		case ENUM_ITEM_TYPE.ITEM_GHOST:
			ghost();
			break;
		case ENUM_ITEM_TYPE.ITEM_ENERGY:
			energy();
			break;
		case ENUM_ITEM_TYPE.ITEM_MAGNET:
			magent();
			break;
		case ENUM_ITEM_TYPE.ITEM_EXCHANGE:
			exchange();
			break;
		case ENUM_ITEM_TYPE.ITEM_TREASUREBOX:
			break;
		}
	}

	private void setBuffer(ItemCfg cmItemCfg)
	{
		for (int i = 0; i < cmItemCfg.BufferCount; i++)
		{
			GameBuffer.GetInstance().StartBuffer(cmItemCfg.getBufferType(i), cmItemCfg.Time, cmItemCfg.getBufferStength(i), cmItemCfg.getBufferEffect(i), cmItemCfg.getBufferStartAudio(i), cmItemCfg.getBufferEndAudio(i));
		}
	}

	private void gold(Transform cmItem)
	{
		if (!(null == cmItem))
		{
			GameGoldCtrl component = cmItem.parent.GetComponent<GameGoldCtrl>();
			component.setTarget(m_cmPlayer);
		}
	}

	private void goldPower()
	{
	}

	private void shockWave()
	{
	}

	private void shield()
	{
	}

	private void resurrection()
	{
		globalVal.GameStatus = GAME_STATUS.GAME_RESURRECTION;
	}

	private void startFromKM()
	{
		globalVal.GameStatus = GAME_STATUS.GAME_WORMHOLE;
	}

	private void ghost()
	{
	}

	private void energy()
	{
	}

	private void magent()
	{
	}

	private void exchange()
	{
	}

	private void treasureBox()
	{
		string treasureBoxItem = globalVal.Config.getTreasureBoxItem();
		switch (treasureBoxItem)
		{
		case "GOLD1":
			globalVal.GameScore += 10;
			break;
		case "GOLD2":
			globalVal.GameScore += 30;
			break;
		case "GOLD3":
			globalVal.GameScore += 100;
			break;
		default:
			UseItem(treasureBoxItem);
			break;
		}
		string treasureBoxEffect = globalVal.Config.getTreasureBoxEffect(treasureBoxItem);
		PlayEffect(treasureBoxEffect);
	}

	private void PlayEffect(string strEffect)
	{
		Transform effect = globalVal.Effect.getEffect(strEffect);
		if (null == effect)
		{
			return;
		}
		ParticleSystem particleSystem = null;
		for (int i = 0; i < effect.childCount; i++)
		{
			particleSystem = effect.GetChild(i).GetComponent<ParticleSystem>();
			if (null != particleSystem)
			{
				particleSystem.Clear();
				particleSystem.Play();
			}
		}
		effect.position = m_cmPlayer.position;
		effect.forward = m_cmPlayer.forward;
		globalVal.Effect.setAlarm(effect, 0.2f);
	}
}
