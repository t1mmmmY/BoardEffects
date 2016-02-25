using UnityEngine;
using System.Collections;

public class BoardEffector : MonoBehaviour 
{
	[Range(0.0f, 0.5f)]
	[SerializeField] float touchAreaMultiplicator = 0.05f;

	[Range(0.0f, 40.0f)]
	[SerializeField] float forceMultiplicator = 20;

	BaseTile[] allTiles;

	void Awake()
	{
		TilesGenerator.onCreateTiles += OnCreateTiles;
		TouchController.onTouch += OnTouch;
	}
	
	void OnDestroy()
	{
		TilesGenerator.onCreateTiles -= OnCreateTiles;
		TouchController.onTouch -= OnTouch;
	}

	public void SetArea(float area)
	{
		touchAreaMultiplicator = area;
	}

	public void SetForce(float force)
	{
		forceMultiplicator = force;
	}

	void OnCreateTiles(BaseTile[] allTiles)
	{
		this.allTiles = allTiles;
	}

	void OnTouch(Vector3 viewportPosition)
	{
		foreach (BaseTile tile in allTiles)
		{
			float force = GetForce(tile.viewportPosition, viewportPosition);
			tile.SetForce(force);
		}
	}

	float GetForce(Vector2 tilePos, Vector2 touchPos)
	{
		float distance = Vector2.Distance(tilePos, touchPos) / touchAreaMultiplicator;
		if (distance == 0)
		{
			return forceMultiplicator;
		}

		float force = Mathf.Clamp(forceMultiplicator / distance, 0.0f, forceMultiplicator);

		return force;
	}

}
