using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameSettings : MonoBehaviour 
{
	[SerializeField] TilesGenerator generator;
	[SerializeField] BoardEffector effector;

	[SerializeField] Animator panelAnimator;

	bool showSettings = false;

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			Exit();
		}
	}

	public void ToggleSettings()
	{
		showSettings = !showSettings;
		panelAnimator.SetBool("show", showSettings);
	}

	public void ChangeWidth(Slider slider)
	{
		generator.SetWidth((int)slider.value);
	}

	public void ChangeHeight(Slider slider)
	{
		generator.SetHeight((int)slider.value);
	}

	public void ChangeArea(Slider slider)
	{
		effector.SetArea(slider.value);
	}
	
	public void ChangeForce(Slider slider)
	{
		effector.SetForce(slider.value);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
