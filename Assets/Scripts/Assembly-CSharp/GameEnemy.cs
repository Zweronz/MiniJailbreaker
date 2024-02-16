using UnityEngine;

public class GameEnemy : MonoBehaviour
{
	private const string c_strDamageEffect = "deadblood";

	private const float c_fSubsidenceSpeed = 1.5f;

	private MonsterCfg m_cmCfg;

	private Transform m_cmInstance;

	private Transform m_cmTarget;

	private ENUM_ENEMY_STATUS m_nStatus = ENUM_ENEMY_STATUS.ENEMY_IDLE;

	private readonly string[] c_strAnimation = new string[5] { "Attack01", "Damage01", "Forward01", "Death01", "Idle01" };

	private float m_fTime;

	private float m_fAnimationTime;

	private Vector3 m_v3Point;

	private Vector3 m_v3CurPoint;

	public Transform EnemyInstance
	{
		get
		{
			return m_cmInstance;
		}
	}

	private void Start()
	{
		if (null == m_cmInstance)
		{
			m_cmInstance = base.transform.Find("Instance");
		}
		if (null == m_cmInstance)
		{
			Debug.Log("Monster Instance is null! " + base.transform.name);
			return;
		}
		if (null != m_cmInstance.GetComponent<Animation>()[c_strAnimation[3]])
		{
			m_cmInstance.GetComponent<Animation>()[c_strAnimation[3]].wrapMode = WrapMode.ClampForever;
			m_cmInstance.GetComponent<Animation>()[c_strAnimation[3]].layer = 1;
		}
		m_fTime = 1f;
	}

	private void Update()
	{
		if (globalVal.GameStatus == GAME_STATUS.GAME_RESURRECTION || globalVal.GameStatus == GAME_STATUS.GAME_READY)
		{
			m_fTime = 1E-05f;
		}
		else
		{
			m_fTime = 1f;
		}
		if (m_nStatus == ENUM_ENEMY_STATUS.ENEMY_DESTROY)
		{
			base.transform.gameObject.SetActiveRecursively(false);
		}
		else
		{
			if (null == m_cmInstance || m_cmCfg == null || null == m_cmTarget)
			{
				return;
			}
			if (m_nStatus == ENUM_ENEMY_STATUS.ENEMY_KNOCK)
			{
				runKnock();
				return;
			}
			if (m_fAnimationTime + 1f <= Time.realtimeSinceStartup && m_nStatus == ENUM_ENEMY_STATUS.ENEMY_SUBSIDENCE)
			{
				m_nStatus = ENUM_ENEMY_STATUS.ENEMY_DESTROY;
			}
			else if (m_nStatus == ENUM_ENEMY_STATUS.ENEMY_IDLE || m_nStatus == ENUM_ENEMY_STATUS.ENEMY_ATTACK || m_nStatus == ENUM_ENEMY_STATUS.ENEMY_RUN)
			{
				Vector3 forward = Vector3.Normalize(m_cmTarget.position - m_cmInstance.position);
				forward.y = 0f;
				m_cmInstance.forward = forward;
				float num = Vector3.Distance(m_cmTarget.position, m_cmInstance.position);
				if (0.8f >= num)
				{
					attack();
				}
				else if (m_cmCfg.MoveRange >= num)
				{
					move();
				}
			}
			if (m_nStatus == ENUM_ENEMY_STATUS.ENEMY_SUBSIDENCE)
			{
				subsidence();
			}
			else
			{
				playAnimation();
			}
			if (m_nStatus == ENUM_ENEMY_STATUS.ENEMY_DEATH)
			{
				m_nStatus = ENUM_ENEMY_STATUS.ENEMY_SUBSIDENCE;
			}
		}
	}

	public void Init(string strCode)
	{
		m_cmCfg = globalVal.Config.getMonsterCfg(strCode);
		if (m_cmCfg == null)
		{
			Debug.Log("not found Config is " + strCode);
			return;
		}
		if (null != m_cmInstance)
		{
			m_cmInstance.localPosition = Vector3.zero;
		}
		m_nStatus = ENUM_ENEMY_STATUS.ENEMY_IDLE;
	}

	public void setTarget(Transform cmTarget)
	{
		m_cmTarget = cmTarget;
	}

