using Constants;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressionManager : MonoBehaviour
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
		EventManager.Instance.GameOver += GainExperience;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.GameOver -= GainExperience;
	}

	#endregion


	private void GainExperience()
	{
		CurrentUserProfileKeeper.Experience += Datas.S_GainedExperience;
		
		Debug.Log("Güncel Tecrübe: " + CurrentUserProfileKeeper.Experience);

		ControlLevelUp();
	}

	private void ControlLevelUp()
	{
		if (CurrentUserProfileKeeper.Experience < CurrentUserProfileKeeper.RequiredExperience)
		{
			return;
		}

		int remainigExperience = 0;
		CurrentUserProfileKeeper.Level++;

		if (CurrentUserProfileKeeper.Experience != CurrentUserProfileKeeper.RequiredExperience)
		{
			remainigExperience = CurrentUserProfileKeeper.Experience - CurrentUserProfileKeeper.RequiredExperience;
		}

		EventManager.Instance.UpdateUserData(UserPaths.PrimaryPaths.Progression, UserPaths.ProgressionPaths.Level, CurrentUserProfileKeeper.Level);
		EventManager.Instance.UpdateUserData(UserPaths.PrimaryPaths.Progression, UserPaths.ProgressionPaths.Experience, remainigExperience);
	}

	private void CalculateRequiredExperience()
	{
		
	}

	private void LevelUp(int level, int remainigExperience)
	{


		//int requiredLevel = CurrentUserProfileKeeper.Level * 10;

		//if (CurrentUserProfileKeeper.Level >= requiredLevel)
		//{
		//	if (CurrentUserProfileKeeper.Level > requiredLevel)
		//	{
		//		RankUp();
		//	}
		//}
	}

	private void RankUp(int level)
	{
		FirebaseManager.UserDatabaseReference.Child(UserPaths.ProgressionPaths.Rank).SetValueAsync(level);
	}
}
