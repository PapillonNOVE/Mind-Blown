using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using ConstantKeeper;
using System.Threading;

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

public class GameManager : MonoBehaviour
{
	[Header("Interface Text")]
	[SerializeField] private TextMeshProUGUI _scoreText;
	[SerializeField] private TextMeshProUGUI _questionNumberText;
	[SerializeField] private TextMeshProUGUI _timerText;

	[Header("Time Slider")]
	[SerializeField] private Slider _timerBar;

	[Header("Question")]
	[SerializeField] private TextMeshProUGUI _questionText;

	[Header("Option Text")]
	[SerializeField] private TextMeshProUGUI _optionText1;
	[SerializeField] private TextMeshProUGUI _optionText2;
	[SerializeField] private TextMeshProUGUI _optionText3;
	[SerializeField] private TextMeshProUGUI _optionText4;

	//[Header("Button")]
	//[SerializeField] private Button _optionButton1;
	//[SerializeField] private Button _optionButton2;
	//[SerializeField] private Button _optionButton3;
	//[SerializeField] private Button _optionButton4;

	[Header("Option Button")]
	[SerializeField] private Button _optionButton1;
	[SerializeField] private Button _optionButton2;
	[SerializeField] private Button _optionButton3;
	[SerializeField] private Button _optionButton4;

	[Header("Button List")]
	[SerializeField] private List<OptionButton> _optionButtons;

	[Header("Button Parent")]
	[SerializeField] private GameObject _buttonParent;

	[Header("Colors")]
	[SerializeField] private Color _greenColor;
	[SerializeField] private Color _redColor;

	[Header("Choose Icon")]
	[SerializeField] private Texture2D _choosenOptionIcon;
	[SerializeField] private Texture2D _correctOptionIcon;

	[Header("Option Bacground")]
	[SerializeField] private Texture2D _wrongOptionBackground;
	[SerializeField] private Texture2D _correctOptionBackground;
	[SerializeField] private Texture2D _defaultOptionBackground;

	[Header("Timer")]
	[SerializeField] private float _timeLimit;
	[SerializeField] private float _timer;
	public float Timer
	{
		get => _timer;
		set
		{
			_timer = value;
			CountDownTimer();
		}
	}

	[Header("Progression")]
	[SerializeField] private float _score;
	[SerializeField] private float _correctAnswerAmount;
	[SerializeField] private float _wrongAnswerAmount;
	[SerializeField] private float _questionNumber;

	private OptionButton _correctOptionButton; 

	public static bool correctAnswer = true;
	public static bool wrongAnswer = false;

	private bool _isQuestionAsked;

	private void Start()
	{
		Subscribe();
	}

	private void OnEnable()
	{
		PrepareGameUI();
		StartCoroutine(ActionManager.Instance.GetQuestion());
	}

	private void OnDisable()
	{
		//UnregisterDelegateAndActions();
	}

	private void Subscribe()
	{
		ActionManager.Instance.AskQuestion += AskQuestion;
		ActionManager.Instance.ControlAnswer += ControlAnswer;
	}

	private void Unsubscribe()
	{
		ActionManager.Instance.AskQuestion -= AskQuestion;
		ActionManager.Instance.ControlAnswer -= ControlAnswer;
	}

	private void PrepareGameUI()
	{
		_score = 0;
		_scoreText.SetText(_score.ToString());

		_questionNumber = 0;
		_questionNumberText.SetText(_questionNumber.ToString());

		Timer = _timeLimit;
		_timerBar.maxValue = _timeLimit;
	}

	private void AskQuestion(QuestionStruct questionStruct)
	{
		_questionText.SetText(questionStruct.question);

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
				ActionManager.Instance.UpdateOptionButton(questionStruct.correctAnswer, (ButtonCode)randomOptionButtonIndex, true);
				_correctOptionButton = _optionButtons[randomOptionButtonIndex];
			}
			else
			{
				ActionManager.Instance.UpdateOptionButton(questionStruct.wrongAnswers[0], (ButtonCode)randomOptionButtonIndex);
				Debug.Log("Dizi büyüklüğü " + questionStruct.wrongAnswers.Count);
				questionStruct.wrongAnswers.RemoveAt(0);
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

		foreach (OptionButton optionButton in _optionButtons)
		{
			optionButton._optionButton.interactable = true;
		}
	}

