using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class InGameSettings : MonoBehaviour 
{
	[SerializeField] TilesGenerator generator;
	[SerializeField] BoardEffector effector;

	[SerializeField] WallSettings wallSettings;
	[SerializeField] Material kernelMaterial;

	[SerializeField] Animator panelAnimator;

	[SerializeField] Text columnsLabel;
	[SerializeField] Text rowsLabel;
	[SerializeField] Text areaLabel;
	[SerializeField] Text forceLabel;

	bool showSettings = false;

	void Start()
	{
		forceLabel.text = kernelMaterial.GetFloat("_Parallax").ToString();
	}

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

	public void ChangeColumns(Slider slider)
	{
		columnsLabel.text = ((int)slider.value).ToString();
		wallSettings.ChangeColumns((int)slider.value);
//		generator.SetWidth((int)slider.value);
	}

	public void ChangeRows(Slider slider)
	{
		rowsLabel.text = ((int)slider.value).ToString();
		wallSettings.ChangeRows((int)slider.value);
//		generator.SetHeight((int)slider.value);
	}

	public void ChangeArea(Slider slider)
	{
		areaLabel.text = slider.value.ToString();
		effector.SetArea(slider.value);
	}
	
	public void ChangeForce(Slider slider)
	{
		forceLabel.text = slider.value.ToString();
		kernelMaterial.SetFloat("_Parallax", slider.value);
//		effector.SetForce(slider.value);
	}

	public void Exit()
	{
		Application.Quit();
	}
}
