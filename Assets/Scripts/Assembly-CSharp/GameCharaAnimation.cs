using UnityEngine;

public class GameCharaAnimation
{
	public enum ENUM_GAME_CHARACTER
	{
		CHARA_IDLE = 0,
		CHARA_RUN = 1,
		CHARA_LEFT = 2,
		CHARA_RIGHT = 3,
		CHARA_JUMP = 4,
		CHARA_JUMPIDEL = 5,
		CHARA_DOWN = 6,
		CHARA_DEAD = 7
	}

	private Transform m_cmCharacter;

	private CharaInfo m_cmInfo;

	private ENUM_GAME_CHARACTER m_nStatus;

	public ENUM_GAME_CHARACTER CharacterAnimation
	{
		get
		{
			return m_nStatus;
		}
	}

	public void Initialize(Transform cCharacter, CharaInfo cmInfo)
	{
		m_cmCharacter = cCharacter;
		m_cmInfo = cmInfo;
		m_nStatus = ENUM_GAME_CHARACTER.CHARA_IDLE;
		InitAnimation();
	}

	private void InitAnimation()
	{
		if (null != m_cmCharacter.GetComponent<Animation>()[m_cmInfo.Run])
		{
			m_cmCharacter.GetComponent<Animation>()[m_cmInfo.Run].wrapMode = WrapMode.Loop;
		}
		if (null != m_cmCharacter.GetComponent<Animation>()[m_cmInfo.Jump1])
		{
			m_cmCharacter.GetComponent<Animation>()[m_cmInfo.Jump1].wrapMode = WrapMode.ClampForever;
		}
		if (null != m_cmCharacter.GetComponent<Animation>()[m_cmInfo.JumpIdle])
		{
			m_cmCharacter.GetComponent<Animation>()[m_cmInfo.JumpIdle].wrapMode = WrapMode.Once;
		}
		if (null != m_cmCharacter.GetComponent<Animation>()[m_cmInfo.Down])
		{
			m_cmCharacter.GetComponent<Animation>()[m_cmInfo.Down].wrapMode = WrapMode.Once;
		}
	}

	public void setCharacterAnimation(ENUM_GAME_CHARACTER nStatus)
	{
		m_cmCharacter.GetComponent<Animation>()[m_cmInfo.Run].speed = 0.9f + (float)globalVal.RandomType * 0.05f;
		if (nStatus != m_nStatus)
		{
			m_nStatus = nStatus;
			PlayAnimation();
		}
	}

	public void setCharacterAnimationByName(string strAnimationName, WrapMode nMode, int nLayer)
	{
		m_cmCharacter.GetComponent<Animation>()[m_cmInfo.Run].speed = 0.9f + (float)globalVal.RandomType * 0.07f;
		if (null != m_cmCharacter.GetComponent<Animation>()[strAnimationName])
		{
			if (m_cmInfo.Run != strAnimationName)
			{
				m_cmCharacter.GetComponent<Animation>().Stop();
			}
			m_cmCharacter.GetComponent<Animation>()[strAnimationName].wrapMode = nMode;
			m_cmCharacter.GetComponent<Animation>()[strAnimationName].layer = nLayer;
			m_cmCharacter.GetComponent<Animation>().CrossFade(strAnimationName, 0.1f);
		}
	}

	public void setCharacterAnimationByName(string strAnimationName)
	{
		setCharacterAnimationByName(strAnimationName, WrapMode.Loop, 0);
	}

	public float getAnimationTime(string strAnimationName)
	{
		if (strAnimationName == null)
		{
			return 1f;
		}
		return m_cmCharacter.GetComponent<Animation>()[strAnimationName].normalizedTime;
	}

	public void PlayAnimation()
	{
		string text = m_cmInfo.Run;
		switch (m_nStatus)
		{
		case ENUM_GAME_CHARACTER.CHARA_RUN:
			text = m_cmInfo.Run;
			break;
		case ENUM_GAME_CHARACTER.CHARA_JUMP:
			text = m_cmInfo.Jump1;
			break;
		case ENUM_GAME_CHARACTER.CHARA_JUMPIDEL:
			text = m_cmInfo.JumpIdle;
			break;
		case ENUM_GAME_CHARACTER.CHARA_DOWN:
			text = m_cmInfo.Down;
			break;
		case ENUM_GAME_CHARACTER.CHARA_LEFT:
			text = m_cmInfo.Left;
			break;
		case ENUM_GAME_CHARACTER.CHARA_RIGHT:
			text = m_cmInfo.Right;
			break;
		}
		if (null != m_cmCharacter.GetComponent<Animation>()[text])
		{
			m_cmCharacter.GetComponent<Animation>().CrossFade(text, 0.1f);
		}
	}

	public void Reset()
	{
		m_cmCharacter.GetComponent<Animation>().Stop();
		m_cmCharacter.GetComponent<Animation>().Play(m_cmInfo.Run);
	}
}
