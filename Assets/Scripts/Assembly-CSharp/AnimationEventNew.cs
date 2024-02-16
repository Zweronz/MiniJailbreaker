using UnityEngine;

public class AnimationEventNew : MonoBehaviour
{
	private enum StoryAniState
	{
		NONE = 0,
		RUN = 1,
		JUMP = 2
	}

	private float startTime;

	private float endTime;

	private bool updateKey;

	private Vector3 startPos = Vector3.zero;

	private Vector3 endPos = Vector3.zero;

	private TAudioController audios;

	private StoryAniState state;

	private void Start()
	{
		GameObject gameObject = GameObject.Find("TAudioController");
		if (gameObject == null)
		{
			gameObject = new GameObject("TAudioController");
			gameObject.AddComponent(typeof(TAudioController));
			Object.DontDestroyOnLoad(gameObject);
		}
		audios = gameObject.GetComponent(typeof(TAudioController)) as TAudioController;
	}

	private void Update()
	{
		if (updateKey)
		{
			startTime += Time.deltaTime;
			float num = 0f;
			num = startTime / endTime;
			if (num > 1f)
			{
				num = 1f;
				updateKey = false;
				MoveEnd();
			}
			base.transform.localPosition = Vector3.Lerp(startPos, endPos, num);
		}
	}

	private void AddAttackEvent()
	{
	}

	private void PlayAnimationOnEnd(string name)
	{
		base.GetComponent<Animation>()[name].speed = 0.1f;
		base.GetComponent<Animation>().PlayQueued(name);
	}

	private void PlayEffectOnEnd(string name)
	{
	}

	private void ClimbStopEvent()
	{
		base.GetComponent<Animation>().Play("storyrun", AnimationPlayMode.Queue);
		base.transform.localPosition = new Vector3(1.250087f, -6.257381f, -5.021229f);
		base.transform.localEulerAngles = Vector3.zero;
		StartMove(endpos: new Vector3(0f, -6.257381f, 2.78f), startpos: base.transform.localPosition, endtime: 1f);
		state = StoryAniState.RUN;
		MonoBehaviour.print("storyjump");
	}

	private void PlayAudio(string name)
	{
		Debug.Log("PlayerAudio name = " + name);
		audios.PlayAudio(name);
	}

	private void StoryJump()
	{
		base.GetComponent<Animation>().CrossFade("storyjump");
		StartMove(endpos: new Vector3(0f, -3f, 3.83f), startpos: base.transform.localPosition, endtime: 0.5f);
		state = StoryAniState.JUMP;
	}

	private void MoveEnd()
	{
		StoryAniState storyAniState = state;
		if (storyAniState == StoryAniState.RUN)
		{
			StoryJump();
		}
	}

	private void StartMove(Vector3 startpos, Vector3 endpos, float endtime)
	{
		startPos = startpos;
		endPos = endpos;
		endTime = endtime;
		startTime = 0f;
		updateKey = true;
	}
}
