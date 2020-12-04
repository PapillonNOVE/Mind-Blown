using Constants;
using System.Collections.Generic;
using System.IO;
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
	[SerializeField] private Button m_SaveButton;

	private void Start()
	{
		OnClickAddListener();
		//OnValueChangedAddListener();
	}

	private void OnEnable()
	{
		SetCategoryToggles();
	}

	private void OnClickAddListener()
	{
		button_GeneralKnowledge.onClick.AddListener(CancelChoosingGeneralKnowledge);
		button_GoToMainMenu.onClick.AddListener(UIManager.Instance.ShowMainMenuPanel);
		m_SaveButton.onClick.AddListener(Save);
	}

	//private void OnValueChangedAddListener()
	//{
	//	foreach (Toggle toggle in specificalCategoryToggles)
	//	{
	//		toggle.onValueChanged.AddListener(ChoosingGeneralKnowledge);
	//	}
	//}

	private void SetCategoryToggles()
	{
		//List<string> categories = new List<string>();

		foreach (CategoryToggleState toggleState in EventManager.Instance.LoadCategories().CategoryToggleStates)
		{
			foreach (Toggle toggle in specificalCategoryToggles)
			{
				if (toggle.GetComponent<CategoryInfo>().category == toggleState.categoryType)
				{
					toggle.isOn = toggleState.isActive;

					//if (toggle.isOn)
					//{
					//	categories.Add(toggleState.name);
					//}
				}
			}
		}

		StartCoroutine(EventManager.Instance.GetQuestionIDs?.Invoke());
		//EventManager.Instance.GetQuestionIDs?.Invoke(categories);
	}

	#region General Knowledge

	private void ChoosingGeneralKnowledge(bool _On)
	{
		foreach (Toggle toggle in specificalCategoryToggles)
		{
			if (!toggle.isOn)
			{
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

	#endregion

	private void Save()
	{
		EventManager.Instance.SaveCategories?.Invoke(specificalCategoryToggles);

		SetCategoryToggles();

		

		//BottomNavigationBarManager.Instance.ShowMainNavigation();
		TransitionManager.Instance.TransitionAnimation(UIManager.Instance.ShowMainMenuPanel);
	}
}
