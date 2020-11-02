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
		CategoryStateHolder categoryStateHolder = new CategoryStateHolder();

		string reading = File.ReadAllText(LocalPaths.CATEGORY_SAVE_PATH);

		categoryStateHolder = JsonUtility.FromJson<CategoryStateHolder>(reading);

		foreach (CategoryToggleState toggleState in categoryStateHolder.CategoryToggleStates)
		{
			foreach (Toggle toggle in specificalCategoryToggles)
			{
				if (toggle.GetComponent<CategoryInfo>().category == toggleState.categoryType)
				{
					toggle.isOn = toggleState.isActive;
				}
			}
		}
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
		CategoryStateHolder categoryStateHolder = new CategoryStateHolder
		{
			CategoryToggleStates = new List<CategoryToggleState>()
		};

		foreach (Toggle spesificalCategoryToggle in specificalCategoryToggles)
		{
			CategoryToggleState categoryToggleState = new CategoryToggleState
			{
				isActive = spesificalCategoryToggle.isOn,
				categoryType = spesificalCategoryToggle.GetComponent<CategoryInfo>().category,
				name = spesificalCategoryToggle.GetComponent<CategoryInfo>().category.ToString()
			};

			categoryStateHolder.CategoryToggleStates.Add(categoryToggleState);
		}

		string json = JsonUtility.ToJson(categoryStateHolder);

		Debug.Log(LocalPaths.CATEGORY_SAVE_PATH);

		File.WriteAllText(LocalPaths.CATEGORY_SAVE_PATH, json);

		if (categoryStateHolder.CategoryToggleStates.Count > 0)
		{
			PlayerPrefs.SetString(PlayerPrefsKeys.CATEGORY_SELECTED, PlayerPrefsKeys.CATEGORY_SELECTED);
		}

		//BottomNavigationBarManager.Instance.ShowMainNavigation();
		TransitionManager.Instance.TransitionAnimation(UIManager.Instance.ShowMainMenuPanel);
	}
}
