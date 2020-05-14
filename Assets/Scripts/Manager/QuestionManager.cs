using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using ConstantKeeper;

public enum GameOverType
{
	TimesUp,
	WrongAnswer
}

public struct QuestionStruct
{
	public string question;
	public string correctAnswer;
	public List<string> wrongAnswers;
	public int optionCount;
	public string questionCategory;
}

public class QuestionManager : MonoBehaviour
{
	[Header("Interface Text")]
	[SerializeField] private TextMeshProUGUI text_Score;
	[SerializeField] private TextMeshProUGUI text_QuestionNumber;
	[SerializeField] private TextMeshProUGUI text_Timer;

	[Header("Interface Image")]
	[SerializeField] private Image image_TimerAnim;

	[Header("Question")]
	[SerializeField] private TextMeshProUGUI text_Question;

	[Header("Option Text")]
	[SerializeField] private TextMeshProUGUI text_Option1;
	[SerializeField] private TextMeshProUGUI text_Option2;
	[SerializeField] private TextMeshProUGUI text_Option3;
	[SerializeField] private TextMeshProUGUI text_Option4;

	[Header("Option Button")]
	[SerializeField] private Button button_Option1;
	[SerializeField] private Button button_Option2;
	[SerializeField] private Button button_Option3;
	[SerializeField] private Button button_Option4;

	[Header("Button List")]
	[SerializeField] private List<Button> _optionButtons;

	[Header("Button Parent")]
	[SerializeField] private GameObject buttonParent;

	[Header("Colors")]
	[SerializeField] private Color color_Green;
	[SerializeField] private Color color_Red;

	[Header("Timer")]
	[SerializeField] private float _timeLimit;
	[SerializeField] private float _timer;

	[Header("Progression")]
	[SerializeField] private float _score;
	[SerializeField] private float _correctAnswerAmount;
	[SerializeField] private float _wrongAnswerAmount;
	[SerializeField] private float _questionNumber;

	private Button _correctOptionButton; 

	public static bool correctAnswer = true;
	public static bool wrongAnswer = false;

	private bool _isQuestionAsked;
	public bool IsQuestionAsked
	{
		get { return _isQuestionAsked; }

		set { _isQuestionAsked = value; }
	}

	private void OnEnable()
	{
		PrepareGameUI();
		RegisterDelegateAndActions();
	}

	private void OnDisable()
	{
		//UnregisterDelegateAndActions();
	}

	private void RegisterDelegateAndActions()
	{
		ActionManager.Instance.AskQuestion += AskQuestion;
		ActionManager.Instance.ControlAnswer += ControlAnswer;
	}

	private void UnregisterDelegateAndActions()
	{
		ActionManager.Instance.AskQuestion -= AskQuestion;
		ActionManager.Instance.ControlAnswer -= ControlAnswer;
	}

	private void PrepareGameUI()
	{
		_score = 0;
		text_Score.SetText(_score.ToString());

		_questionNumber = 0;
		text_QuestionNumber.SetText(_questionNumber.ToString());

		_timer = _timeLimit;
	}

	public void AskQuestion(QuestionStruct _QuestionStruct)
	{
		text_Question.SetText(_QuestionStruct.question);

		_isQuestionAsked = true;

		List<int> indexes = new List<int>();

		int counter = _optionButtons.Count;
		int randomOptionButtonIndex;

		Debug.Log(counter);

		for (int i = 0; i < counter; i++)
		{
			do
			{
				randomOptionButtonIndex = Random.Range(0, counter);
			} while (indexes.Contains(randomOptionButtonIndex));

			if (i == 0)
			{
				ActionManager.Instance.UpdateOptionButton(_QuestionStruct.correctAnswer, (ButtonCode)randomOptionButtonIndex, true);
				_correctOptionButton = _optionButtons[randomOptionButtonIndex];
				//	_correctOptionButton.GetComponent<Image>().color = Color.white;
			}
			else
			{
				ActionManager.Instance.UpdateOptionButton(_QuestionStruct.wrongAnswers[0], (ButtonCode)randomOptionButtonIndex);
				Debug.Log("Dizi büyüklüğü " + _QuestionStruct.wrongAnswers.Count);
				_QuestionStruct.wrongAnswers.RemoveAt(0);
			}

			// Debug.Log("Wrong Number : " + randomOptionButtonIndex);
			indexes.Add(randomOptionButtonIndex);
		}

		if (_questionNumber > 0)
		{
			NewQuestionAnimation();
		}
		else
		{
			NewQuestionAnimation(true);
		}

		foreach (Button button in _optionButtons)
		{
			button.interactable = true;
		}
	}

	private void Update()
	{
		if (_isQuestionAsked)
		{
			if (_timer >= 0)
			{
				_timer -= Time.deltaTime;
				CountDownTimer();
			}
			else
			{
			    GameOver(GameOverType.TimesUp);
			}
		}
	}

	private void CountDownTimer()
	{
		text_Timer.SetText(_timer.ToString("#"));

		float x = _timer / _timeLimit;
		image_TimerAnim.fillAmount = x;

		image_TimerAnim.color = Color.Lerp(color_Red, color_Green, x);
	}

