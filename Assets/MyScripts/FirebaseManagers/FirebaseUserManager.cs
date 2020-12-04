using Constants;
using Firebase.Database;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System;


public enum Ranks
{
	// = 1,
	/*
	
	bilgili 
	kitap kurdu
	entelektüel
	ayaklı kütüphane
	allamei cihan
	kozmik bilge
	 */
	Beginner = 1,
	Intermediate = 5,
	Upper = 10,
	Better = 25,
	Best = 50,
	Infinity = 100
}

public class FirebaseUserManager : MonoBehaviour
{
	private void OnEnable()
	{
		Subscribe();
		StartCoroutine(AddUserDataListener());
	}

	private void OnDisable()
	{
		GeneralControls.ControlQuit(Unsubscribe);
		RemoveUserDataListener();
	}

	#region Event Subscribe/Unsubscribe

	private void Subscribe()
	{
		EventManager.Instance.CreatUserProfile += CreateUserProfile;
		EventManager.Instance.UpdateUserData += UpdateUserData;
		EventManager.Instance.GetCurrentUserProfile += GetCurrentUserProfile;
		EventManager.Instance.DeleteUserProfile += DeleteUserProfile;

		EventManager.Instance.ControlIsUsernameExist += ControlIsUsernameExist;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.CreatUserProfile -= CreateUserProfile;
		EventManager.Instance.UpdateUserData -= UpdateUserData;
		EventManager.Instance.GetCurrentUserProfile -= GetCurrentUserProfile;
		EventManager.Instance.DeleteUserProfile -= DeleteUserProfile;

		EventManager.Instance.ControlIsUsernameExist -= ControlIsUsernameExist;
	}

	#endregion

	//--------------------------------------------------

	#region DataListener Subscribe/Unsubscribe

	private IEnumerator AddUserDataListener()
	{
		while (FirebaseManager.UserDatabaseReference == null)
		{
			yield return null;
		}

		FirebaseManager.UserDatabaseReference.ChildChanged += UserProfileDataListener;
		//FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.General).ChildChanged += GetuserDAtaBridge;
		//FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.Progression).ChildChanged += GetuserDAtaBridge;
		//FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.Consumable).ChildChanged += GetuserDAtaBridge;
	}

	private void RemoveUserDataListener()
	{
		FirebaseManager.UserDatabaseReference.ChildChanged -= UserProfileDataListener;
		//FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.General).ChildChanged -= UserProfileDataListener;
		//FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.Progression).ChildChanged -= UserProfileDataListener;
		//FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.Consumable).ChildChanged -= UserProfileDataListener;
	}

	#endregion

	private IEnumerator ControlIsUsernameExist(string _Username, Action onSuccessCallback, Action onFailCallback)
	{
		Task<DataSnapshot> task = FirebaseManager.UserNullDatabaseReference.GetValueAsync();

		yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);

