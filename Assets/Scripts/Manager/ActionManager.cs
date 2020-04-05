using UnityEngine.Events;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.UI;

public class ActionManager : Singleton<ActionManager>
{
    // Prepare Game
    public UnityAction QuickGame;

    // Authentication
    public delegate IEnumerator SignUpWithEmailPasswordDelegate(SignUpStruct _Email, Action _SuccesCallback, Action _FailCallback);
    public SignUpWithEmailPasswordDelegate SignUpWithEmailPassword;

    public delegate IEnumerator SignInWithEmailPasswordDelegate(string _Email, string _Password, Action _SuccesCallback, Action _FailCallback);
    public SignInWithEmailPasswordDelegate SignInWithEmailPassword;

    public delegate IEnumerator ResetPasswordWithEmailDelegate(string _Email, Action _SuccesCallback, Action _FailCallback);
    public ResetPasswordWithEmailDelegate ResetPasswordWithEmail;
    
    public UnityAction DeleteUser;
    public UnityAction SignOut;

    // User
    public UnityAction<string, string> CreatUserProfile;
    public UnityAction<string, string, object> UpdateUserData;
    public UnityAction CallGetCurrentUserProfile;
    public UnityAction DeleteUserProfile;

    public delegate IEnumerator ControlIsUsernameExistDelegate(string _Username, Action _SuccesCallback, Action _FailCallback);
    public ControlIsUsernameExistDelegate ControlIsUsernameExist;

    // Game
    public UnityAction<string> ShowWhoseTurn;
    public UnityAction<string> ShowLastEstimation;
    public UnityAction<int> SendEstimation;
    public UnityAction<bool, Button> ControlAnswer;

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
