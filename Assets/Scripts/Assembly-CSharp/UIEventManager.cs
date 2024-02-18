using UnityEngine;

public class UIEventManager : MonoBehaviour, TUIHandler
{
	private TUI m_tui;

	private TAudioController audios;

	private Transform m_cmSwitchShader;

	public void Start()
	{
		Application.targetFrameRate = 240;
		m_tui = TUI.Instance("TUI");
		m_tui.SetHandler(this);
		GameObject gameObject = GameObject.Find("TAudioController");
		if (gameObject == null)
		{
			gameObject = new GameObject("TAudioController");
			gameObject.AddComponent(typeof(TAudioController));
			Object.DontDestroyOnLoad(gameObject);
		}
		audios = gameObject.GetComponent(typeof(TAudioController)) as TAudioController;
		m_cmSwitchShader = GameObject.Find("TUI/TUIControl/shader").transform;
	}

	public void Update()
	{
		doInput();
	}

	public void doInput()
	{
		TUIInput[] input = TUIInputManager.GetInput();
		for (int i = 0; i < input.Length; i++)
		{
			m_tui.HandleInput(input[i]);
		}
	}

	public void HandleEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (eventType == 3)
		{
			switch (control.name)
			{
			case "avatar_listslider":
				Debug.Log("TUIButtonSlider.CommandSlider = " + control.name + " lparam = " + lparam);
				break;
			}
		}
		if (eventType == 1)
		{
			switch (control.name)
			{
			case "game_input":
				if (globalVal.GameStatus == GAME_STATUS.GAME_RUNNING || globalVal.GameStatus == GAME_STATUS.GAME_READY)
				{
					globalVal.GameJumpClick = true;
				}
				break;
			}
		}
		if (eventType == 2)
		{
			switch (control.name)
			{
			case "gun_input":
				globalVal.GameGunClick = false;
				break;
			}
		}
		if (eventType == 3 && control.GetType() == typeof(TUIButtonClick))
		{
			switch (control.name)
			{
			case "Button":
				break;
			case "options":
				globalVal.UIState = UILayer.OPTIONS;
				Application.LoadLevel("scene_ui_option");
				audios.PlayAudio("UIclick");
				break;
			case "ranking":
				audios.PlayAudio("UIclick");
				break;
			case "trophy":
			{
				GameObject gameObject15 = GameObject.Find("TUI");
				if (!(null == gameObject15))
				{
					GameCenterManager gameCenterManager = gameObject15.GetComponent(typeof(GameCenterManager)) as GameCenterManager;
					if (!(null == gameCenterManager))
					{
						globalVal.g_bSumbit = false;
						gameCenterManager.openRanking();
						audios.PlayAudio("UIclick");
					}
				}
				break;
			}
			case "option_credits":
				globalVal.UIState = UILayer.CREDITS;
				Application.LoadLevel("scene_ui_credits");
				audios.PlayAudio("UIclick");
				break;
			case "option_howto":
				globalVal.UIState = UILayer.HOWTO;
				Application.LoadLevel("scene_ui_howto");
				audios.PlayAudio("UIclick");
				break;
			case "option_review":
				Application.OpenURL("http://itunes.apple.com/us/app/minijailbreaker/id518240242?ls=1&mt=8");
				audios.PlayAudio("UIclick");
				break;
			case "option_support":
				Application.OpenURL("http://www.trinitigame.com/support?game=mjail&version=" + globalVal.version);
				audios.PlayAudio("UIclick");
				break;
			case "playNow":
				doSwitch(false, "scene_loading");
				globalVal.UIState = UILayer.LOAD;
				globalVal.isskipkey = false;
				audios.PlayAudio("UIclick");
				break;
			case "theStash":
				globalVal.UIState = UILayer.THESTASH;
				Application.LoadLevel("scene_ui_thestash");
				audios.PlayAudio("UIclick");
				break;
			case "gameover_menu":
				globalVal.UIState = UILayer.MENU;
				Application.LoadLevel("scene_ui_menu");
				audios.PlayAudio("UIclick");
				Time.timeScale = 1f;
				break;
			case "gameover_retry":
				audios.PlayAudio("UIclick");
				break;
			case "gameover_thestash":
				globalVal.UIState = UILayer.THESTASH;
				Application.LoadLevel("scene_ui_thestash");
				audios.PlayAudio("UIclick");
				break;
			case "pause_btn":
				Time.timeScale = 0f;
				audios.PlayAudio("UIclick");
				break;
			case "pause_resume":
			{
				GameObject gameObject16 = GameObject.Find("TUI/TUIControl");
				Time.timeScale = 1f;
				audios.PlayAudio("UIback");
				break;
			}
			case "pause_menu":
				Time.timeScale = 1f;
				globalVal.UIState = UILayer.MENU;
				Application.LoadLevel("scene_ui_menu");
				audios.PlayAudio("UIclick");
				break;
			case "pause_retry":
				Time.timeScale = 1f;
				Application.LoadLevel("scene_game");
				audios.PlayAudio("UIclick");
				break;
			case "stash_back":
				if (globalVal.UIState != UILayer.GAMEOVER && globalVal.UIState != UILayer.GAME)
				{
					globalVal.UIState = UILayer.MENU;
					Application.LoadLevel("scene_ui_menu");
				}
				audios.PlayAudio("UIback");
				break;
			case "stash_getmore":
				globalVal.UIState = UILayer.TBANK;
				Application.LoadLevel("scene_ui_tbank");
				audios.PlayAudio("UIclick");
				break;
			case "thestash_shop":
				globalVal.UIState = UILayer.TBANK;
				Application.LoadLevel("scene_ui_tbank");
				audios.PlayAudio("UIclick");
				break;
			case "option_back":
				globalVal.UIState = UILayer.MENU;
				Application.LoadLevel("scene_ui_menu");
				audios.PlayAudio("UIback");
				break;
			case "avatar_back":
			case "items_back":
			case "upgrades_back":
			case "tbank_back":
			case "profile_back":
				if (globalVal.UIState == UILayer.GAMEOVER)
				{
					globalVal.UIState = UILayer.THESTASH;
					Application.LoadLevel("scene_ui_thestash");
				}
				else
				{
					globalVal.UIState = UILayer.THESTASH;
					Application.LoadLevel("scene_ui_thestash");
				}
				audios.PlayAudio("UIback");
				break;
			case "thestash_profile":
				globalVal.UIState = UILayer.PROFILE;
				Application.LoadLevel("scene_ui_profile");
				audios.PlayAudio("UIclick");
				break;
			case "pop_dialog_buy":
			{
				MonoBehaviour.print(control.name);
				int num2 = 0;
				GameObject gameObject17 = GameObject.Find("TUI/TUIControl");
				menu menu8 = null;
				UILayer uILayer2 = UILayer.NONE;
				if (globalVal.UIState != UILayer.GAMEOVER)
				{
					menu8 = gameObject17.GetComponent(typeof(menu)) as menu;
					uILayer2 = globalVal.UIState;
					num2 = menu8.selectIndex;
				}
				switch (uILayer2)
				{
				case UILayer.UPGRADES:
				{
					ItemAttribute itemAttribute = ItemManagerClass.body.GetItemAttribute(num2);
					if (itemAttribute == null)
					{
						break;
					}
					int num3 = (int)globalVal.g_itemlevel[itemAttribute.index];
					menu8.InitAllItems();
					if (num3 + 1 >= itemAttribute.level.Count - 1)
					{
						return;
					}
					ItemSubAttr itemSubAttr = itemAttribute.level[num3 + 1] as ItemSubAttr;
					if (globalVal.g_gold >= itemSubAttr.price)
					{
						globalVal.g_gold -= itemSubAttr.price;
						globalVal.g_itemlevel[itemAttribute.index] = num3 + 1;
						globalVal.SaveFile("saveData.txt");
						if (globalVal.UIState != UILayer.GAMEOVER)
						{
							menu8.InitUpgradesList();
						}
						audios.PlayAudio("UIbuy");
					}
					break;
				}
				case UILayer.ITEMS:
				{
					ItemOnceAttribute itemOnceAttribute = ItemManagerClass.body.itemOnceArray[num2] as ItemOnceAttribute;
					if (itemOnceAttribute == null)
					{
						break;
					}
					menu8.InitAllItems();
					if (globalVal.g_gold >= itemOnceAttribute.price && (int)globalVal.g_item_once_count[num2] == 0)
					{
						globalVal.g_gold -= itemOnceAttribute.price;
						globalVal.g_item_once_count[num2] = 1;
						globalVal.SaveFile("saveData.txt");
						if (globalVal.UIState != UILayer.GAMEOVER)
						{
							menu8.InitItemList();
						}
						audios.PlayAudio("UIbuy");
					}
					break;
				}
				case UILayer.AVATAR:
				{
					AvatarAttribute avatarAttribute = ItemManagerClass.body.avatarArray[num2] as AvatarAttribute;
					if (avatarAttribute == null)
					{
						break;
					}
					menu8.InitAllItems();
					if (globalVal.g_gold >= avatarAttribute.price && (int)globalVal.g_avatar_isbuy[num2] == 0)
					{
						globalVal.g_gold -= avatarAttribute.price;
						globalVal.g_avatar_isbuy[num2] = 1;
						globalVal.SaveFile("saveData.txt");
						if (globalVal.UIState != UILayer.GAMEOVER)
						{
							menu8.InitAvatarList();
						}
						audios.PlayAudio("UIbuy");
					}
					if ((int)globalVal.g_avatar_isbuy[num2] == 1)
					{
						globalVal.g_avatar_id = num2;
						globalVal.SaveFile("saveData.txt");
						if (globalVal.UIState != UILayer.GAMEOVER)
						{
							menu8.InitAvatarList();
						}
						audios.PlayAudio("UIavatar");
					}
					GameObject gameObject18 = GameObject.Find("AvatarCamera");
					if (gameObject18 != null)
					{
						gameObject18.GetComponent<Camera>().enabled = false;
						gameObject18.GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;
					}
					break;
				}
				}
				if (globalVal.UIState != UILayer.GAMEOVER)
				{
					menu8.ShowDialog(false, 0);
				}
				break;
			}
			case "pop_dialog_close":
			{
				GameObject gameObject13 = GameObject.Find("TUI/TUIControl");
				int num = 0;
				menu menu7 = null;
				UILayer uILayer = UILayer.NONE;
				if (globalVal.UIState != UILayer.GAMEOVER)
				{
					menu7 = gameObject13.GetComponent(typeof(menu)) as menu;
					uILayer = globalVal.UIState;
					num = menu7.selectIndex;
				}
				menu7.InitAllItems();
				switch (uILayer)
				{
				case UILayer.UPGRADES:
					if (globalVal.UIState != UILayer.GAMEOVER)
					{
						menu7.InitUpgradesList();
					}
					break;
				case UILayer.ITEMS:
					if (globalVal.UIState != UILayer.GAMEOVER)
					{
						menu7.InitItemList();
					}
					break;
				case UILayer.AVATAR:
					if (globalVal.UIState != UILayer.GAMEOVER)
					{
						menu7.InitAvatarList();
					}
					break;
				}
				if (globalVal.UIState != UILayer.GAMEOVER)
				{
					menu7.ShowDialog(false, 0);
				}
				GameObject gameObject14 = GameObject.Find("AvatarCamera");
				if (gameObject14 != null)
				{
					gameObject14.GetComponent<Camera>().enabled = false;
					gameObject14.GetComponent<Camera>().clearFlags = CameraClearFlags.Nothing;
				}
				audios.PlayAudio("UIback");
				break;
			}
			case "control_left":
			{
				GameObject gameObject12 = GameObject.Find("player2/Bip01");
				break;
			}
			case "control_right":
			{
				GameObject gameObject11 = GameObject.Find("player2/Bip01");
				break;
			}
			case "control_rejump":
			{
				GameObject gameObject10 = GameObject.Find("player2/Bip01");
				break;
			}
			case "task_button":
			{
				MonoBehaviour.print(control.name);
				GameObject gameObject9 = GameObject.Find("TUI/TUIControl/task_page");
				TaskManager taskManager4 = gameObject9.GetComponent(typeof(TaskManager)) as TaskManager;
				taskManager4.PopTaskList();
				break;
			}
			case "task_list1_button":
			{
				MonoBehaviour.print(control.name);
				GameObject gameObject8 = GameObject.Find("TUI/TUIControl/task_page");
				TaskManager taskManager3 = gameObject8.GetComponent(typeof(TaskManager)) as TaskManager;
				taskManager3.TaskComplete(1);
				break;
			}
			case "task_list2_button":
			{
				MonoBehaviour.print(control.name);
				GameObject gameObject7 = GameObject.Find("TUI/TUIControl/task_page");
				TaskManager taskManager2 = gameObject7.GetComponent(typeof(TaskManager)) as TaskManager;
				taskManager2.TaskComplete(2);
				break;
			}
			case "task_list3_button":
			{
				MonoBehaviour.print(control.name);
				GameObject gameObject6 = GameObject.Find("TUI/TUIControl/task_page");
				TaskManager taskManager = gameObject6.GetComponent(typeof(TaskManager)) as TaskManager;
				taskManager.TaskComplete(3);
				break;
			}
			case "game_pause":
			{
				GameObject gameObject5 = GameObject.Find("TUI/TUIControl");
				menu menu6 = gameObject5.GetComponent(typeof(menu)) as menu;
				globalVal.GamePause = true;
				menu6.ShowOption(true);
				audios.PlayAudio("UIback");
				OpenClikPlugin.Show(false);
				Time.timeScale = 0f;
				break;
			}
			case "option_menu":
			{
				Transform transform2 = GameObject.Find("GameRun").transform;
				Game game2 = transform2.GetComponent(typeof(Game)) as Game;
				game2.Destroy();
				Application.LoadLevel("scene_ui_menu");
				globalVal.UIState = UILayer.MENU;
				audios.PlayAudio("UIclick");
				globalVal.GamePause = false;
				Time.timeScale = 1f;
				break;
			}
			case "option_retry":
			{
				Transform transform = GameObject.Find("GameRun").transform;
				Game game = transform.GetComponent(typeof(Game)) as Game;
				game.Retry();
				audios.PlayAudio("UIclick");
				GameObject gameObject4 = GameObject.Find("TUI/TUIControl");
				menu menu5 = gameObject4.GetComponent(typeof(menu)) as menu;
				menu5.ShowGame(true);
				globalVal.GamePause = false;
				Time.timeScale = 1f;
				break;
			}
			case "option_resume":
			{
				GameObject gameObject3 = GameObject.Find("TUI/TUIControl");
				menu menu4 = gameObject3.GetComponent(typeof(menu)) as menu;
				globalVal.GamePause = false;
				menu4.ShowGame(true);
				menu4.ShowTitle();
				OpenClikPlugin.Hide();
				Time.timeScale = 1f;
				if (globalVal.GameStatus == GAME_STATUS.GAME_RUNNING)
				{
					globalVal.GameStatus = GAME_STATUS.GAME_READY;
				}
				if (globalVal.GameStatus == GAME_STATUS.GAME_DIALOG)
				{
					menu4.ShowItemDialog(true);
				}
				globalVal.ReadyTime = Time.realtimeSinceStartup;
				audios.PlayAudio("UIback");
				break;
			}
			case "onoff_sound":
				globalVal.sound = !globalVal.sound;
				TAudioManager.instance.isSoundOn = globalVal.sound;
				audios.PlayAudio("UIclick");
				UpdateSoundState();
				break;
			case "onoff_music":
				globalVal.music = !globalVal.music;
				TAudioManager.instance.isMusicOn = globalVal.music;
				audios.PlayAudio("UIclick");
				UpdateMusicState();
				break;
			case "cancel":
			{
				GameObject gameObject2 = GameObject.Find("TUI/TUIControl");
				menu menu3 = gameObject2.GetComponent(typeof(menu)) as menu;
				menu3.ShowItemDialog(false);
				globalVal.GameStatus = globalVal.NextGameStatus;
				Debug.Log("globalVal.GameStatus = " + globalVal.GameStatus);
				break;
			}
			case "ok":
			{
				Debug.Log("globalVal.CurItemName = " + globalVal.CurItemName);
				GameItem.GetInstance().UseItem(globalVal.CurItemName);
				GameObject gameObject = GameObject.Find("TUI/TUIControl");
				menu menu2 = gameObject.GetComponent(typeof(menu)) as menu;
				menu2.ShowItemDialog(false);
				break;
			}
			case "menu":
				goMenu();
				break;
			case "retry":
				Application.LoadLevel("scene_loading");
				globalVal.UIState = UILayer.LOAD;
				globalVal.isskipkey = false;
				break;
			case "thestash":
				globalVal.UIState = UILayer.THESTASH;
				Application.LoadLevel("scene_ui_thestash");
				audios.PlayAudio("UIclick");
				break;
			case "twitter":
				Application.OpenURL("http://www.twitter.com/TRINITIgames");
				audios.PlayAudio("UIclick");
				break;
			}
			return;
		}
		if (eventType == 3 && control.GetType() == typeof(TUIButtonClick_Pressed))
		{
			switch (control.name)
			{
			case "thestash_avatar":
				MonoBehaviour.print(globalVal.UIState.ToString());
				if (globalVal.UIState == UILayer.GAMEOVER)
				{
					GameObject gameObject21 = GameObject.Find("TUI/TUIControl");
				}
				else
				{
					globalVal.UIState = UILayer.AVATAR;
					Application.LoadLevel("scene_ui_avatar");
				}
				audios.PlayAudio("UIclick");
				break;
			case "thestash_items":
				if (globalVal.UIState == UILayer.GAMEOVER)
				{
					GameObject gameObject20 = GameObject.Find("TUI/TUIControl");
				}
				else
				{
					globalVal.UIState = UILayer.ITEMS;
					Application.LoadLevel("scene_ui_items");
				}
				audios.PlayAudio("UIclick");
				break;
			case "thestash_upgrades":
				if (globalVal.UIState == UILayer.GAMEOVER)
				{
					GameObject gameObject19 = GameObject.Find("TUI/TUIControl");
				}
				else
				{
					globalVal.UIState = UILayer.UPGRADES;
					Application.LoadLevel("scene_ui_upgrade");
				}
				audios.PlayAudio("UIclick");
				break;
			}
			return;
		}
		if (eventType == 1 && control.GetType() == typeof(TUIButtonSelect))
		{
			switch (control.name)
			{
			case "avatar_item":
			{
				int num4 = (int)((control.transform.localPosition.y - 60f) / 65f);
				MonoBehaviour.print("item index = " + num4 + " control.transform.localPosition.y = " + control.transform.localPosition.y);
				num4 *= -1;
				GameObject gameObject22 = GameObject.Find("TUI/TUIControl");
				if (globalVal.UIState != UILayer.GAMEOVER)
				{
					menu menu9 = gameObject22.GetComponent(typeof(menu)) as menu;
					menu9.ShowDialog(true, num4);
				}
				break;
			}
			}
			return;
		}
		if ((eventType == 1 && control.GetType() == typeof(TUIButtonPush)) || (eventType == 2 && control.GetType() == typeof(TUIButtonPush)))
		{
			switch (control.name)
			{
			case "onoff_music_1":
			case "onoff_music_2":
			case "onoff_music":
				globalVal.music = !globalVal.music;
				TAudioManager.instance.isMusicOn = globalVal.music;
				audios.PlayAudio("UIclick");
				UpdateMusicState();
				break;
			case "onoff_sound_1":
			case "onoff_sound_2":
			case "onoff_sound":
				globalVal.sound = !globalVal.sound;
				TAudioManager.instance.isSoundOn = globalVal.sound;
				audios.PlayAudio("UIclick");
				UpdateSoundState();
				break;
			}
			return;
		}
		switch (eventType)
		{
		case 2:
			if ("touch" == control.name)
			{
				globalVal.MoveDistance = wparam;
				globalVal.MoveEnd = false;
			}
			break;
		case 3:
			if ("touch" == control.name)
			{
				globalVal.MoveEnd = true;
			}
			break;
		}
	}

	public void UpdateMusicState()
	{
		GameObject gameObject = GameObject.Find("TUI/TUIControl");
		TUIButtonPush tUIButtonPush = null;
		if (globalVal.UIState == UILayer.PAUSE)
		{
			tUIButtonPush = gameObject.transform.Find("pause_page/onoff_music_1").GetComponent(typeof(TUIButtonPush)) as TUIButtonPush;
			tUIButtonPush.SetPressed(globalVal.music);
			tUIButtonPush = gameObject.transform.Find("pause_page/onoff_music_2").GetComponent(typeof(TUIButtonPush)) as TUIButtonPush;
			tUIButtonPush.SetPressed(!globalVal.music);
		}
		else if (globalVal.UIState == UILayer.OPTIONS)
		{
			tUIButtonPush = gameObject.transform.Find("option/onoff_music_1").GetComponent(typeof(TUIButtonPush)) as TUIButtonPush;
			tUIButtonPush.SetPressed(globalVal.music);
			tUIButtonPush = gameObject.transform.Find("option/onoff_music_2").GetComponent(typeof(TUIButtonPush)) as TUIButtonPush;
			tUIButtonPush.SetPressed(!globalVal.music);
		}
		else if (globalVal.UIState == UILayer.GAME)
		{
			TUIMeshSprite tUIMeshSprite = gameObject.transform.Find("option/musicpic").GetComponent(typeof(TUIMeshSprite)) as TUIMeshSprite;
			if (globalVal.music)
			{
				tUIMeshSprite.frameName = "music_on";
			}
			else
			{
				tUIMeshSprite.frameName = "music_off";
			}
			tUIMeshSprite.UpdateMesh();
		}
	}

	public void UpdateSoundState()
	{
		GameObject gameObject = GameObject.Find("TUI/TUIControl");
		TUIButtonPush tUIButtonPush = null;
		if (globalVal.UIState == UILayer.PAUSE)
		{
			tUIButtonPush = gameObject.transform.Find("pause_page/onoff_sound_1").GetComponent(typeof(TUIButtonPush)) as TUIButtonPush;
			tUIButtonPush.SetPressed(globalVal.sound);
			tUIButtonPush = gameObject.transform.Find("pause_page/onoff_sound_2").GetComponent(typeof(TUIButtonPush)) as TUIButtonPush;
			tUIButtonPush.SetPressed(!globalVal.sound);
		}
		else if (globalVal.UIState == UILayer.OPTIONS)
		{
			tUIButtonPush = gameObject.transform.Find("option/onoff_sound_1").GetComponent(typeof(TUIButtonPush)) as TUIButtonPush;
			tUIButtonPush.SetPressed(globalVal.sound);
			tUIButtonPush = gameObject.transform.Find("option/onoff_sound_2").GetComponent(typeof(TUIButtonPush)) as TUIButtonPush;
			tUIButtonPush.SetPressed(!globalVal.sound);
		}
		else if (globalVal.UIState == UILayer.GAME)
		{
			TUIMeshSprite tUIMeshSprite = gameObject.transform.Find("option/soundpic").GetComponent(typeof(TUIMeshSprite)) as TUIMeshSprite;
			if (globalVal.sound)
			{
				tUIMeshSprite.frameName = "sound_on";
			}
			else
			{
				tUIMeshSprite.frameName = "sound_off";
			}
			tUIMeshSprite.UpdateMesh();
		}
	}

	private void goMenu()
	{
		Transform transform = GameObject.Find("Main Camera").transform;
		Game game = transform.GetComponent(typeof(Game)) as Game;
		if (null != game)
		{
			game.Destroy();
		}
		Application.LoadLevel("scene_ui_menu");
		globalVal.UIState = UILayer.MENU;
		audios.PlayAudio("UIclick");
		globalVal.GamePause = false;
		Time.timeScale = 1f;
	}

	public void doSwitch(bool bOpen, string strNextLevel)
	{
		if (null != m_cmSwitchShader)
		{
			Switch component = m_cmSwitchShader.GetComponent<Switch>();
			if (bOpen)
			{
				component.Open();
			}
			else
			{
				component.Close(strNextLevel);
			}
		}
	}
}
