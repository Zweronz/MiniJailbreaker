using System.Collections.Generic;
using UnityEngine;

public class TAudioManager : MonoBehaviour
{
	private static string version = "1.4.3";

	private List<AudioSource> m_loopSounds = new List<AudioSource>();

	private List<AudioSource> m_loopMusics = new List<AudioSource>();

	private List<ITAudioEvent> m_loopSoundEvts = new List<ITAudioEvent>();

	private List<ITAudioEvent> m_loopMusicEvts = new List<ITAudioEvent>();

	private Dictionary<string, List<ITAudioRule>> m_audio_rules = new Dictionary<string, List<ITAudioRule>>();

	private bool m_isMusicOn = true;

	private bool m_isSoundOn = true;

	private AudioListener audioListener;

	private static TAudioManager s_instance;

	public static TAudioManager instance
	{
		get
		{
			if (s_instance == null && Application.isPlaying)
			{
				GameObject target = new GameObject("TAudioManager", typeof(TAudioManager));
				Object.DontDestroyOnLoad(target);
			}
			return s_instance;
		}
	}

	public static bool checkInstance
	{
		get
		{
			return s_instance != null;
		}
	}

	public bool isMusicOn
	{
		get
		{
			return m_isMusicOn;
		}
		set
		{
			m_isMusicOn = value;
			if (m_isMusicOn)
			{
				foreach (AudioSource loopMusic in m_loopMusics)
				{
					loopMusic.Play();
				}
				foreach (ITAudioEvent loopMusicEvt in m_loopMusicEvts)
				{
					loopMusicEvt.Trigger();
				}
			}
			else
			{
				foreach (AudioSource loopMusic2 in m_loopMusics)
				{
					loopMusic2.Stop();
				}
				foreach (ITAudioEvent loopMusicEvt2 in m_loopMusicEvts)
				{
					loopMusicEvt2.Stop();
				}
			}
			PlayerPrefs.SetInt("MusicOff", (!m_isMusicOn) ? 1 : 0);
		}
	}

	public bool isSoundOn
	{
		get
		{
			return m_isSoundOn;
		}
		set
		{
			m_isSoundOn = value;
			if (m_isSoundOn)
			{
				foreach (AudioSource loopSound in m_loopSounds)
				{
					loopSound.Play();
				}
				foreach (ITAudioEvent loopSoundEvt in m_loopSoundEvts)
				{
					loopSoundEvt.Trigger();
				}
			}
			else
			{
				foreach (AudioSource loopSound2 in m_loopSounds)
				{
					loopSound2.Stop();
				}
				foreach (ITAudioEvent loopSoundEvt2 in m_loopSoundEvts)
				{
					loopSoundEvt2.Stop();
				}
			}
			PlayerPrefs.SetInt("SoundOff", (!m_isSoundOn) ? 1 : 0);
		}
	}

	public AudioListener AudioListener
	{
		get
		{
			return audioListener;
		}
	}

	private void Awake()
	{
		m_isMusicOn = PlayerPrefs.GetInt("MusicOff") == 0;
		m_isSoundOn = PlayerPrefs.GetInt("SoundOff") == 0;
		if (s_instance != null)
		{
			Object.Destroy(s_instance.gameObject);
		}
		AudioListener audioListener = Object.FindObjectOfType(typeof(AudioListener)) as AudioListener;
		if (!audioListener)
		{
			GameObject gameObject = new GameObject("AudioListener", typeof(AudioListener));
			Object.DontDestroyOnLoad(gameObject);
			audioListener = gameObject.GetComponent<AudioListener>();
		}
		this.audioListener = audioListener;
		s_instance = this;
	}

	private void OnDestroy()
	{
		s_instance = null;
	}

	public void Pause(AudioSource audio)
	{
		audio.Pause();
	}

	public void PlaySound(AudioSource audio)
	{
		if (TryPlay(audio.gameObject, audio.clip.length / audio.pitch))
		{
			if (audio.loop && !m_loopSounds.Contains(audio))
			{
				m_loopSounds.Add(audio);
			}
			if (m_isSoundOn)
			{
				audio.Play();
			}
		}
	}

	public AudioSource PlaySound(AudioClip clip, bool loop)
	{
		GameObject gameObject = new GameObject(clip.name + "_SFX", typeof(AudioSource));
		gameObject.GetComponent<AudioSource>().loop = loop;
		gameObject.GetComponent<AudioSource>().clip = clip;
		gameObject.GetComponent<AudioSource>().playOnAwake = false;
		if (loop)
		{
			m_loopSounds.Add(gameObject.GetComponent<AudioSource>());
		}
		if (m_isSoundOn)
		{
			gameObject.GetComponent<AudioSource>().Play();
		}
		return gameObject.GetComponent<AudioSource>();
	}

