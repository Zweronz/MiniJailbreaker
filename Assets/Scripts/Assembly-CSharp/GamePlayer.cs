using UnityEngine;

public class GamePlayer
{
	private const int c_nListCount = 0;

	private const string c_strCharacterPath = "Prefabs/Character/Chara";

	private const float c_fJumpTime = 0.75f;

	private const float c_fKnockSpeed = 20f;

	private const float c_fMaxWelterSpeed = 30f;

	private Vector3[] m_v3PointList;

	private string[] m_strAnimList;

	private int m_nListIndex;

	private Transform m_cmPlayer;

	private Transform m_cmBip;

	private GameCharaAnimation m_cmAnimation;

	private CharaInfo m_cmInfo;

	private GameBuffer m_cmBuffer;

	private GameItem m_cmItem;

	private bool m_bGround;

	private bool m_bJump;

	private float m_fJumpPower;

	private bool m_bAcme;

	private Transform m_cmLastButton;

	private bool m_bCollider;

	private Transform m_cmCollider;

	private float m_fVertical;

	private float m_fHorizontal;

	private float m_fMoveDistance;

	private Transform m_cmShadow;

	private Transform m_cmSky;

	private Transform m_cmSearchLight1;

	private Transform m_cmSearchLight2;

	private Transform m_cmLightWay;

	private float m_fDValue;

	private Transform m_cmOldWayPoint;

	private Vector3 m_v3NewForward;

	private int m_nMaxAmmunition;

	private int m_nCurAmmunition;

	private bool m_bFire;

	private float m_fLastFireTime;

	private bool m_bReloadShot;

	private Transform m_cmEffect;

	private Transform m_cmWayAxis;

	private Transform m_cmPlane;

	private float m_fJumpTime;

	private float m_fDown;

	private CharaMoviePoint m_cmMoviePoints;

	private Transform m_cmMovieObject;

	private int m_nCurActionPointIndex;

	private CharaAction m_cmTarget;

	private string m_strLastJump;

	private Vector3 m_v3OldPoint;

	private int c_nSpeedRate = 1000;

	private float m_fSpeedUpTime;

	private bool m_bShowSpeedUP;

	private WayPointNode m_cmCurPoint;

	private WayPointNode m_cmNextPoint;

	private float c_fCheckDistance = 3f;

	private string m_strDeath;

	private bool m_bDead;

	private string c_strResurrection = "resurrect";

	private Vector3 m_v3KnockPoint;

	private float m_fWelterSpeed;

	private bool m_bWelter;

	private int m_nPlaybackIndex;

	private int m_nPlaybackSpeed;

	public Transform Player
	{
		get
		{
			return m_cmPlayer;
		}
	}

	public Transform PlayerBip
	{
		get
		{
			return m_cmBip;
		}
	}

	public Transform WayPoint
	{
		get
		{
			return m_cmWayAxis;
		}
	}

	public Vector3 position
	{
		get
		{
			return m_cmPlayer.position;
		}
	}

	public float MoveDistance
	{
		get
		{
			float fMoveDistance = m_fMoveDistance;
			m_fMoveDistance = 0f;
			return fMoveDistance;
		}
	}

