using System.Collections;
using UnityEngine;

public class EffectManager : MonoBehaviour
{
	private bool updateKey;

	private GameObject player;

	private GameObject effectObj;

	private float startTime;

	private float endTime;

	private Transform trans;

	private GameObject InstantObj;

	public GameObject[] effData = new GameObject[0];

	public ArrayList effGold = new ArrayList();

	private GameObject effground;

	private ArrayList effect_follow = new ArrayList();

	private ArrayList follow_target = new ArrayList();

	private void Start()
	{
		EffectManagerClass.body = this;
		effground = new GameObject("eff");
		InitGoldEffectContainer();
		player = GameObject.Find("player2/Bip01");
		updateKey = false;
		effect_follow.Clear();
		follow_target.Clear();
	}

	private void Update()
	{
		if (updateKey)
		{
			startTime += Time.deltaTime;
			if (startTime < endTime)
			{
				effectObj.transform.position = player.transform.position;
			}
			else
			{
				updateKey = false;
			}
		}
		if (trans != null && InstantObj != null)
		{
			InstantObj.transform.position = trans.position;
		}
		for (int i = 0; i < effect_follow.Count; i++)
		{
			Transform transform = follow_target[i] as Transform;
			if (transform == null)
			{
				follow_target.RemoveAt(i);
				effect_follow.RemoveAt(i);
			}
			Transform transform2 = effect_follow[i] as Transform;
			if (transform2 == null)
			{
				effect_follow.RemoveAt(i);
				follow_target.RemoveAt(i);
			}
			else
			{
				transform2.position = transform.position;
			}
		}
	}

	private void InitGoldEffectContainer()
	{
		effGold.Clear();
		for (int i = 0; i < 20; i++)
		{
			GameObject gameObject = Object.Instantiate(effData[3]) as GameObject;
			gameObject.transform.parent = effground.transform;
			gameObject.name = effData[3].name;
			gameObject.active = false;
			effGold.Add(gameObject);
		}
	}

	private GameObject GetGoldFlyEff()
	{
		GameObject result = null;
		if (effGold.Count < 1)
		{
			return result;
		}
		result = effGold[0] as GameObject;
		effGold.RemoveAt(0);
		return result;
	}

	public void PlayGoldFlyEffect(Transform itemTrans, int value)
	{
		GameObject goldFlyEff = GetGoldFlyEff();
		if (goldFlyEff != null)
		{
			goldFlyEff.active = true;
			GoldEffectScript goldEffectScript = goldFlyEff.GetComponent(typeof(GoldEffectScript)) as GoldEffectScript;
			goldEffectScript.GoldFly(Camera.main.WorldToViewportPoint(itemTrans.position), value);
		}
	}

	public void PlayGoldFlyEffect(Transform itemTrans, int value, bool isbig)
	{
		GameObject goldFlyEff = GetGoldFlyEff();
		if (goldFlyEff != null)
		{
			goldFlyEff.active = true;
			GoldEffectScript goldEffectScript = goldFlyEff.GetComponent(typeof(GoldEffectScript)) as GoldEffectScript;
			if (isbig)
			{
				goldEffectScript.GoldFly_big(Camera.main.WorldToViewportPoint(itemTrans.position), value);
			}
			else
			{
				goldEffectScript.GoldFly(Camera.main.WorldToViewportPoint(itemTrans.position), value);
			}
		}
	}

	public GameObject PlayGoldFlyEffect_UI(Transform itemTrans, int value)
	{
		GameObject goldFlyEff = GetGoldFlyEff();
		if (goldFlyEff != null)
		{
			goldFlyEff.active = true;
			GoldEffectScript goldEffectScript = goldFlyEff.GetComponent(typeof(GoldEffectScript)) as GoldEffectScript;
			Camera cameraByName = GetCameraByName("TUICamera");
			goldEffectScript.GoldFly_UI(cameraByName.WorldToViewportPoint(itemTrans.position), value);
		}
		return goldFlyEff;
	}

	private Camera GetCameraByName(string name)
	{
		Camera[] allCameras = Camera.allCameras;
		Camera camera = null;
		for (int i = 0; i < allCameras.Length; i++)
		{
			camera = allCameras[i];
			if (camera.name == name)
			{
				return camera;
			}
		}
		return camera;
	}

	private void playCloudCrosse(GameObject obj)
	{
		startTime = 0f;
		endTime = 3f;
		updateKey = true;
		effectObj = obj;
	}

	private void playGoodStarEffect(GameObject obj, float time)
	{
		startTime = 0f;
		endTime = time;
		updateKey = true;
		effectObj = obj;
	}

