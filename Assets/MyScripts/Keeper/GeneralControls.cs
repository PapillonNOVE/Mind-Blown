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


		//	int totalExp = 0;
		//	int requiredExperience = 0;

		//	for (int i = 1; i <= 100; i++)
		//	{
		//		//requiredExperience = ((int)Mathf.Pow(i + 1, 2f) - (int)Mathf.Pow(i, 2f)) * 5 + 85;
		//		requiredExperience = i * 10 + 90;

		//		totalExp += requiredExperience;

		//		//if (i == 1 || i == 2 || i == 3 || i == 4 || i == 5 || i == 10 || i == 20 || i == 50 || i == 99)
		//		{
		//			Debug.LogError(requiredExperience + " : " + i);
		//		}
		//	}

		//	Debug.LogWarning(totalExp + " Toplam gereken exp ");
		//	Debug.LogWarning(totalExp / 10 + " Toplam gereken soru ");
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

		StartCoroutine(EventManager.Instance.GetQuestionIDs(category.categories));
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
