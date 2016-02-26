using UnityEngine;
using System.Collections;
using Kvant;

[ExecuteInEditMode]
public class WallSettings : MonoBehaviour 
{
	[SerializeField] Wall wall;

	[Range(10, 320)]
	[SerializeField] int columns = 160;
	[Range(10, 320)]
	[SerializeField] int rows = 90;

	Vector2 currentSize = Vector2.zero;

	public void ChangeColumns(int width)
	{
		columns = width;
	}

	public void ChangeRows(int height)
	{
		rows = height;
	}


	void Update()
	{
		if (wall == null)
		{
			return;
		}

		if (IsSizeChanged())
		{
			ChangeSize();
		}
	}

	void ChangeSize()
	{
		wall.columns = columns;
		wall.rows = rows;

		wall.baseScale = new Vector3(wall.extent.x / columns, wall.extent.y / rows, wall.baseScale.z);
	}

	bool IsSizeChanged()
	{
		if (currentSize.x != columns || currentSize.y != rows)
		{
			currentSize = new Vector2(columns, rows);
			return true;
		}
		else
		{
			return false;
		}
	}

}
