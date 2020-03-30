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
    public UnityAction<SignUpStruct> SignUpWithEmailPassword;
    public UnityAction<string, string> SignInWithEmailPassword;
    public UnityAction<string> ResetPasswordWithMail;
    public UnityAction DeleteUser;
    public UnityAction SignOut;

    // User
    public UnityAction<string, string> CreatUserProfile;
    public UnityAction<string, string, object> UpdateUserData;
    public UnityAction CallGetCurrentUserProfile;
    public UnityAction DeleteUserProfile;

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


    //public Func<, >
    public delegate IEnumerator GetQuestionDelegate();
	public GetQuestionDelegate GetQuestion;

    public UnityAction<QuestionStruct> AskQuestion;
	//public UnityAction<IEnumerator<>> GetQuestion;

	public UnityAction<Dictionary<string, object>> SendQuestion;

	public delegate void UpdateButtonDelegate(string _OptionText, ButtonCode buttonCode, bool isCorrectAnswer = false);
	public UpdateButtonDelegate UpdateOptionButton;
	// public UnityAction<string,ButtonCode, bool> PrepareOptionButton;

}
