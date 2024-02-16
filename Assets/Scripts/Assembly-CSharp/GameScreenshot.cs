using UnityEngine;

public class GameScreenshot
{
	private static GameScreenshot m_cmInstance;

	public static GameScreenshot GetInstance()
	{
		if (m_cmInstance == null)
		{
			m_cmInstance = new GameScreenshot();
		}
		return m_cmInstance;
	}

	public RenderTexture Screenshot(Transform cmTarget)
	{
		Camera camera = GameObject.Find("Screenshot").GetComponent<Camera>();
		camera.enabled = true;
		Vector3 position = cmTarget.position;
		position.y += 5f;
		camera.transform.position = position;
		Transform transform = cmTarget.Find("Charactor");
		if (null != transform)
		{
			camera.transform.LookAt(transform.position);
		}
		RenderTexture renderTexture2 = (camera.targetTexture = new RenderTexture((int)Screen.width, (int)Screen.height, 24));
		camera.Render();
		RenderTexture.active = renderTexture2;
		Texture2D texture2D = new Texture2D(renderTexture2.width, renderTexture2.height, TextureFormat.RGB24, false);
		texture2D.ReadPixels(new Rect(0f, 0f, renderTexture2.width, renderTexture2.height), 0, 0);
		RenderTexture.active = null;
		camera.enabled = false;
		return renderTexture2;
	}
}
