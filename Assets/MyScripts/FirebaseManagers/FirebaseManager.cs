using UnityEngine;
using Firebase;
using Firebase.Database;
using Firebase.Auth;
using System;
using Constants;
using System.Threading.Tasks;
using System.Collections;
using UnityEngine.UI;

public class FirebaseManager : MonoBehaviour
{
	public static FirebaseAuth auth;

	public static DatabaseReference UserDatabaseReference;
	public static DatabaseReference QuestionsDatabaseReference;
	public static DatabaseReference PublishedQuestionsDatabaseReference;
	public static DatabaseReference PendingQuestionsDatabaseReference;
	public static DatabaseReference UserNullDatabaseReference;
	public static DatabaseReference GameSettingsDatabaseReference;
	public static DatabaseReference RoomDatabaseReference;

	public Text bugger;

	public static DependencyStatus dependencyStatus;
	public static FirebaseUser user;
	public static string displayName;
	public static string emailAddress;

	private void Start()
	{
		RegisterActions();
		StartFirebase();
	}

	private void RegisterActions()
	{
		//ActionManager.Instance.StartFirebase += StartFirebase;
	}

	private void StartFirebase()
	{
		//Task<DependencyStatus> dependencyStatusTask = FirebaseApp.CheckDependenciesAsync();

		//dependencyStatus = dependencyStatusTask.Result;

		//FirebaseApp.CheckDependenciesAsync().ContinueWith(task =>
		//{
		dependencyStatus = FirebaseApp.CheckDependencies();

		if (dependencyStatus == DependencyStatus.Available)
		{
			StartCoroutine(InitializeDatabase());
			//Debug.Log("ilkinde oldu");
			//	ActionManager.Instance.LoadingPanelSelfDestruction(3f);
		}
		else if (dependencyStatus != DependencyStatus.Available)
		{
			FirebaseApp.FixDependenciesAsync().ContinueWith(task =>
			{
				//dependencyStatusTask = FirebaseApp.CheckDependenciesAsync();

				//dependencyStatus = dependencyStatusTask.Result;
				Debug.Log("ilkinde olmadı, buraya geldi");
				dependencyStatus = FirebaseApp.CheckDependencies();

				if (dependencyStatus == DependencyStatus.Available)
				{
					Debug.Log("Gelmiyor");
					StartCoroutine(InitializeDatabase());
					// ActionManager.Instance.LoadingPanelSelfDestruction(3f);
				}
				else
				{
					Debug.LogError("Database`e ulaşılamadı");
				}
			});
		}
		//});
	}

	private IEnumerator InitializeDatabase()
	{
		auth = FirebaseAuth.DefaultInstance;

		yield return new WaitForSeconds(0);

		SetDatabaseReferences();

		//if (auth.CurrentUser != null)
		//{
		//	SetUserDatabaseReference();
		//}

		auth.StateChanged += AuthStateChanged;

		//StartCoroutine(ActionManager.Instance.GetQuestion());
		LoadingUI.S_IsFirebaseInitialized = true;

		BottomNavigationBarManager.Instance.FirstLoad();
	}



	private void AuthStateChanged(object sender, EventArgs eventArgs)
	{
		if (auth.CurrentUser != user)
		{                   
			bool signedIn = user != auth.CurrentUser && auth.CurrentUser != null;
			
			if (!signedIn && user != null)
			{
				Debug.Log("Signed out " + user.UserId);
				BottomNavigationBarManager.Instance.ShowUserNavigation();

				UserDatabaseReference = UserNullDatabaseReference;
			}

			user = auth.CurrentUser;
			
			if (signedIn)
			{
				Debug.Log("Signed in " + user.UserId);
				displayName = user.DisplayName ?? "";
				emailAddress = user.Email ?? "";
				SetUserDatabaseReference();
				BottomNavigationBarManager.Instance.ShowMainNavigation();
			}
		}

		LoadingUI.S_IsAuthControlled = true;
	}

	private void SetDatabaseReferences()
	{
		UserNullDatabaseReference = FirebaseDatabase.DefaultInstance.GetReference($"{UserPaths.Users}/{UserPaths.UserID}");
		QuestionsDatabaseReference = FirebaseDatabase.DefaultInstance.GetReference($"{QuestionPaths.Questions}");
		PublishedQuestionsDatabaseReference = FirebaseDatabase.DefaultInstance.GetReference($"{QuestionPaths.Questions}/{QuestionPaths.PrimaryPaths.PublishedQuestions}");
		PendingQuestionsDatabaseReference = FirebaseDatabase.DefaultInstance.GetReference($"{QuestionPaths.Questions}/{QuestionPaths.PrimaryPaths.PendingQuestions}");
		GameSettingsDatabaseReference = FirebaseDatabase.DefaultInstance.GetReference($"{GameSettingsPaths.GameSettings}");
		LoadingUI.S_IsDatabaseReferencesCreated = true;
	}

	public void SetUserDatabaseReference()
	{
		UserDatabaseReference = FirebaseDatabase.DefaultInstance.GetReference($"{UserPaths.Users}/{UserPaths.UserID}/{auth.CurrentUser.UserId}");
		StartCoroutine(EventManager.Instance.GetCurrentUserProfile());
	}
}
