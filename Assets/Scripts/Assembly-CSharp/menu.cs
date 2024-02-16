using System.Collections;
using UnityEngine;

public class menu : MonoBehaviour
{
	private const string c_strOpenClikKey = "3C8DCABE-1033-460D-9F87-1E9B65ED104A";

	private TAudioController audios;

	public int selectIndex = -1;

	public UILayer layer;

	private Transform listener;

	private UIEventManager ui_event;

	private float fReDisTime;

	private void Awake()
	{
		Application.targetFrameRate = 500;
	}

	private void Start()
	{
		globalVal.Limt = false;
		GameObject gameObject = GameObject.Find("DontDestroy");
		if (gameObject == null)
		{
			gameObject = new GameObject("DontDestroy");
			Object.DontDestroyOnLoad(gameObject);
			GameObject gameObject2 = (GameObject)Object.Instantiate(Resources.Load("SoundEvent/Music_meu"));
			ITAudioEvent iTAudioEvent = gameObject2.transform.GetComponent(typeof(ITAudioEvent)) as ITAudioEvent;
			gameObject2.transform.parent = gameObject.transform;
			iTAudioEvent.Trigger();
		}
		else if (layer == UILayer.LOAD)
		{
			Object.DestroyImmediate(gameObject);
		}
		GameObject gameObject3 = GameObject.Find("TAudioController");
		if (null == gameObject3)
		{
			gameObject3 = new GameObject("TAudioController");
			gameObject3.AddComponent<TAudioController>();
		}
		audios = gameObject3.GetComponent(typeof(TAudioController)) as TAudioController;
		GameCenterPlugin.Initialize();
		OpenClikPlugin.Initialize("3C8DCABE-1033-460D-9F87-1E9B65ED104A");
		if (!GameCenterPlugin.IsLogin())
		{
			GameCenterPlugin.Login();
		}
		//iPhoneSettings.screenCanDarken = false;
		Time.timeScale = 1f;
		InitAllMenu();
		if (layer != 0)
		{
			globalVal.UIState = layer;
		}
		bool show_full = false;
		switch (globalVal.UIState)
		{
		case UILayer.MENU:
			if (!globalVal.FirstCome)
			{
				show_full = true;
				globalVal.FirstCome = true;
			}
			InitMenu();
			Time.timeScale = 0.5f;
			break;
		case UILayer.THESTASH:
			InitTheStash();
			break;
		case UILayer.OPTIONS:
			InitOption();
			break;
		case UILayer.AVATAR:
			InitAvatar();
			break;
		case UILayer.ITEMS:
			InitItems();
			break;
		case UILayer.UPGRADES:
			InitDeathWindrow();
			break;
		case UILayer.PROFILE:
			InitProfile();
			break;
		case UILayer.TBANK:
			InitTbank();
			break;
		case UILayer.LOAD:
		case UILayer.GAME:
			InitGame();
			ShowLoading(true);
			ShowItemDialog(false);
			break;
		case UILayer.GAMEOVER:
			if (globalVal.GameCount % 3 == 2)
			{
				show_full = true;
			}
			InitGameOver();
			globalVal.GameCount++;
			break;
		case UILayer.CREDITS:
			InitCredits();
			break;
		case UILayer.HOWTO:
			InitHowTo();
			break;
		}
		OpenClikPlugin.Show(show_full);
	}

	private void Update()
	{
		if (globalVal.IAPIndex == -1)
		{
			return;
		}
		IAPPlugin.Status purchaseStatus = IAPPlugin.GetPurchaseStatus();
		if (purchaseStatus == IAPPlugin.Status.kSuccess)
		{
			switch (globalVal.IAPIndex)
			{
			case 1:
				globalVal.g_gold += 99;
				break;
			case 2:
				globalVal.g_gold += 199;
				break;
			case 3:
				globalVal.g_gold += 299;
				break;
			case 4:
				globalVal.g_gold += 499;
				break;
			case 5:
				globalVal.g_gold += 999;
				break;
			case 6:
				globalVal.g_gold += 1999;
				break;
			case 7:
				globalVal.g_gold += 2999;
				break;
			case 8:
				globalVal.g_gold += 4999;
				break;
			}
			globalVal.IAPIndex = -1;
		}
	}

	private void InitAllMenu()
	{
		Transform transform = null;
		for (int i = 0; i < base.transform.childCount; i++)
		{
			transform = base.transform.GetChild(i);
			transform.localPosition = new Vector3(-2000f, -2000f, 0f);
		}
		OpenClikPlugin.Hide();
	}

