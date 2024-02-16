using System;
using System.Collections;
using System.Xml;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
	private GameObject baseObj;

	public ArrayList dataArray = new ArrayList();

	public ArrayList goldArray = new ArrayList();

	public ArrayList goldGroundArray = new ArrayList();

	public ArrayList attributeArray = new ArrayList();

	public ArrayList itemOnceArray = new ArrayList();

	public ArrayList avatarArray = new ArrayList();

	private GameObject items;

	public ArrayList ItemContainer = new ArrayList();

	private GameObject player;

	private float baseX;

	private float goldBaseX;

	private float offsetX = 40f;

	private float goldOffsetX = 30f;

	private float playerFlyDis;

	public int RefreshTime;

	private TAudioController audios;

	public Hashtable hashTask = new Hashtable();

	public ArrayList taskListArray = new ArrayList();

	private Vector3 lastCameraPoint = Vector3.zero;

	private float startTime;

	private float endTime;

	private bool slowKey;

	private float groundY;

	private RaycastHit ray_hit;

	private bool itemGoldRefreshKey;

	private int itemRefreshCount;

	private Hashtable hashItemTime = new Hashtable();

	private float m_time;

	private void Start()
	{
		ItemManagerClass.body = this;
		Debug.Log("ItemManagerClass.body = " + ItemManagerClass.body);
		hashTask.Clear();
		hashItemTime.Clear();
		GameObject gameObject = GameObject.Find("TAudioController");
		if (gameObject == null)
		{
			gameObject = new GameObject("TAudioController");
			gameObject.AddComponent(typeof(TAudioController));
			UnityEngine.Object.DontDestroyOnLoad(gameObject);
		}
		audios = gameObject.GetComponent(typeof(TAudioController)) as TAudioController;
		ReadAvatarList();
		ReadTaskInfo();
		ReadTaskList();
		ReadItemOnceList();
		ReadItemAttr_itemAttr();
	}

	public void InitStar()
	{
		AddItem("item_recruit1");
	}

	public void ResetItems()
	{
		foreach (Transform item in items.transform)
		{
			int itemIndexID = ItemManagerClass.body.GetItemIndexID(item.gameObject.name);
			if (itemIndexID > ItemManagerClass.body.ItemContainer.Count - 1)
			{
				continue;
			}
			ArrayList arrayList = ItemManagerClass.body.ItemContainer[itemIndexID] as ArrayList;
			if (!item.gameObject.active)
			{
				continue;
			}
			item.gameObject.active = false;
			foreach (Transform item2 in item)
			{
				item2.gameObject.active = false;
			}
			arrayList.Add(item.gameObject);
		}
	}

	public GameObject GetItem(string prefabName)
	{
		GameObject result = null;
		int itemIndexID = GetItemIndexID(prefabName);
		ArrayList arrayList = ItemContainer[itemIndexID] as ArrayList;
		if (arrayList.Count < 1)
		{
			return result;
		}
		result = arrayList[0] as GameObject;
		arrayList.RemoveAt(0);
		return result;
	}

	private void AddItemRandom()
	{
		string prefabName = string.Empty;
		int num = -1;
		if (player.transform.position.y < 10f + groundY)
		{
			num = UnityEngine.Random.Range(0, 1000) % 7;
		}
		else if (player.transform.position.y > 35f + groundY && player.transform.position.y < 75f + groundY)
		{
			num = 7;
		}
		else if (player.transform.position.y > 80f + groundY && player.transform.position.y < 100f + groundY)
		{
			num = 10;
		}
		else
		{
			if (!(player.transform.position.y > 105f + groundY) || !(player.transform.position.y < 150f + groundY))
			{
				return;
			}
			num = 12;
		}
		if (num == 5 && player.transform.position.x < 2000f)
		{
			num = 1;
		}
		switch (num)
		{
		case 0:
			prefabName = "item_bomb1";
			audios.PlayAudio("SVOzombiez");
			break;
		case 1:
			prefabName = "item_zombies1";
			break;
		case 2:
			prefabName = "item_bed";
			break;
		case 3:
			prefabName = "item_wagon";
			break;
		case 4:
			prefabName = "item_trampoline";
			break;
		case 5:
			prefabName = "Zombie_Batcher";
			break;
		case 6:
			prefabName = "item_landmine";
			break;
		case 7:
			prefabName = "item_bomb2";
			break;
		case 8:
			prefabName = "item_fallingstar1";
			break;
		case 9:
			prefabName = "item_gold";
			break;
		case 10:
			prefabName = "item_fireballoon";
			break;
		case 11:
			prefabName = "item_ufo";
			break;
		case 12:
			prefabName = "item_airliner";
			break;
		}
		switch (num)
		{
		case 11:
		case 12:
			if (UnityEngine.Random.Range(0, 100) < 30)
			{
				AddItem(prefabName);
			}
			break;
		case 1:
			AddZombieGround(prefabName);
			break;
		case 5:
			AddZombie_Batcher();
			break;
		default:
			AddItem(prefabName);
			break;
		}
	}

	private void AddZombieGround(string prefabName)
	{
		Vector3 position = player.transform.position;
		position.y = 0f + groundY;
		position.z = 0f;
		position.x += 20f;
		int num = 0;
		num = ((!(player.transform.position.x > 1500f)) ? UnityEngine.Random.Range(1, 2) : UnityEngine.Random.Range(3, 5));
		Vector3 pos = position;
		for (int i = 0; i < num; i++)
		{
			float num2 = UnityEngine.Random.Range(2f, 5f);
			pos += new Vector3(num2, 0f, 0f);
			AddOneZombie(prefabName, pos);
			baseX += num2;
		}
	}

	private void AddZombie_Batcher()
	{
		Vector3 position = player.transform.position;
		Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
		float num = (velocity.y + Mathf.Sqrt(velocity.y * velocity.y + 19.6f * (position.y - groundY))) / 9.8f;
		Vector3 zero = Vector3.zero;
		zero.x = position.x + velocity.x * num;
		zero.y = groundY;
		zero.z = position.z;
		string prefabName = "Zombie_Batcher";
		AddOneZombie(prefabName, zero);
	}

	private void AddOneZombie(string prefabName, Vector3 pos)
	{
		GameObject item = GetItem(prefabName);
		if (prefabName != "Zombie_Batcher")
		{
			item.GetComponent<Collider>().enabled = true;
		}
		item.name = prefabName;
		item.active = true;
		foreach (Transform item2 in item.transform)
		{
			item2.gameObject.active = true;
		}
		int num = UnityEngine.Random.Range(-1, 1);
		if (num == 0)
		{
			num = 1;
		}
		if (prefabName != "Zombie_Batcher")
		{
			item.GetComponent<Rigidbody>().velocity = new Vector3(5 * num, 0f, 0f);
			Vector3 eulerAngles = item.transform.eulerAngles;
			eulerAngles.y = 180 + 90 * num * -1;
			item.transform.eulerAngles = eulerAngles;
		}
		item.transform.position = pos;
		audios.PlayAudio("Ani_zombie_run");
	}

	private void AddGoldGround()
	{
		Vector3 position = player.transform.position;
		position.z = 0f;
		int num = UnityEngine.Random.Range(3, 5);
		float num2 = 15f;
		Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
		float num3 = Vector3.Angle(Vector3.up, velocity);
		num3 += UnityEngine.Random.Range(-10f, 10f);
		Vector3 zero = Vector3.zero;
		zero.x = Mathf.Sin(num3 * ((float)Math.PI / 180f)) * num2;
		zero.y = Mathf.Cos(num3 * ((float)Math.PI / 180f)) * num2;
		Vector3 vector = position + zero;
		if (!(vector.y <= 10f + groundY))
		{
			for (int i = 0; i < num; i++)
			{
				Vector3 pos = vector + new Vector3(2 * i, 0f, 0f);
				AddOneGold("item_gold", pos);
			}
		}
	}

	private void InitContainer()
	{
		ItemContainer.Clear();
		int num = 10;
		InitItemData("item_gold", num * 9);
		InitItemData("item_fallingstar1", num);
		InitItemData("item_bomb1", num);
		InitItemData("item_bomb2", num);
		InitItemData("item_zombies1", num);
		InitItemData("item_recruit1", num);
		InitItemData("item_gold2", num * 3);
		InitItemData("item_gold3", num * 3);
		InitItemData("item_bed", num);
		InitItemData("item_wagon", num);
		InitItemData("item_trampoline", num);
		InitItemData("Zombie_Batcher", num);
		InitItemData("item_fireballoon", num);
		InitItemData("item_bird1", num);
		InitItemData("item_airliner", num);
		InitItemData("item_ufo", num);
		InitItemData("item_landmine", num);
	}

	public int GetItemIndexID(string prefabName)
	{
		int result = -1;
		switch (prefabName)
		{
		case "item_gold":
			result = 0;
			break;
		case "item_fallingstar1":
			result = 1;
			break;
		case "item_bomb1":
			result = 2;
			break;
		case "item_bomb2":
			result = 3;
			break;
		case "item_zombies1":
			result = 4;
			break;
		case "item_recruit1":
			result = 5;
			break;
		case "item_gold2":
			result = 6;
			break;
		case "item_gold3":
			result = 7;
			break;
		case "item_bed":
			result = 8;
			break;
		case "item_wagon":
			result = 9;
			break;
		case "item_trampoline":
			result = 10;
			break;
		case "Zombie_Batcher":
			result = 11;
			break;
		case "item_fireballoon":
			result = 12;
			break;
		case "item_bird1":
			result = 13;
			break;
		case "item_airliner":
			result = 14;
			break;
		case "item_ufo":
			result = 15;
			break;
		case "item_landmine":
			result = 16;
			break;
		}
		return result;
	}

	public void SetBaseX(float posx)
	{
		baseX = posx + offsetX;
		if (posx == 0f)
		{
			goldBaseX = posx + offsetX;
		}
		playerFlyDis = 0f;
	}

	private void InitItemData(string prefabName, int count)
	{
		baseObj = Resources.Load("Prefab/" + prefabName) as GameObject;
		if (!baseObj)
		{
			return;
		}
		ArrayList arrayList = new ArrayList();
		for (int i = 0; i < count; i++)
		{
			GameObject gameObject = UnityEngine.Object.Instantiate(baseObj) as GameObject;
			gameObject.name = baseObj.name;
			ItemType itemStateByName = GetItemStateByName(gameObject.name);
			gameObject.layer = LayerMask.NameToLayer("items");
			gameObject.transform.parent = items.transform;
			gameObject.transform.position = new Vector3(-10000f, 0f, 0f);
			gameObject.active = false;
			foreach (Transform item in gameObject.transform)
			{
				item.gameObject.active = false;
			}
			arrayList.Add(gameObject);
		}
		ItemContainer.Add(arrayList);
	}

	private void AddOneGold(string prefabName, Vector3 pos)
	{
		GameObject item = GetItem(prefabName);
		item.GetComponent<Collider>().enabled = true;
		item.name = prefabName;
		item.active = true;
		item.transform.position = pos;
		SetItemAttr(item);
	}

	private void AddOneGold(string prefabName)
	{
		Vector3 position = player.transform.position;
		position.z = 0f;
		int num = UnityEngine.Random.Range(0, 3);
		float num2 = 15f;
		Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
		float num3 = Vector3.Angle(Vector3.up, velocity);
		num3 += UnityEngine.Random.Range(-10f, 10f);
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < num; i++)
		{
			switch (i)
			{
			case 1:
				num3 += 30f;
				break;
			case 2:
				num3 -= 30f;
				break;
			}
			zero.x = Mathf.Sin(num3 * ((float)Math.PI / 180f)) * num2;
			zero.y = Mathf.Cos(num3 * ((float)Math.PI / 180f)) * num2;
			Vector3 position2 = position + zero;
			if (!(position2.y <= 10f + groundY))
			{
				GameObject item = GetItem(prefabName);
				item.GetComponent<Collider>().enabled = true;
				item.name = prefabName;
				item.active = true;
				item.transform.position = position2;
				SetItemAttr(item);
			}
		}
	}

	private void AddOneGold_group()
	{
		Vector3 position = player.transform.position;
		position.z = 0f;
		int num = UnityEngine.Random.Range(0, 3);
		float num2 = 15f;
		Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
		float num3 = Vector3.Angle(Vector3.up, velocity);
		num3 += UnityEngine.Random.Range(-10f, 10f);
		Vector3 zero = Vector3.zero;
		for (int i = 0; i < num; i++)
		{
			switch (i)
			{
			case 1:
				num3 += 30f;
				break;
			case 2:
				num3 -= 30f;
				break;
			}
			zero.x = Mathf.Sin(num3 * ((float)Math.PI / 180f)) * num2;
			zero.y = Mathf.Cos(num3 * ((float)Math.PI / 180f)) * num2;
			Vector3 offset = position + zero;
			if (!(offset.y <= 10f + groundY))
			{
				int index = UnityEngine.Random.Range(0, goldArray.Count - 1);
				ArrayList groundArray = goldArray[index] as ArrayList;
				GameObject gameObject = new GameObject();
				ShowGoldGround showGoldGround = gameObject.AddComponent(typeof(ShowGoldGround)) as ShowGoldGround;
				showGoldGround.StartShow(groundArray, offset);
			}
		}
	}

	private float GetRandTime(string prefabName)
	{
		float result = 0f;
		switch (prefabName)
		{
		case "item_airliner":
			result = UnityEngine.Random.Range(4f, 6f);
			break;
		case "item_ufo":
			result = UnityEngine.Random.Range(15f, 20f);
			break;
		case "item_bomb2":
			result = UnityEngine.Random.Range(2f, 4f);
			break;
		case "item_fireballoon":
			result = UnityEngine.Random.Range(3f, 5f);
			break;
		case "item_gold3":
			result = UnityEngine.Random.Range(2f, 4f);
			break;
		}
		return result;
	}

	public void AddItem(string prefabName)
	{
		MonoBehaviour.print("add item " + prefabName);
		float num = 0f;
		if (hashItemTime.Contains(prefabName))
		{
			num = (float)hashItemTime[prefabName];
			if (!(m_time > num))
			{
				return;
			}
			hashItemTime[prefabName] = m_time + GetRandTime(prefabName);
		}
		else
		{
			hashItemTime.Add(prefabName, Time.realtimeSinceStartup);
		}
		Vector3 position = Camera.main.transform.position;
		position.z = 0f;
		position.y += UnityEngine.Random.Range(-5, 5);
		float num2 = 15f;
		Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
		float num3 = Vector3.Angle(Vector3.up, velocity);
		Vector3 zero = Vector3.zero;
		zero.x = Mathf.Sin(num3 * ((float)Math.PI / 180f)) * num2;
		zero.y = Mathf.Cos(num3 * ((float)Math.PI / 180f)) * num2;
		Vector3 zero2 = Vector3.zero;
		ItemType itemStateByName = GetItemStateByName(prefabName);
		switch (itemStateByName)
		{
		case ItemType.AIRLINER:
			position.x -= 30f;
			zero2 = position + zero;
			audios.PlayAudio("FXairplane");
			break;
		case ItemType.UFO:
			position.x -= 30f;
			zero2 = position + zero;
			audios.PlayAudio("FXufo");
			break;
		case ItemType.DAODAN:
			position.x -= 30f;
			zero2 = position + zero;
			if (zero2.y < 35f)
			{
				return;
			}
			break;
		case ItemType.FIREBALLOON:
			position.x += 20f;
			zero2 = position + zero;
			break;
		case ItemType.ZOMBIES1:
			position.y = 0f + groundY;
			position.x += 20f;
			audios.PlayAudio("Ani_zombie_run");
			zero2 = position;
			break;
		case ItemType.ZHADAN:
			position.y = 0.4753061f + groundY;
			position.x += 20f;
			zero2 = position;
			break;
		case ItemType.LANDMINE:
			position.y = 0.3106229f + groundY;
			position.x += 20f;
			zero2 = position;
			break;
		case ItemType.BED:
			position.y = 1.177445f + groundY;
			position.x += 20f;
			zero2 = position;
			break;
		case ItemType.WAGON:
			position.y = 1.233272f + groundY;
			position.x += 20f;
			zero2 = position;
			break;
		case ItemType.TRAMPOLINE:
			position.y = 0.50165f + groundY;
			position.x += 20f;
			zero2 = position;
			break;
		case ItemType.CLOACA:
			position.y = 0f + groundY;
			position.x += 20f;
			zero2 = position;
			break;
		case ItemType.STAR:
			zero2 = position + zero;
			break;
		case ItemType.GUANGHUAN:
			position.x = 13.92987f;
			position.y = 23.16217f;
			zero2 = position;
			break;
		default:
			zero2 = position + zero;
			break;
		}
		GameObject item = GetItem(prefabName);
		if (prefabName != "Zombie_Batcher")
		{
			item.GetComponent<Collider>().enabled = true;
		}
		item.name = prefabName;
		item.active = true;
		foreach (Transform item2 in item.transform)
		{
			item2.gameObject.active = true;
		}
		item.transform.position = zero2;
		switch (itemStateByName)
		{
		case ItemType.ZOMBIES1:
		{
			int num4 = UnityEngine.Random.Range(-1, 1);
			if (num4 == 0)
			{
				num4 = 1;
			}
			item.GetComponent<Rigidbody>().velocity = new Vector3(5 * num4, 0f, 0f);
			Vector3 eulerAngles = item.transform.eulerAngles;
			eulerAngles.y = 180 + 90 * num4 * -1;
			item.transform.eulerAngles = eulerAngles;
			break;
		}
		case ItemType.BED:
		{
			float y2 = UnityEngine.Random.Range(150, 220);
			item.transform.eulerAngles = new Vector3(270f, y2, 0f);
			break;
		}
		case ItemType.WAGON:
		{
			float y = UnityEngine.Random.Range(-50, 40);
			item.transform.eulerAngles = new Vector3(270f, y, 0f);
			break;
		}
		case ItemType.STAR:
			foreach (Transform item3 in item.transform)
			{
				item3.gameObject.active = true;
			}
			break;
		}
		SetItemAttr(item);
	}

	public ItemAttribute GetAttributeByName(string prefabName)
	{
		for (int i = 0; i < attributeArray.Count; i++)
		{
			if ((attributeArray[i] as ItemAttribute).type == prefabName)
			{
				return attributeArray[i] as ItemAttribute;
			}
		}
		return null;
	}

	public int GetItemLevelByName(string prefabName)
	{
		return 0;
	}

	private ItemType GetItemStateByName(string prefabName)
	{
		ItemType result = ItemType.NONE;
		switch (prefabName)
		{
		case "item_recruit1":
			result = ItemType.GUANGHUAN;
			break;
		case "item_bomb1":
			result = ItemType.ZHADAN;
			break;
		case "item_bomb2":
			result = ItemType.DAODAN;
			break;
		case "item_zombies1":
			result = ItemType.ZOMBIES1;
			break;
		case "item_fallingstar1":
			result = ItemType.STAR;
			break;
		case "item_gold":
			result = ItemType.GOLD;
			break;
		case "item_gold2":
			result = ItemType.GOLD2;
			break;
		case "item_gold3":
			result = ItemType.GOLD3;
			break;
		case "item_cloud":
			result = ItemType.CLOUD;
			break;
		case "item_bed":
			result = ItemType.BED;
			break;
		case "item_wagon":
			result = ItemType.WAGON;
			break;
		case "item_trampoline":
			result = ItemType.TRAMPOLINE;
			break;
		case "Zombie_Batcher":
			result = ItemType.CLOACA;
			break;
		case "item_fireballoon":
			result = ItemType.FIREBALLOON;
			break;
		case "item_bird1":
			result = ItemType.BIRD1;
			break;
		case "item_airliner":
			result = ItemType.AIRLINER;
			break;
		case "item_ufo":
			result = ItemType.UFO;
			break;
		case "item_landmine":
			result = ItemType.LANDMINE;
			break;
		}
		return result;
	}

	public void SlowGame()
	{
		startTime = 0f;
		endTime = 0.3f;
		slowKey = true;
		Time.timeScale = 0.3f;
	}

	private void SetItemAttr(GameObject items)
	{
	}

	private void ReadTaskInfo()
	{
		XmlDocument xmlDocument = new XmlDocument();
		TextAsset textAsset = Resources.Load("taskInfo") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		hashTask.Clear();
		for (int i = 0; i < childNodes.Count; i++)
		{
			taskInfo taskInfo2 = new taskInfo();
			XmlElement xmlElement = (XmlElement)childNodes.Item(i);
			taskInfo2.type = (ENUM_TASK_TYPE)int.Parse(xmlElement.GetAttribute("type"));
			taskInfo2.id = xmlElement.GetAttribute("id");
			taskInfo2.info = xmlElement.GetAttribute("info");
			taskInfo2.info = taskInfo2.info.Replace("\\n", "\n");
			taskInfo2.value1 = float.Parse(xmlElement.GetAttribute("value1"));
			taskInfo2.value2 = float.Parse(xmlElement.GetAttribute("value2"));
			taskInfo2.golds = int.Parse(xmlElement.GetAttribute("golds"));
			taskInfo2.score = int.Parse(xmlElement.GetAttribute("score"));
			hashTask.Add(taskInfo2.id, taskInfo2);
		}
	}

	private void ReadTaskList()
	{
		XmlDocument xmlDocument = new XmlDocument();
		TextAsset textAsset = Resources.Load("taskList") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		taskListArray.Clear();
		for (int i = 0; i < childNodes.Count; i++)
		{
			XmlNode xmlNode2 = childNodes.Item(i);
			XmlNodeList childNodes2 = xmlNode2.ChildNodes;
			ArrayList arrayList = new ArrayList();
			for (int j = 0; j < childNodes2.Count; j++)
			{
				arrayList.Add(childNodes2.Item(j).InnerText);
			}
			taskListArray.Add(arrayList);
		}
	}

	private void ReadAvatarList()
	{
		XmlDocument xmlDocument = new XmlDocument();
		TextAsset textAsset = Resources.Load("avatarList") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		avatarArray.Clear();
		globalVal.g_avatar_isbuy.Clear();
		for (int i = 0; i < childNodes.Count; i++)
		{
			XmlElement xmlElement = childNodes.Item(i) as XmlElement;
			AvatarAttribute avatarAttribute = new AvatarAttribute();
			avatarAttribute.name = xmlElement.GetAttribute("name");
			avatarAttribute.picname = xmlElement.GetAttribute("picname");
			avatarAttribute.info = xmlElement.GetAttribute("info");
			avatarAttribute.info = avatarAttribute.info.Replace("\\n", "\n");
			avatarAttribute.price = int.Parse(xmlElement.GetAttribute("price"));
			avatarAttribute.modelname = xmlElement.GetAttribute("modelname");
			if (i == 0)
			{
				avatarAttribute.isbuy = 1;
			}
			else
			{
				avatarAttribute.isbuy = 0;
			}
			globalVal.g_avatar_isbuy.Add(avatarAttribute.isbuy);
			avatarArray.Add(avatarAttribute);
		}
	}

	private void ReadItemOnceList()
	{
		XmlDocument xmlDocument = new XmlDocument();
		TextAsset textAsset = Resources.Load("itemAttr_Once") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		itemOnceArray.Clear();
		globalVal.g_item_once_count.Clear();
		for (int i = 0; i < childNodes.Count; i++)
		{
			XmlElement xmlElement = childNodes.Item(i) as XmlElement;
			ItemOnceAttribute itemOnceAttribute = new ItemOnceAttribute();
			itemOnceAttribute.name = xmlElement.GetAttribute("name");
			itemOnceAttribute.picname = xmlElement.GetAttribute("picname");
			itemOnceAttribute.info = xmlElement.GetAttribute("info");
			itemOnceAttribute.info = itemOnceAttribute.info.Replace("\\n", "\n");
			itemOnceAttribute.price = int.Parse(xmlElement.GetAttribute("price"));
			itemOnceAttribute.value = float.Parse(xmlElement.GetAttribute("value"));
			globalVal.g_item_once_count.Add(0);
			itemOnceArray.Add(itemOnceAttribute);
		}
	}

	private void ReadItemAttr_itemAttr()
	{
		XmlDocument xmlDocument = new XmlDocument();
		attributeArray.Clear();
		TextAsset textAsset = Resources.Load("itemAttr") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		globalVal.g_itemlevel.Clear();
		for (int i = 0; i < childNodes.Count; i++)
		{
			ItemAttribute itemAttribute = new ItemAttribute();
			XmlElement xmlElement = (XmlElement)childNodes.Item(i);
			itemAttribute.type = xmlElement.GetAttribute("type");
			if (xmlElement.GetAttribute("inshop") == "1")
			{
				itemAttribute.inshop = true;
			}
			itemAttribute.index = i;
			itemAttribute.colliderCount = 0;
			globalVal.g_itemlevel.Add(0);
			itemAttribute.level.Clear();
			XmlNodeList childNodes2 = xmlElement.ChildNodes;
			for (int j = 0; j < childNodes2.Count; j++)
			{
				ItemSubAttr itemSubAttr = new ItemSubAttr();
				XmlElement xmlElement2 = (XmlElement)childNodes2.Item(j);
				if (xmlElement2.GetAttribute("level") != string.Empty)
				{
					itemSubAttr.level = int.Parse(xmlElement2.GetAttribute("level"));
				}
				itemSubAttr.name = xmlElement2.GetAttribute("name");
				itemSubAttr.picname = xmlElement2.GetAttribute("picname");
				if (xmlElement2.GetAttribute("price") != string.Empty)
				{
					itemSubAttr.price = int.Parse(xmlElement2.GetAttribute("price"));
				}
				if (xmlElement2.GetAttribute("dir") != string.Empty)
				{
					itemSubAttr.dir = float.Parse(xmlElement2.GetAttribute("dir"));
				}
				if (xmlElement2.GetAttribute("strength") != string.Empty)
				{
					itemSubAttr.strength = float.Parse(xmlElement2.GetAttribute("strength"));
				}
				if (xmlElement2.GetAttribute("length") != string.Empty)
				{
					itemSubAttr.length = float.Parse(xmlElement2.GetAttribute("length"));
				}
				if (xmlElement2.GetAttribute("value") != string.Empty)
				{
					itemSubAttr.value = int.Parse(xmlElement2.GetAttribute("value"));
				}
				itemSubAttr.info = xmlElement2.GetAttribute("info");
				itemSubAttr.info = itemSubAttr.info.Replace("\\n", "\n");
				itemAttribute.level.Add(itemSubAttr);
			}
			attributeArray.Add(itemAttribute);
		}
		globalVal.ReadFile("saveData.txt");
	}

	private void ReadData()
	{
		XmlDocument xmlDocument = new XmlDocument();
		dataArray.Clear();
		TextAsset textAsset = Resources.Load("data") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		XmlNode xmlNode = xmlDocument.SelectSingleNode("root");
		XmlNodeList childNodes = xmlNode.ChildNodes;
		for (int i = 0; i < childNodes.Count; i++)
		{
			ArrayList arrayList = new ArrayList();
			XmlNode xmlNode2 = childNodes.Item(i);
			XmlNodeList childNodes2 = xmlNode2.ChildNodes;
			for (int j = 0; j < childNodes2.Count; j++)
			{
				XmlElement xmlElement = (XmlElement)childNodes2.Item(j);
				string attribute = xmlElement.GetAttribute("type");
				Vector3 zero = Vector3.zero;
				zero.x = float.Parse(xmlElement.GetAttribute("x")) * 1f;
				zero.y = float.Parse(xmlElement.GetAttribute("y")) * 1f;
				zero.z = float.Parse(xmlElement.GetAttribute("z")) * 1f;
				PointInfo pointInfo = new PointInfo();
				pointInfo.name = attribute;
				pointInfo.pos = zero;
				arrayList.Add(pointInfo);
			}
			dataArray.Add(arrayList);
		}
		MonoBehaviour.print(dataArray.Count);
		goldArray.Clear();
		textAsset = Resources.Load("golddata") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		xmlNode = xmlDocument.SelectSingleNode("root");
		childNodes = xmlNode.ChildNodes;
		for (int k = 0; k < childNodes.Count; k++)
		{
			ArrayList arrayList2 = new ArrayList();
			XmlNode xmlNode3 = childNodes.Item(k);
			XmlNodeList childNodes3 = xmlNode3.ChildNodes;
			for (int l = 0; l < childNodes3.Count; l++)
			{
				XmlElement xmlElement2 = (XmlElement)childNodes3.Item(l);
				string attribute2 = xmlElement2.GetAttribute("type");
				Vector3 zero2 = Vector3.zero;
				zero2.x = float.Parse(xmlElement2.GetAttribute("x")) * 1f;
				zero2.y = float.Parse(xmlElement2.GetAttribute("y")) * 1f;
				zero2.z = float.Parse(xmlElement2.GetAttribute("z")) * 1f;
				PointInfo pointInfo2 = new PointInfo();
				pointInfo2.name = attribute2;
				pointInfo2.pos = zero2;
				arrayList2.Add(pointInfo2);
			}
			goldArray.Add(arrayList2);
		}
		MonoBehaviour.print(goldArray.Count);
		goldGroundArray.Clear();
		textAsset = Resources.Load("goldground") as TextAsset;
		xmlDocument.LoadXml(textAsset.text);
		xmlNode = xmlDocument.SelectSingleNode("root");
		childNodes = xmlNode.ChildNodes;
		for (int m = 0; m < childNodes.Count; m++)
		{
			ArrayList arrayList3 = new ArrayList();
			XmlNode xmlNode4 = childNodes.Item(m);
			XmlNodeList childNodes4 = xmlNode4.ChildNodes;
			for (int n = 0; n < childNodes4.Count; n++)
			{
				XmlElement xmlElement3 = (XmlElement)childNodes4.Item(n);
				string attribute3 = xmlElement3.GetAttribute("type");
				Vector3 zero3 = Vector3.zero;
				zero3.x = float.Parse(xmlElement3.GetAttribute("x")) * 1f;
				zero3.y = float.Parse(xmlElement3.GetAttribute("y")) * 1f;
				zero3.z = float.Parse(xmlElement3.GetAttribute("z")) * 1f;
				PointInfo pointInfo3 = new PointInfo();
				pointInfo3.name = attribute3;
				pointInfo3.pos = zero3;
				arrayList3.Add(pointInfo3);
			}
			goldGroundArray.Add(arrayList3);
		}
		MonoBehaviour.print(goldGroundArray.Count);
	}

	private void ShowData_new(int index)
	{
	}

	private void ShowData_gold(int index)
	{
		ArrayList groundArray = goldArray[index] as ArrayList;
		Vector3 position = player.transform.position;
		position.z = 0f;
		float num = 10f;
		Vector3 velocity = player.GetComponent<Rigidbody>().velocity;
		float num2 = Vector3.Angle(Vector3.up, velocity);
		num2 += UnityEngine.Random.Range(-10f, 10f);
		Vector3 zero = Vector3.zero;
		zero.x = Mathf.Sin(num2 * ((float)Math.PI / 180f)) * num;
		zero.y = Mathf.Cos(num2 * ((float)Math.PI / 180f)) * num;
		Vector3 offset = position + zero;
		GameObject gameObject = new GameObject();
		ShowGoldGround showGoldGround = gameObject.AddComponent(typeof(ShowGoldGround)) as ShowGoldGround;
		showGoldGround.StartShow(groundArray, offset);
	}

	private void ShowData_gold(int index, Vector3 pos)
	{
		ArrayList groundArray = goldArray[index] as ArrayList;
		GameObject gameObject = new GameObject();
		ShowGoldGround showGoldGround = gameObject.AddComponent(typeof(ShowGoldGround)) as ShowGoldGround;
		showGoldGround.StartShow(groundArray, pos);
	}

	private void ShowData_groundgold(int index)
	{
		ArrayList groundArray = goldGroundArray[index] as ArrayList;
		Vector3 position = player.transform.position;
		position.y = 0f + groundY;
		position.x += 20f;
		position.z = 0f;
		GameObject gameObject = new GameObject();
		ShowGoldGround showGoldGround = gameObject.AddComponent(typeof(ShowGoldGround)) as ShowGoldGround;
		showGoldGround.StartShow(groundArray, position);
	}

	private void ShowData(int index)
	{
	}

	public ItemAttribute GetItemAttribute(int index)
	{
		ItemAttribute itemAttribute = null;
		int num = 0;
		for (int i = 0; i < attributeArray.Count; i++)
		{
			itemAttribute = attributeArray[i] as ItemAttribute;
			if (itemAttribute.inshop)
			{
				if (num == index)
				{
					return itemAttribute;
				}
				num++;
			}
		}
		return itemAttribute;
	}

	private void FixedUpdate()
	{
	}
}
