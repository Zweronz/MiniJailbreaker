using System.Collections;
using UnityEngine;

public class GameCenterManager : MonoBehaviour
{
	public enum ENUM_SUBMIT_STATUS
	{
		SUBMIT_STATUS_IDLE = 0,
		SUBMIT_STATUS_READY = 1,
		SUBMIT_STATUS_WAIT = 2
	}

	private class Submit
	{
		private int m_nIndex;

		private bool m_bWait;

		public int AccomplishmentID
		{
			get
			{
				return m_nIndex;
			}
			set
			{
				m_nIndex = value;
			}
		}

		public bool Wait
		{
			get
			{
				return m_bWait;
			}
			set
			{
				m_bWait = value;
			}
		}
	}

	private string[] c_strAccomplishment = new string[10] { "com.trinitigame.minirunner.a1", "com.trinitigame.minirunner.a2", "com.trinitigame.minirunner.a3", "com.trinitigame.minirunner.a4", "com.trinitigame.minirunner.a5", "com.trinitigame.minirunner.a6", "com.trinitigame.minirunner.a7", "com.trinitigame.minirunner.a8", "com.trinitigame.minirunner.a9", "com.trinitigame.minirunner.a10" };

	private string c_strRankingList = "com.trinitigame.minirunner.l1";

	private ArrayList m_alAccomplishment;

	private ENUM_SUBMIT_STATUS m_nRankingSubmitStatus;

	private void Start()
	{
		if (m_alAccomplishment == null)
		{
			m_alAccomplishment = new ArrayList();
		}
		if (!globalVal.g_bSumbit)
		{
			submitRanking();
		}
	}

	private void Update()
	{
		if (GameCenterPlugin.IsLogin())
		{
			return;
		}
		GameCenterPlugin.SUBMIT_STATUS sUBMIT_STATUS = GameCenterPlugin.SUBMIT_STATUS.SUBMIT_STATUS_IDLE;
		if (m_nRankingSubmitStatus == ENUM_SUBMIT_STATUS.SUBMIT_STATUS_READY)
		{
			GameCenterPlugin.SubmitScore(c_strRankingList, globalVal.g_bestScore);
		}
		else if (m_nRankingSubmitStatus == ENUM_SUBMIT_STATUS.SUBMIT_STATUS_WAIT)
		{
			sUBMIT_STATUS = GameCenterPlugin.SubmitScoreStatus(c_strRankingList, globalVal.g_bestScore);
		}
		if (sUBMIT_STATUS == GameCenterPlugin.SUBMIT_STATUS.SUBMIT_STATUS_SUCCESS)
		{
			m_nRankingSubmitStatus = ENUM_SUBMIT_STATUS.SUBMIT_STATUS_IDLE;
			globalVal.g_bSumbit = true;
			globalVal.SaveFile("saveData.txt");
		}
		sUBMIT_STATUS = GameCenterPlugin.SUBMIT_STATUS.SUBMIT_STATUS_IDLE;
		if (m_alAccomplishment == null || 0 >= m_alAccomplishment.Count)
		{
			return;
		}
		Submit submit = null;
		for (int i = 0; i < m_alAccomplishment.Count; i++)
		{
			submit = m_alAccomplishment[i] as Submit;
			if (submit == null)
			{
				m_alAccomplishment.Remove(submit);
				break;
			}
			if (!submit.Wait)
			{
				switch (GameCenterPlugin.SubmitAchievementStatus(c_strAccomplishment[submit.AccomplishmentID], 100))
				{
				case GameCenterPlugin.SUBMIT_STATUS.SUBMIT_STATUS_SUCCESS:
					submitClose(submit.AccomplishmentID);
					m_alAccomplishment.Remove(submit);
					return;
				case GameCenterPlugin.SUBMIT_STATUS.SUBMIT_STATUS_ERROR:
					submit.Wait = true;
					break;
				}
			}
			else if (submit.Wait)
			{
				GameCenterPlugin.SubmitAchievement(c_strAccomplishment[submit.AccomplishmentID], 100);
				submit.Wait = false;
			}
		}
	}

	private void submitClose(int nID)
	{
		if (nID < globalVal.Accomplishment.Length)
		{
			globalVal.Accomplishment[nID * 2 + 1] = true;
			globalVal.SaveFile("saveData.txt");
		}
	}

	public void submitAccomplishment(int nID)
	{
		if (m_alAccomplishment == null)
		{
			m_alAccomplishment = new ArrayList();
		}
		if (nID < c_strAccomplishment.Length)
		{
			Submit submit = new Submit();
			submit.AccomplishmentID = nID;
			submit.Wait = false;
			m_alAccomplishment.Add(submit);
		}
	}

	public void submitRanking()
	{
		m_nRankingSubmitStatus = ENUM_SUBMIT_STATUS.SUBMIT_STATUS_READY;
	}

	public void openRanking()
	{
		GameCenterPlugin.OpenLeaderboard();
	}
}
