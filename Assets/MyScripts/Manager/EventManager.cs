using UnityEngine.Events;
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

    public delegate IEnumerator SignUpWithEmailPasswordDelegate(SignUpStruct _Email, Action onSuccessCallback, Action onFailCallback);
    public SignUpWithEmailPasswordDelegate SignUpWithEmailPassword;

    public delegate IEnumerator SignInWithEmailPasswordDelegate(string _Email, string _Password, Action onSuccessCallback, Action onFailCallback);
    public SignInWithEmailPasswordDelegate SignInWithEmailPassword;

    public delegate IEnumerator ResetPasswordWithEmailDelegate(string _Email, Action onSuccessCallback, Action onFailCallback);
    public ResetPasswordWithEmailDelegate ResetPasswordWithEmail;

    public UnityAction DeleteUser;
    public delegate void SignOutDelegaate(Action onSuccessCallback, Action onFailCallback);
    public SignOutDelegaate SignOut;

	#endregion

	#region User

	public UnityAction<string, string> CreatUserProfile;

    public UnityAction<string, string, object> UpdateUserData;

    public delegate IEnumerator GetCurrentUserProfileDelegate();
    public GetCurrentUserProfileDelegate GetCurrentUserProfile;

    public UnityAction DeleteUserProfile;

    public delegate IEnumerator ControlIsUsernameExistDelegate(string _Username, Action _OnSuccesCallback, Action _OnFailCallback);
    public ControlIsUsernameExistDelegate ControlIsUsernameExist;

    #endregion

    #region Save/Load

    public UnityAction<List<Toggle>> SaveCategories;
    public Func<CategoryStateHolder> LoadCategories;

	#endregion

	// Game
	public UnityAction<string> ShowWhoseTurn;
    public UnityAction<string> ShowLastEstimation;
    public UnityAction<int> SendEstimation;
    public UnityAction<bool, OptionButton> ControlAnswer;
    public UnityAction GameOver;

    // Panels
    public UnityAction ShowMenuPanel;
    public UnityAction ShowSignUpPanel;
    public UnityAction ShowSignInPanel;
    public UnityAction ShowUserProfilePanel;


    public UnityAction UsernameAvaliable;
    public UnityAction UsernameNotAvaliable;


    public delegate IEnumerator GetQuestionIDsDelegate(/*List<string> categories*/);
    public GetQuestionIDsDelegate GetQuestionIDs;

    public delegate IEnumerator GetQuestionDelegate();
    public GetQuestionDelegate GetQuestion;


    public UnityAction<Question> AskQuestion;
    //public UnityAction<IEnumerator<>> GetQuestion;

    public UnityAction<Question> SendQuestion;
    //public UnityAction<Dictionary<string, object>> SendQuestion;

    public delegate void UpdateButtonDelegate(string _OptionText, ButtonCode buttonCode, bool isCorrectAnswer = false);
    public UpdateButtonDelegate UpdateOptionButton;
    // public UnityAction<string,ButtonCode, bool> PrepareOptionButton;


    // Game UI
    public UnityAction<float> CountdownTimeIndicator;
    public UnityAction<float> UpdateGameUI;
}
