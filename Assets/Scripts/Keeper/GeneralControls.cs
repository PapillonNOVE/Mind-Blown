using ConstantKeeper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GeneralControls : MonoBehaviour
{
    public static bool isQuitting;

	private void Start()
	{
		EnteringControl();
		StartCoroutine(LoadCategories());
	}

	private void OnApplicationQuit()
	{
		isQuitting = true;
	}

	private void EnteringControl() 
	{
		if (PlayerPrefs.HasKey(PlayerPrefsKeys.FIRST_ENTRY))
		{
			return;
		}
		else
		{
			PlayerPrefs.SetString(PlayerPrefsKeys.FIRST_ENTRY, "False");
		}
	}

	private IEnumerator LoadCategories() 
	{
		Category category = new Category
		{
			categories = new List<string>()
		};

		string reading = File.ReadAllText(LocalPaths.CATEGORY_SAVE_PATH);

		category = JsonUtility.FromJson<Category>(reading);

		while (FirebaseManager.PublishedQuestionsDatabaseReference == null)
		{
			yield return null;
		}

		StartCoroutine(ActionManager.Instance.GetQuestionIDs(category.categories));
	}

	public static void ControlQuit(in Action targetFunc)
	{
		if (isQuitting)
		{
			return;
		}

		targetFunc();
	}

}
