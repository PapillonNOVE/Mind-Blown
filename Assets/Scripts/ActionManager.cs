using UnityEngine.Events;
using System.Collections.Generic;
using System;
using System.Collections;
using UnityEngine.UI;

public class ActionManager : Singleton<ActionManager>
{
	//public Func<, >
	public delegate IEnumerator GetQuestionDelegate();
	public GetQuestionDelegate GetQuestion;

	public UnityAction<bool, Button> ControlAnswer;

	public UnityAction<QuestionStruct> AskQuestion;
	//public UnityAction<IEnumerator<>> GetQuestion;

	public UnityAction<Dictionary<string, object>> SendQuestion;

	public delegate void UpdateButtonDelegate(string _OptionText, ButtonCode buttonCode, bool isCorrectAnswer = false);
	public UpdateButtonDelegate UpdateOptionButton;
	// public UnityAction<string,ButtonCode, bool> PrepareOptionButton;

}
