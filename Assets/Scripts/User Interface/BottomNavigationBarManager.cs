using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum Panels
{
	User,
	Main,
	SendQuestion,
	Settings
}

public class BottomNavigationBarManager : MonoBehaviour
{
	[Header("Navigation")]
    [SerializeField] private GameObject _userNavigation;
    [SerializeField] private GameObject _mainNavigation;
    [SerializeField] private GameObject _sendQuestionNavigation;
    [SerializeField] private GameObject _settingsNavigation;

	[Header("RectTransform")]
	private RectTransform _rectTransform_Parent;
	private RectTransform _rectTransform_Main;
	private RectTransform _rectTransform_Settings;
	private RectTransform _rectTransform_User;
	private RectTransform _rectTransform_SendQuestion;

	[SerializeField] private List<GameObject> _navigationList;

	[Header("Navigation Button")]
	[SerializeField] private Button _userNavigationButton;
	[SerializeField] private Button _mainNavigationButton;
	[SerializeField] private Button _sendQuestionNavigationButton;
	[SerializeField] private Button _settingsNavigationButton;

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
	}

	private void OnClickAddListener() 
	{
		_userNavigationButton.onClick.AddListener(ShowUserNavigation);
		_mainNavigationButton.onClick.AddListener(ShowMainNavigation);
		_sendQuestionNavigationButton.onClick.AddListener(ShowSendQuestionNavigation);
		_settingsNavigationButton.onClick.AddListener(ShowSettingsNavigation);
	}

	private void RectTransformSetter()
	{
		_rectTransform_User = _userNavigation.GetComponent<RectTransform>();
		_rectTransform_Main = _mainNavigation.GetComponent<RectTransform>();
		_rectTransform_SendQuestion = _sendQuestionNavigation.GetComponent<RectTransform>();
		_rectTransform_Settings = _settingsNavigation.GetComponent<RectTransform>();
	}

	private void ShowUserNavigation() { StartCoroutine(PanelChanger(Panels.User)); }
	private void ShowMainNavigation() { StartCoroutine(PanelChanger(Panels.Main)); }
	private void ShowSendQuestionNavigation() { StartCoroutine(PanelChanger(Panels.SendQuestion)); }
	private void ShowSettingsNavigation() { StartCoroutine(PanelChanger(Panels.Settings)); }

	private IEnumerator PanelChanger(Panels panel)
	{
		PanelOpener();

		RectTransform tempRectTransform;

		switch (panel)
		{
			case Panels.Main:
				tempRectTransform = _rectTransform_Main;
				break;
			case Panels.Settings:
				tempRectTransform = _rectTransform_Settings;
				break;
			case Panels.User:
				tempRectTransform = _rectTransform_User;
				break;
			case Panels.SendQuestion:
				tempRectTransform = _rectTransform_SendQuestion;
				break;
			default:
				tempRectTransform = new RectTransform();
				break;
		}

		Sequence panelSequence = DOTween.Sequence();

		panelSequence.Append(_rectTransform_Parent.DOAnchorPosX(-tempRectTransform.anchoredPosition.x, 0.3f))
					 .Append(_rectTransform_Parent.DOAnchorPosY(-tempRectTransform.anchoredPosition.y, 0.3f));


		yield return panelSequence.WaitForCompletion();

		_userNavigation.SetActive(panel == Panels.User);
		_mainNavigation.SetActive(panel == Panels.Main);
		_sendQuestionNavigation.SetActive(panel == Panels.SendQuestion);
		_settingsNavigation.SetActive(panel == Panels.Settings);
	}

	private void PanelOpener()
	{
		foreach (GameObject panel in _navigationList)
		{
			panel.SetActive(true);
		}
	}
}
