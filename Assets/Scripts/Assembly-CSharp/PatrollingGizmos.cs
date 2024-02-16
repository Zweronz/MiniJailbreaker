using UnityEngine;

public class PatrollingGizmos : MonoBehaviour
{
	public Color drawColor;

	public Vector3 lastPoint;

	public void OnDrawGizmos()
	{
		Gizmos.color = drawColor;
		Gizmos.DrawSphere(base.transform.position, 0.6f);
		if (lastPoint == Vector3.zero)
		{
			lastPoint = base.transform.position;
		}
		Gizmos.DrawLine(base.gameObject.transform.position, lastPoint);
	}

	public void OnSelectionGizmos()
	{
		Gizmos.color = drawColor;
		Gizmos.DrawSphere(base.transform.position, 1f);
		if (lastPoint == Vector3.zero)
		{
			lastPoint = base.transform.position;
		}
		Gizmos.DrawLine(base.gameObject.transform.position, lastPoint);
	}
}