	public void Initialize()
	{
		if (null == m_cmPlayer)
		{
			GameObject gameObject = (GameObject)Object.Instantiate(Resources.Load("Prefabs/Character/Chara" + (globalVal.g_avatar_id + 1)));
			m_cmPlayer = gameObject.transform;
			m_cmPlayer.name = "Charactor";
			m_cmWayAxis = new GameObject("WayAxis").transform;
			m_cmPlayer.parent = m_cmWayAxis;
			m_cmBip = m_cmPlayer.Find("Bip01");
			m_fDValue = m_cmBip.position.y - m_cmPlayer.position.y;
		}
		m_cmWayAxis.position = new Vector3(0f, 0f, -100f);
		m_cmWayAxis.forward = Vector3.forward;
		m_cmPlayer.localPosition = Vector3.zero;
		m_cmPlayer.localEulerAngles = Vector3.zero;
		m_cmInfo = globalVal.CharaInfo;
		if (m_cmAnimation == null)
		{
			m_cmAnimation = new GameCharaAnimation();
			m_cmAnimation.Initialize(m_cmPlayer, m_cmInfo);
		}
		if (m_cmItem == null)
		{
			m_cmItem = GameItem.GetInstance();
			m_cmItem.setPlayer(m_cmPlayer);
		}
		m_bAcme = true;
		m_fJumpPower = 1f;
		m_fLastFireTime = Time.realtimeSinceStartup;
		if (null == m_cmSky)
		{
			m_cmSky = ((GameObject)Object.Instantiate(Resources.Load("Prefabs/Character/Charasky"))).transform;
			if (null != m_cmSky)
			{
				m_cmSky.position = Vector3.zero;
			}
		}
		if (m_cmBuffer == null)
		{
			m_cmBuffer = GameBuffer.GetInstance();
			m_cmBuffer.setPlayer(m_cmWayAxis);
		}
		m_cmNextPoint = null;
		if (m_v3PointList != null)
		{
			m_v3PointList = null;
		}
		m_v3PointList = new Vector3[0];
		if (m_strAnimList == null)
		{
			m_strAnimList = new string[0];
		}
		m_nListIndex = 0;
		if (null == m_cmSearchLight1)
		{
			m_cmSearchLight1 = ((GameObject)Object.Instantiate(Resources.Load("Prefabs/Character/Charalight1"))).transform;
			m_cmSearchLight1.parent = m_cmWayAxis;
			m_cmSearchLight1.localPosition = new Vector3(0f, 0.51f, 0f);
		}
		if (null == m_cmSearchLight2)
		{
			m_cmSearchLight2 = ((GameObject)Object.Instantiate(Resources.Load("Prefabs/Character/Charalight2"))).transform;
			m_cmSearchLight2.parent = m_cmWayAxis;
			m_cmSearchLight2.localPosition = new Vector3(0f, 0.51f, 0f);
		}
		if (null == m_cmPlane)
		{
			m_cmPlane = ((GameObject)Object.Instantiate(Resources.Load("Prefabs/Character/CharaPlane"))).transform;
			m_cmPlane.position = new Vector3(1000f, 1000f, 1000f);
			Plane plane = m_cmPlane.GetComponent(typeof(Plane)) as Plane;
			plane.Player = m_cmWayAxis;
			plane.SearchLight1 = m_cmSearchLight1;
			plane.SearchLight2 = m_cmSearchLight2;
		}
		if (null == m_cmLightWay)
		{
			m_cmLightWay = ((GameObject)Object.Instantiate(Resources.Load("Prefabs/Character/CharaLightWay"))).transform;
			m_cmLightWay.parent = m_cmWayAxis;
			m_cmLightWay.localPosition = new Vector3(0f, 0.51f, 0f);
			m_cmLightWay.name = "LightWay";
			m_cmLightWay.gameObject.SetActive(false);
		}
		if (null == m_cmEffect)
		{
			m_cmEffect = globalVal.Effect.getEffect(m_cmInfo.MoveEffect);
			m_cmEffect.parent = m_cmPlayer;
			m_cmEffect.localPosition = Vector3.zero;
		}
		if (null == m_cmShadow)
		{
			GameObject gameObject2 = (GameObject)Object.Instantiate(Resources.Load("Prefabs/Character/CharaShadow"));
			m_cmShadow = gameObject2.transform;
			m_cmShadow.parent = m_cmWayAxis;
		}
		globalVal.ItemCheckRange = 1f;
	}

	public void Reset()
	{
		m_cmNextPoint = null;
		m_cmCurPoint = null;
		m_cmWayAxis.forward = Vector3.forward;
		m_cmPlayer.forward = Vector3.forward;
		m_cmShadow.localPosition = Vector3.zero;
		m_fHorizontal = 0f;
		globalVal.GameJumpClick = false;
		m_cmBuffer.clearBuffer();
		m_cmAnimation.Reset();
		m_bDead = false;
	}

	public void Destroy()
	{
	}

	public void recordInput()
	{
		m_fHorizontal = 0f - Input2.acceleration.x;
		if (-0.01f <= m_fHorizontal && 0.01 >= (double)m_fHorizontal)
		{
			m_fHorizontal = 0f;
		}
		else if (-0.3f >= m_fHorizontal)
		{
			m_fHorizontal = -0.3f;
		}
		else if (0.3f <= m_fHorizontal)
		{
			m_fHorizontal = 0.3f;
		}
		if (0f > Input2.acceleration.y)
		{
			m_fHorizontal *= -1f;
		}
		m_fHorizontal *= 0.1f;
		if (globalVal.Exchange)
		{
			m_fHorizontal *= -1f;
		}
	}

	public void doAction()
	{
		m_cmBuffer.Run();
		m_cmItem.Run();
		addShadow();
		autoSpeed();
		float skyAnimation = (globalVal.SpeedPower + 100f) / 100f * (1f + (float)globalVal.RandomType * 0.05f);
		setSkyAnimation(skyAnimation);
	}

	public RaycastHit getHitPoint(Vector3 v3Start, Vector3 v3End)
	{
		float distance = Vector3.Distance(v3Start, v3End);
		Vector3 direction = Vector3.Normalize(v3End - v3Start);
		RaycastHit hitInfo;
		if (Physics.Raycast(v3Start, direction, out hitInfo, distance, 1 << LayerMask.NameToLayer("Default")))
		{
			Debug.Log("hit.name = " + hitInfo.transform.name);
		}
		return hitInfo;
	}

	private bool checkGround()
	{
		float num = 5f;
		num += m_cmBip.position.y - m_cmPlayer.position.y;
		Vector3 origin = m_cmBip.position;
		RaycastHit hitInfo;
		if (Physics.Raycast(origin, Vector3.down, out hitInfo, num, 1 << LayerMask.NameToLayer("Default")))
		{
			TerrainTrigger[] components = hitInfo.transform.GetComponents<TerrainTrigger>();
			if (components != null && 0 < components.Length)
			{
				for (int i = 0; i < components.Length; i++)
				{
					components[i].Trigger(hitInfo.point);
				}
			}
		}
		return globalVal.OnGround;
	}

