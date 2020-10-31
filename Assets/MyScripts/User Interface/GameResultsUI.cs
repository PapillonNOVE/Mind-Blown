using TMPro;
using UnityEngine;

public class GameResultsUI : MonoBehaviour
{
	[SerializeField] private TextMeshProUGUI m_CorrectAnswerAmountText;
	[SerializeField] private TextMeshProUGUI m_GainedExpAmountText;

	private void OnEnable()
	{
		//Subscribe();
		ShowResults();
	}

	private void OnDisable()
	{
		//GeneralControls.ControlQuit(Unsubscribe);
	}

	#region Event Subscribe/Unsubscribe

	private void Subscribe()
	{
		EventManager.Instance.GameOver += ShowResults;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.GameOver -= ShowResults;
	}

	#endregion

	private void ShowResults()
	{
		m_CorrectAnswerAmountText.SetText(Datas.S_CorrectAnswerAmount.ToString());
		m_GainedExpAmountText.SetText(Datas.S_GainedExperience.ToString());
	}
}