	private void ControlAnswer(bool _Answer, Button _ChoosenOptionButton)
	{
		_isQuestionAsked = false;

		foreach (Button button in _optionButtons)
		{
			button.interactable = false;
		}

		if (_Answer)
		{
			CorrectAnswerAnim(4);
			//PreviousQuestionAnimation();
			StartCoroutine(AnsweredCorrectly());
		}
		else
		{
			WrongAnswerAnim(_ChoosenOptionButton);
			StartCoroutine(GameOver(GameOverType.WrongAnswer));
		}
	}

	private IEnumerator AnsweredCorrectly()
	{
		Debug.Log("Bildin");

		_correctAnswerAmount++;

		_score += 10;
		text_Score.SetText(_score.ToString());

		_questionNumber++;
		text_QuestionNumber.SetText(_questionNumber.ToString());

		yield return new WaitForSeconds(1f);

		_timer = _timeLimit;

		StartCoroutine(ActionManager.Instance.GetQuestion());
	}

	private IEnumerator GameOver(GameOverType _GameOverType)
	{
		_wrongAnswerAmount++;

		if (_GameOverType == GameOverType.TimesUp)
		{
			text_Timer.SetText("Bitti!");
		}
		else if (_GameOverType == GameOverType.WrongAnswer)
		{
			
		}

		ActionManager.Instance.UpdateUserData(UserPaths.PrimaryPaths.Progression, UserPaths.ProgressionPaths.CorrectAnswers, _correctAnswerAmount);
		ActionManager.Instance.UpdateUserData(UserPaths.PrimaryPaths.Progression, UserPaths.ProgressionPaths.WrongAnswers, _wrongAnswerAmount);

		yield return new WaitForSeconds(1f);
	}

	#region Animations

	private void PreviousQuestionAnimation()
	{
		Sequence newQuestionAnimSeq = DOTween.Sequence();

		newQuestionAnimSeq.Append(text_Question.rectTransform.DOAnchorPos(new Vector2(-1000, 0), 0f))
						  .Join(buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-2000, 0), 0f));

	}

	private void NewQuestionAnimation(bool _IsFirstQuestion = false)
	{
		Vector3 questionTextStartPos = text_Question.rectTransform.anchoredPosition;
		Vector3 buttonParentObjStartpos = buttonParent.GetComponent<RectTransform>().anchoredPosition;

		Sequence newQuestionAnimSeq = DOTween.Sequence();

		if (_IsFirstQuestion)
		{
			newQuestionAnimSeq.Append(text_Question.rectTransform.DOAnchorPos(new Vector2(1000, 0), 0f))
							  .Join(buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(2000, 0), 0f))
							  .Append(text_Question.rectTransform.DOAnchorPos(questionTextStartPos, 1f).SetEase(Ease.InOutBack))
							  .Join(buttonParent.GetComponent<RectTransform>().DOAnchorPos(buttonParentObjStartpos, 1f).SetEase(Ease.InOutBack));
		}

		else
		{
			newQuestionAnimSeq.Append(text_Question.rectTransform.DOAnchorPos(new Vector2(-1000, 0), 0.7f).SetEase(Ease.InOutBack))
							  .OnStepComplete(() => text_Question.gameObject.SetActive(false))
							  .Join(buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1500, 0), 0.7f).SetEase(Ease.InOutBack))
							  .OnStepComplete(() => buttonParent.gameObject.SetActive(false))
							  .Append(text_Question.rectTransform.DOAnchorPos(new Vector2(1000, 0), 0f))
							  .OnStepComplete(() => text_Question.gameObject.SetActive(true))
							  .Join(buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1500, 0), 0f))
							  .OnStepComplete(() => buttonParent.gameObject.SetActive(true))
							  .Append(text_Question.rectTransform.DOAnchorPos(questionTextStartPos, 0.7f).SetEase(Ease.InOutBack))
							  .Join(buttonParent.GetComponent<RectTransform>().DOAnchorPos(buttonParentObjStartpos, 0.7f).SetEase(Ease.InOutBack));
		}
	}

	private void WrongAnswerAnim(Button _ChoosenOptionButton)
	{
		_ChoosenOptionButton.GetComponent<RawImage>().color = color_Red;

		CorrectAnswerAnim(2);
	}

	private void CorrectAnswerAnim(int _AnimCount)
	{
		Sequence colorSeq = DOTween.Sequence();

		for (int i = 0; i < _AnimCount; i++)
		{
			colorSeq.Append(_correctOptionButton.GetComponent<RawImage>().DOColor(color_Green, 0.1f))
					.Append(_correctOptionButton.GetComponent<RawImage>().DOColor(Color.white, 0.1f))
					.Append(_correctOptionButton.GetComponent<RawImage>().DOColor(color_Green, 0.1f));
		}

		_correctOptionButton.GetComponent<RawImage>().color = Color.white;
	}

	#endregion
}