	public float checkFun()
	{
		Vector3 vector = m_cmPlayer.position;
		vector.y = m_cmBip.position.y;
		Vector3 v3End = vector;
		float num = m_fHorizontal * 450f * Time.deltaTime;
		float num2 = (globalVal.SpeedPower + 100f) / 100f * (1f + (float)globalVal.RandomType * 0.05f);
		num2 *= m_cmInfo.Speed * Time.deltaTime;
		v3End.x += num;
		v3End.z += num2;
		RaycastHit hitPoint = getHitPoint(vector, v3End);
		if (null != hitPoint.transform)
		{
			Vector3 normal = hitPoint.normal;
			normal.y = 0f;
			if (75f >= Vector3.Angle(normal, -m_cmPlayer.forward))
			{
				v3End.z = hitPoint.point.z;
				v3End.z -= 0.2f;
				m_cmCollider = hitPoint.transform;
				m_strDeath = hitPoint.transform.name;
				if (null != m_cmCollider.GetComponent<Animation>() && null != m_cmCollider.GetComponent<Animation>()["002"])
				{
					m_cmCollider.GetComponent<Animation>()["002"].wrapMode = WrapMode.Once;
					m_cmCollider.GetComponent<Animation>().PlayQueued("002");
				}
				doDead(false);
			}
			else
			{
				v3End.x = hitPoint.point.x;
				if (0f > m_fHorizontal)
				{
					v3End.x += 0.5f;
				}
				else if (0f < m_fHorizontal)
				{
					v3End.x -= 0.5f;
				}
			}
		}
		v3End.y -= m_fDValue;
		if (-2.5 > (double)v3End.x)
		{
			v3End.x = -2.5f;
		}
		else if (2.5f < v3End.x)
		{
			v3End.x = 2.5f;
		}
		num = v3End.z - vector.z;
		vector = m_cmPlayer.position;
		vector.x = v3End.x;
		m_cmPlayer.position = vector;
		vector = m_cmWayAxis.position;
		vector.z = v3End.z;
		m_cmWayAxis.position = vector;
		return num;
	}

	private Vector3 PlayerCollider(bool isLeft, float fDistance)
	{
		Vector3 point = m_cmPlayer.position;
		point.y += 0.5f;
		Vector3 point2 = m_cmPlayer.position;
		point2.y += 1.5f;
		Vector3 zero = Vector3.zero;
		Vector3 vector = m_cmPlayer.right;
		if (isLeft)
		{
			vector = -vector;
		}
		float num = fDistance;
		RaycastHit[] array = Physics.CapsuleCastAll(point, point2, 0.4f, vector, fDistance, 1 << LayerMask.NameToLayer("Default"));
		point2 = Vector3.zero;
		RaycastHit[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			RaycastHit raycastHit = array2[i];
			if (!(30f < Vector3.Angle(raycastHit.normal, -vector)))
			{
				zero = raycastHit.point;
				zero.y = m_cmPlayer.position.y;
				fDistance = Vector3.Distance(m_cmPlayer.position, zero);
				if (num > fDistance)
				{
					num = fDistance;
					point2 = zero;
				}
			}
		}
		return point2;
	}

	private Vector3 getGravity(bool bJump, float fLift, float fGravity)
	{
		Vector3 zero = Vector3.zero;
		float num = 0f;
		if (globalVal.OnGround)
		{
			m_fDown = 0f;
		}
		if (!m_bAcme)
		{
			m_bAcme = m_fVertical < 0f;
		}
		else if (globalVal.OnGround && Mathf.Abs(num) >= 0.5f)
		{
			zero.y = 0f - num;
		}
		else if (Mathf.Abs(num) < Mathf.Abs(zero.y))
		{
			zero.y = 0f - num;
		}
		return zero;
	}

	private Vector3 getHorizontal(float fDistance)
	{
		Vector3 zero = Vector3.zero;
		if (0f > m_fHorizontal)
		{
			if (Vector3.zero != (zero = PlayerCollider(true, Mathf.Abs(fDistance) + 0.5f)))
			{
				fDistance = Vector3.Distance(m_cmPlayer.position, zero);
				fDistance -= 0.5f;
				fDistance *= -1f;
			}
			else if (Vector3.zero != (zero = PlayerCollider(false, 2f)))
			{
				float num = Vector3.Distance(m_cmPlayer.position, zero);
				num -= 0.5f;
				if (0f > num && num < fDistance)
				{
					fDistance = num;
				}
			}
		}
		else if (0f < m_fHorizontal)
		{
			if (Vector3.zero != (zero = PlayerCollider(false, Mathf.Abs(fDistance) + 0.5f)))
			{
				fDistance = Vector3.Distance(m_cmPlayer.position, zero);
				fDistance -= 0.5f;
			}
			else if (Vector3.zero != (zero = PlayerCollider(true, 2f)))
			{
				float num2 = Vector3.Distance(m_cmPlayer.position, zero);
				num2 -= 0.5f;
				num2 *= -1f;
				if (0f < num2 && num2 > fDistance)
				{
					fDistance = num2;
				}
			}
		}
		else if (m_fHorizontal == 0f)
		{
			if (Vector3.zero != (zero = PlayerCollider(true, 2f)))
			{
				float num3 = Vector3.Distance(m_cmPlayer.position, zero);
				num3 -= 0.5f;
				num3 *= -1f;
				if (0f < num3)
				{
					fDistance = num3;
				}
			}
			else if (Vector3.zero != (zero = PlayerCollider(false, 2f)))
			{
				float num4 = Vector3.Distance(m_cmPlayer.position, zero);
				num4 -= 0.5f;
				if (0f > num4)
				{
					fDistance = num4;
				}
			}
		}
		return fDistance * m_cmWayAxis.right;
	}

