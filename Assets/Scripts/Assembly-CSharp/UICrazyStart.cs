using UnityEngine;

public class UICrazyStart : MonoBehaviour, TUIHandler
{
	private TUI m_tui;

	public void Start()
	{
		m_tui = TUI.Instance("TUI");
		m_tui.SetHandler(this);
		m_tui.transform.Find("TUIControl").Find("Fade").GetComponent<TUIFade>()
			.FadeIn();
	}

	public void Update()
	{
		TUIInput[] input = TUIInputManager.GetInput();
		for (int i = 0; i < input.Length; i++)
		{
			m_tui.HandleInput(input[i]);
		}
	}

	public void HandleEvent(TUIControl control, int eventType, float wparam, float lparam, object data)
	{
		if (control.name == "Button" && eventType == 3)
		{
			OnNextScene();
		}
	}

	public void OnNextScene()
	{
		m_tui.transform.Find("TUIControl").Find("Fade").GetComponent<TUIFade>()
			.FadeOut("CrazyUI");
	}
}