	private void InitMenu()
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("shader");
		transform.localPosition = new Vector3(0f, 0f, -3f);
		Transform transform2 = GameObject.Find("Main Camera").transform;
		transform = base.transform.Find("shader");
		transform.localPosition = new Vector3(0f, 0f, -2f);
		transform = base.transform.Find("menu");
		transform.localPosition = Vector3.zero;
		Transform transform3 = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			transform3 = transform.GetChild(i);
			switch (transform.GetChild(i).name)
			{
			case "options":
				uIMoveControl = transform3.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(111.2443f, -50.85039f, -1f);
				uIMoveControl.RightToLeft(1.18f, transform3);
				break;
			case "playNow":
				uIMoveControl = transform3.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(80f, 24f, -1f);
				uIMoveControl.RightToLeft(1.08f, transform3);
				break;
			case "theStash":
				uIMoveControl = transform3.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(91.50391f, -10.2587f, -1f);
				uIMoveControl.RightToLeft(1.13f, transform3);
				break;
			case "buttom_bg":
				uIMoveControl = transform3.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(-95.97375f, -177.4086f, -2f);
				uIMoveControl.DownToUp(1.4f, transform3);
				break;
			case "buttom_bg2":
				uIMoveControl = transform3.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(-97.26331f, -163.5124f, -1f);
				uIMoveControl.DownToUp(1.4f, transform3);
				break;
			case "ranking":
				uIMoveControl = transform3.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(-114.3286f, -160.9865f, -2f);
				uIMoveControl.DownToUp(1.45f, transform3);
				break;
			case "trophy":
				uIMoveControl = transform3.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(-131.7188f, -149.4573f, -2f);
				uIMoveControl.DownToUp(1.5f, transform3);
				break;
			}
		}
		showRim();
	}

	public void showRim()
	{
	}

	public void InitTbank()
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("bg");
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		uIMoveControl.SetPos(0f, 0f, 1f);
		transform = base.transform.Find("tbank");
		transform.localPosition = Vector3.zero;
		Transform transform2 = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			transform2 = transform.GetChild(i);
			switch (transform.GetChild(i).name)
			{
			case "title":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(0f, 140f, 0f);
				uIMoveControl.UpToDown(0f, transform2);
				break;
			case "tbank_1":
				IAPPlugin.NowPurchaseProduct("com.trinitigame.minirunner.099cents", "1");
				globalVal.IAPIndex = 1;
				break;
			case "tbank_2":
				IAPPlugin.NowPurchaseProduct("com.trinitigame.minirunner.199cents", "1");
				globalVal.IAPIndex = 2;
				break;
			case "tbank_3":
				IAPPlugin.NowPurchaseProduct("com.trinitigame.minirunner.299cents", "1");
				globalVal.IAPIndex = 3;
				break;
			case "tbank_4":
				IAPPlugin.NowPurchaseProduct("com.trinitigame.minirunner.499cents", "1");
				globalVal.IAPIndex = 4;
				break;
			case "tbank_5":
				IAPPlugin.NowPurchaseProduct("com.trinitigame.minirunner.999cents", "1");
				globalVal.IAPIndex = 5;
				break;
			case "tbank_6":
				IAPPlugin.NowPurchaseProduct("com.trinitigame.minirunner.1999cents", "1");
				globalVal.IAPIndex = 6;
				break;
			case "tbank_7":
				IAPPlugin.NowPurchaseProduct("com.trinitigame.minirunner.2999cents", "1");
				globalVal.IAPIndex = 7;
				break;
			case "tbank_8":
				IAPPlugin.NowPurchaseProduct("com.trinitigame.minirunner.4999cents", "1");
				globalVal.IAPIndex = 8;
				break;
			}
		}
	}

	private void InitTheStash()
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("bg");
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		uIMoveControl.SetPos(0f, 0f, 1f);
		transform = base.transform.Find("thestash");
		transform.localPosition = Vector3.zero;
		Transform transform2 = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			transform2 = transform.GetChild(i);
			switch (transform.GetChild(i).name)
			{
			case "title":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(0f, 140f, 0f);
				uIMoveControl.UpToDown(0f, transform2);
				break;
			case "thestash_avatar":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(0f, 95f, -1f);
				uIMoveControl.LeftToRight(0.1f, transform2);
				break;
			case "thestash_upgrades":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(0f, -50f, -1f);
				uIMoveControl.RightToLeft(0.3f, transform2);
				break;
			case "thestash_profile":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(-84f, -175f, -1f);
				uIMoveControl.DownToUp(0.2f, transform2);
				break;
			case "thestash_shop":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(70f, -175f, -1f);
				uIMoveControl.DownToUp(0.3f, transform2);
				break;
			}
		}
		TUIMeshText tUIMeshText = transform.Find("title/label_glod").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
		tUIMeshText.text = string.Empty + globalVal.g_gold;
	}

	private void InitOption()
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		if (null != globalVal.MainCamera)
		{
			ui_event = globalVal.MainCamera.GetComponent(typeof(UIEventManager)) as UIEventManager;
		}
		else
		{
			ui_event = Camera.main.GetComponent(typeof(UIEventManager)) as UIEventManager;
		}
		transform = base.transform.Find("option");
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		uIMoveControl.RightToLeft(0f, transform);
	}

	private void InitAvatar()
	{
		InitAvatarList();
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("bg");
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		uIMoveControl.SetPos(0f, 0f, 1f);
		transform = base.transform.Find("avatar");
		transform.localPosition = Vector3.zero;
		Transform transform2 = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			transform2 = transform.GetChild(i);
			switch (transform.GetChild(i).name)
			{
			case "title":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(0f, 140f, -1f);
				uIMoveControl.UpToDown(0.1f, transform2);
				break;
			case "listground":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(-11f, 90f, -1f);
				uIMoveControl.RightToLeft(0.1f, transform2);
				break;
			case "sliderbg":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(217f, -11f, -1f);
				uIMoveControl.RightToLeft(0.1f, transform2);
				break;
			}
		}
	}

	private void InitItems()
	{
		InitItemList();
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("bg");
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		uIMoveControl.SetPos(0f, 0f, 1f);
		transform = base.transform.Find("items");
		transform.localPosition = Vector3.zero;
		Transform transform2 = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			transform2 = transform.GetChild(i);
			switch (transform.GetChild(i).name)
			{
			case "title":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(0f, 140f, -1f);
				uIMoveControl.UpToDown(0.1f, transform2);
				break;
			case "listground":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(-11f, 90f, -1f);
				uIMoveControl.RightToLeft(0.1f, transform2);
				break;
			case "sliderbg":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(217f, -11f, -1f);
				uIMoveControl.RightToLeft(0.1f, transform2);
				break;
			}
		}
	}

	private void InitUpgrades()
	{
		InitUpgradesList();
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("bg");
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		uIMoveControl.SetPos(0f, 0f, 1f);
		transform = base.transform.Find("upgrades");
		transform.localPosition = Vector3.zero;
		Transform transform2 = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			transform2 = transform.GetChild(i);
			switch (transform.GetChild(i).name)
			{
			case "title":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(0f, 140f, -1f);
				uIMoveControl.UpToDown(0.1f, transform2);
				break;
			case "listground":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(-11f, 90f, -1f);
				uIMoveControl.RightToLeft(0.1f, transform2);
				break;
			case "sliderbg":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(217f, -11f, -1f);
				uIMoveControl.RightToLeft(0.1f, transform2);
				break;
			}
		}
	}

	public void InitItemList()
	{
		ArrayList itemOnceArray = ItemManagerClass.body.itemOnceArray;
		Transform transform = null;
		GameObject original = Resources.Load("Prefab/list_item") as GameObject;
		transform = base.transform.Find("items");
		Transform transform2 = transform.Find("listground");
		RemoveAllList(transform2);
		float num = 50f;
		TUIRect clip = transform.Find("clipRect").GetComponent(typeof(TUIRect)) as TUIRect;
		int num2 = 0;
		for (int i = 0; i < itemOnceArray.Count; i++)
		{
			ItemOnceAttribute itemOnceAttribute = itemOnceArray[i] as ItemOnceAttribute;
			GameObject gameObject = Object.Instantiate(original) as GameObject;
			gameObject.name = "avatar_item";
			gameObject.transform.parent = transform2;
			gameObject.transform.localPosition = new Vector3(0f, (float)(-num2) * num, 0f);
			TUIMeshTextClip tUIMeshTextClip = null;
			TUIMeshSpriteClip tUIMeshSpriteClip = null;
			tUIMeshTextClip = gameObject.transform.Find("label").GetComponent(typeof(TUIMeshTextClip)) as TUIMeshTextClip;
			tUIMeshTextClip.text = itemOnceAttribute.name;
			tUIMeshTextClip.clip = clip;
			tUIMeshTextClip = gameObject.transform.Find("label_gold").GetComponent(typeof(TUIMeshTextClip)) as TUIMeshTextClip;
			tUIMeshTextClip.text = itemOnceAttribute.price.ToString();
			tUIMeshTextClip.clip = clip;
			tUIMeshSpriteClip = gameObject.transform.Find("picture").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
			tUIMeshSpriteClip.frameName = itemOnceAttribute.picname;
			tUIMeshSpriteClip.clip = clip;
			tUIMeshSpriteClip = gameObject.transform.Find("Normal").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
			tUIMeshSpriteClip.clip = clip;
			tUIMeshSpriteClip = gameObject.transform.Find("Pressed").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
			tUIMeshSpriteClip.clip = clip;
			tUIMeshSpriteClip = gameObject.transform.Find("pic_gold").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
			tUIMeshSpriteClip.clip = clip;
			tUIMeshSpriteClip = gameObject.transform.Find("pic_gold_bg").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
			tUIMeshSpriteClip.clip = clip;
			TUIButtonSelect tUIButtonSelect = gameObject.GetComponent(typeof(TUIButtonSelect)) as TUIButtonSelect;
			if (tUIButtonSelect != null && (int)globalVal.g_item_once_count[i] == 1)
			{
				MonoBehaviour.print((int)globalVal.g_item_once_count[i]);
				tUIButtonSelect.SetSelected(true);
			}
			num2++;
		}
		TUIMeshText tUIMeshText = transform.Find("title/label_glod").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
		tUIMeshText.text = string.Empty + globalVal.g_gold;
	}

	public void InitAvatarList()
	{
		ArrayList avatarArray = ItemManagerClass.body.avatarArray;
		Transform transform = null;
		GameObject original = Resources.Load("Prefab/list_item") as GameObject;
		transform = base.transform.Find("avatar");
		Transform transform2 = transform.Find("listground");
		RemoveAllList(transform2);
		float num = 65f;
		TUIRect clip = transform.Find("clipRect").GetComponent(typeof(TUIRect)) as TUIRect;
		int num2 = 0;
		for (int i = 0; i < avatarArray.Count; i++)
		{
			AvatarAttribute avatarAttribute = avatarArray[i] as AvatarAttribute;
			GameObject gameObject = Object.Instantiate(original) as GameObject;
			gameObject.name = "avatar_item";
			gameObject.transform.parent = transform2;
			gameObject.transform.localPosition = new Vector3(11f, (float)(-num2 + 1) * num - 5f, 0f);
			TUIMeshTextClip tUIMeshTextClip = null;
			TUIMeshSpriteClip tUIMeshSpriteClip = null;
			tUIMeshTextClip = gameObject.transform.Find("label").GetComponent(typeof(TUIMeshTextClip)) as TUIMeshTextClip;
			tUIMeshTextClip.text = avatarAttribute.name;
			tUIMeshTextClip.clip = clip;
			tUIMeshTextClip = gameObject.transform.Find("label_gold").GetComponent(typeof(TUIMeshTextClip)) as TUIMeshTextClip;
			tUIMeshTextClip.text = avatarAttribute.price.ToString();
			tUIMeshTextClip.clip = clip;
			tUIMeshSpriteClip = gameObject.transform.Find("picture").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
			tUIMeshSpriteClip.frameName = avatarAttribute.picname;
			tUIMeshSpriteClip.clip = clip;
			tUIMeshSpriteClip = gameObject.transform.Find("Normal").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
			tUIMeshSpriteClip.clip = clip;
			tUIMeshSpriteClip = gameObject.transform.Find("Pressed").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
			tUIMeshSpriteClip.clip = clip;
			tUIMeshSpriteClip = gameObject.transform.Find("pic_gold").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
			tUIMeshSpriteClip.clip = clip;
			tUIMeshSpriteClip = gameObject.transform.Find("pic_gold_bg").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
			tUIMeshSpriteClip.clip = clip;
			if ((int)globalVal.g_avatar_isbuy[i] == 1)
			{
				gameObject.transform.Find("pucrhased").gameObject.SetActive(true);
			}
			else
			{
				gameObject.transform.Find("pucrhased").gameObject.SetActive(false);
			}
			if (i == globalVal.g_avatar_id)
			{
				gameObject.transform.Find("equip").gameObject.SetActive(true);
			}
			else
			{
				gameObject.transform.Find("equip").gameObject.SetActive(false);
			}
			num2++;
		}
	}

	private void RemoveAllList(Transform root)
	{
		foreach (Transform item in root)
		{
			Object.Destroy(item.gameObject);
		}
	}

	public void InitDeathWindrow()
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("title");
		transform.localPosition = new Vector3(0f, 140f, 0f);
	}

	public void InitUpgradesList()
	{
		ArrayList attributeArray = ItemManagerClass.body.attributeArray;
		Transform transform = null;
		GameObject original = Resources.Load("Prefab/list_item") as GameObject;
		transform = base.transform.Find("upgrades");
		Transform transform2 = transform.Find("listground");
		RemoveAllList(transform2);
		float num = 50f;
		TUIRect clip = transform.Find("clipRect").GetComponent(typeof(TUIRect)) as TUIRect;
		int num2 = 0;
		for (int i = 0; i < attributeArray.Count; i++)
		{
			ItemAttribute itemAttribute = attributeArray[i] as ItemAttribute;
			int num3 = (int)globalVal.g_itemlevel[itemAttribute.index];
			if (itemAttribute.inshop)
			{
				GameObject gameObject = Object.Instantiate(original) as GameObject;
				gameObject.name = "avatar_item";
				gameObject.transform.parent = transform2;
				gameObject.transform.localPosition = new Vector3(0f, (float)(-num2) * num, 0f);
				TUIMeshTextClip tUIMeshTextClip = null;
				TUIMeshSpriteClip tUIMeshSpriteClip = null;
				tUIMeshTextClip = gameObject.transform.Find("label").GetComponent(typeof(TUIMeshTextClip)) as TUIMeshTextClip;
				tUIMeshTextClip.text = (itemAttribute.level[num3 + 1] as ItemSubAttr).name;
				tUIMeshTextClip.clip = clip;
				tUIMeshTextClip = gameObject.transform.Find("label_gold").GetComponent(typeof(TUIMeshTextClip)) as TUIMeshTextClip;
				tUIMeshTextClip.text = (itemAttribute.level[num3 + 1] as ItemSubAttr).price.ToString();
				tUIMeshTextClip.clip = clip;
				tUIMeshSpriteClip = gameObject.transform.Find("picture").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
				tUIMeshSpriteClip.frameName = (itemAttribute.level[num3 + 1] as ItemSubAttr).picname;
				tUIMeshSpriteClip.clip = clip;
				tUIMeshSpriteClip = gameObject.transform.Find("Normal").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
				tUIMeshSpriteClip.clip = clip;
				tUIMeshSpriteClip = gameObject.transform.Find("Pressed").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
				tUIMeshSpriteClip.clip = clip;
				tUIMeshSpriteClip = gameObject.transform.Find("pic_gold").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
				tUIMeshSpriteClip.clip = clip;
				tUIMeshSpriteClip = gameObject.transform.Find("pic_gold_bg").GetComponent(typeof(TUIMeshSpriteClip)) as TUIMeshSpriteClip;
				tUIMeshSpriteClip.clip = clip;
				num2++;
			}
		}
		TUIMeshText tUIMeshText = transform.Find("title/label_glod").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
		tUIMeshText.text = string.Empty + globalVal.g_gold;
	}

	private void InitProfile()
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("bg");
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		uIMoveControl.SetPos(0f, 0f, 1f);
		transform = base.transform.Find("title");
		transform.localPosition = new Vector3(0f, 140f, -1.1f);
		TUIMeshText tUIMeshText = transform.Find("label_glod").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
		tUIMeshText.text = string.Empty + globalVal.g_gold;
	}

	private void InitPopInfo(int index)
	{
		switch (globalVal.UIState)
		{
		case UILayer.AVATAR:
		{
			ArrayList avatarArray = ItemManagerClass.body.avatarArray;
			Transform transform2 = null;
			transform2 = base.transform.Find("popdialog");
			selectIndex = index;
			int num2 = 0;
			AvatarAttribute avatarAttribute = avatarArray[index] as AvatarAttribute;
			Debug.Log("Avatar index  = " + index);
			TUIMeshText tUIMeshText2 = null;
			TUIMeshSprite tUIMeshSprite2 = transform2.Find("picture").GetComponent(typeof(TUIMeshSprite)) as TUIMeshSprite;
			tUIMeshSprite2.frameName = avatarAttribute.modelname;
			tUIMeshText2 = transform2.Find("label_name").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
			tUIMeshText2.text = avatarAttribute.name;
			tUIMeshText2 = transform2.Find("label_info").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
			tUIMeshText2.text = avatarAttribute.info;
			tUIMeshText2 = transform2.Find("label_glod").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
			tUIMeshText2.text = avatarAttribute.price.ToString();
			TUIButtonClick tUIButtonClick2 = null;
			tUIButtonClick2 = transform2.Find("pop_dialog_buy").GetComponent(typeof(TUIButtonClick)) as TUIButtonClick;
			if (globalVal.g_gold >= avatarAttribute.price || (int)globalVal.g_avatar_isbuy[index] == 1)
			{
				tUIButtonClick2.SetDisabled(false);
			}
			else
			{
				tUIButtonClick2.SetDisabled(true);
			}
			if ((int)globalVal.g_avatar_isbuy[index] == 1)
			{
				tUIMeshSprite2 = transform2.Find("pop_dialog_buy/Normal").GetComponent(typeof(TUIMeshSprite)) as TUIMeshSprite;
				tUIMeshSprite2.frameName = "use-bottom";
				tUIMeshSprite2 = transform2.Find("pop_dialog_buy/Pressed").GetComponent(typeof(TUIMeshSprite)) as TUIMeshSprite;
				tUIMeshSprite2.frameName = "use-bottom2";
			}
			else
			{
				tUIMeshSprite2 = transform2.Find("pop_dialog_buy/Normal").GetComponent(typeof(TUIMeshSprite)) as TUIMeshSprite;
				tUIMeshSprite2.frameName = "yellow-bottom1";
				tUIMeshSprite2 = transform2.Find("pop_dialog_buy/Pressed").GetComponent(typeof(TUIMeshSprite)) as TUIMeshSprite;
				tUIMeshSprite2.frameName = "yellow-bottom1.1";
			}
			MonoBehaviour.print(globalVal.g_gold + "    " + avatarAttribute.price);
			break;
		}
		case UILayer.UPGRADES:
		{
			ArrayList attributeArray = ItemManagerClass.body.attributeArray;
			Transform transform3 = null;
			transform3 = base.transform.Find("popdialog");
			selectIndex = index;
			int num3 = 0;
			for (int j = 0; j < attributeArray.Count; j++)
			{
				ItemAttribute itemAttribute = attributeArray[j] as ItemAttribute;
				if (!itemAttribute.inshop)
				{
					continue;
				}
				if (num3 == index)
				{
					TUIMeshText tUIMeshText3 = null;
					TUIMeshSprite tUIMeshSprite3 = null;
					int num4 = (int)globalVal.g_itemlevel[itemAttribute.index];
					ItemSubAttr itemSubAttr = itemAttribute.level[num4 + 1] as ItemSubAttr;
					tUIMeshText3 = transform3.Find("label_name").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
					tUIMeshText3.text = itemSubAttr.name;
					tUIMeshText3 = transform3.Find("label_info").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
					tUIMeshText3.text = itemSubAttr.info;
					transform3.Find("picture").GetComponent<Renderer>().enabled = true;
					tUIMeshSprite3 = transform3.Find("picture").GetComponent(typeof(TUIMeshSprite)) as TUIMeshSprite;
					tUIMeshSprite3.frameName = itemSubAttr.picname + "_b";
					MonoBehaviour.print(tUIMeshSprite3.frameName);
					TUIButtonClick tUIButtonClick3 = null;
					tUIButtonClick3 = transform3.Find("pop_dialog_buy").GetComponent(typeof(TUIButtonClick)) as TUIButtonClick;
					tUIMeshText3 = transform3.Find("pop_dialog_buy/label").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
					if (globalVal.g_gold >= itemSubAttr.price)
					{
						tUIButtonClick3.SetDisabled(false);
						tUIMeshText3.gameObject.SetActive(true);
					}
					else
					{
						tUIButtonClick3.SetDisabled(true);
						tUIMeshText3.gameObject.SetActive(false);
					}
					if (num4 + 1 >= itemAttribute.level.Count - 1)
					{
						tUIButtonClick3.SetDisabled(true);
						tUIMeshText3.gameObject.SetActive(false);
						MonoBehaviour.print(itemAttribute.level.Count);
					}
					break;
				}
				num3++;
			}
			break;
		}
		case UILayer.ITEMS:
		{
			ArrayList itemOnceArray = ItemManagerClass.body.itemOnceArray;
			Transform transform = null;
			transform = base.transform.Find("popdialog");
			selectIndex = index;
			int num = 0;
			for (int i = 0; i < itemOnceArray.Count; i++)
			{
				ItemOnceAttribute itemOnceAttribute = itemOnceArray[i] as ItemOnceAttribute;
				if (num == index)
				{
					TUIMeshText tUIMeshText = null;
					TUIMeshSprite tUIMeshSprite = null;
					tUIMeshText = transform.Find("label_name").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
					tUIMeshText.text = itemOnceAttribute.name;
					tUIMeshText = transform.Find("label_info").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
					tUIMeshText.text = itemOnceAttribute.info;
					transform.Find("picture").GetComponent<Renderer>().enabled = true;
					TUIButtonClick tUIButtonClick = null;
					tUIButtonClick = transform.Find("pop_dialog_buy").GetComponent(typeof(TUIButtonClick)) as TUIButtonClick;
					if (globalVal.g_gold >= itemOnceAttribute.price)
					{
						tUIButtonClick.SetDisabled(false);
					}
					else
					{
						tUIButtonClick.SetDisabled(true);
					}
					tUIMeshSprite = transform.Find("picture").GetComponent(typeof(TUIMeshSprite)) as TUIMeshSprite;
					tUIMeshSprite.frameName = itemOnceAttribute.picname + "_b";
					MonoBehaviour.print(tUIMeshSprite.frameName);
					MonoBehaviour.print(globalVal.g_gold + "    " + itemOnceAttribute.price);
					break;
				}
				num++;
			}
			break;
		}
		}
	}

	public void ShowDialog(bool show, int index)
	{
		if (show)
		{
			InitPopInfo(index);
		}
		if (show)
		{
			audios.PlayAudio("UIpopup");
		}
		string empty = string.Empty;
		switch (globalVal.UIState)
		{
		case UILayer.AVATAR:
			empty = "avatar";
			break;
		case UILayer.ITEMS:
			empty = "items";
			break;
		case UILayer.UPGRADES:
			empty = "upgrades";
			break;
		}
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("popdialog");
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		if (show)
		{
			uIMoveControl.SetEndPos(0f, 0f, 0f);
			uIMoveControl.ScaleNoMove(0f, transform);
			if (globalVal.UIState == UILayer.AVATAR)
			{
				SetUIAvatar(selectIndex);
				uIMoveControl.SetCallBack(base.transform, "ShowAvatarVeiw");
			}
		}
		else
		{
			uIMoveControl.SetPos(-1000f, -1000f, 0f);
		}
		if (!show)
		{
			selectIndex = -1;
		}
	}

	public void InitAllItems()
	{
		string text = string.Empty;
		switch (globalVal.UIState)
		{
		case UILayer.AVATAR:
			text = "avatar";
			break;
		case UILayer.ITEMS:
			text = "items";
			break;
		case UILayer.UPGRADES:
			text = "upgrades";
			break;
		}
		Transform transform = base.transform.Find(text + "/listground");
		for (int i = 0; i < transform.childCount; i++)
		{
			Transform child = transform.GetChild(i);
			TUIButtonSelect tUIButtonSelect = child.GetComponent(typeof(TUIButtonSelect)) as TUIButtonSelect;
			tUIButtonSelect.SetSelected(false);
		}
	}

	private void SetUIAvatar(int index)
	{
		AvatarAttribute avatarAttribute = ItemManagerClass.body.avatarArray[index] as AvatarAttribute;
		GameObject gameObject = Resources.Load("Prefabs/Character/Chara" + (globalVal.g_avatar_id + 1)) as GameObject;
		if (!(null == gameObject))
		{
			GameObject obj = Object.Instantiate(gameObject) as GameObject;
			GameObject gameObject2 = GameObject.Find("Avatar_Character");
			if (!(gameObject2 == null))
			{
				gameObject2.transform.parent = null;
				Object.Destroy(obj);
				Resources.UnloadUnusedAssets();
			}
		}
	}

	public void ShowAvatarVeiw()
	{
		GameObject gameObject = GameObject.Find("AvatarCamera");
		if (gameObject != null)
		{
			gameObject.GetComponent<Camera>().enabled = true;
			gameObject.GetComponent<Camera>().clearFlags = CameraClearFlags.Depth;
		}
	}

	public void ShowGame(bool bShow)
	{
		InitAllMenu();
		if (bShow)
		{
			InitGame();
		}
	}

	public void ShowOption(bool bShow)
	{
		InitAllMenu();
		if (bShow)
		{
			InitOption();
		}
	}

	public void refurbishScore()
	{
		Transform transform = base.transform.Find("gametitle/game_gold");
		if (null == transform)
		{
			Debug.Log("not found Gold");
			return;
		}
		string text = globalVal.AllGold.ToString();
		for (int i = 0; i < 7; i++)
		{
			Transform transform2 = transform.Find(i.ToString());
			TUIMeshSprite component = transform2.GetComponent<TUIMeshSprite>();
			if (i >= text.Length)
			{
				component.frameName = string.Empty;
			}
			else
			{
				component.frameName = "g" + text.Substring(text.Length - i - 1, 1);
			}
			component.UpdateMesh();
		}
	}

	public void refurbishShot(float fShot)
	{
		UILayer uIState = globalVal.UIState;
		if (uIState != UILayer.GAME)
		{
			return;
		}
		Transform transform = null;
		TUIMeshText tUIMeshText = null;
		transform = base.transform.Find("gametitle/title/FPS");
		if (null == transform)
		{
			MonoBehaviour.print("not found transRoot");
			return;
		}
		tUIMeshText = transform.GetComponent(typeof(TUIMeshText)) as TUIMeshText;
		if (null == tUIMeshText)
		{
			MonoBehaviour.print("not found ctrl");
			return;
		}
		tUIMeshText.text = fShot.ToString();
		tUIMeshText.UpdateMesh();
	}

	public void refurbishDistance()
	{
		if (0.1f >= Time.realtimeSinceStartup - fReDisTime)
		{
			return;
		}
		Transform transform = base.transform.Find("gametitle/game_distance");
		if (null == transform)
		{
			return;
		}
		string text = Mathf.FloorToInt(globalVal.CurDistance).ToString();
		float x = -45f + (float)(text.Length - 1) * 7.5f;
		transform.localPosition = new Vector3(x, 0f, 0f);
		for (int i = 0; i < 7; i++)
		{
			Transform transform2 = transform.Find(i.ToString());
			TUIMeshSprite component = transform2.GetComponent<TUIMeshSprite>();
			if (i >= text.Length)
			{
				component.frameName = string.Empty;
			}
			else
			{
				component.frameName = text.Substring(text.Length - i - 1, 1);
			}
			component.UpdateMesh();
		}
		fReDisTime = Time.realtimeSinceStartup;
	}

	public void showPicture(string strName, bool bShow)
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("notice");
		if (null == transform)
		{
			return;
		}
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		if (!(null == uIMoveControl))
		{
			TUIMeshSprite component = transform.GetComponent<TUIMeshSprite>();
			if (null != component)
			{
				component.frameName = strName;
			}
			if (bShow)
			{
				uIMoveControl.SetEndPos(0f, 85f, -1f);
				uIMoveControl.ScaleNoMove(0f, transform);
			}
			else
			{
				uIMoveControl.SetPos(-1000f, 0f, 0f);
			}
			component.UpdateMesh();
		}
	}

	public void ShowItemDialog(bool bShow)
	{
	}

	public void ShowLoading(bool bShow)
	{
		Transform transform = null;
		transform = base.transform.Find("gameloading");
		if (bShow)
		{
			transform.localPosition = new Vector3(0f, 0f, -12f);
		}
		else
		{
			transform.localPosition = new Vector3(-1000f, 0f, 0f);
		}
		transform.gameObject.SetActiveRecursively(bShow);
		if (!bShow)
		{
			ShowTitle();
		}
	}

	public void ShowTitle()
	{
		Transform transform = null;
		transform = base.transform.Find("gametitle");
		transform.localPosition = Vector3.zero;
		if (Screen.width == 640 && Screen.height == 1136)
		{
			transform.localPosition += new Vector3(0f, 30f, 0f);
		}
	}

	private void InitGame()
	{
		MonoBehaviour.print("init game");
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("Lightning");
		transform.localScale = new Vector3(Screen.width + 10, 1f, Screen.height + 10);
		transform.localPosition = Vector3.zero;
		transform = base.transform.Find("gameloading");
		transform.localPosition = new Vector3(0f, 0f, -1f);
		transform = base.transform.Find("shader");
		transform.localPosition = new Vector3(0f, 0f, -2f);
	}

	private void fullTask(int nId)
	{
		ArrayList arrayList = ItemManagerClass.body.taskListArray[nId] as ArrayList;
		string key = arrayList[globalVal.cur_task_id[nId]] as string;
		taskInfo taskInfo2 = ItemManagerClass.body.hashTask[key] as taskInfo;
		if (taskInfo2 != null)
		{
			Transform transform = null;
			UIMoveControl uIMoveControl = null;
			transform = base.transform.Find("option/mission" + (nId + 1));
			Debug.Log("transRoot = " + transform);
			TUIMeshText tUIMeshText = transform.Find("mission_text").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
			tUIMeshText.text = taskInfo2.info;
			tUIMeshText.UpdateMesh();
			tUIMeshText = transform.Find("mission_gold").GetComponent(typeof(TUIMeshText)) as TUIMeshText;
			tUIMeshText.text = taskInfo2.golds.ToString();
			tUIMeshText.UpdateMesh();
		}
	}

	private void InitGameOver()
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("shader");
		transform.localPosition = new Vector3(0f, 0f, -3f);
		transform = base.transform.Find("account");
		uIMoveControl = transform.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
		uIMoveControl.SetEndPos(0f, 0f, -1f);
		Transform transform2 = null;
		for (int i = 0; i < transform.childCount; i++)
		{
			transform2 = transform.GetChild(i);
			switch (transform2.name)
			{
			case "menu":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(0f, -180f, -1f);
				uIMoveControl.RightToLeft(1.38f, transform2);
				break;
			case "retry":
				uIMoveControl = transform2.GetComponent(typeof(UIMoveControl)) as UIMoveControl;
				uIMoveControl.SetEndPos(0f, -116f, -1f);
				uIMoveControl.RightToLeft(1.33f, transform2);
				break;
			}
		}
		setAccount();
		transform = base.transform.Find("gametitle");
		transform.localPosition = new Vector3(0f, 0f, -1f);
		refurbishScore();
		showRim();
	}

	private void InitCredits()
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("title");
		transform.localPosition = new Vector3(-1f, 140f, -1f);
	}

	private void InitHowTo()
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("title");
		transform.localPosition = new Vector3(-1f, 140f, -1f);
	}

	public void setAccount()
	{
		Transform transform = null;
		UIMoveControl uIMoveControl = null;
		transform = base.transform.Find("account/dikuang1");
		Transform transform2 = null;
		TUIMeshText tUIMeshText = null;
		globalVal.g_gold += globalVal.GameScore;
		for (int i = 0; i < transform.childCount; i++)
		{
			transform2 = transform.GetChild(i);
			tUIMeshText = transform2.GetComponent(typeof(TUIMeshText)) as TUIMeshText;
			switch (transform2.name)
			{
			case "distance_value":
				tUIMeshText.text = Mathf.FloorToInt(globalVal.CurDistance) + " M";
				break;
			case "time_value":
			{
				string empty = string.Empty;
				float num = globalVal.EndTime - globalVal.StartTime;
				Debug.Log("fTime = " + num);
				empty = Mathf.FloorToInt(num / 60f) + " M " + Mathf.FloorToInt(num % 60f) + " s";
				tUIMeshText.text = empty;
				break;
			}
			case "bouns_value":
				tUIMeshText.text = globalVal.AccomplishmentScore.ToString();
				break;
			case "score_value":
				tUIMeshText.text = globalVal.AllGold.ToString();
				break;
			case "monster_count":
				tUIMeshText.text = globalVal.CurMonster.ToString();
				break;
			case "gdd_value":
				tUIMeshText.text = globalVal.CurGold.ToString();
				break;
			case "reward_value":
				tUIMeshText.text = globalVal.AccomplishmentGold.ToString();
				break;
			}
			if (null != tUIMeshText)
			{
				tUIMeshText.UpdateMesh();
			}
		}
		transform2 = transform.Find("neuwbest");
		if (null != transform2)
		{
			transform2.gameObject.SetActiveRecursively(false);
		}
		bool flag = false;
		if (globalVal.g_bestScore < globalVal.CurGold)
		{
			globalVal.g_bestScore = globalVal.CurGold;
			flag = true;
			transform2.gameObject.SetActiveRecursively(true);
		}
		if ((float)globalVal.g_bestDistance < globalVal.CurDistance)
		{
			globalVal.g_bestDistance = Mathf.FloorToInt(globalVal.CurDistance);
			flag = true;
			transform2.gameObject.SetActiveRecursively(true);
		}
		transform2 = base.transform.Find("account/title/label_glod");
		if (null != transform2)
		{
			tUIMeshText = transform2.GetComponent(typeof(TUIMeshText)) as TUIMeshText;
			tUIMeshText.text = globalVal.g_gold.ToString();
			tUIMeshText.UpdateMesh();
		}
		globalVal.SaveFile("saveData.txt");
		submitScore();
	}

	private void submitScore()
	{
		GameObject gameObject = GameObject.Find("TUI");
		if (!(null == gameObject))
		{
			GameCenterManager gameCenterManager = gameObject.GetComponent(typeof(GameCenterManager)) as GameCenterManager;
			if (!(null == gameCenterManager))
			{
				globalVal.g_bSumbit = false;
				gameCenterManager.submitRanking();
			}
		}
	}

	public int calculateScore()
	{
		float num = 0f;
		num += Mathf.Pow(globalVal.CurDistance * 10f, 1.2f);
		num += Mathf.Pow(globalVal.CurMonster * 3, 1.7f);
		num += (float)globalVal.AccomplishmentScore;
		return Mathf.FloorToInt(num);
	}

	public void addBlood(Vector3 v3Point)
	{
		Transform effect = globalVal.Effect.getEffect("blood");
		effect.parent = base.transform;
		GameObject gameObject = GameObject.Find("TUI/TUICamera");
		v3Point = gameObject.GetComponent<Camera>().ScreenToWorldPoint(v3Point);
		effect.position = v3Point;
		FrameEffect frameEffect = effect.GetComponent(typeof(FrameEffect)) as FrameEffect;
		if (null != frameEffect)
		{
			frameEffect.showEffect();
		}
		globalVal.Effect.setAlarm(effect, 1f);
	}

	public void updateMonsterCount()
	{
		Transform transform = base.transform.Find("gametitle/title/monster_count");
		if (!(null == transform))
		{
			TUIMeshText tUIMeshText = transform.GetComponent(typeof(TUIMeshText)) as TUIMeshText;
			if (!(null == tUIMeshText))
			{
				tUIMeshText.text = globalVal.CurMonster.ToString();
				tUIMeshText.UpdateMesh();
			}
		}
	}

	public void setRoadName(string strCode)
	{
		Transform transform = base.transform.Find("gametitle/title/road");
		if (!(null == transform))
		{
			TUIMeshText tUIMeshText = transform.GetComponent(typeof(TUIMeshText)) as TUIMeshText;
			if (!(null == tUIMeshText))
			{
				tUIMeshText.text = strCode;
				tUIMeshText.UpdateMesh();
			}
		}
	}

	public void showSpeedUP(bool bShow)
	{
		Transform transform = base.transform.Find("speedup");
		if (!(null == transform))
		{
			if (bShow)
			{
				transform.localPosition = new Vector3(0f, 85f, -1f);
			}
			else
			{
				transform.localPosition = new Vector3(-1000f, 0f, 0f);
			}
		}
	}

	public void updateLife(int nCount)
	{
		Transform transform = base.transform.Find("gametitle/title/life1");
		if (null != transform && nCount >= 1)
		{
			transform.gameObject.SetActive(true);
		}
		else if (null != transform)
		{
			transform.gameObject.SetActive(false);
		}
		transform = null;
		transform = base.transform.Find("gametitle/title/life2");
		if (null != transform && nCount >= 2)
		{
			transform.gameObject.SetActive(true);
		}
		else if (null != transform)
		{
			transform.gameObject.SetActive(false);
		}
		transform = null;
		transform = base.transform.Find("gametitle/title/life3");
		if (null != transform && nCount >= 3)
		{
			transform.gameObject.SetActive(true);
		}
		else if (null != transform)
		{
			transform.gameObject.SetActive(false);
		}
	}
}
