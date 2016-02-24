using UnityEngine;
using System.Collections;

public abstract class BaseTileEffector : MonoBehaviour 
{
	[SerializeField] protected MeshRenderer meshRenderer;
	[SerializeField] protected Transform meshTransform;

	//if need to change exactly this object. Mesh renderer also from this object.
	[SerializeField] bool selfObject = true;

	protected float currentForce = 0;

	protected virtual void Awake()
	{
		if (selfObject)
		{
			meshRenderer = this.GetComponent<MeshRenderer>();
			meshTransform = this.transform;
			
			if (meshRenderer == null)
			{
				Debug.LogError("MeshRenderer == null; " + this.gameObject.name);
			}
		}
	}


	public abstract void SetForce(float force);
}