	private float getAdvance(float fDistance)
	{
		Vector3 vector = m_cmPlayer.position;
		Vector3 vector2 = m_cmPlayer.position;
		vector2.y += 0.8f;
		vector = m_cmPlayer.position;
		vector.y += 0.8f;
		vector2 = m_cmPlayer.position;
		vector2.y += 1.6f;
		RaycastHit[] array = Physics.CapsuleCastAll(vector, vector2, 0.4f, m_cmPlayer.forward, fDistance + 0.4f, 1 << LayerMask.NameToLayer("Default"));
		RaycastHit[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			RaycastHit raycastHit = array2[i];
			if (!("R" == raycastHit.transform.name) && !("L" == raycastHit.transform.name) && !(45f < Vector3.Angle(raycastHit.normal, -m_cmPlayer.forward)))
			{
				m_cmCollider = raycastHit.transform;
				m_strDeath = raycastHit.transform.name;
				if (null != m_cmCollider.GetComponent<Animation>() && null != m_cmCollider.GetComponent<Animation>()["002"])
				{
					m_cmCollider.GetComponent<Animation>()["002"].wrapMode = WrapMode.Once;
					m_cmCollider.GetComponent<Animation>().PlayQueued("002");
				}
				if (m_cmPlayer.position.y + 0.7f >= raycastHit.point.y)
				{
					doDead(false);
				}
				else
				{
					doDead(true);
				}
				if (m_bDead)
				{
					return fDistance;
				}
			}
		}
		return fDistance;
	}

	private void showSearchLight(bool bMust)
	{
		bool flag = bMust;
		if (!bMust)
		{
			flag = Random.RandomRange(1, 100) <= 1;
		}
		if (flag && null != m_cmPlane)
		{
			(m_cmPlane.GetComponent(typeof(Plane)) as Plane).StartFly(bMust);
		}
	}

	public Vector3 Move(CharaInfo cmInfo)
	{
		Vector3 gravity = getGravity(m_bJump, cmInfo.Lift, cmInfo.Gravity);
		float fDistance = m_fHorizontal * 450f * Time.deltaTime;
		Vector3 horizontal = getHorizontal(fDistance);
		float num = (globalVal.SpeedPower + 100f) / 100f * (1f + (float)globalVal.RandomType * 0.05f);
		setSkyAnimation(num);
		float num2 = cmInfo.Speed * num;
		if (!m_bAcme && 0.03f < gravity.y)
		{
			string jump = cmInfo.Jump1;
			m_cmAnimation.setCharacterAnimationByName(jump, WrapMode.ClampForever, 1);
		}
		else if (!globalVal.OnGround && 0.03f >= gravity.y)
		{
			string jump = cmInfo.Jump1;
			m_cmAnimation.setCharacterAnimationByName(jump, WrapMode.ClampForever, 1);
		}
		else
		{
			string jump = cmInfo.Run;
			m_cmAnimation.setCharacterAnimationByName(jump);
		}
		float y = m_cmPlayer.position.y;
		Vector3 vector = m_cmPlayer.position;
		fDistance = 100f;
		if (m_cmNextPoint != null && null != m_cmNextPoint.WayPoint)
		{
			fDistance = m_cmNextPoint.WayPoint.position.z - m_cmWayAxis.position.z;
		}
		if (fDistance < num2 * Time.deltaTime * 3f)
		{
			m_cmCurPoint = m_cmNextPoint;
			if (3f >= m_cmNextPoint.NextPoint.WayPoint.position.z - m_cmNextPoint.WayPoint.position.z)
			{
				m_cmNextPoint = m_cmNextPoint.NextPoint.NextPoint;
			}
			else
			{
				m_cmNextPoint = m_cmNextPoint.NextPoint;
			}
			globalVal.AreaType = m_cmNextPoint.Area;
			Vector3 forward = m_cmPlayer.forward;
			m_cmWayAxis.forward = m_cmNextPoint.WayPoint.position - m_cmWayAxis.position;
			m_cmPlayer.forward = forward;
			if (m_cmCurPoint.SearchLight == ENUM_SHOW_SEARCHLIGHT.SEARCHLIGHT_SHOW_NORMAL)
			{
				showSearchLight(false);
			}
			else if (m_cmCurPoint.SearchLight == ENUM_SHOW_SEARCHLIGHT.SEARCHLIGHT_SHOW_NE)
			{
				showSearchLight(true);
			}
			GameObject gameObject = GameObject.Find("TUI/TUIControl");
			menu menu2 = gameObject.GetComponent(typeof(menu)) as menu;
			menu2.setRoadName(m_cmCurPoint.Name);
		}
		fDistance = num2 * Time.deltaTime;
		if (!m_cmBuffer.canTrap() && 0f < fDistance)
		{
			fDistance = getAdvance(fDistance);
		}
		Vector3 vector2 = fDistance * m_cmWayAxis.forward;
		m_cmWayAxis.position += vector2;
		m_cmPlayer.position = new Vector3(m_cmPlayer.position.x, y, m_cmPlayer.position.z);
		fDistance = m_cmWayAxis.position.z - vector.z;
		globalVal.CurDistance += fDistance;
		m_cmPlayer.position += horizontal;
		m_cmPlayer.position += gravity;
		return gravity;
	}

