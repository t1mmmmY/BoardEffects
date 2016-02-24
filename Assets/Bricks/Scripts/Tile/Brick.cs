using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BrickEffector))]
public class Brick : BaseTile 
{
	BrickEffector effector;

	void Awake()
	{
		effector = GetComponent<BrickEffector>();
	}

	public override void SetForce(float force)
	{
		base.SetForce(force);

		effector.SetForce(force);
	}
}