	public void PlaySound(AudioSource audio, AudioClip clip, bool loop, bool cutoff)
	{
		if (!TryPlay(audio.gameObject, clip.length / audio.pitch))
		{
			return;
		}
		if (loop)
		{
			if (!m_loopSounds.Contains(audio))
			{
				m_loopSounds.Add(audio);
			}
			audio.loop = true;
			audio.clip = clip;
			if (m_isSoundOn)
			{
				audio.Play();
			}
			return;
		}
		if (m_loopSounds.Contains(audio))
		{
			m_loopSounds.Remove(audio);
		}
		audio.loop = false;
		if (cutoff)
		{
			audio.clip = clip;
		}
		if (m_isSoundOn)
		{
			if (cutoff)
			{
				audio.Play();
			}
			else
			{
				audio.PlayOneShot(clip);
			}
		}
	}

	public void PlaySound(AudioSource audio, AudioClip clip, bool loop)
	{
		PlaySound(audio, clip, loop, false);
	}

	public void StopSound(AudioSource audio)
	{
		audio.Stop();
		if (m_loopSounds.Contains(audio))
		{
			m_loopSounds.Remove(audio);
		}
		TryStop(audio.gameObject);
	}

	public void PlayMusic(AudioSource audio)
	{
		if (TryPlay(audio.gameObject, audio.clip.length / audio.pitch))
		{
			if (audio.loop && !m_loopMusics.Contains(audio))
			{
				m_loopMusics.Add(audio);
			}
			if (m_isMusicOn)
			{
				audio.Play();
			}
		}
	}

	public AudioSource PlayMusic(AudioClip clip, bool loop)
	{
		GameObject gameObject = new GameObject(clip.name + "_BGM", typeof(AudioSource));
		gameObject.GetComponent<AudioSource>().loop = loop;
		gameObject.GetComponent<AudioSource>().clip = clip;
		gameObject.GetComponent<AudioSource>().playOnAwake = false;
		if (loop)
		{
			m_loopMusics.Add(gameObject.GetComponent<AudioSource>());
		}
		if (m_isMusicOn)
		{
			gameObject.GetComponent<AudioSource>().Play();
		}
		return gameObject.GetComponent<AudioSource>();
	}

	public void PlayMusic(AudioSource audio, AudioClip clip, bool loop)
	{
		PlayMusic(audio, clip, loop, false);
	}

	public void PlayMusic(AudioSource audio, AudioClip clip, bool loop, bool cutoff)
	{
		if (!TryPlay(audio.gameObject, clip.length / audio.pitch))
		{
			return;
		}
		if (loop)
		{
			audio.loop = true;
			audio.clip = clip;
			if (!m_loopMusics.Contains(audio))
			{
				m_loopMusics.Add(audio);
			}
			if (m_isMusicOn)
			{
				audio.Play();
			}
			return;
		}
		audio.loop = false;
		if (m_loopMusics.Contains(audio))
		{
			m_loopMusics.Remove(audio);
		}
		if (cutoff)
		{
			audio.clip = clip;
		}
		if (m_isMusicOn)
		{
			if (cutoff)
			{
				audio.Play();
			}
			else
			{
				audio.PlayOneShot(clip);
			}
		}
	}

	public void StopMusic(AudioSource audio)
	{
		audio.Stop();
		if (m_loopMusics.Contains(audio))
		{
			m_loopMusics.Remove(audio);
		}
		TryStop(audio.gameObject);
	}

	public void ClearLoop()
	{
		m_loopMusics.Clear();
		m_loopSounds.Clear();
		m_loopSoundEvts.Clear();
	}

	public void AddLoopSoundEvt(ITAudioEvent evt)
	{
		if (m_loopSoundEvts.Contains(evt))
		{
			m_loopSoundEvts.Add(evt);
		}
	}

	public void AddLoopMusicEvt(ITAudioEvent evt)
	{
		if (m_loopMusicEvts.Contains(evt))
		{
			m_loopMusicEvts.Add(evt);
		}
	}

	private bool TryPlay(GameObject go, float length)
	{
		string text = go.name;
		for (int num = text.IndexOf("(Clone)"); num >= 0; num = text.IndexOf("(Clone)"))
		{
			text = text.Substring(0, num);
		}
		List<ITAudioRule> value = null;
		if (m_audio_rules.TryGetValue(text, out value))
		{
			float over_time = Time.realtimeSinceStartup + length;
			foreach (ITAudioRule item in value)
			{
				if (!item.Try(text, go, over_time))
				{
					return false;
				}
			}
		}
		return true;
	}

	private void TryStop(GameObject go)
	{
		string text = go.name;
		for (int num = text.IndexOf("(Clone)"); num >= 0; num = text.IndexOf("(Clone)"))
		{
			text = text.Substring(0, num);
		}
		List<ITAudioRule> value = null;
		if (!m_audio_rules.TryGetValue(text, out value))
		{
			return;
		}
		foreach (ITAudioRule item in value)
		{
			item.Stop(go);
		}
	}

	public void RegistRule(string name, ITAudioRule rule)
	{
		List<ITAudioRule> value = null;
		if (m_audio_rules.TryGetValue(name, out value))
		{
			value.Add(rule);
			return;
		}
		value = new List<ITAudioRule>();
		value.Add(rule);
		m_audio_rules.Add(name, value);
	}
}
