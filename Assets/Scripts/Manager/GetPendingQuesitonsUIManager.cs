using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public struct PendingQuestionStruct 
{
	public string Question;
	public string CorrectOption;
	public string WrongOption1;
	public string WrongOption2;
	public string WrongOption3;
	public string SenderPlayerID;
}

public class GetPendingQuesitonsUIManager : MonoBehaviour
{
	[Header("Button")]
	[SerializeField] private Button button_Home;
	[SerializeField] private Button button_RefreshList;

	[SerializeField] private GameObject _pendingQuestionParent;

	[SerializeField] private Button _buttonPref_PendingQuestion;
    private TextMeshProUGUI _text_PendingQuestion;

	private void Start()
	{
		RegisterActions();
		AddListenersToButtons();
	}

	private void RegisterActions()
	{
		ActionManager.Instance.CreatePendingQuestionList += CreatePendingQuestionList;
	}

	private void AddListenersToButtons()
	{
		button_Home.onClick.AddListener(UIManager.Instance.ShowMainMenuPanel);
		button_RefreshList.onClick.AddListener(() => StartCoroutine(ActionManager.Instance.GetPendingQuestions()));
	}

	private void CreatePendingQuestionList(List<string> pendingQuestionList) 
	{
		int questionAmount = 0;

		foreach (string questionPack in pendingQuestionList)
		{
			PendingQuestionStruct pendingQuestionStruct = JsonUtility.FromJson<PendingQuestionStruct>(questionPack);

		 	Button newButton = Instantiate(_buttonPref_PendingQuestion, _pendingQuestionParent.transform);
			_text_PendingQuestion = newButton.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
			Debug.Log(pendingQuestionStruct.WrongOption1);
			_text_PendingQuestion.SetText(pendingQuestionStruct.Question);

			questionAmount++;
		}

		_pendingQuestionParent.GetComponent<RectTransform>().sizeDelta = new Vector2(0, questionAmount * 150 + 100);
	}
}
