using UnityEngine;
using System.Collections;

public class TouchController : MonoBehaviour 
{
	[SerializeField] Camera boardCamera;

	public static System.Action<Vector3> onTouch;
	public static System.Action onBeginTouch;
	public static System.Action onEndTouch;


	void LateUpdate()
	{
		if (Input.GetMouseButtonDown(0))
		{
			if (onBeginTouch != null)
			{
				onBeginTouch();
			}
		}

		if (Input.GetMouseButton(0))
		{
			Vector3 viewportPosition = GetWorldPosition(Input.mousePosition);

			if (onTouch != null)
			{
				onTouch(viewportPosition);
			}
		}

		if (Input.GetMouseButtonUp(0))
		{
			if (onEndTouch != null)
			{
				onEndTouch();
			}
		}
	}

	Vector3 GetWorldPosition(Vector3 position)
	{
		return boardCamera.ScreenToViewportPoint(position);
	}

}