	public void setSkyAnimation(float fRate)
	{
		TextureAnimation textureAnimation = null;
		if (null == m_cmSky)
		{
			return;
		}
		Transform transform = m_cmSky.Find("Skyway_sky/back02").transform;
		if (null != transform)
		{
			textureAnimation = transform.GetComponent(typeof(TextureAnimation)) as TextureAnimation;
			if (null != textureAnimation)
			{
				textureAnimation.OffsetX = fRate * 0.3f;
			}
		}
		transform = m_cmSky.Find("Skyway_sky/back03").transform;
		if (null != transform)
		{
			textureAnimation = transform.GetComponent(typeof(TextureAnimation)) as TextureAnimation;
			if (null != textureAnimation)
			{
				textureAnimation.OffsetX = fRate * 0.15f;
			}
		}
	}

	public void PlayMoive()
	{
		if (null == m_cmMoviePoints)
		{
			globalVal.GameStatus = GAME_STATUS.GAME_RUNNING;
			return;
		}
		if (m_cmTarget == null)
		{
			m_nCurActionPointIndex = 0;
			m_cmTarget = m_cmMoviePoints.getNextPoint(m_nCurActionPointIndex);
		}
		m_cmPlayer.forward = m_cmTarget.TargetObject.position - m_cmPlayer.position;
		float num = (float)m_cmTarget.Speed * Time.deltaTime;
		if (num >= Vector3.Distance(m_cmPlayer.position, m_cmTarget.TargetObject.position))
		{
			m_cmPlayer.position = m_cmTarget.TargetObject.position;
			m_nCurActionPointIndex++;
			m_cmTarget = m_cmMoviePoints.getNextPoint(m_nCurActionPointIndex);
			if (m_cmTarget == null)
			{
				m_cmMoviePoints = null;
				m_cmWayAxis.position = m_cmPlayer.position;
				m_cmPlayer.localPosition = Vector3.zero;
				m_cmCurPoint = m_cmNextPoint;
				m_cmNextPoint = m_cmNextPoint.NextPoint;
				globalVal.AreaType = m_cmNextPoint.Area;
			}
		}
		else
		{
			m_cmPlayer.position += m_cmPlayer.forward * num;
		}
		if (m_cmTarget != null)
		{
			m_cmAnimation.setCharacterAnimationByName(m_cmTarget.AnimationName);
		}
		if (null != m_cmSky)
		{
			m_cmSky.position = m_cmPlayer.position;
		}
	}

	private bool checkDrop()
	{
		Collider[] array = Physics.OverlapSphere(m_cmPlayer.position, 0.5f, 1 << LayerMask.NameToLayer("Default"));
		if (0 >= array.Length)
		{
			return true;
		}
		return false;
	}

	public void PhysicsMove()
	{
		if (m_bDead)
		{
			return;
		}
		checkGround();
		m_fJumpTime += Time.deltaTime;
		if (m_fJumpTime >= 0.75f)
		{
			if (globalVal.GameJumpClick)
			{
				if (50 >= Random.RandomRange(1, 100))
				{
					m_strLastJump = m_cmInfo.Jump1;
				}
				else
				{
					m_strLastJump = m_cmInfo.Jump2;
				}
				m_cmAnimation.setCharacterAnimationByName(m_strLastJump, WrapMode.Once, 0);
				m_bJump = false;
				globalVal.JumpStart = true;
				m_fJumpTime = 0f;
			}
			else
			{
				m_cmAnimation.setCharacterAnimationByName(m_cmInfo.Run, WrapMode.Loop, 0);
				if (checkDrop())
				{
					m_strDeath = "drop";
					doDead();
				}
			}
		}
		globalVal.GameJumpClick = false;
		float num = checkFun();
		globalVal.CurDistance += num;
		if (null != m_cmSky)
		{
			m_cmSky.position = m_cmWayAxis.position;
			if (m_cmCurPoint != null && globalVal.Config.getAreaSky(m_cmCurPoint.Area) != m_cmSky.gameObject.activeInHierarchy)
			{
				m_cmSky.gameObject.SetActiveRecursively(!m_cmSky.gameObject.activeInHierarchy);
			}
		}
		if (null != m_cmEffect)
		{
			if (globalVal.JumpStart && m_cmEffect.gameObject.activeInHierarchy)
			{
				m_cmEffect.gameObject.SetActiveRecursively(false);
			}
			else if (!globalVal.JumpStart && !m_cmEffect.gameObject.activeInHierarchy)
			{
				m_cmEffect.gameObject.SetActiveRecursively(true);
			}
		}
		checkItem();
		axisAmend();
		if (m_cmNextPoint == null)
		{
			m_cmNextPoint = globalVal.Terrain.getHead();
		}
		else if (m_cmWayAxis.position.z >= m_cmNextPoint.WayPoint.position.z)
		{
			m_cmCurPoint = m_cmNextPoint;
			m_cmNextPoint = m_cmNextPoint.NextPoint;
			if (m_cmCurPoint.SearchLight == ENUM_SHOW_SEARCHLIGHT.SEARCHLIGHT_SHOW_NORMAL)
			{
				showSearchLight(false);
			}
		}
	}

	public bool checkMapButton()
	{
		RaycastHit hitInfo;
		if (Physics.Raycast(m_cmPlayer.position, m_cmPlayer.forward, out hitInfo, 3f, 256) && null != hitInfo.transform && "MapButton" == hitInfo.transform.name && m_cmLastButton != hitInfo.transform)
		{
			m_cmLastButton = hitInfo.transform;
			return true;
		}
		return false;
	}

