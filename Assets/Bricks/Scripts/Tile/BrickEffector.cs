using UnityEngine;
using System.Collections;
using DG.Tweening;

public class BrickEffector : BaseTileEffector 
{
	float baseSize = 0;
	Tweener tweener;

	protected override void Awake ()
	{
		base.Awake ();

		baseSize = meshTransform.localScale.y;
	}

	public override void SetForce(float force)
	{
		if (tweener != null)
		{
			if (tweener.IsPlaying())
			{
//				tweener.Pause();
			}
		}

		tweener = meshTransform.DOScaleY(baseSize + force, 1.0f).SetEase(Ease.OutExpo).OnComplete(OnComplete);
	}

	void OnComplete()
	{
		meshTransform.DOScaleY(baseSize, 3.0f).SetEase(Ease.OutSine);
	}
}