		if (task.IsCanceled)
		{
			Debug.LogWarning(GetDataTaskDebugs.GetData + DebugPaths.IsCanceled);
		}
		else if (task.IsFaulted)
		{
			Debug.LogError(GetDataTaskDebugs.GetData + DebugPaths.IsFaulted);
		}
		else if (task.IsCompleted)
		{
			DataSnapshot snapshot = task.Result;

			if (snapshot.ChildrenCount <= 0)
			{
				onSuccessCallback();
			}
			else
			{
				foreach (DataSnapshot userID in snapshot.Children)
				{
					string username = userID.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.Username).Value.ToString();

					if (username.Equals(_Username))
					{
						onFailCallback();
						break;
					}

					onSuccessCallback();
				}
			}
		}
	}

	private void CreateUserProfile(string username, string language)
	{

		#region General -------------------------------------------------

		UserGeneralMold userGeneralMold = new UserGeneralMold
		{
			Username = username,
			SignUpDate = DateTime.Now.ToString("dd/MM/yyyy"),
			Language = language,
			SignInStatus = UserPaths.GeneralPaths.Online
		};

		string generalJson = JsonUtility.ToJson(userGeneralMold);
		Debug.Log(generalJson);
		FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.General).SetRawJsonValueAsync(generalJson);

		#endregion


		#region Progression ---------------------------------------------

		UserProgressionMold userProgressionMold = new UserProgressionMold
		{
			SeenQuestions = 0,
			Experience = 0,
			WrongAnswers = 0,
			CorrectAnswers = 0,
			Level = 0,
			Rank = "Cahil",
			HighScore = 0,
			TotalPlayTime = 0f
		};

		string progressionJson = JsonUtility.ToJson(userProgressionMold);
		Debug.Log(progressionJson);
		FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.Progression).SetRawJsonValueAsync(progressionJson);

		#endregion


		#region Consumable ----------------------------------------------

		UserConsumableMold userConsumableMold = new UserConsumableMold
		{
			Energy = 5,
			Gem = 0,
			Papcoin = 100,
			Joker = 3
		};

		string consumableJson = JsonUtility.ToJson(userConsumableMold);
		Debug.Log(consumableJson);
		FirebaseManager.UserDatabaseReference.Child(UserPaths.PrimaryPaths.Consumable).SetRawJsonValueAsync(consumableJson);

		#endregion

	}

	private void UpdateUserData(string primaryPath, string key, object value)
	{
		//AddUserDataListener();
		Debug.Log("Database referansı " + FirebaseManager.UserDatabaseReference.Child(primaryPath).Child(key));
		FirebaseManager.UserDatabaseReference.Child(primaryPath).Child(key).SetValueAsync(value);
	}

	private void GetUsers()
	{
		FirebaseManager.UserDatabaseReference.GetValueAsync().ContinueWith(task =>
		{
			if (task.IsFaulted)
			{
				//           LogTaskCompletion(task, "Kullanıcı verileri çekme işlemi");
			}
			else if (task.IsCompleted)
			{
				DataSnapshot snapshot = task.Result;
				Dictionary<string, object> dictionary = new Dictionary<string, object>();

				foreach (DataSnapshot x in snapshot.Children)
				{
					string key = x.Key;
					object value = snapshot.Child(key).Value;
					//Debug.Log(key);
					//Debug.Log(value);
					dictionary.Add(key, value);
					Debug.Log(dictionary);
				}
			}
		}
		);
	}

	private IEnumerator GetCurrentUserProfile()
	{
		Task<DataSnapshot> task = FirebaseManager.UserDatabaseReference.GetValueAsync();

		yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);

		if (task.IsCanceled)
		{
			Debug.LogWarning(UserTaskDebugs.GetCurrentUserProfile + DebugPaths.IsCanceled);
		}
		else if (task.IsFaulted)
		{
			Debug.LogError(UserTaskDebugs.GetCurrentUserProfile + DebugPaths.IsFaulted);
		}
		else if (task.IsCompleted)
		{
			DataSnapshot snapshot = task.Result;

			//string json = snapshot.GetRawJsonValue();
			//Debug.Log(json);

			// String General 
			CurrentUserProfileKeeper.Username = snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.Username).Value.ToString();
			//CurrentUserProfileKeeper.Country = snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.Country).Value.ToString();
			CurrentUserProfileKeeper.Language = snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.Language).Value.ToString();
			CurrentUserProfileKeeper.SignUpDate = snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.SignUpDate).Value.ToString();
			//CurrentUserProfileKeeper.LastSeen = snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.LastSeen).Value.ToString();

			// Bool General                                   
			//CurrentUserProfileKeeper.SignInStatus = bool.Parse(snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.SignInStatus).Value.ToString());
			//CurrentUserProfileKeeper.Intermateable = bool.Parse(snapshot.Child(UserPaths.PrimaryPaths.General).Child(UserPaths.GeneralPaths.Intermateable).Value.ToString());


			// Int Progression
			CurrentUserProfileKeeper.Level = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Level).Value.ToString());
			//CurrentUserProfileKeeper.Cup = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Cup).Value.ToString());
			CurrentUserProfileKeeper.Rank = snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Rank).Value.ToString();
			//CurrentUserProfileKeeper.Rank = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Rank).Value.ToString());
			CurrentUserProfileKeeper.TotalPlayTime = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.TotalPlayTime).Value.ToString());
			CurrentUserProfileKeeper.HighScore = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.HighScore).Value.ToString());
			//CurrentUserProfileKeeper.TotalMatches = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.TotalMatches).Value.ToString());
			//CurrentUserProfileKeeper.CompletedMatches = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.CompletedMatches).Value.ToString());
			//CurrentUserProfileKeeper.AbandonedMatches = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.AbandonedMatches).Value.ToString());
			CurrentUserProfileKeeper.CorrectAnswers = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.CorrectAnswers).Value.ToString());
			CurrentUserProfileKeeper.WrongAnswers = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.WrongAnswers).Value.ToString());
			//CurrentUserProfileKeeper.Wins = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Wins).Value.ToString());
			//CurrentUserProfileKeeper.Losses = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Losses).Value.ToString());
			//CurrentUserProfileKeeper.WinningStreak = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.WinningStreak).Value.ToString());
			CurrentUserProfileKeeper.Experience = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Progression).Child(UserPaths.ProgressionPaths.Experience).Value.ToString()); ;
			CurrentUserProfileKeeper.RequiredExperience = CurrentUserProfileKeeper.Level * 10 + 90;

			// Int Consumable
			//CurrentUserProfileKeeper.Papcoin = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Consumable).Child(UserPaths.ConsumablePaths.Papcoin).Value.ToString());
			//CurrentUserProfileKeeper.Gem = int.Parse(snapshot.Child(UserPaths.PrimaryPaths.Consumable).Child(UserPaths.ConsumablePaths.Gem).Value.ToString());

			////Debug.Log("bir de bu ");

			//ActionManager.Instance.ShowUserProfilePanel();

			LoadingUI.S_IsUserProfileReady = true;
		}
	}

	private void DeleteUserProfile()
	{
		FirebaseManager.UserDatabaseReference.RemoveValueAsync();
	}

	private void UserProfileDataListener(object sender, ChildChangedEventArgs args)
	{
		if (FirebaseManager.auth.CurrentUser == null)
		{
			return;
		}

		StartCoroutine(GetCurrentUserProfile());
	}
}