	public void checkItemByMove()
	{
		Vector3 direction = Vector3.Normalize(m_cmPlayer.position - m_v3OldPoint);
		float distance = Vector3.Distance(m_cmPlayer.position, m_v3OldPoint);
		Vector3 v3OldPoint = m_v3OldPoint;
		v3OldPoint.y += 2f;
		RaycastHit[] array = Physics.CapsuleCastAll(m_v3OldPoint, v3OldPoint, 0.5f, direction, distance, 1 << LayerMask.NameToLayer("Item"));
		RaycastHit[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			RaycastHit raycastHit = array2[i];
			if (globalVal.Config.isItem(raycastHit.transform.name))
			{
				m_cmItem.targetItem(raycastHit.transform);
			}
		}
	}

	public void checkItem()
	{
		if (globalVal.ItemCheckRange == 0f)
		{
			return;
		}
		Collider[] array = Physics.OverlapSphere(m_cmBip.position, globalVal.ItemCheckRange, 1 << LayerMask.NameToLayer("Item"));
		Collider[] array2 = array;
		foreach (Collider collider in array2)
		{
			if (globalVal.Config.isItem(collider.transform.parent.name))
			{
				m_cmItem.targetItem(collider.transform);
			}
		}
	}

	public void addShadow()
	{
		if (!(null == m_cmShadow))
		{
			m_cmShadow.localPosition = new Vector3(m_cmPlayer.localPosition.x, 0.2f, 0f);
		}
	}

	public void activationItem()
	{
		Collider[] array = Physics.OverlapSphere(m_cmPlayer.position, 1f, 1 << LayerMask.NameToLayer("Button"));
		Collider[] array2 = array;
		foreach (Collider collider in array2)
		{
			switch (collider.name)
			{
			case "AnimationButton":
			{
				Transform parent2 = collider.transform.parent;
				parent2 = parent2.Find("Animation");
				if (null == parent2 || null == parent2.GetComponent<Animation>()["action"])
				{
					return;
				}
				parent2.GetComponent<Animation>()["action"].wrapMode = WrapMode.Once;
				parent2.GetComponent<Animation>().Play("action");
				break;
			}
			case "ActiveButton":
			{
				Transform parent = collider.transform.parent;
				GameTerrainAction gameTerrainAction = parent.GetComponent(typeof(GameTerrainAction)) as GameTerrainAction;
				if (!(null == gameTerrainAction))
				{
					gameTerrainAction.trigger(m_cmPlayer);
				}
				break;
			}
			case "emitButton":
			{
				for (int j = 0; j < collider.transform.childCount; j++)
				{
					EffectTrigger effectTrigger = collider.transform.GetComponent(typeof(EffectTrigger)) as EffectTrigger;
					if (effectTrigger != null)
					{
						effectTrigger.Trigger();
					}
				}
				break;
			}
			case "MoiveButton":
			{
				m_cmMovieObject = collider.transform.parent;
				m_cmMoviePoints = m_cmMovieObject.GetComponent(typeof(CharaMoviePoint)) as CharaMoviePoint;
				GameTerrain gameTerrain = m_cmMovieObject.GetComponent(typeof(GameTerrain)) as GameTerrain;
				if (null != gameTerrain)
				{
					m_cmNextPoint = gameTerrain.Tail;
				}
				globalVal.GameStatus = GAME_STATUS.GAME_MOIVE;
				break;
			}
			}
		}
	}

	public void autoSpeed()
	{
		if (m_bShowSpeedUP && 1f < Time.realtimeSinceStartup - m_fSpeedUpTime)
		{
			m_bShowSpeedUP = false;
			globalVal.UIScript.showSpeedUP(false);
		}
		ENUM_DIFFICULTY_TYPE randomType = globalVal.RandomType;
		int num = (int)(globalVal.CurDistance / (float)c_nSpeedRate);
		if (8 <= num)
		{
			globalVal.RandomType = ENUM_DIFFICULTY_TYPE.DIFFICULTY_LEVEL8;
		}
		else
		{
			globalVal.RandomType = (ENUM_DIFFICULTY_TYPE)num;
		}
		if (randomType != globalVal.RandomType)
		{
			globalVal.UIScript.showSpeedUP(true);
			m_fSpeedUpTime = Time.realtimeSinceStartup;
			m_bShowSpeedUP = true;
		}
	}

	public void axisAmend()
	{
		if (m_cmNextPoint == null)
		{
			m_cmNextPoint = globalVal.Terrain.getHead();
			if (m_cmNextPoint == null)
			{
				return;
			}
		}
		if (m_cmNextPoint != null && !(null == m_cmNextPoint.WayPoint))
		{
			Vector3 vector = m_cmNextPoint.WayPoint.position;
			Vector3 forward = m_cmPlayer.forward;
			Vector3 vector2 = m_cmPlayer.position;
			Vector3 vector3 = m_cmWayAxis.position;
			vector = m_cmNextPoint.WayPoint.position;
			m_cmPlayer.forward = forward;
			m_cmPlayer.position = vector2;
			Vector3 forward2 = m_cmWayAxis.forward;
			forward2.y = 0f;
			forward2 = Vector3.Normalize(forward2);
			if (m_cmPlayer.forward != m_cmWayAxis.forward)
			{
				forward2 = m_cmWayAxis.forward;
				forward2.y = 0f;
				m_cmPlayer.forward = Vector3.Lerp(m_cmPlayer.forward, forward2, Time.deltaTime * 1.5f);
			}
		}
	}

