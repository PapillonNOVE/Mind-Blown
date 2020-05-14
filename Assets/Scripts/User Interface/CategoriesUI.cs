using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CategoriesUI : MonoBehaviour
{
	[Header("Specifical Category Toggle")]
	[SerializeField] private Toggle toggle_History;
	[SerializeField] private Toggle toggle_Technology;
	[SerializeField] private Toggle toggle_Movie;
	[SerializeField] private Toggle toggle_Documentary;
	[SerializeField] private Toggle toggle_VideoGame;
	[SerializeField] private Toggle toggle_Music;
	[SerializeField] private Toggle toggle_Language;
	[SerializeField] private Toggle toggle_Anime;
	[SerializeField] private Toggle toggle_Math;
	[SerializeField] private Toggle toggle_Book;

	[Header("Specifical Category Toggles Parent")]
	[SerializeField] private GameObject specificalCategoryToggleParent;

	[Header("Specifical Category Toggle List")]
	[SerializeField] private List<Toggle> specificalCategoryToggles;

	[Header("Text")]
	[SerializeField] private TextMeshProUGUI text_HistoryToggle;
	[SerializeField] private TextMeshProUGUI text_TechnologyToggle;
	[SerializeField] private TextMeshProUGUI text_MovieToggle;
	[SerializeField] private TextMeshProUGUI text_DocumentaryToggle;
	[SerializeField] private TextMeshProUGUI text_VideoGameToggle;
	[SerializeField] private TextMeshProUGUI text_MusicToggle;
	[SerializeField] private TextMeshProUGUI text_LanguageToggle;
	[SerializeField] private TextMeshProUGUI text_AnimeToggle;
	[SerializeField] private TextMeshProUGUI text_MathToggle;
	[SerializeField] private TextMeshProUGUI text_BookToggle;

	[Header("Button")]
	[SerializeField] private Button button_GeneralKnowledge;
	[SerializeField] private Button button_GoToMainMenu;
	[SerializeField] private Button button_Play;

	private void Start()
	{
		OnClickAddListener();
		OnValueChangedAddListener();
	}

	private void OnClickAddListener()
	{
		button_GeneralKnowledge.onClick.AddListener(CancelChoosingGeneralKnowledge);
		button_GoToMainMenu.onClick.AddListener(UIManager.Instance.ShowMainMenuPanel);
		button_Play.onClick.AddListener(Play);
	}

	private void OnValueChangedAddListener()
	{
		foreach (Toggle toggle in specificalCategoryToggles)
		{
			toggle.onValueChanged.AddListener(ChoosingGeneralKnowledge);
		}
	}

	private void ChoosingGeneralKnowledge(bool _On)
	{
		foreach (Toggle toggle in specificalCategoryToggles)
		{
			if (!toggle.isOn)
			{
				Debug.Log("1");
				return;
			}
		}
		button_GeneralKnowledge.gameObject.SetActive(true);
		specificalCategoryToggleParent.SetActive(false);
	}

	private void CancelChoosingGeneralKnowledge()
	{
		specificalCategoryToggleParent.SetActive(true);
		button_GeneralKnowledge.gameObject.SetActive(false);
	}

	private void Play()
	{
		foreach (Toggle spesificalCategoryToggle in specificalCategoryToggles)
		{
			if (spesificalCategoryToggle.isOn)
			{
				string categoryName = spesificalCategoryToggle.transform.GetChild(0).transform.GetChild(1).GetComponent<TextMeshProUGUI>().text;
				Debug.Log(categoryName);
				FirebaseQuestionManager.categories.Add(categoryName);
			}
		}
		UIManager.Instance.ShowGamePanel();
		StartCoroutine(ActionManager.Instance.GetQuestion());
	}
}
