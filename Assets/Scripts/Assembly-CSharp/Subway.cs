using UnityEngine;

public class Subway : MonoBehaviour
{
	public string TrainName;

	private void Start()
	{
	}

	private void Update()
	{
	}

	public void shootTrain(WayPointNode cmNode)
	{
		if (TrainName == null || 0 >= TrainName.Length)
		{
			Debug.Log("TrainName is error ! " + TrainName);
			return;
		}
		Transform channel = globalVal.Channel.getChannel(TrainName);
		if (null == channel)
		{
			Debug.Log("Train is null !");
			return;
		}
		Train train = channel.GetComponent(typeof(Train)) as Train;
		if (null == train)
		{
			Debug.Log("Script is null !");
			return;
		}
		channel.position = cmNode.WayPoint.position;
		channel.gameObject.SetActiveRecursively(true);
		train.init(cmNode.PrePoint);
	}
}
