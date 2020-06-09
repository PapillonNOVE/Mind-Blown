using UnityEngine.Events;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.UI;

public class ActionManager : Singleton<ActionManager>
{
    //Firebase Initialization
    public delegate void StartFirebaseDelegate(Action _OnSuccessCallback);
    public StartFirebaseDelegate StartFirebase;

    //Loading Screen
    public UnityAction<float> LoadingPanelSelfDestruction;

    // Prepare Game
    public UnityAction QuickGame;

    // Authentication
    public delegate IEnumerator SignUpWithEmailPasswordDelegate(SignUpStruct _Email, Action onSuccessCallback, Action onFailCallback);
    public SignUpWithEmailPasswordDelegate SignUpWithEmailPassword;

    public delegate IEnumerator SignInWithEmailPasswordDelegate(string _Email, string _Password, Action onSuccessCallback, Action onFailCallback);
    public SignInWithEmailPasswordDelegate SignInWithEmailPassword;

    public delegate IEnumerator ResetPasswordWithEmailDelegate(string _Email, Action onSuccessCallback, Action onFailCallback);
    public ResetPasswordWithEmailDelegate ResetPasswordWithEmail;
    
    public UnityAction DeleteUser;
    public delegate void SignOutDelegaate(Action onSuccessCallback, Action onFailCallback);
    public SignOutDelegaate SignOut;

    // User
    public UnityAction<string, string> CreatUserProfile;
    public UnityAction<string, string, object> UpdateUserData;
    public delegate IEnumerator GetCurrentUserProfileDelegate();
    public GetCurrentUserProfileDelegate GetCurrentUserProfile;
    public UnityAction DeleteUserProfile;

    public delegate IEnumerator ControlIsUsernameExistDelegate(string _Username, Action _OnSuccesCallback, Action _OnFailCallback);
    public ControlIsUsernameExistDelegate ControlIsUsernameExist;

    // Game
    public UnityAction<string> ShowWhoseTurn;
    public UnityAction<string> ShowLastEstimation;
    public UnityAction<int> SendEstimation;
    public UnityAction<bool, OptionButton> ControlAnswer;

    public UnityAction CreateSecretNumber;

    // Panels
    public UnityAction ShowMenuPanel;
    public UnityAction ShowSignUpPanel;
    public UnityAction ShowSignInPanel;
    public UnityAction ShowUserProfilePanel;


    public UnityAction UsernameAvaliable;
    public UnityAction UsernameNotAvaliable;
   

    public delegate IEnumerator GetPendingQuestionsDelegate();
    public GetPendingQuestionsDelegate GetPendingQuestions;

    public UnityAction<List<string>> CreatePendingQuestionList;

    public delegate IEnumerator GetQuestionDelegate();
	public GetQuestionDelegate GetQuestion;

    public UnityAction<QuestionStruct> AskQuestion;
	//public UnityAction<IEnumerator<>> GetQuestion;

	public UnityAction<Dictionary<string, object>> SendQuestion;

	public delegate void UpdateButtonDelegate(string _OptionText, ButtonCode buttonCode, bool isCorrectAnswer = false);
	public UpdateButtonDelegate UpdateOptionButton;
	// public UnityAction<string,ButtonCode, bool> PrepareOptionButton;

}
