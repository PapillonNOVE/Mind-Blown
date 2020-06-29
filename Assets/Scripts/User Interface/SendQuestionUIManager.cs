﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using ConstantKeeper;
using DG.Tweening.Plugins.Core.PathCore;

public class SendQuestionUIManager : MonoBehaviour
{
	[Header("Question Input Fields")]
	[SerializeField] private TMP_InputField _questionInputField;
	
	[Header("Correct Answer Input Fields")]
	[SerializeField] private TMP_InputField _correctOptionInputField;
	
	[Header("Wrong Answer Input Fields")]
	[SerializeField] private TMP_InputField _wrongOptionInputField1;
	[SerializeField] private TMP_InputField _wrongOptionInputField2;
	[SerializeField] private TMP_InputField _wrongOptionInputField3;

	[Header("Button")]
	[SerializeField] private Button _sendQuestionButton;
	[SerializeField] private Button _goMainMenuButton;


	private void Start()
	{
		OnClickAddListeners();	
	}

	private void OnClickAddListeners() 
	{
		_sendQuestionButton.onClick.AddListener(Send);
		_goMainMenuButton.onClick.AddListener(BottomNavigationBarManager.Instance.ShowMainNavigation);
	}

	private void Send() 
	{
		if (_questionInputField.text != null && _correctOptionInputField.text != null && _wrongOptionInputField1.text != null && _wrongOptionInputField2.text != null && _wrongOptionInputField3.text != null)
		{
			Dictionary<string, object> sendedQuestionPack = new Dictionary<string, object>()
			{
				[QuestionPaths.QuesitonPath.Question] = _questionInputField.text,
				[QuestionPaths.QuesitonPath.CorrectOption] = _correctOptionInputField.text,
				[QuestionPaths.QuesitonPath.WrongOption1] = _wrongOptionInputField1.text,
				[QuestionPaths.QuesitonPath.WrongOption2] = _wrongOptionInputField2.text,
				[QuestionPaths.QuesitonPath.WrongOption3] = _wrongOptionInputField3.text,
				[QuestionPaths.QuesitonPath.SenderPlayerID] = FirebaseManager.auth.CurrentUser.UserId
			};

			ActionManager.Instance.SendQuestion(sendedQuestionPack);
			ResetFields();
		}
	}

	private void ResetFields() 
	{
		_questionInputField.text = "";
		_correctOptionInputField.text = "";
		_wrongOptionInputField1.text = "";
		_wrongOptionInputField2.text = "";
		_wrongOptionInputField3.text = "";
	}

}