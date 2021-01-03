﻿using UnityEngine.Events;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.UI;

public class EventManager : Singleton<EventManager>
{
	//Firebase Initialization
	public delegate void StartFirebaseDelegate(Action _OnSuccessCallback);
	public StartFirebaseDelegate StartFirebase;

	//Loading Screen
	public UnityAction<float> LoadingPanelSelfDestruction;

	// Prepare Game
	public UnityAction QuickGame;

	#region Authentication

	public delegate IEnumerator SignUpWithEmailPasswordDelegate(SignUpStruct email, Action onSuccessCallback, Action onFailCallback);
	public SignUpWithEmailPasswordDelegate SignUpWithEmailPassword;

	public delegate IEnumerator SignInWithEmailPasswordDelegate(string email, string password, Action onSuccessCallback, Action onFailCallback);
	public SignInWithEmailPasswordDelegate SignInWithEmailPassword;

	public delegate IEnumerator ResetPasswordWithEmailDelegate(string email, Action onSuccessCallback, Action onFailCallback);
	public ResetPasswordWithEmailDelegate ResetPasswordWithEmail;

	public Action DeleteUser;

	public Action<Action,Action> SignOut;

	#endregion

	#region User

	public Action<string, string> CreatUserProfile;

	public Action<string, string, object> UpdateUserData;

	public delegate IEnumerator GetCurrentUserProfileDelegate();
	public GetCurrentUserProfileDelegate GetCurrentUserProfile;

	public Action DeleteUserProfile;

	public delegate IEnumerator ControlIsUsernameExistDelegate(string username, Action onSuccesCallback, Action onFailCallback);
	public ControlIsUsernameExistDelegate ControlIsUsernameExist;

	#endregion

	#region Save/Load

	public UnityAction<List<Toggle>> SaveCategories;
	public Func<CategoryStateHolder> LoadCategories;

	#endregion

	#region UI

	// Game
	public Action<string> ShowWhoseTurn;
	public Action<string> ShowLastEstimation;
	public Action<int> SendEstimation;
	public Action<bool, OptionButton> ControlAnswer;
	public Action GameOver;

	// Animations
	public Action QuestionFadeInAnim;
	public Action QuestionFadeOutAnim;

	// Panels
	public Action ShowMenuPanel;
	public Action ShowSignUpPanel;
	public Action ShowSignInPanel;
	public Action ShowUserProfilePanel;

	// Game UI
	public Action<float> CountdownTimeIndicator;
	public Action<float> UpdateGameUI;

	#endregion

	public Action UsernameAvaliable;
	public Action UsernameNotAvaliable;

	public Func<IEnumerator> GetQuestionIDs;

	public Func<IEnumerator> GetQuestion;


	public Action<Question> AskQuestion;

	public Action<Question> SendQuestion;
}
