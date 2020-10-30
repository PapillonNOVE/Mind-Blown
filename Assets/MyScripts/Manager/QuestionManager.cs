using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
using Constants;

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
	
	[Header("Button Array")]
	[SerializeField] private OptionButton[] _optionButtonArray;

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
	[SerializeField] private float _responseTimeLimit;
	[SerializeField] private float _responseTimer;
	public float ResponseTimer
	{
		get => _responseTimer;
		set
		{
			_responseTimer = value;
			EventManager.Instance.CountdownTimeIndicator?.Invoke(value);
		}
	}

	[Header("Progression")]
	[SerializeField] private int _score;
	[SerializeField] private int _correctAnswerAmount;
	[SerializeField] private int _wrongAnswerAmount;
	[SerializeField] private int _questionNumber;

	private OptionButton _correctOptionButton; 

	public static bool correctAnswer = true;
	public static bool wrongAnswer = false;

	private bool _isQuestionAsked;

	private void OnEnable()
	{
		Subscribe();
		StartCoroutine(ResetValues());

		if (FirebaseQuestionManager.questionIDs.Count > 0)
		{
			StartCoroutine(EventManager.Instance.GetQuestion());
		}
	}

	private void OnDisable()
	{
		GeneralControls.ControlQuit(Unsubscribe);
	}

	private void Subscribe()
	{
		EventManager.Instance.AskQuestion += AskQuestion;
		//EventManager.Instance.ControlAnswer += ControlAnswer;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.AskQuestion -= AskQuestion;
		//EventManager.Instance.ControlAnswer -= ControlAnswer;
	}

	private IEnumerator ResetValues()
	{
		yield return new WaitForSeconds(0.1f);

		_score = 0;
		_correctAnswerAmount = 0;
		_wrongAnswerAmount = 0;
		_questionNumber = 1;
		ResponseTimer = _responseTimeLimit;
		EventManager.Instance.UpdateGameUI?.Invoke(_score,_questionNumber,_responseTimeLimit);
	}

	private void Update()
	{
		if (_isQuestionAsked)
		{
			if (ResponseTimer >= 0)
			{
				ResponseTimer -= Time.deltaTime;
			}
			else
			{
				GameOver(GameOverType.TimesUp);
			}
		}
	}

	private void AskQuestion(Question question)
	{
		_questionText.SetText(question.QuestionText);

		_isQuestionAsked = true;

		for (int i = 0; i < _optionButtons.Count; i++)
		{
			Debug.Log(i);
			int randomOptionButtonIndex = Random.Range(0, question.OptionList.Count);

			_optionButtons[i].Init(question.OptionList[randomOptionButtonIndex], ControlAnswer);

			question.OptionList.RemoveAt(randomOptionButtonIndex);
		}

		foreach (OptionButton optionButton in _optionButtonArray)
		{
			optionButton._optionButton.interactable = true;
		}
	}

	private void ControlAnswer(bool answer)
	{
		_isQuestionAsked = false;

		foreach (OptionButton optionButton in _optionButtonArray)
		{
			optionButton._optionButton.interactable = false;
		}

		if (answer)
		{
			//CorrectAnswerAnim(4);
			//PreviousQuestionAnimation();
			StartCoroutine(AnsweredCorrectly());
		}
		else
		{
			//WrongAnswerAnim(choosenOptionButton);
			OptionButton._showCorrectOption?.Invoke();
			StartCoroutine(GameOver(GameOverType.WrongAnswer));
		}
	}

	private IEnumerator AnsweredCorrectly()
	{
		_correctAnswerAmount++;
		_score += 10;
		_questionNumber++;

		yield return new WaitForSeconds(1f);

		ResponseTimer = _responseTimeLimit;

		EventManager.Instance.UpdateGameUI(_score, _questionNumber, _responseTimeLimit);
		StartCoroutine(EventManager.Instance.GetQuestion());
		//if (_questionNumber > 0)
		//{
		//	NewQuestionAnimation();
		//}
		//else
		//{
		//	NewQuestionAnimation(true);
		//}
	}

	private IEnumerator GameOver(GameOverType gameOverType)
	{
		if (gameOverType == GameOverType.TimesUp)
		{
			//_timerText.SetText("Bitti!");
			Debug.Log("Süre Bitti!");
		}
		else if (gameOverType == GameOverType.WrongAnswer)
		{
			_wrongAnswerAmount++;
			Debug.Log("Yanlış Cevap!");
		}

		EventManager.Instance.GainExperience(_correctAnswerAmount, _wrongAnswerAmount);

		EventManager.Instance.UpdateUserData(UserPaths.PrimaryPaths.Progression, UserPaths.ProgressionPaths.CorrectAnswers, CurrentUserProfileKeeper.CorrectAnswers + _correctAnswerAmount);
		EventManager.Instance.UpdateUserData(UserPaths.PrimaryPaths.Progression, UserPaths.ProgressionPaths.WrongAnswers, CurrentUserProfileKeeper.WrongAnswers + _wrongAnswerAmount);

		yield return new WaitForSeconds(1f);
	}

	#region Animations

	private void PreviousQuestionAnimation()
	{
		Sequence newQuestionAnimSeq = DOTween.Sequence();

		newQuestionAnimSeq.Append(_questionText.rectTransform.DOAnchorPos(new Vector2(-1000, 0), 0f))
						  .Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-2000, 0), 0f));

	}

	private IEnumerator NewQuestionAnimation(bool isFirstQuestion = false)
	{
		Vector3 questionTextStartPos = _questionText.rectTransform.anchoredPosition;
		Vector3 buttonParentObjStartpos = _buttonParent.GetComponent<RectTransform>().anchoredPosition;

		Sequence newQuestionOutAnimSeq = DOTween.Sequence();
		Sequence newQuestionInAnimSeq = DOTween.Sequence();

		if (isFirstQuestion)
		{
			newQuestionOutAnimSeq.Append(_questionText.rectTransform.DOAnchorPos(new Vector2(1000, 0), 0f));
			newQuestionOutAnimSeq.Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(2000, 0), 0f));
			newQuestionOutAnimSeq.Append(_questionText.rectTransform.DOAnchorPos(questionTextStartPos, 1f).SetEase(Ease.InOutBack));
			newQuestionOutAnimSeq.Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(buttonParentObjStartpos, 1f).SetEase(Ease.InOutBack));
		}

		else
		{
			newQuestionOutAnimSeq.Append(_questionText.rectTransform.DOAnchorPos(new Vector2(-1000, 0), 0.7f).SetEase(Ease.InOutBack));
			newQuestionOutAnimSeq.OnStepComplete(() => _questionText.gameObject.SetActive(false));
			newQuestionOutAnimSeq.Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1500, 0), 0.7f).SetEase(Ease.InOutBack));
			newQuestionOutAnimSeq.OnStepComplete(() => _buttonParent.gameObject.SetActive(false));
			newQuestionOutAnimSeq.Append(_questionText.rectTransform.DOAnchorPos(new Vector2(1000, 0), 0f));
			newQuestionOutAnimSeq.OnStepComplete(() => _questionText.gameObject.SetActive(true));
			newQuestionOutAnimSeq.Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1500, 0), 0f));
			newQuestionOutAnimSeq.OnStepComplete(() => _buttonParent.gameObject.SetActive(true));
			newQuestionOutAnimSeq.Append(_questionText.rectTransform.DOAnchorPos(questionTextStartPos, 0.7f).SetEase(Ease.InOutBack));
			newQuestionOutAnimSeq.Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(buttonParentObjStartpos, 0.7f).SetEase(Ease.InOutBack));

			//newQuestionOutAnimSeq.Append(_questionText.rectTransform.DOAnchorPos(new Vector2(-1000, 0), 0.7f).SetEase(Ease.InOutBack))
			//				  .OnStepComplete(() => _questionText.gameObject.SetActive(false))
			//				  .Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1500, 0), 0.7f).SetEase(Ease.InOutBack))
			//				  .OnStepComplete(() => _buttonParent.gameObject.SetActive(false))
			//				  .OnStepComplete(() => StartCoroutine(ActionManager.Instance.GetQuestion()))
			//				  .Append(_questionText.rectTransform.DOAnchorPos(new Vector2(1000, 0), 0f))
			//				  .OnStepComplete(() => _questionText.gameObject.SetActive(true))
			//				  .Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1500, 0), 0f))
			//				  .OnStepComplete(() => _buttonParent.gameObject.SetActive(true))
			//				  .Append(_questionText.rectTransform.DOAnchorPos(questionTextStartPos, 0.7f).SetEase(Ease.InOutBack))
			//				  .Join(_buttonParent.GetComponent<RectTransform>().DOAnchorPos(buttonParentObjStartpos, 0.7f).SetEase(Ease.InOutBack));
		}

		yield return newQuestionOutAnimSeq.WaitForCompletion();
		Debug.LogWarning("geliyor");
		StartCoroutine(EventManager.Instance.GetQuestion());
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
