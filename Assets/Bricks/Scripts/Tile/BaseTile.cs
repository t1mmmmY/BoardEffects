using UnityEngine;
using System.Collections;

public abstract class BaseTile : MonoBehaviour 
{
	[SerializeField] MeshRenderer meshRenderer;
	[SerializeField] Transform meshTransform;

	//if need to change exactly this object. Mesh renderer also from this object.
	[SerializeField] bool selfObject = true;

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

}
