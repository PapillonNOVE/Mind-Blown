using Constants;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class CategorySaveLoadManager : MonoBehaviour
{

	private void OnEnable()
	{
		Subscribe();
	}

	private void OnDisable()
	{
		GeneralControls.ControlQuit(Unsubscribe);
	}

	#region Event Subscribe/Unsubscribe

	private void Subscribe()
	{
		EventManager.Instance.SaveCategories += SaveCategories;
		EventManager.Instance.LoadCategories += LoadCategories;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.SaveCategories -= SaveCategories;
		EventManager.Instance.LoadCategories -= LoadCategories;
	}

	#endregion

	private void SaveCategories(List<Toggle> specificalCategoryToggles)
	{
		CategoryStateHolder categoryStateHolder = new CategoryStateHolder
		{
			CategoryToggleStates = new List<CategoryToggleState>()
		};

		foreach (Toggle spesificalCategoryToggle in specificalCategoryToggles)
		{
			CategoryToggleState categoryToggleState = new CategoryToggleState
			{
				isActive = spesificalCategoryToggle.isOn,
				categoryType = spesificalCategoryToggle.GetComponent<CategoryInfo>().category,
				name = spesificalCategoryToggle.GetComponent<CategoryInfo>().category.ToString()
			};

			categoryStateHolder.CategoryToggleStates.Add(categoryToggleState);
		}

		string json = JsonUtility.ToJson(categoryStateHolder);

		Debug.Log(LocalPaths.CATEGORY_SAVE_PATH);

		File.WriteAllText(LocalPaths.CATEGORY_SAVE_PATH, json);

		if (categoryStateHolder.CategoryToggleStates.Count > 0)
		{
			PlayerPrefs.SetString(PlayerPrefsKeys.CATEGORY_SELECTED, PlayerPrefsKeys.CATEGORY_SELECTED);
		}
	}

	private CategoryStateHolder LoadCategories()
	{
		CategoryStateHolder categoryStateHolder = new CategoryStateHolder();

		string reading = File.ReadAllText(LocalPaths.CATEGORY_SAVE_PATH);

		categoryStateHolder = JsonUtility.FromJson<CategoryStateHolder>(reading);

		return categoryStateHolder;
	}
}
