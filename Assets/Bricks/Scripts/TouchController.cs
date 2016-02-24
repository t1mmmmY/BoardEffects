using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour 
{
	[SerializeField] Camera boardCamera;

	public static System.Action<Vector3> onTouch;

	void Update()
	{
		if (Input.GetMouseButton(0))
		{
			Vector3 viewportPosition = GetWorldPosition(Input.mousePosition);

			if (onTouch != null)
			{
				onTouch(viewportPosition);
			}
		}
	}

	Vector3 GetWorldPosition(Vector3 position)
	{
		return boardCamera.ScreenToViewportPoint(position);
	}

}
