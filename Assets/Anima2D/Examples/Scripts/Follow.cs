using UnityEngine;
using System.Collections;

[ExecuteInEditMode]
public class Follow : MonoBehaviour
{
	public Transform target;
	public Vector3 offset;

	void LateUpdate()
	{
		if(target)
		{
			//transform.position = target.position + offset;
			transform.position = new Vector3 (target.transform.position.x, 4f, -10f);
		}
	}
}