	public void PlayEffect(string name, Transform itemTrans, bool isUpdata)
	{
		GameObject original = null;
		switch (name)
		{
		case "effect_bleeding":
			original = effData[0];
			break;
		case "effect_fireballoon":
			original = effData[15];
			break;
		case "effect_goldtouch":
			original = effData[18];
			break;
		}
		InstantObj = Object.Instantiate(original) as GameObject;
		InstantObj.transform.position = itemTrans.position;
		InstantObj.transform.rotation = itemTrans.rotation;
		if (isUpdata)
		{
			effect_follow.Add(InstantObj.transform);
			follow_target.Add(itemTrans);
		}
	}

	public void PlayEffect(string name, Transform itemTrans)
	{
		Quaternion identity = Quaternion.identity;
		identity = itemTrans.rotation;
		GameObject gameObject = null;
		switch (name)
		{
		case "effect_bleeding":
			gameObject = effData[0];
			break;
		case "effect_blowup":
			gameObject = effData[1];
			break;
		case "effect_fallingstar":
			gameObject = effData[2];
			break;
		case "effect_qiliu_1":
			gameObject = effData[3];
			break;
		case "effect_qiliu_2":
			gameObject = effData[4];
			break;
		case "effect_rockfire":
			gameObject = effData[5];
			break;
		case "effect_cannonfire":
			gameObject = effData[7];
			break;
		case "effect_zombie_dead":
		{
			gameObject = effData[6];
			Vector3 position = itemTrans.position;
			position.y = 0f;
			gameObject.transform.position = position;
			identity = Quaternion.identity;
			break;
		}
		case "effect_cloudtouch":
			gameObject = effData[8];
			break;
		case "effect_cloudcrosse":
			gameObject = effData[9];
			break;
		case "effect_bed":
		{
			gameObject = effData[10];
			Vector3 eulerAngles3 = identity.eulerAngles;
			eulerAngles3.x = 0f;
			identity.eulerAngles = eulerAngles3;
			break;
		}
		case "effect_wagon":
		{
			gameObject = effData[11];
			Vector3 eulerAngles2 = itemTrans.eulerAngles;
			eulerAngles2.y += 180f;
			if (eulerAngles2.y >= 360f)
			{
				eulerAngles2.y -= 360f;
			}
			if (eulerAngles2.y < 0f)
			{
				eulerAngles2.y += 360f;
			}
			identity.eulerAngles = eulerAngles2;
			break;
		}
		case "effect_airliner":
			gameObject = effData[12];
			break;
		case "effect_ufo":
			gameObject = effData[13];
			break;
		case "effect_trampoline":
		{
			gameObject = effData[14];
			Vector3 eulerAngles = itemTrans.eulerAngles;
			eulerAngles.x = 0f;
			identity.eulerAngles = eulerAngles;
			break;
		}
		case "effect_fireballoon":
			gameObject = effData[15];
			break;
		case "effect_speed":
			gameObject = effData[16];
			break;
		case "effect_goodstartouch":
			gameObject = effData[17];
			break;
		case "effect_goldtouch":
			gameObject = effData[18];
			break;
		}
		GameObject gameObject2 = Object.Instantiate(gameObject) as GameObject;
		gameObject2.transform.position = itemTrans.position;
		gameObject2.transform.rotation = identity;
		if (name == "effect_zombie_dead")
		{
			gameObject2.GetComponent<Animation>()["Take 001"].speed = 0.5f;
		}
		if (name == "effect_bed")
		{
			gameObject2.GetComponent<Animation>()["idle"].speed = 0.5f;
		}
		if (name == "effect_airliner")
		{
			gameObject2.transform.parent = effground.transform;
			gameObject2.GetComponent<Rigidbody>().velocity = new Vector3(player.GetComponent<Rigidbody>().velocity.x * 1.5f, 0f, player.GetComponent<Rigidbody>().velocity.x * 0.3f);
		}
		if (name == "effect_ufo")
		{
			gameObject2.GetComponent<Rigidbody>().velocity = new Vector3(75f, -30f, 0f);
		}
		if (name == "effect_cloudcrosse")
		{
			playCloudCrosse(gameObject2);
		}
		if (name == "effect_goodstartouch")
		{
			trans = null;
			playGoodStarEffect(gameObject2, 3f);
		}
		if (name == "effect_bleeding")
		{
			float num = 0f;
			int layerMask = 1 << LayerMask.NameToLayer("floor");
			RaycastHit hitInfo;
			if (Physics.Raycast(gameObject2.transform.position, Vector3.down, out hitInfo, 100f, layerMask) && hitInfo.transform != null)
			{
				num = hitInfo.point.y;
			}
			Vector3 position2 = gameObject2.transform.position;
			position2.y = 0.07f + num;
			gameObject2.transform.position = position2;
			gameObject2.transform.rotation = Quaternion.identity;
		}
	}
}
