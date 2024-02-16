using System.Collections;
using UnityEngine;

public class GameBuffer
{
	public static GameBuffer m_cmInstance;

	private Transform m_cmWayAxis;

	private Transform m_cmPlayer;

	public Hashtable m_htBuffer;

	private int m_nLastIndex;

	private Hashtable m_htSpeed;

	private Transform m_cmLightWay;

	private float m_fRunLength;

	private bool m_bAllRun;

	private float m_fTime;

	private GameBuffer()
	{
	}

	public static GameBuffer GetInstance()
	{
		if (m_cmInstance == null)
		{
			m_cmInstance = new GameBuffer();
			m_cmInstance.Initialize();
		}
		return m_cmInstance;
	}

	public void setPlayer(Transform cmPlayer)
	{
		m_cmWayAxis = cmPlayer;
		m_cmPlayer = cmPlayer.Find("Charactor");
	}

	public void Initialize()
	{
		if (m_htBuffer == null)
		{
			m_htBuffer = new Hashtable();
		}
		m_nLastIndex = 0;
		if (m_htSpeed == null)
		{
			m_htSpeed = new Hashtable(4);
		}
		m_bAllRun = true;
	}

	public void Destroy()
	{
		m_cmInstance = null;
		m_htBuffer = null;
	}

	public bool haveBuffer()
	{
		if (0 >= m_htBuffer.Count)
		{
			return false;
		}
		return true;
	}

	public bool canTrap()
	{
		if (m_htBuffer == null || m_htBuffer[ENUM_CHARA_BUFFER.BUFFER_INVINCIBILITY] == null)
		{
			return false;
		}
		return true;
	}

	public void clearBuffer()
	{
		globalVal.SpeedPower = 0f;
		globalVal.ShieldCount = 0;
		globalVal.ItemCheckRange = 1f;
		globalVal.Invincibility = false;
		if (null != m_cmLightWay && m_cmLightWay.gameObject.activeInHierarchy)
		{
			m_cmLightWay.gameObject.SetActive(false);
		}
		Transform transform = m_cmPlayer.Find("Charactor");
		if (null != transform)
		{
			transform.GetComponent<Renderer>().material.shader = Shader.Find("Triniti/Model");
		}
		globalVal.ShowFarise = false;
		ICollection keys = m_htBuffer.Keys;
		foreach (object item in keys)
		{
			BufferInfo bufferInfo = (BufferInfo)m_htBuffer[(ENUM_CHARA_BUFFER)(int)item];
			undoEffect(bufferInfo.EffectName);
			undoAudio(bufferInfo.StartAudio);
			doAudio(bufferInfo.EndAudio);
		}
		m_htBuffer.Clear();
		m_nLastIndex = 0;
		m_htSpeed.Clear();
		globalVal.Exchange = false;
		MaterialEffect materialEffect = m_cmPlayer.GetComponent(typeof(MaterialEffect)) as MaterialEffect;
		if (null != materialEffect)
		{
			materialEffect.changeMaterial(0);
		}
	}

	public void StartBuffer(ENUM_CHARA_BUFFER nBuffer, float fContinueTime, int nStrength, string strEffect, string strStartAudio, string strEndAudio)
	{
		switch (nBuffer)
		{
		case ENUM_CHARA_BUFFER.BUFFER_SPEED:
			globalVal.SpeedPower = nStrength;
			break;
		case ENUM_CHARA_BUFFER.BUFFER_SHIELD:
			globalVal.ShieldCount = nStrength;
			break;
		case ENUM_CHARA_BUFFER.BUFFER_MAGENT:
			globalVal.ItemCheckRange = nStrength;
			break;
		case ENUM_CHARA_BUFFER.BUFFER_INVINCIBILITY:
		{
			globalVal.Invincibility = true;
			MaterialEffect materialEffect = m_cmPlayer.GetComponent(typeof(MaterialEffect)) as MaterialEffect;
			if (null != materialEffect)
			{
				materialEffect.changeMaterial(1);
			}
			break;
		}
		case ENUM_CHARA_BUFFER.BUFFER_LIGHT_WAY:
			if (null == m_cmLightWay)
			{
				m_cmLightWay = m_cmPlayer.parent.Find("LightWay");
			}
			m_cmLightWay.gameObject.SetActive(true);
			break;
		case ENUM_CHARA_BUFFER.BUFFER_TRANSPARENT:
		{
			Transform transform = m_cmPlayer.Find("Charactor");
			if (null != transform)
			{
				Texture mainTexture = transform.GetComponent<Renderer>().material.mainTexture;
				transform.GetComponent<Renderer>().material.shader = Shader.Find("Triniti/LightMapTransparent");
				transform.GetComponent<Renderer>().material.mainTexture = mainTexture;
			}
			break;
		}
		case ENUM_CHARA_BUFFER.BUFFER_SHOCK_WAVE:
			globalVal.ShowFarise = false;
			break;
		case ENUM_CHARA_BUFFER.BUFFER_WORMHOLE:
			m_fRunLength = globalVal.Distance[7];
			globalVal.Wormhole = true;
			globalVal.SpeedPower += 200f;
			break;
		case ENUM_CHARA_BUFFER.BUFFER_EXCHANGE:
			globalVal.Exchange = true;
			break;
		}
		BufferInfo bufferInfo = ((m_htBuffer[nBuffer] != null) ? ((BufferInfo)m_htBuffer[nBuffer]) : default(BufferInfo));
		bufferInfo.Buffer = nBuffer;
		if (fContinueTime == -1f)
		{
			bufferInfo.BufferTime = -1f;
		}
		else
		{
			bufferInfo.BufferTime = Time.realtimeSinceStartup + fContinueTime;
		}
		bufferInfo.Strength = nStrength;
		bufferInfo.EffectName = strEffect;
		bufferInfo.StartAudio = strStartAudio;
		bufferInfo.EndAudio = strEndAudio;
		bufferInfo.Effect = globalVal.Effect.playEffect(strEffect);
		if (null != bufferInfo.Effect)
		{
			bufferInfo.Effect.parent = m_cmPlayer;
			bufferInfo.Effect.localPosition = Vector3.zero;
		}
		m_htBuffer.Remove(nBuffer);
		m_htBuffer.Add(bufferInfo.Buffer, bufferInfo);
		doAudio(bufferInfo.StartAudio);
	}

