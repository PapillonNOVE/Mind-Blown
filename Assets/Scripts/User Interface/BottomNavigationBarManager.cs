using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Tabs
{
	User,
	Main,
	SendQuestion,
	Settings
}

public class BottomNavigationBarManager : Singleton<BottomNavigationBarManager>
{
	[Header("Navigation Parent")]
    [SerializeField] private GameObject _navigationParent;

	[Header("Navigation")]
    [SerializeField] private GameObject _userNavigation;
    [SerializeField] private GameObject _mainNavigation;
    [SerializeField] private GameObject _sendQuestionNavigation;
    [SerializeField] private GameObject _settingsNavigation;

	[Header("List")]
	[SerializeField] private List<GameObject> _navigations;
	[SerializeField] private List<TabButton> _tabButtons;

	[Header("Tab Button")]
	[SerializeField] private Button _userTabButton;
	[SerializeField] private Button _mainTabButton;
	[SerializeField] private Button _sendQuestionTabButton;
	[SerializeField] private Button _settingsTabButton;

	[Header("Tab Button Background")]
	[SerializeField] private RawImage _tabButtonBackground;

	// RectTransform 
	private RectTransform _rectTransform_NavigationParent;
	private RectTransform _rectTransform_MainNavigation;
	private RectTransform _rectTransform_SettingsNavigation;
	private RectTransform _rectTransform_UserNavigation;
	private RectTransform _rectTransform_SendQuestionNavigation;


	// Selected Tab Button
	private TabButton _selectedTabButton;
	
	
	//private void OnEnable()
	//{
	//	ActionManager.Instance.ShowSignInPanel += ShowSignInPanel;
	//	ActionManager.Instance.ShowSignUpPanel += ShowSignUpPanel;
	//	ActionManager.Instance.ShowUserProfilePanel += ShowUserProfilePanel;
	//}

	//private void OnDisable()
	//{
	//	ActionManager.Instance.ShowSignInPanel -= ShowSignInPanel;
	//	ActionManager.Instance.ShowSignUpPanel -= ShowSignUpPanel;
	//	ActionManager.Instance.ShowUserProfilePanel -= ShowUserProfilePanel;
	//}

	//private void OnApplicationQuit()
	//{
	//	ActionManager.Instance.ShowSignInPanel -= ShowSignInPanel;
	//	ActionManager.Instance.ShowSignUpPanel -= ShowSignUpPanel;
	//	ActionManager.Instance.ShowUserProfilePanel -= ShowUserProfilePanel;
	//}

	private void Start()
	{
		OnClickAddListener();
		RectTransformSetter();
		FirstLoad();
	}

	private void FirstLoad() 
	{
		if (FirebaseManager.auth.CurrentUser == null)
		{
			Debug.Log("Kullanıcı yok!");
			ShowUserNavigation();
		}
	}

	private void OnClickAddListener() 
	{
		_userTabButton.onClick.AddListener(ShowUserNavigation);
		_mainTabButton.onClick.AddListener(ShowMainNavigation);
		_sendQuestionTabButton.onClick.AddListener(ShowSendQuestionNavigation);
		_settingsTabButton.onClick.AddListener(ShowSettingsNavigation);
	}

	private void RectTransformSetter()
	{
		_rectTransform_NavigationParent = _navigationParent.GetComponent<RectTransform>();
		_rectTransform_UserNavigation = _userNavigation.GetComponent<RectTransform>();
		_rectTransform_MainNavigation = _mainNavigation.GetComponent<RectTransform>();
		_rectTransform_SendQuestionNavigation = _sendQuestionNavigation.GetComponent<RectTransform>();
		_rectTransform_SettingsNavigation = _settingsNavigation.GetComponent<RectTransform>();
	}

	public void OnTabSelected(TabButton tabButton) 
	{
		_selectedTabButton = tabButton;

		_tabButtonBackground.rectTransform.DOMoveX(tabButton.parentRectTransform.localPosition.x, 0.5f);

		tabButton.SetActiveIcon();
		ResetTabGroup();
	}

	public void ShowUserNavigation() 
	{
		if (FirebaseManager.auth.CurrentUser != null)
		{
			UIManager.Instance.ShowUserProfilePanel();
		}
		else
		{
			UIManager.Instance.ShowSignInPanel();
		}

		StartCoroutine(PanelChanger(Tabs.User)); 
	}

	public void ShowMainNavigation()
	{
		UIManager.Instance.ShowMainMenuPanel();
		StartCoroutine(PanelChanger(Tabs.Main));
	}

	public void ShowSendQuestionNavigation()
	{
		UIManager.Instance.ShowSendQuestionPanel();
		StartCoroutine(PanelChanger(Tabs.SendQuestion));
	}

	public void ShowSettingsNavigation()
	{
		UIManager.Instance.ShowSettingsPanel();
		StartCoroutine(PanelChanger(Tabs.Settings));
	}

	private IEnumerator PanelChanger(Tabs tabs)
	{
		PanelActivator();

		RectTransform tempRectTransform = new RectTransform();

		switch (tabs)
		{
			case Tabs.Main:
				tempRectTransform = _rectTransform_MainNavigation;
				break;
			case Tabs.Settings:
				tempRectTransform = _rectTransform_SettingsNavigation;
				break;
			case Tabs.User:
				tempRectTransform = _rectTransform_UserNavigation;
				break;
			case Tabs.SendQuestion:
				tempRectTransform = _rectTransform_SendQuestionNavigation;
				break;
			default:
				tempRectTransform = new RectTransform();
				break;
		}

		Sequence navigationSequence = DOTween.Sequence();

		navigationSequence.Append(_rectTransform_NavigationParent.DOAnchorPosX(-tempRectTransform.anchoredPosition.x, 0.3f))
					 .Append(_rectTransform_NavigationParent.DOAnchorPosY(-tempRectTransform.anchoredPosition.y, 0.3f));


		yield return navigationSequence.WaitForCompletion();

		_userNavigation.SetActive(tabs == Tabs.User);
		_mainNavigation.SetActive(tabs == Tabs.Main);
		_sendQuestionNavigation.SetActive(tabs == Tabs.SendQuestion);
		_settingsNavigation.SetActive(tabs == Tabs.Settings);
	}

	private void PanelActivator()
	{
		foreach (GameObject panel in _navigations)
		{
			panel.SetActive(true);
		}
	}

	private void ResetTabGroup()
	{
		foreach (TabButton button in _tabButtons)
		{
			if (_selectedTabButton != null && button == _selectedTabButton)
			{
				continue;
			}
			button.SetPassiveIcon();
		}
	}
}