	private void move()
	{
		Vector3 position = m_cmInstance.position;
		position.y += 2f;
		Vector3 position2 = m_cmInstance.position;
		position.y += 0.2f;
		float num = m_cmCfg.MoveSpeed * Time.deltaTime * m_fTime;
		RaycastHit[] array = Physics.CapsuleCastAll(position, position2, 0.5f, m_cmInstance.forward, num, 1 << LayerMask.NameToLayer("Default"));
		if (0 < array.Length)
		{
			m_cmInstance.position = array[0].point;
		}
		else
		{
			m_cmInstance.position += num * m_cmInstance.forward;
		}
		position = m_cmInstance.position;
		position.y += 1f;
		RaycastHit hitInfo;
		if (Physics.Raycast(position, Vector3.down, out hitInfo, 2f, 1 << LayerMask.NameToLayer("Default")))
		{
			m_cmInstance.position = hitInfo.point;
		}
		else
		{
			m_cmInstance.position += Vector3.down;
		}
		m_nStatus = ENUM_ENEMY_STATUS.ENEMY_RUN;
	}

	private void attack()
	{
		Vector3 position = m_cmInstance.position;
		position.y += 1f;
		RaycastHit hitInfo;
		if (Physics.Raycast(position, m_cmInstance.forward, out hitInfo, 1f, 1 << LayerMask.NameToLayer("Player")))
		{
			globalVal.AttackPlayer = true;
			m_nStatus = ENUM_ENEMY_STATUS.ENEMY_ATTACK;
		}
	}

	private void playAnimation()
	{
		if (ENUM_ENEMY_STATUS.MAX_ENEMY_STATUS <= m_nStatus)
		{
			return;
		}
		if (null == m_cmInstance.GetComponent<Animation>()[c_strAnimation[(int)m_nStatus]])
		{
			Debug.Log(string.Concat("m_nStatus = ", m_nStatus, " transform = ", base.transform.name));
		}
		else
		{
			if (m_nStatus == ENUM_ENEMY_STATUS.ENEMY_DEATH)
			{
				m_cmInstance.GetComponent<Animation>().Stop();
			}
			m_cmInstance.GetComponent<Animation>()[c_strAnimation[(int)m_nStatus]].speed = m_fTime;
			m_cmInstance.GetComponent<Animation>().Play(c_strAnimation[(int)m_nStatus]);
		}
	}

	public void damage()
	{
		m_nStatus = ENUM_ENEMY_STATUS.ENEMY_DEATH;
		m_fAnimationTime = Time.realtimeSinceStartup;
		Transform effect = globalVal.Effect.getEffect("deadblood");
		if (null == effect)
		{
			return;
		}
		Vector3 position = m_cmInstance.position;
		position.y += 1f;
		effect.position = position;
		effect.forward = 1.5f * m_cmInstance.forward;
		for (int i = 0; i < effect.childCount; i++)
		{
			if (null != effect.GetChild(i).GetComponent<ParticleSystem>())
			{
				effect.GetChild(i).GetComponent<ParticleSystem>().Clear();
				effect.GetChild(i).GetComponent<ParticleSystem>().Play();
			}
		}
		globalVal.Effect.setAlarm(effect, 0.5f);
		globalVal.CurMonster++;
	}

	private void subsidence()
	{
		m_cmInstance.position += Vector3.down * 1.5f * Time.deltaTime * m_fTime;
	}

	public void Destroy()
	{
	}

	public void knock()
	{
		float x = Random.RandomRange(0, 480);
		float y = Random.RandomRange(0, 320);
		m_v3Point = new Vector3(x, y, 10f);
		m_v3CurPoint = globalVal.MainCamera.WorldToScreenPoint(base.transform.position);
		m_nStatus = ENUM_ENEMY_STATUS.ENEMY_KNOCK;
	}

	private void runKnock()
	{
		Vector3 vector = globalVal.MainCamera.ScreenToWorldPoint(m_v3Point);
		Vector3 vector2 = Vector3.Normalize(vector - base.transform.position);
		float num = Vector3.Distance(vector, base.transform.position);
		if (10f <= num)
		{
			num = 150f * Time.deltaTime;
		}
		else
		{
			GameObject gameObject = GameObject.Find("TUI/TUIControl");
			menu menu2 = gameObject.GetComponent(typeof(menu)) as menu;
			if (null != menu2)
			{
				menu2.addBlood(m_v3Point);
			}
			base.transform.gameObject.SetActiveRecursively(false);
		}
		base.transform.position += num * vector2;
	}

	private void gliding()
	{
		GameObject gameObject = GameObject.Find("TUI/TUIControl");
		menu menu2 = gameObject.GetComponent(typeof(menu)) as menu;
		if (null != menu2)
		{
			menu2.addBlood(m_v3Point);
		}
	}
}
