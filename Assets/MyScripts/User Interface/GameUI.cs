using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
	[SerializeField] private GameObject m_GameResultsPanel;

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

		CloseResultsPanel();
	}

	#region Event Subsribe/Unsubscribe

	private void Subscribe()
	{
		EventManager.Instance.GameOver += OpenResultsPanel;
		EventManager.Instance.CountdownTimeIndicator += CountdownTimeDemonstrator;
		EventManager.Instance.UpdateGameUI += UpdateGameUI;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.GameOver -= OpenResultsPanel;
		EventManager.Instance.CountdownTimeIndicator -= CountdownTimeDemonstrator;
		EventManager.Instance.UpdateGameUI -= UpdateGameUI;
	}

	#endregion

	private void UpdateGameUI(float responseTimeLimit)
	{
		_scoreText.SetText(Datas.S_Score.ToString());

		_questionNumberText.SetText(Datas.S_QuestionNumber.ToString());

		_timerBar.maxValue = responseTimeLimit;
		_timerBar.value = responseTimeLimit;
	}

	private void CountdownTimeDemonstrator(float responseTimer)
    {
		//_timerText.SetText(_timer.ToString("#"));
        _timerBar.value = responseTimer;

        //_timerBar = Color.Lerp(_redColor, _greenColor, x);
    }

	private void OpenResultsPanel() 
	{
		m_GameResultsPanel.SetActive(true);
	}

	private void CloseResultsPanel()
	{ 
		m_GameResultsPanel.SetActive(false);
	}
}
