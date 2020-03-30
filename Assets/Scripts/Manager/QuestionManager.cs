using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;

public enum GameOverType
{
	TimesUp,
	WrongAnswer
}

public struct QuestionStruct
{
	public string questionCategory;
	public string question;
	public string correctAnswer;
	public List<string> wrongAnswers;
	public int optionCount;
}

public class QuestionManager : MonoBehaviour
{
	[Header("Interface Texts")]
	[SerializeField] private TextMeshProUGUI _textScore;
	[SerializeField] private TextMeshProUGUI _textQuestionNumber;
	[SerializeField] private TextMeshProUGUI _textTimer;

	[Header("Interface Images")]
	[SerializeField] private Image _imageTimerAnim;

	[Header("Question")]
	[SerializeField] private TextMeshProUGUI _textQuestion;

	[Header("Option Texts")]
	[SerializeField] private TextMeshProUGUI _textOption1;
	[SerializeField] private TextMeshProUGUI _textOption2;
	[SerializeField] private TextMeshProUGUI _textOption3;
	[SerializeField] private TextMeshProUGUI _textOption4;

	[Header("Option Buttons")]
	[SerializeField] private Button _buttonOption1;
	[SerializeField] private Button _buttonOption2;
	[SerializeField] private Button _buttonOption3;
	[SerializeField] private Button _buttonOption4;

	[SerializeField] private List<Button> _optionButtons;

	[Header("Button Parent")]
	[SerializeField] private GameObject _gObjButtonParent;

	[Header("Timer")]
	[SerializeField] private float _timeLimit;
	[SerializeField] private float _timer;

	[Header("Progression")]
	[SerializeField] private float _score;
	[SerializeField] private float _questionNumber;

	[Header("Colors")]
	[SerializeField] private Color _colorGreen;
	[SerializeField] private Color _colorRed;

	[SerializeField] private Gradient _gradient;

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
		_textScore.SetText(_score.ToString());

		_questionNumber = 0;
		_textQuestionNumber.SetText(_questionNumber.ToString());

		_timer = _timeLimit;
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
		_textTimer.SetText(_timer.ToString("#"));

		float x = _timer / _timeLimit;
		_imageTimerAnim.fillAmount = x;

		_imageTimerAnim.color = Color.Lerp(_colorRed, _colorGreen, x);
	}


	public void AskQuestion(QuestionStruct _QuestionStruct)
	{

		_textQuestion.SetText(_QuestionStruct.question);

		_isQuestionAsked = true;

		List<int> indexes = new List<int>();

		/*_optionButtons.Add(_buttonOption1);
		_optionButtons.Add(_buttonOption2);
		_optionButtons.Add(_buttonOption3);
		_optionButtons.Add(_buttonOption4);*/

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
				_QuestionStruct.wrongAnswers.RemoveAt(0);
			}

			Debug.Log("Wrong Number : " + randomOptionButtonIndex);
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

	private void PreviousQuestionAnimation()
	{
		Sequence newQuestionAnimSeq = DOTween.Sequence();

		newQuestionAnimSeq.Append(_textQuestion.rectTransform.DOAnchorPos(new Vector2(-1000, 0), 0f))
						  .Join(_gObjButtonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-2000, 0), 0f));
						  
	}

	private void NewQuestionAnimation(bool _IsFirstQuestion = false)
	{
		Vector3 questionTextStartPos = _textQuestion.rectTransform.anchoredPosition;
		Vector3 buttonParentObjStartpos = _gObjButtonParent.GetComponent<RectTransform>().anchoredPosition;

		Sequence newQuestionAnimSeq = DOTween.Sequence();

		if (_IsFirstQuestion)
		{
			newQuestionAnimSeq.Append(_textQuestion.rectTransform.DOAnchorPos(new Vector2(1000, 0), 0f))
							  .Join(_gObjButtonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(2000, 0), 0f))
							  .Append(_textQuestion.rectTransform.DOAnchorPos(questionTextStartPos, 1f).SetEase(Ease.InOutBack))
							  .Join(_gObjButtonParent.GetComponent<RectTransform>().DOAnchorPos(buttonParentObjStartpos, 1f).SetEase(Ease.InOutBack));
		}

		else
		{
			newQuestionAnimSeq.Append(_textQuestion.rectTransform.DOAnchorPos(new Vector2(-1000, 0), 0.7f).SetEase(Ease.InOutBack))
							  .OnStepComplete(() => _textQuestion.gameObject.SetActive(false))
							  .Join(_gObjButtonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-1500, 0), 0.7f).SetEase(Ease.InOutBack))
							  .OnStepComplete(() => _gObjButtonParent.gameObject.SetActive(false))
							  .Append(_textQuestion.rectTransform.DOAnchorPos(new Vector2(1000, 0), 0f))
							  .OnStepComplete(() => _textQuestion.gameObject.SetActive(true))
							  .Join(_gObjButtonParent.GetComponent<RectTransform>().DOAnchorPos(new Vector2(1500, 0), 0f))
							  .OnStepComplete(() => _gObjButtonParent.gameObject.SetActive(true))
							  .Append(_textQuestion.rectTransform.DOAnchorPos(questionTextStartPos, 0.7f).SetEase(Ease.InOutBack))
							  .Join(_gObjButtonParent.GetComponent<RectTransform>().DOAnchorPos(buttonParentObjStartpos, 0.7f).SetEase(Ease.InOutBack));
		}
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

	private void WrongAnswerAnim(Button _ChoosenOptionButton) 
	{
		_ChoosenOptionButton.GetComponent<Image>().color = _colorRed;

		CorrectAnswerAnim(2);
	}

	private void CorrectAnswerAnim(int _AnimCount) 
	{
		Sequence colorSeq = DOTween.Sequence();
		
		for (int i = 0; i < _AnimCount; i++)
		{
			colorSeq.Append(_correctOptionButton.GetComponent<Image>().DOColor(_colorGreen, 0.1f))
					.Append(_correctOptionButton.GetComponent<Image>().DOColor(Color.white, 0.1f))	
					.Append(_correctOptionButton.GetComponent<Image>().DOColor(_colorGreen, 0.1f));
		}

		_correctOptionButton.GetComponent<Image>().color = Color.white;
	}

	private IEnumerator AnsweredCorrectly()
	{
		Debug.Log("Bildin");

		_score += 10;
		_textScore.SetText(_score.ToString());

		_questionNumber++;
		_textQuestionNumber.SetText(_questionNumber.ToString());

		yield return new WaitForSeconds(1f);

		_timer = _timeLimit;

		StartCoroutine(ActionManager.Instance.GetQuestion());

		

	}

	private IEnumerator GameOver(GameOverType _GameOverType)
	{
		if (_GameOverType == GameOverType.TimesUp)
		{
			_textTimer.SetText("Bitti!");
		}
		else if (_GameOverType == GameOverType.WrongAnswer)
		{
			
		}
		yield return new WaitForSeconds(1f);
	}
}