	private void Update()
	{
		if (_isQuestionAsked)
		{
			if (Timer >= 0)
			{
				Timer -= Time.deltaTime;
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
		//_timerText.SetText(_timer.ToString("#"));

		_timerBar.value = Timer;

		//_timerBar = Color.Lerp(_redColor, _greenColor, x);
	}

	private void ControlAnswer(bool answer, OptionButton choosenOptionButton)
	{
		_isQuestionAsked = false;

		foreach (OptionButton optionButton in _optionButtons)
		{
			optionButton._optionButton.interactable = false;
		}

		if (answer)
		{
			//CorrectAnswerAnim(4);
			//PreviousQuestionAnimation();
			choosenOptionButton.CorrectOption();
			StartCoroutine(AnsweredCorrectly());
		}
		else
		{
			//WrongAnswerAnim(choosenOptionButton);
			choosenOptionButton.WrongOption();
			_correctOptionButton.ShowCorrectOption();
			StartCoroutine(GameOver(GameOverType.WrongAnswer));
		}
	}

	private IEnumerator AnsweredCorrectly()
	{
		Debug.Log("Bildin");

		_correctAnswerAmount++;

		_score += 10;
		_scoreText.SetText(_score.ToString());

		_questionNumber++;
		_questionNumberText.SetText(_questionNumber.ToString());

		yield return new WaitForSeconds(1f);

		Timer = _timeLimit;

		StartCoroutine(ActionManager.Instance.GetQuestion());
	}

	private IEnumerator GameOver(GameOverType gameOverType)
	{
		_wrongAnswerAmount++;

		if (gameOverType == GameOverType.TimesUp)
		{
			//_timerText.SetText("Bitti!");
		}
		else if (gameOverType == GameOverType.WrongAnswer)
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

		newQuestionAnimSeq.Append(_questionText.rectTransform.DOAnchorPos(new Vector2(-1000, 0), 0f))
						  .Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-2000, 0), 0f));

	}

	private void NewQuestionAnimation(bool isFirstQuestion = false)
	{
		Vector3 questionTextStartPos = _questionText.rectTransform.anchoredPosition;
		Vector3 buttonParentObjStartpos = _buttonParent.GetComponent<RectTransform>().anchoredPosition;

		Sequence newQuestionAnimSeq = DOTween.Sequence();

		if (isFirstQuestion)
		{
			newQuestionAnimSeq.Append(_questionText.rectTransform.DOAnchorPos(new Vector2(1000, 0), 0f))
							  .Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(2000, 0), 0f))
							  .Append(_questionText.rectTransform.DOAnchorPos(questionTextStartPos, 1f).SetEase(Ease.InOutBack))
							  .Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(buttonParentObjStartpos, 1f).SetEase(Ease.InOutBack));
		}

		else
		{
			newQuestionAnimSeq.Append(_questionText.rectTransform.DOAnchorPos(new Vector2(-1000, 0), 0.7f).SetEase(Ease.InOutBack))
							  .OnStepComplete(() => _questionText.gameObject.SetActive(false))
							  .Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1500, 0), 0.7f).SetEase(Ease.InOutBack))
							  .OnStepComplete(() => _buttonParent.gameObject.SetActive(false))
							  .Append(_questionText.rectTransform.DOAnchorPos(new Vector2(1000, 0), 0f))
							  .OnStepComplete(() => _questionText.gameObject.SetActive(true))
							  .Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1500, 0), 0f))
							  .OnStepComplete(() => _buttonParent.gameObject.SetActive(true))
							  .Append(_questionText.rectTransform.DOAnchorPos(questionTextStartPos, 0.7f).SetEase(Ease.InOutBack))
							  .Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(buttonParentObjStartpos, 0.7f).SetEase(Ease.InOutBack));
		}
	}

	//private void WrongAnswerAnim(OptionButton _choosenOptionButton)
	//{
	//	//_ChoosenOptionButton.GetComponent<RawImage>().color = _redColor;

	//	_choosenOptionButton._optionBackgroundImage.texture = _wrongOptionBackground;
	//	_choosenOptionButton.

	//	CorrectAnswerAnim(2);
	//}

	//private void CorrectAnswerAnim(int _AnimCount)
	//{
	//	Sequence colorSeq = DOTween.Sequence();

	//	for (int i = 0; i < _AnimCount; i++)
	//	{
	//		colorSeq.Append(_correctOptionButton.GetComponent<RawImage>().DOColor(_greenColor, 0.1f))
	//				.Append(_correctOptionButton.GetComponent<RawImage>().DOColor(Color.white, 0.1f))
	//				.Append(_correctOptionButton.GetComponent<RawImage>().DOColor(_greenColor, 0.1f));
	//	}

	//	_correctOptionButton.GetComponent<RawImage>().color = Color.white;
	//}

	#endregion
}