	private void doDead()
	{
		doDead(true);
	}

	private void doDead(bool bBody)
	{
		if (0 < globalVal.ShieldCount)
		{
			globalVal.ShieldCount--;
			return;
		}
		DeathCfg death = globalVal.Config.getDeath(m_strDeath);
		doDeadSound(m_strDeath);
		string text = "strike04";
		if (death != null)
		{
			text = death.Death1;
			globalVal.CurDeath = death.ID1;
			if (!bBody)
			{
				text = death.Death2;
				globalVal.CurDeath = death.ID2;
			}
		}
		if (text == null)
		{
			m_cmAnimation.setCharacterAnimation(GameCharaAnimation.ENUM_GAME_CHARACTER.CHARA_DEAD);
		}
		m_cmAnimation.setCharacterAnimationByName(text, WrapMode.ClampForever, 0);
		Vector3 vector = m_cmPlayer.position;
		vector += new Vector3(0f, 1f, 0f);
		if ("strike01" == text)
		{
			globalVal.GameStatus = GAME_STATUS.GAME_STRIKE1;
			m_fWelterSpeed = 30f;
		}
		else if ("strike02" == text)
		{
			globalVal.GameStatus = GAME_STATUS.GAME_STRIKE2;
			m_fWelterSpeed = 30f;
		}
		else if ("strike03" == text)
		{
			globalVal.GameStatus = GAME_STATUS.GAME_STRIKE3;
			m_fWelterSpeed = 60f;
			m_cmAnimation.setCharacterAnimationByName(text, WrapMode.Loop, 1);
			m_bWelter = false;
			GameAudio.GetInstance().playSound("shortvo_death_short03", m_cmPlayer.position);
		}
		else if ("strike04" == text)
		{
			globalVal.GameStatus = GAME_STATUS.GAME_STRIKE4;
			m_fWelterSpeed = 15f;
		}
		else if ("Death05" == text)
		{
			globalVal.GameStatus = GAME_STATUS.GAME_DROP;
			m_cmAnimation.setCharacterAnimationByName(text, WrapMode.Once, 1);
			m_fJumpTime = 0f;
			GameAudio.GetInstance().playSound("shortvo_death_long01", m_cmBip.position);
		}
		else
		{
			globalVal.GameStatus = GAME_STATUS.GAME_PLAYBACK;
		}
		if ("Death05" != text)
		{
			Transform effect = globalVal.Effect.getEffect("ef_04");
			effect.position = m_cmBip.position;
			globalVal.Effect.playEffect(effect);
			globalVal.VibrateScreen = true;
		}
		m_bDead = true;
		if (!(null == m_cmShadow))
		{
			m_cmShadow.position = Vector3.zero;
			m_bCollider = false;
			GameBuffer.GetInstance().clearBuffer();
			if ("Train" == m_strDeath)
			{
				Time.timeScale = 0.1f;
			}
			setSkyAnimation(0f);
		}
	}

	private void doDeadSound(string strCode)
	{
		switch (strCode)
		{
		case "car2":
			GameAudio.GetInstance().playSound("mat_Charactor_strike_car", m_cmPlayer.position);
			break;
		case "oiltank01":
		case "oiltank02":
			GameAudio.GetInstance().playSound("mat_Charactor_strike_oiltank", m_cmPlayer.position);
			break;
		case "moto01":
		case "moto02":
			GameAudio.GetInstance().playSound("mat_Charactor_strike_moto", m_cmPlayer.position);
			break;
		case "rail01":
			GameAudio.GetInstance().playSound("mat_Charactor_strike_rail", m_cmPlayer.position);
			break;
		}
	}

	public void wormhole()
	{
		if (Vector3.forward != m_v3NewForward)
		{
			m_v3NewForward = Vector3.forward;
			m_cmPlayer.forward = Vector3.forward;
		}
		m_cmPlayer.position += 2f * m_cmPlayer.forward;
		globalVal.CurDistance += 20f;
	}

	public void resurrection()
	{
		m_cmCurPoint = globalVal.Terrain.addCemetery();
		if (m_cmCurPoint != null && m_cmCurPoint.NextPoint != null)
		{
			m_cmNextPoint = m_cmCurPoint.NextPoint;
		}
		m_cmWayAxis.position = m_cmCurPoint.WayPoint.position;
		Vector3 origin = m_cmWayAxis.position;
		origin.y += 50f;
		RaycastHit hitInfo;
		origin = ((!Physics.Raycast(origin, Vector3.down, out hitInfo, 100f, 1 << LayerMask.NameToLayer("Default"))) ? m_cmWayAxis.position : hitInfo.point);
		m_cmPlayer.position = origin;
		m_cmSky.position = m_cmPlayer.position;
		m_cmWayAxis.forward = Vector3.Normalize(m_cmNextPoint.WayPoint.position - m_cmCurPoint.WayPoint.position);
		m_cmPlayer.forward = m_cmWayAxis.forward;
		Transform transform = m_cmPlayer.Find("Bip01");
		if (null != transform)
		{
			transform.localPosition = Vector3.zero;
		}
		m_cmAnimation.setCharacterAnimationByName(m_cmInfo.Run, WrapMode.Loop, 0);
		Transform effect = globalVal.Effect.getEffect(c_strResurrection);
		if (null != effect)
		{
			effect.parent = m_cmPlayer;
			effect.localPosition = new Vector3(0f, 0.2f, 0f);
			globalVal.Effect.setAlarm(effect, 2f);
		}
		m_cmBuffer.Run();
		globalVal.ReadyTime = Time.realtimeSinceStartup;
		globalVal.AttackPlayer = false;
		m_fDown = 0f;
		m_bDead = false;
	}

