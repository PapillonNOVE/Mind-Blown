using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SendQuestionManager : MonoBehaviour
{
	[Header("Question Input Fields")]
	[SerializeField] private TMP_InputField _inputFieldQuestion;
	
	[Header("Correct Answer Input Fields")]
	[SerializeField] private TMP_InputField _inputFieldCorrectAnswer;
	
	[Header("Wrong Answer Input Fields")]
	[SerializeField] private TMP_InputField _inputFieldWrongAnswer1;
	[SerializeField] private TMP_InputField _inputFieldWrongAnswer2;
	[SerializeField] private TMP_InputField _inputFieldWrongAnswer3;

	[Header("Buttons")]
	[SerializeField] private Button _btnSend;


	private void Start()
	{
		AddListenersToButtons();	
	}

	private void AddListenersToButtons() 
	{
		_btnSend.onClick.AddListener(Send);
	}

	private void Send() 
	{
		if (_inputFieldQuestion.text != null && _inputFieldCorrectAnswer.text != null && _inputFieldWrongAnswer1.text != null && _inputFieldWrongAnswer2.text != null && _inputFieldWrongAnswer3.text != null)
		{
			Dictionary<string, object> sendedQuestionPack = new Dictionary<string, object>()
			{
				["Question"] = _inputFieldQuestion.text,
				["Correct Option"] = _inputFieldCorrectAnswer.text,
				["Wrong Option1"] = _inputFieldWrongAnswer1.text,
				["Wrong Option2"] = _inputFieldWrongAnswer2.text,
				["Wrong Option3"] = _inputFieldWrongAnswer3.text,
				["Sender Player ID"] = FirebaseManager.auth.CurrentUser.UserId
			};

			ActionManager.Instance.SendQuestion(sendedQuestionPack);
		}
	}

}