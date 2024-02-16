using System.Collections;
using UnityEngine;

public class ShowGoldGround : MonoBehaviour
{
	private int index;

	private ArrayList array = new ArrayList();

	private bool canUpdate;

	private Vector3 offsetPoint = Vector3.zero;

	private void Start()
	{
	}

	private void Update()
	{
		if (canUpdate)
		{
			for (int i = 0; i < 5; i++)
			{
				UpdateOnce();
			}
		}
	}

	private void UpdateOnce()
	{
		index = array.Count;
		if (index < 1)
		{
			canUpdate = false;
			Object.Destroy(this);
			Object.Destroy(base.gameObject);
			return;
		}
		PointInfo pointInfo = array[index - 1] as PointInfo;
		if (pointInfo != null)
		{
			GameObject item = ItemManagerClass.body.GetItem(pointInfo.name);
			if (item != null)
			{
				if (item.GetComponent(typeof(GoldRotateScript)) == null)
				{
					item.AddComponent(typeof(GoldRotateScript));
				}
				item.name = pointInfo.name;
				item.GetComponent<Collider>().enabled = true;
				item.active = true;
				Vector3 position = offsetPoint + pointInfo.pos;
				position.y -= 100f;
				position.z = 0f;
				position.x -= 8f;
				item.transform.position = position;
			}
		}
		index--;
		array.RemoveAt(index);
	}

	public void StartShow(ArrayList groundArray, Vector3 offset)
	{
		array.Clear();
		array = (ArrayList)groundArray.Clone();
		offsetPoint = offset;
		index = array.Count;
		canUpdate = true;
	}
}
