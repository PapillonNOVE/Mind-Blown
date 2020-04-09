using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : Singleton<UIManager>
{
	[Header("Panel")]
	[SerializeField] private GameObject mainCanvas;
	
	[Header("Panel")]
	[SerializeField] private GameObject panel_MainMenu;
	//[SerializeField] private GameObject pnl_Lobby;
	//[SerializeField] private GameObject pnl_CreateRoom;
	[SerializeField] private GameObject panel_Game;
	[SerializeField] private GameObject panel_Settings;
	//[SerializeField] private GameObject pnl_Store;
	[SerializeField] private GameObject panel_SignIn;
	[SerializeField] private GameObject panel_SignUp;
	[SerializeField] private GameObject panel_ResetPassword;
	[SerializeField] private GameObject panel_UserProfile;
	[SerializeField] private GameObject panel_SendQuestion;
	[SerializeField] private GameObject panel_ApprovePendingQuestions;
	[SerializeField] private GameObject panel_PanelParent;
	//[SerializeField] private GameObject pnl_Lose;
	//[SerializeField] private GameObject pnl_Win;

	[Header("RectTransform")]
	private RectTransform rectTransform_MainMenu;
	private RectTransform rectTransform_Game;
	private RectTransform rectTransform_Settings;
	private RectTransform rectTransform_SignIn;
	private RectTransform rectTransform_SignUp;
	private RectTransform rectTransform_ResetPassword;
	private RectTransform rectTransform_UserProfile;
	private RectTransform rectTransform_SendQuestion;
	private RectTransform rectTransform_ApprovePendingQuestions;
	private RectTransform rectTransform_Parent;

	[SerializeField] private List<GameObject> panelList;

	/*   [Space(5)]

	   [Header("Panel / Lobby")]
	   [SerializeField] TextMeshProUGUI txt_Lobby_Header;
	   [SerializeField] TMP_Dropdown drpdwn_Lobby_RoomList;
	   [SerializeField] Button btn_Lobby_Home;
	   //[SerializeField] Button btn_Lobby_Settings;
	   [SerializeField] Button btn_Lobby_Refresh;
	   [SerializeField] Button btn_Lobby_Enter;
	   [SerializeField] Button btn_Lobby_Create;
	   //[SerializeField] Button btn_Lobby_Search_Game;


	   [Space(5)]

	   [Header("Panel / Create Room")]
	   [SerializeField] TextMeshProUGUI txt_CreateRoom_Header;
	   //[SerializeField] TMP_Dropdown drpdwn_Lobby_RoomList;
	   [SerializeField] TextMeshProUGUI txt_CreateRoom_RoomName;
	   [SerializeField] TextMeshProUGUI txt_CreateRoom_RoomPassword;
	   [SerializeField] Button btn_CreateRoom_Home;
	   [SerializeField] Button btn_CreateRoom_Create;
	   //[SerializeField] Button btn_Lobby_Settings;
	   //[SerializeField] Button btn_Lobby_Refresh;
	   //[SerializeField] Button btn_CreateRoom_Enter;
	   //[SerializeField] Button btn_Lobby_Search_Game;



	   [Header("Panel / Store")]
	   [SerializeField] TextMeshProUGUI txt_Store_Header;
	   [SerializeField] Button btn_Store_Home;


	   [Space(5)]

	   [Header("Panel / Lose")]
	   [SerializeField] TextMeshProUGUI txt_Lose_Header;
	   [SerializeField] TextMeshProUGUI txt_Lose_Outcome;
	   [SerializeField] TextMeshProUGUI txt_Lose_PlayerScore;
	   [SerializeField] TextMeshProUGUI txt_Lose_RivalScore;
	   [SerializeField] Button btn_Lose_Home;
	   [SerializeField] Button btn_Lose_Play;

	   [Space(5)]

	   [Header("Panel / Win")]
	   [SerializeField] TextMeshProUGUI txt_Win_Header;
	   [SerializeField] TextMeshProUGUI txt_Win_Outcome;
	   [SerializeField] TextMeshProUGUI txt_Win_PlayerScore;
	   [SerializeField] TextMeshProUGUI txt_Win_RivalScore;
	   [SerializeField] Button btn_Win_Home;
	   [SerializeField] Button btn_Win_Play;
	   */

	private void OnEnable()
	{
		ActionManager.Instance.ShowSignInPanel += ShowSignInPanel;
		ActionManager.Instance.ShowSignUpPanel += ShowSignUpPanel;
		ActionManager.Instance.ShowUserProfilePanel += ShowUserProfilePanel;
	}

	private void OnDisable()
	{
		ActionManager.Instance.ShowSignInPanel -= ShowSignInPanel;
		ActionManager.Instance.ShowSignUpPanel -= ShowSignUpPanel;
		ActionManager.Instance.ShowUserProfilePanel -= ShowUserProfilePanel;
	}

	private void OnApplicationQuit()
	{
		ActionManager.Instance.ShowSignInPanel -= ShowSignInPanel;
		ActionManager.Instance.ShowSignUpPanel -= ShowSignUpPanel;
		ActionManager.Instance.ShowUserProfilePanel -= ShowUserProfilePanel;
	}

	private void Start()
	{


		// Lobby Panel Add Click Listener
		//     btn_Lobby_Home.onClick.AddListener(ShowMenuPanel);
		//btn_Lobby_Refresh.onClick.AddListener(RefreshRoomList);
		// btn_Lobby_Enter.onClick.AddListener(EnterRoom);
		//    btn_Lobby_Create.onClick.AddListener(ShowCreateRoomPanel);


		// Create Room Panel Add Click Listener
		//    btn_CreateRoom_Home.onClick.AddListener(ShowMenuPanel);
		// btn_CreateRoom_Create.onClick.AddListener(CreateRoom);



		/* 
         btn_Store_Home.onClick.AddListener(ShowMenuPanel);
         btn_Lose_Home.onClick.AddListener(ShowMenuPanel);
         btn_Win_Home.onClick.AddListener(ShowMenuPanel);*/

		// Script Specification
		RectTransformSetter();
	}

	private void RectTransformSetter()
	{
		rectTransform_MainMenu = panel_MainMenu.GetComponent<RectTransform>();
		rectTransform_Game = panel_Game.GetComponent<RectTransform>();
		rectTransform_Settings = panel_Settings.GetComponent<RectTransform>();
		rectTransform_SignIn = panel_SignIn.GetComponent<RectTransform>();
		rectTransform_SignUp = panel_SignUp.GetComponent<RectTransform>();
		rectTransform_ResetPassword = panel_ResetPassword.GetComponent<RectTransform>();
		rectTransform_UserProfile = panel_UserProfile.GetComponent<RectTransform>();
		rectTransform_SendQuestion = panel_SendQuestion.GetComponent<RectTransform>();
		rectTransform_ApprovePendingQuestions = panel_ApprovePendingQuestions.GetComponent<RectTransform>();
		rectTransform_Parent = panel_PanelParent.GetComponent<RectTransform>();
	}

	public enum Panels
	{
		MainMenu,
		Lobby,
		CreateRoom,
		Game,
		Settings,
		Store,
		SignIn,
		SignUp,
		ResetPassword,
		UserProfile,
		Lose,
		Win,
		SendQuestion,
		AprrovePendingQuestions
	}


	public void ShowMainMenuPanel() { StartCoroutine(PanelChanger(Panels.MainMenu)); }
	public void ShowLobbyPanel() { StartCoroutine(PanelChanger(Panels.Lobby)); }
	public void ShowCreateRoomPanel() { StartCoroutine(PanelChanger(Panels.CreateRoom)); }
	public void ShowGamePanel() { StartCoroutine(PanelChanger(Panels.Game)); }
	public void ShowSettingsPanel() { StartCoroutine(PanelChanger(Panels.Settings)); }
	public void ShowStorePanel() { StartCoroutine(PanelChanger(Panels.Store)); }
	public void ShowSignUpPanel() { StartCoroutine(PanelChanger(Panels.SignUp)); }
	public void ShowSignInPanel() { StartCoroutine(PanelChanger(Panels.SignIn)); }
	public void ShowResetPasswordPanel() { StartCoroutine(PanelChanger(Panels.ResetPassword)); }
	public void ShowUserProfilePanel() { StartCoroutine(PanelChanger(Panels.UserProfile)); }
	public void ShowLosePanel() { StartCoroutine(PanelChanger(Panels.Lose)); }
	public void ShowWinPanel() { StartCoroutine(PanelChanger(Panels.Win)); }
	public void ShowSendQuestionPanel() { StartCoroutine(PanelChanger(Panels.SendQuestion)); }
	public void ShowAprrovePendingQuestionsPanel() { StartCoroutine(PanelChanger(Panels.AprrovePendingQuestions)); }

	private IEnumerator PanelChanger(Panels panels)
	{
		PanelOpener();

		RectTransform tempRectTransform = new RectTransform();

		switch (panels)
		{
			case Panels.MainMenu:
				tempRectTransform = rectTransform_MainMenu;
				break;
			case Panels.Lobby:
				break;
			case Panels.CreateRoom:
				break;
			case Panels.Game:
				tempRectTransform = rectTransform_Game;
				break;
			case Panels.Settings:
				tempRectTransform = rectTransform_Settings;
				break;
			case Panels.Store:
				break;
			case Panels.SignIn:
				tempRectTransform = rectTransform_SignIn;
				break;
			case Panels.SignUp:
				tempRectTransform = rectTransform_SignUp;
				break;
			case Panels.ResetPassword:
				tempRectTransform = rectTransform_ResetPassword;
				break;
			case Panels.UserProfile:
				tempRectTransform = rectTransform_UserProfile;
				break;
			case Panels.Lose:
				break;
			case Panels.Win:
				break;
			case Panels.SendQuestion:
				tempRectTransform = rectTransform_SendQuestion;
				break;
			case Panels.AprrovePendingQuestions:
				tempRectTransform = rectTransform_ApprovePendingQuestions;
				break;
			default:
				tempRectTransform = new RectTransform();
				break;
		}

		Sequence panelSequence = DOTween.Sequence();

		panelSequence.Append(rectTransform_Parent.DOAnchorPosX(-tempRectTransform.anchoredPosition.x, 0.3f))
					 .Join(rectTransform_Parent.DOAnchorPosY(-tempRectTransform.anchoredPosition.y, 0.3f));


		yield return panelSequence.WaitForCompletion();

		panel_MainMenu.SetActive(panels == Panels.MainMenu);
		panel_Game.SetActive(panels == Panels.Game);
		panel_Settings.SetActive(panels == Panels.Settings);
		panel_SignUp.SetActive(panels == Panels.SignUp);
		panel_SignIn.SetActive(panels == Panels.SignIn);
		panel_ResetPassword.SetActive(panels == Panels.ResetPassword);
		panel_UserProfile.SetActive(panels == Panels.UserProfile);
		panel_SendQuestion.SetActive(panels == Panels.SendQuestion);
		panel_ApprovePendingQuestions.SetActive(panels == Panels.AprrovePendingQuestions);



		//pnl_Lobby.SetActive(panels == Panels.Lobby);
		//pnl_Store.SetActive(panels == Panels.Store);
		//pnl_Lose.SetActive(panels == Panels.Lose);
		//pnl_Win.SetActive(panels == Panels.Win);
	}

	private void PanelOpener()
	{
		//mainCanvas.SetActive(true);

		foreach (GameObject panel in panelList)
		{
			panel.SetActive(true);
		}
	}
}