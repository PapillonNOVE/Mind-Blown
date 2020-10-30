using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Constants;
using EasyMobile;

public class SendQuestionUI : MonoBehaviour
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

	[SerializeField] private List<TMP_InputField> _inputFields;
	private void Start()
	{
		OnClickAddListeners();
	}

	private void OnClickAddListeners()
	{
		_sendQuestionButton.onClick.AddListener(Send);
		_goMainMenuButton.onClick.AddListener(OpenMainMenu);
	}

	private void ResetFields()
	{
		_questionInputField.text = "";
		_correctOptionInputField.text = "";
		_wrongOptionInputField1.text = "";
		_wrongOptionInputField2.text = "";
		_wrongOptionInputField3.text = "";
	}

	private void Send()
	{
		//if (_questionInputField.text != null && _correctOptionInputField.text != null && _wrongOptionInputField1.text != null && _wrongOptionInputField2.text != null && _wrongOptionInputField3.text != null)
		if (IsAnyInputFieldNull())
		{
			NativeUI.ShowToast($"{SendQuestionDebugs.QuestionSendFailed}");

			return;
		}

		Question question = new Question
		{
			QuestionText = _questionInputField.text,
			
			Options = new Options
			{
				CorrectOption = new Option
				{
					IsCorrectOption = true,
					OptionText = _correctOptionInputField.text
				},

				WrongOption1 = new Option
				{
					IsCorrectOption = false,
					OptionText = _wrongOptionInputField1.text
				},

				WrongOption2 = new Option
				{
					IsCorrectOption = false,
					OptionText = _wrongOptionInputField2.text
				},

				WrongOption3 = new Option
				{
					IsCorrectOption = false,
					OptionText = _wrongOptionInputField3.text
				}
			},

			SenderPlayerID = FirebaseManager.auth.CurrentUser.UserId
		};

		EventManager.Instance.SendQuestion(question);

		NativeUI.ShowToast($"{SendQuestionDebugs.QuestionSendSuccessful}");

		ResetFields();
	}

	private bool IsAnyInputFieldNull()
	{
		foreach (TMP_InputField inputField in _inputFields)
		{
			if (string.IsNullOrEmpty(inputField.text))
			{
				return true;
			}
		}

		return false;
	}

	private void OpenMainMenu()
	{
		BottomNavigationBarManager.Instance.ShowMainNavigation();
	}
}