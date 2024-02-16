public class GameStatus
{
	public enum ENUM_GAME_PROCESS
	{
		GAME_LOAD = 0,
		GAME_MENU = 1,
		GAME_OPTION = 2,
		GAME_RUNNING = 3,
		GAME_DESTROY = 4
	}

	public enum ENUM_GAME_RUNNING
	{
		RUNNING_SCENE_LOAD = 0,
		RUNNING_SCENE_INITIALIZE = 1,
		RUNNING_GAME_START = 2,
		RUNNING_GAME_READY = 3,
		RUNNING_LOGIC = 4,
		RUNNING_INTO_VEHICLE = 5,
		RUNNING_GAME_OVER = 6,
		RUNNING_MENU = 7,
		RUNNING_PAUSE = 8,
		RUNNING_GAME_DESTROY = 9
	}

	private static GameStatus m_cmStatus;

	private ENUM_GAME_PROCESS m_nProcess;

	private ENUM_GAME_RUNNING m_nScene;

	public static GameStatus GetInstance()
	{
		if (m_cmStatus == null)
		{
			m_cmStatus = new GameStatus();
			m_cmStatus.Initialize();
		}
		return m_cmStatus;
	}

	public void Initialize()
	{
		m_nProcess = ENUM_GAME_PROCESS.GAME_LOAD;
		m_nScene = ENUM_GAME_RUNNING.RUNNING_GAME_DESTROY;
	}

	public void GameProcess(ENUM_GAME_PROCESS nProcessID)
	{
		switch (nProcessID)
		{
		case ENUM_GAME_PROCESS.GAME_LOAD:
			break;
		case ENUM_GAME_PROCESS.GAME_MENU:
			if (m_nProcess != ENUM_GAME_PROCESS.GAME_DESTROY)
			{
				m_nProcess = nProcessID;
			}
			break;
		case ENUM_GAME_PROCESS.GAME_OPTION:
			if (m_nProcess == ENUM_GAME_PROCESS.GAME_MENU)
			{
				m_nProcess = nProcessID;
			}
			break;
		case ENUM_GAME_PROCESS.GAME_RUNNING:
			if (m_nProcess == ENUM_GAME_PROCESS.GAME_RUNNING)
			{
				m_nProcess = nProcessID;
			}
			break;
		case ENUM_GAME_PROCESS.GAME_DESTROY:
			if (m_nProcess == ENUM_GAME_PROCESS.GAME_MENU)
			{
				m_nProcess = nProcessID;
			}
			break;
		}
	}

	public ENUM_GAME_PROCESS getGameProcess()
	{
		return m_nProcess;
	}

	public void setRunningStatus(ENUM_GAME_RUNNING nScene)
	{
		switch (nScene)
		{
		case ENUM_GAME_RUNNING.RUNNING_SCENE_LOAD:
			if (m_nScene == ENUM_GAME_RUNNING.RUNNING_GAME_DESTROY)
			{
				m_nScene = nScene;
			}
			break;
		case ENUM_GAME_RUNNING.RUNNING_SCENE_INITIALIZE:
			if (m_nScene == ENUM_GAME_RUNNING.RUNNING_SCENE_LOAD || m_nScene == ENUM_GAME_RUNNING.RUNNING_GAME_DESTROY)
			{
				m_nScene = nScene;
			}
			break;
		case ENUM_GAME_RUNNING.RUNNING_GAME_START:
			if (m_nScene == ENUM_GAME_RUNNING.RUNNING_SCENE_INITIALIZE)
			{
				m_nScene = nScene;
			}
			break;
		case ENUM_GAME_RUNNING.RUNNING_GAME_READY:
			if (m_nScene == ENUM_GAME_RUNNING.RUNNING_GAME_START || m_nScene == ENUM_GAME_RUNNING.RUNNING_PAUSE || m_nScene == ENUM_GAME_RUNNING.RUNNING_MENU)
			{
				m_nScene = nScene;
			}
			break;
		case ENUM_GAME_RUNNING.RUNNING_LOGIC:
			if (m_nScene == ENUM_GAME_RUNNING.RUNNING_SCENE_INITIALIZE || m_nScene == ENUM_GAME_RUNNING.RUNNING_GAME_READY || m_nScene == ENUM_GAME_RUNNING.RUNNING_MENU || m_nScene == ENUM_GAME_RUNNING.RUNNING_PAUSE || m_nScene == ENUM_GAME_RUNNING.RUNNING_INTO_VEHICLE)
			{
				m_nScene = nScene;
			}
			break;
		case ENUM_GAME_RUNNING.RUNNING_INTO_VEHICLE:
			if (m_nScene == ENUM_GAME_RUNNING.RUNNING_LOGIC)
			{
				m_nScene = nScene;
			}
			break;
		case ENUM_GAME_RUNNING.RUNNING_GAME_OVER:
		case ENUM_GAME_RUNNING.RUNNING_MENU:
		case ENUM_GAME_RUNNING.RUNNING_PAUSE:
			if (m_nScene == ENUM_GAME_RUNNING.RUNNING_LOGIC)
			{
				m_nScene = nScene;
			}
			break;
		case ENUM_GAME_RUNNING.RUNNING_GAME_DESTROY:
			if (m_nScene == ENUM_GAME_RUNNING.RUNNING_GAME_OVER || m_nScene == ENUM_GAME_RUNNING.RUNNING_LOGIC)
			{
				m_nScene = nScene;
			}
			break;
		}
	}

	public ENUM_GAME_RUNNING getRunningStatus()
	{
		return m_nScene;
	}
}
