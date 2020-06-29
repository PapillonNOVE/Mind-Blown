using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	[Header("Button")]
	[SerializeField] private Button _goToMainMenuButton;

	[Header("Text")]
	[SerializeField] private TextMeshProUGUI _scoreText;
	[SerializeField] private TextMeshProUGUI _questionNumberText;

	[Header("Time Slider")]
	[SerializeField] private Slider _timerBar;

	private void OnEnable()
	{
		Subscribe();
	}

	private void OnDisable()
	{
		GeneralControls.ControlQuit(Unsubscribe);
	}

	private void Subscribe()
	{
		ActionManager.Instance.CountdownTimeDemonstrator += CountdownTimeDemonstrator;
		ActionManager.Instance.UpdateGameUI += UpdateGameUI;
	}

	private void Unsubscribe()
	{
		ActionManager.Instance.CountdownTimeDemonstrator -= CountdownTimeDemonstrator;
		ActionManager.Instance.UpdateGameUI -= UpdateGameUI;
	}

	private void UpdateGameUI(int score, int questionNumber, float responseTimeLimit)
	{
		_scoreText.SetText(score.ToString());

		_questionNumberText.SetText(questionNumber.ToString());

		_timerBar.maxValue = responseTimeLimit;
		_timerBar.value = responseTimeLimit;
	}

	private void CountdownTimeDemonstrator(float responseTimer)
    {
		//_timerText.SetText(_timer.ToString("#"));
        _timerBar.value = responseTimer;

        //_timerBar = Color.Lerp(_redColor, _greenColor, x);
    }
}
