using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[ExecuteInEditMode]
public class TilesGenerator : MonoBehaviour 
{
	[Range(5, 200)]
	[SerializeField] int width = 36;

	[Range(5, 200)]
	[SerializeField] int height = 20;

	[SerializeField] Transform background;
	[SerializeField] BaseTile tilePrefab;

	List<BaseTile> allTiles;
	Vector2 currentSize = Vector2.zero;

	float backgroudMeshSize = 10;
	Vector2 backgroundSize = Vector2.zero;
	Vector2 cellSize = Vector2.zero;

	void Awake()
	{
		allTiles = new List<BaseTile>();

		if (background != null)
		{
			SetBackgroundDimensions();
		}
	}

	void Start() 
	{
		GenerateTiles();
	}


	void Update() 
	{
		if (IsSizeChanged())
		{
			GenerateTiles();
		}
	}

	void GenerateTiles()
	{
		RemoveOldTiles();
		SetBackgroundDimensions();

		for (int i = 0; i < width; i++)
		{
			for (int j = 0; j < height; j++)
			{
				BaseTile tile = GameObject.Instantiate<BaseTile>(tilePrefab);
				tile.transform.position = GetTileGlobalPosition(i, j);
				tile.transform.rotation = Quaternion.identity;
				tile.transform.localScale = GetTileSize();

				tile.transform.parent = this.transform;
				TrimName(tile.gameObject);

				allTiles.Add(tile);
			}
		}
	}

	void RemoveOldTiles()
	{
		try
		{
			foreach (BaseTile tile in allTiles)
			{
				DestroyImmediate(tile.gameObject);
			}
		} 
		catch (System.Exception ex)
		{
			Debug.LogException(ex);
		}

		allTiles = new List<BaseTile>();
	}

	Vector3 GetTileGlobalPosition(int x, int y)
	{
		Vector3 position = Vector3.zero;

		position.x = cellSize.x * x - backgroundSize.x / 2 + cellSize.x / 2;
		position.y = background.position.y;
		position.z = cellSize.y * y - backgroundSize.y / 2 + cellSize.y / 2;

		return position;
	}

	Vector3 GetTileSize()
	{
		Vector3 size = Vector3.one;
		size.x = cellSize.x;
		size.y = Random.Range(1.0f, 5.0f);
		size.z = cellSize.y;

		return size;
	}

	void SetBackgroundDimensions()
	{
		cellSize = new Vector2(backgroundSize.x / width, backgroundSize.y / height);
		backgroundSize = new Vector2(background.localScale.x * backgroudMeshSize, background.localScale.z * backgroudMeshSize);
	}

	bool IsSizeChanged()
	{
		if (currentSize.x != width || currentSize.y != height)
		{
			currentSize = new Vector2(width, height);
			return true;
		}
		else
		{
			return false;
		}
	}

	//Becase I don't like names like brick(Clone)
	private void TrimName(GameObject someObject)
	{
		someObject.name = someObject.name.Replace("(Clone)", "");
	}

}