	private void undoEffect(Transform cmEffect)
	{
		globalVal.Effect.closeEffect(cmEffect);
	}

	private void undoEffect(string strCode)
	{
		globalVal.Effect.closeEffect(strCode);
	}

	private void undoAudio(string strCode)
	{
		globalVal.Audio.stopSound(strCode);
	}

	public void StopBuffer(BufferInfo cmBuffer)
	{
		switch (cmBuffer.Buffer)
		{
		case ENUM_CHARA_BUFFER.BUFFER_SPEED:
			globalVal.SpeedPower = 0f;
			break;
		case ENUM_CHARA_BUFFER.BUFFER_MAGENT:
			globalVal.ItemCheckRange = 1f;
			break;
		case ENUM_CHARA_BUFFER.BUFFER_INVINCIBILITY:
		{
			globalVal.Invincibility = false;
			MaterialEffect materialEffect = m_cmPlayer.GetComponent(typeof(MaterialEffect)) as MaterialEffect;
			if (null != materialEffect)
			{
				materialEffect.changeMaterial(0);
			}
			break;
		}
		case ENUM_CHARA_BUFFER.BUFFER_LIGHT_WAY:
			if (null != m_cmLightWay)
			{
				m_cmLightWay.gameObject.SetActive(false);
			}
			break;
		case ENUM_CHARA_BUFFER.BUFFER_TRANSPARENT:
		{
			Transform transform = m_cmPlayer.Find("Charactor");
			if (null != transform)
			{
				Texture mainTexture = transform.GetComponent<Renderer>().material.mainTexture;
				transform.GetComponent<Renderer>().material.shader = Shader.Find("Triniti/Model");
				transform.GetComponent<Renderer>().material.mainTexture = mainTexture;
			}
			break;
		}
		case ENUM_CHARA_BUFFER.BUFFER_SHOCK_WAVE:
			globalVal.ShowFarise = true;
			StartBuffer(ENUM_CHARA_BUFFER.BUFFER_INVINCIBILITY, 0.3f, 0, string.Empty, string.Empty, string.Empty);
			break;
		case ENUM_CHARA_BUFFER.BUFFER_WORMHOLE:
			globalVal.Wormhole = false;
			globalVal.SpeedPower -= 200f;
			globalVal.GameStatus = GAME_STATUS.GAME_READY;
			break;
		case ENUM_CHARA_BUFFER.BUFFER_EXCHANGE:
			globalVal.Exchange = false;
			break;
		}
		undoEffect(cmBuffer.Effect);
		undoAudio(cmBuffer.StartAudio);
		doAudio(cmBuffer.EndAudio);
		m_htBuffer.Remove(cmBuffer.Buffer);
	}

	private void doEffect(string strCode)
	{
	}

	private void doAudio(string strCode)
	{
		globalVal.Audio.playSound(strCode, m_cmPlayer.position);
	}

	public bool RunEffect(BufferInfo cmBuffer)
	{
		switch (cmBuffer.Buffer)
		{
		case ENUM_CHARA_BUFFER.BUFFER_WORMHOLE:
			if ((float)cmBuffer.Strength <= globalVal.CurDistance - m_fRunLength)
			{
				StopBuffer(cmBuffer);
				return true;
			}
			break;
		}
		doEffect(cmBuffer.EffectName);
		globalVal.Audio.resetPosition(cmBuffer.StartAudio, m_cmPlayer.position);
		return false;
	}

	public void traversalBuffer()
	{
		if (Time.realtimeSinceStartup - m_fTime < 0.5f)
		{
			return;
		}
		m_fTime = Time.realtimeSinceStartup;
		if (0 >= m_htBuffer.Count)
		{
			return;
		}
		ICollection keys = m_htBuffer.Keys;
		foreach (object item in keys)
		{
			BufferInfo cmBuffer = (BufferInfo)m_htBuffer[(ENUM_CHARA_BUFFER)(int)item];
			if (cmBuffer.isTime())
			{
				StopBuffer(cmBuffer);
				break;
			}
			if (RunEffect(cmBuffer))
			{
				break;
			}
		}
	}

	public void Run()
	{
		traversalBuffer();
	}

	public void setBufferStength(ENUM_CHARA_BUFFER nType, int nStength)
	{
		if (m_htBuffer[nType] != null)
		{
			BufferInfo bufferInfo = (BufferInfo)m_htBuffer[nType];
			bufferInfo.Strength = nStength;
		}
	}

	public int getBufferStength(ENUM_CHARA_BUFFER nType)
	{
		if (m_htBuffer[nType] == null)
		{
			return -1;
		}
		return ((BufferInfo)m_htBuffer[nType]).Strength;
	}

	public float getBufferTime(ENUM_CHARA_BUFFER nType)
	{
		if (m_htBuffer == null || m_htBuffer[nType] == null)
		{
			return 0f;
		}
		return ((BufferInfo)m_htBuffer[nType]).BufferTime - Time.realtimeSinceStartup;
	}
}
