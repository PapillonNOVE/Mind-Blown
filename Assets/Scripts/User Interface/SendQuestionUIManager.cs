using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class SendQuestionUIManager : MonoBehaviour
{
	[Header("Question Input Fields")]
	[SerializeField] private TMP_InputField inputField_Question;
	
	[Header("Correct Answer Input Fields")]
	[SerializeField] private TMP_InputField inputField_CorrectOption;
	
	[Header("Wrong Answer Input Fields")]
	[SerializeField] private TMP_InputField inputField_WrongOption1;
	[SerializeField] private TMP_InputField inputField_WrongOption2;
	[SerializeField] private TMP_InputField inputField_WrongOption3;

	[Header("Buttons")]
	[SerializeField] private Button btn_SendQuestion;


	private void Start()
	{
		AddListenersToButtons();	
	}

	private void AddListenersToButtons() 
	{
		btn_SendQuestion.onClick.AddListener(Send);
	}

	private void Send() 
	{
		if (inputField_Question.text != null && inputField_CorrectOption.text != null && inputField_WrongOption1.text != null && inputField_WrongOption2.text != null && inputField_WrongOption3.text != null)
		{
			Dictionary<string, object> sendedQuestionPack = new Dictionary<string, object>()
			{
				["Question"] = inputField_Question.text,
				["CorrectOption"] = inputField_CorrectOption.text,
				["WrongOption1"] = inputField_WrongOption1.text,
				["WrongOption2"] = inputField_WrongOption2.text,
				["WrongOption3"] = inputField_WrongOption3.text,
			//	["Sender Player ID"] = FirebaseManager.auth.CurrentUser.UserId
			};

			ActionManager.Instance.SendQuestion(sendedQuestionPack);
		}
	}

}