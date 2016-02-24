using UnityEngine;
using System.Collections;

public abstract class BaseTile : MonoBehaviour 
{
	public Vector2 viewportPosition { get; private set; }
	public float force { get; protected set; }


	public void SetViewportPosition(Vector2 position)
	{
		viewportPosition = position;
	}

	public virtual void SetForce(float force)
	{
		this.force = force;
	}

}
