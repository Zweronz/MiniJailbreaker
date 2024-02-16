using System.Collections;
using UnityEngine;

public class GameView
{
	private const float c_fLookLength = 2f;

	private Transform m_cmCameraBox;

	private Transform m_cmCamera;

	private Transform m_cmTarget;

	private float m_fFollow;

	private float m_fFollowHeight;

	private GameViewMovie m_cmWay;

	private Vector3 m_cmNextPoint = Vector3.zero;

	private bool m_bStart;

	private Vector3 m_v3LastTarget;

	private float m_fDistance;

	private ArrayList m_alHideItem;

	private float fVibrateTime;

	private bool m_bLeft;

	private int m_nVibrate;

	private bool m_bVibrate;

	private int m_nCount;

	public void Initialize(Transform cmTarget)
	{
		if (null == m_cmCameraBox)
		{
			m_cmCameraBox = GameObject.Find("CameraBox").transform;
		}
		if (null == m_cmCamera)
		{
			m_cmCamera = m_cmCameraBox.Find("Main Camera").transform;
		}
		globalVal.MainCamera = m_cmCamera.GetComponent<Camera>();
		m_cmCamera.localPosition = Vector3.zero;
		m_cmCamera.localEulerAngles = Vector3.zero;
		m_cmTarget = cmTarget;
		m_fFollow = 7f;
		m_fFollowHeight = 3.8f;
		m_cmCameraBox.position = new Vector3(20f, m_fFollowHeight, -105f);
		m_cmWay = m_cmCamera.GetComponent(typeof(GameViewMovie)) as GameViewMovie;
		m_cmNextPoint = m_cmWay.getNextPoint();
		m_bStart = true;
	}

	public void Destroy()
	{
	}

	public void Reset()
	{
		m_cmWay.Reset();
	}

	public void StartScreen()
	{
		if (!m_bStart)
		{
			RunningScreen();
			return;
		}
		if (0.3f >= Vector3.Distance(m_cmCameraBox.position, m_cmNextPoint))
		{
			m_cmNextPoint = m_cmWay.getNextPoint();
		}
		Vector3 vector = Vector3.Normalize(m_cmNextPoint - m_cmCameraBox.position);
		m_cmCameraBox.position += vector * m_cmWay.getCurSpeed() * Time.deltaTime;
		m_cmCameraBox.LookAt(m_cmTarget);
		if (!(Vector3.zero != m_cmNextPoint))
		{
			Vector3 position = m_cmTarget.position;
			Vector3 position2 = m_cmCameraBox.position;
			position.y = 0f;
			position2.y = 0f;
			if (m_fFollow + 10f <= Vector3.Distance(position, position2))
			{
				m_bStart = false;
			}
		}
	}

	public void RunningScreen()
	{
		if (!globalVal.GamePause)
		{
			m_cmCameraBox.localEulerAngles = Vector3.zero;
			Vector3 position = m_cmCameraBox.position;
			Vector3 position2 = m_cmTarget.position;
			position2.y += 0.5f;
			float y = position.y;
			position2 = m_cmTarget.position;
			position2.y = m_fFollowHeight;
			m_cmCameraBox.position = position2;
			m_cmCameraBox.position -= m_cmTarget.forward * m_fFollow;
			position2 = m_cmTarget.position;
			position2 += m_cmTarget.forward * (m_fFollow + 2f);
			RaycastHit hitInfo;
			if (Physics.Raycast(position2, Vector3.down, out hitInfo, 13f, 1 << LayerMask.NameToLayer("Safety")))
			{
				position2 = hitInfo.point;
			}
			joltScreen();
			m_cmCamera.LookAt(position2);
			if (m_alHideItem != null && 0 < m_alHideItem.Count)
			{
				Transform cmHideItem = (Transform)m_alHideItem[0];
				Hide(cmHideItem, false);
			}
			vibrateScreen();
		}
	}

	private void checkHide(Vector3 v3LookAt)
	{
		Vector3 direction = Vector3.Normalize(v3LookAt - m_cmCameraBox.position);
		float num = Vector3.Distance(v3LookAt, m_cmCameraBox.position);
		num -= 1f;
		RaycastHit[] array = Physics.RaycastAll(m_cmCameraBox.position, direction, num, 1 << LayerMask.NameToLayer("Default"));
		RaycastHit[] array2 = array;
		for (int i = 0; i < array2.Length; i++)
		{
			RaycastHit raycastHit = array2[i];
			if (m_alHideItem == null)
			{
				m_alHideItem = new ArrayList();
			}
			Hide(raycastHit.transform, true);
			m_alHideItem.Add(raycastHit.transform);
			GameItemHide gameItemHide = null;
		}
	}

	public void Hide(Transform cmHideItem, bool bHide)
	{
		float a = 1f;
		if (bHide)
		{
			a = 0.4f;
		}
		Material[] materials = cmHideItem.GetComponent<Renderer>().materials;
		foreach (Material material in materials)
		{
			if (Shader.Find("Sonke/AlphaCtrl") == material.shader)
			{
				material.SetColor("_Color", new Color(1f, 1f, 1f, a));
			}
		}
	}

	public void GameOverScreen()
	{
	}

	public void startVibrate(float fTime)
	{
		fVibrateTime = Time.realtimeSinceStartup + fTime;
	}

	public void vibrateScreen()
	{
		if (!(Time.realtimeSinceStartup >= fVibrateTime))
		{
			m_nCount++;
			if (m_nCount % 2 != 0)
			{
				float num = (float)Random.Range(-8, 8) / 100f;
				float num2 = (float)Random.Range(-8, 8) / 100f;
				m_cmCamera.localPosition = Vector3.zero;
				m_cmCamera.position += num * m_cmCamera.up;
				m_cmCamera.position += num2 * m_cmCamera.right;
				float angle = (float)(Random.Range(0, 3) * ((!m_bVibrate) ? 1 : (-1))) / 100f;
				m_cmCamera.RotateAround(m_cmCamera.forward, angle);
				m_bVibrate = !m_bVibrate;
				m_nCount++;
			}
		}
	}

	public void Rain()
	{
		float x = m_cmCameraBox.position.x;
		x *= -12f;
		GameObject gameObject = GameObject.Find("TUI/TUIControl");
		Transform transform = gameObject.transform.Find("Rain1");
		transform.localPosition = new Vector3(x, 0f, -0.5f);
		transform = gameObject.transform.Find("Rain2");
		transform.localPosition = new Vector3(x * 0.8f, 0f, -0.3f);
		transform = gameObject.transform.Find("Rain3");
		transform.localPosition = new Vector3(x * 0.5f, 0f, -0.1f);
	}

	public void joltScreen()
	{
		if (globalVal.OnGround && globalVal.GameStatus != GAME_STATUS.GAME_PLAYBACK)
		{
			m_nVibrate++;
			m_cmCamera.position += 0.01f * (m_cmCamera.up * ((m_nVibrate / 5 % 2 != 0) ? 1 : (-1))) * (1f + (float)globalVal.RandomType * 0.05f);
			m_cmCamera.position += 0.01f * (m_cmCamera.right * ((!m_bLeft) ? 1 : (-1))) * (1f + (float)globalVal.RandomType * 0.1f);
			m_cmCamera.RotateAround(m_cmCamera.forward, 0.005f * (float)((!m_bLeft) ? 1 : (-1)));
			if (m_nVibrate % 10 == 0)
			{
				m_bLeft = !m_bLeft;
			}
		}
	}
}