	public void getScreenTexture()
	{
		RenderTexture renderTexture = GameScreenshot.GetInstance().Screenshot(m_cmWayAxis);
		if (null == renderTexture)
		{
			Debug.Log("texture is null");
			return;
		}
		GameObject gameObject = GameObject.Find("Screen");
		if (null == gameObject)
		{
			Debug.Log("cmPlane is null");
			return;
		}
		gameObject.GetComponent<Renderer>().material.shader = Shader.Find("Triniti/Model");
		gameObject.GetComponent<Renderer>().material.mainTexture = renderTexture;
	}

	public void knock()
	{
		if (!m_bDead)
		{
			if (m_cmBuffer.canTrap())
			{
				globalVal.GameStatus = GAME_STATUS.GAME_RUNNING;
				return;
			}
			m_strDeath = "sub_moto01";
			doDead();
		}
	}

	public void strike1()
	{
		if (m_fWelterSpeed == 30f)
		{
			m_cmPlayer.position += new Vector3(0f, 0.7f, 0f);
			m_fWelterSpeed = 0f;
		}
		m_cmPlayer.position -= new Vector3(0f, 0.02f, 0f);
		if (0f >= m_cmPlayer.position.y)
		{
			globalVal.GameStatus = GAME_STATUS.GAME_PLAYBACK;
		}
	}

	public void strike2()
	{
		Vector3 vector = m_cmPlayer.position;
		vector.y += 0.3f;
		vector -= m_cmPlayer.forward * 0.5f;
		float num = m_fWelterSpeed * Time.deltaTime;
		Vector3 gravity = getGravity(30f == m_fWelterSpeed, 12f, 50f);
		Vector3 horizontal = getHorizontal(0f);
		m_cmPlayer.position += gravity;
		m_cmPlayer.position += horizontal;
		m_cmPlayer.position += num * m_cmPlayer.forward;
		m_fWelterSpeed -= Time.deltaTime * 30f * 1.2f;
		addRecord(m_cmPlayer.position, "strike01");
		if (0f >= m_fWelterSpeed)
		{
			globalVal.GameStatus = GAME_STATUS.GAME_PLAYBACK;
		}
	}

	public void strike3()
	{
		Vector3 vector = m_cmPlayer.position;
		vector.y += 0.3f;
		vector -= m_cmPlayer.forward * 0.5f;
		float num = m_fWelterSpeed * Time.deltaTime;
		Vector3 gravity = getGravity(m_fWelterSpeed % 30f == 0f, 30f, 38f);
		Vector3 horizontal = getHorizontal(0f);
		m_cmPlayer.position += gravity;
		m_cmPlayer.position += horizontal;
		m_cmPlayer.position += num * m_cmPlayer.forward;
		m_fWelterSpeed -= Time.deltaTime * 30f;
		if (0f >= m_fWelterSpeed)
		{
			if (!m_bWelter)
			{
				m_cmAnimation.setCharacterAnimationByName("Death04", WrapMode.ClampForever, 0);
				m_fWelterSpeed = 10f;
				m_bWelter = true;
				GameAudio.GetInstance().stopSound("shortvo_death_short03");
			}
			else
			{
				globalVal.GameStatus = GAME_STATUS.GAME_PLAYBACK;
			}
		}
		GameAudio.GetInstance().resetPosition("shortvo_death_short03", m_cmPlayer.position);
	}

	public void strike4()
	{
		Vector3 vector = m_cmPlayer.position;
		vector.y += 0.3f;
		vector -= m_cmPlayer.forward * 0.5f;
		float num = m_fWelterSpeed * Time.deltaTime;
		Vector3 gravity = getGravity(30f == m_fWelterSpeed, 6f, 15f);
		Vector3 horizontal = getHorizontal(0f);
		m_cmPlayer.position += gravity;
		m_cmPlayer.position += horizontal;
		m_cmPlayer.position += num * -m_cmPlayer.forward;
		m_fWelterSpeed -= Time.deltaTime * 30f;
		if (0f >= m_fWelterSpeed)
		{
			globalVal.GameStatus = GAME_STATUS.GAME_PLAYBACK;
		}
	}

	public void Drop()
	{
		m_fJumpTime += Time.deltaTime;
		if (1f < m_fJumpTime)
		{
			globalVal.GameStatus = GAME_STATUS.GAME_PLAYBACK;
		}
		globalVal.Audio.resetPosition("shortvo_death_long01", m_cmBip.position);
	}

	public void Playback()
	{
		globalVal.GameStatus = GAME_STATUS.GAME_OVER;
	}

	private void addRecord(Vector3 v3Point, string strAnimation)
	{
	}
}
