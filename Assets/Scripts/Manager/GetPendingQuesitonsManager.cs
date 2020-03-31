using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct PendingQuestionStruct 
{
	public string question;
	public string correctOption;
	public string wrongOption1;
	public string wrongOption2;
	public string wrongOption3;
	public string senderPlayerID;
}

public class GetPendingQuesitonsManager : MonoBehaviour
{
	[SerializeField] private GameObject _pendingQuestionParent;

	[SerializeField] private Button _buttonPref_PendingQuestion;
    private TextMeshProUGUI _text_PendingQuestion;

	private void Start()
	{
		RegisterActions();
	}

	private void RegisterActions()
	{
		ActionManager.Instance.CreatePendingQuestionList += CreatePendingQuestionList;
	}

	private void CreatePendingQuestionList(List<string> pendingQuestionList) 
	{
		int questionAmount = 0;

		foreach (string questionPack in pendingQuestionList)
		{
			PendingQuestionStruct pendingQuestionStruct = JsonUtility.FromJson<PendingQuestionStruct>(questionPack);

		 	Button newButton = Instantiate(_buttonPref_PendingQuestion, _pendingQuestionParent.transform);
			_text_PendingQuestion = newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
			
			_text_PendingQuestion.SetText(pendingQuestionStruct.question);

			questionAmount++;
		}

		_pendingQuestionParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, questionAmount * 150 + 100);
	}
}
