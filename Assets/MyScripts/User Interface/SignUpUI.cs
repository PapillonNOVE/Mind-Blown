using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using EasyMobile;
using Constants;

public class SignUpUI : MonoBehaviour
{
    //[SerializeField] private TextMeshProUGUI txt_SignUp_Header;
    [Header("Color")]
    [SerializeField] private Color color_Green;
    [SerializeField] private Color color_Red;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI text_UsernameWarning;
    [SerializeField] private TextMeshProUGUI text_EmailWarning;
    [SerializeField] private TextMeshProUGUI text_ConfirmEmailWarning;
    [SerializeField] private TextMeshProUGUI text_PasswordWarning;
    [SerializeField] private TextMeshProUGUI text_ConfirmPasswordWarning;
    [Space(10)]
    [SerializeField] private TextMeshProUGUI text_EnglishToggle;
    [SerializeField] private TextMeshProUGUI text_TurkishToggle;

    [Header("Input Field")]
    [SerializeField] private TMP_InputField inputField_Username;
    [SerializeField] private TMP_InputField inputField_Email;
    [SerializeField] private TMP_InputField inputField_ConfirmEmail;
    [SerializeField] private TMP_InputField inputField_Password;
    [SerializeField] private TMP_InputField inputField_ConfirmPassword;

    [Header("Toggle")]
    [SerializeField] private ToggleGroup toggleGroup_LanguageToggle;
    [SerializeField] private Toggle toggle_EnglishLanguage;
    [SerializeField] private Toggle toggle_TurkishLanguage;

    [Header("Button")]
    [SerializeField] private Button btn_GoToMainMenu;
    [SerializeField] private Button btn_SignUp;
    [SerializeField] private Button btn_GoToSignIn;

    [Header("RectTransform")]
    [SerializeField] private RectTransform panel_Parent;

    [Header("Boolean")]
    [SerializeField] private bool isUsernameUnique = false;
    [SerializeField] private bool isEmailFormatCorrect = false;
    [SerializeField] private bool isPasswordFormatCorrect = false;
    [SerializeField] private bool isConfirmPasswordSamePassword = false;
    bool isLanguageSelected = false;

    public const string MatchEmailPattern = @"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
       + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
       + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
       + @"([a-zA-Z]+[\w-]+\.)+[a-zA-Z]{2,4})$";

    [SerializeField] private int passwordMinLenght;
    [SerializeField] private int passwordMaxLenght;

    private void OnEnable()
    {
        OnClickAddListener();
        OnValueChangedAddListener();
    }

    private void OnDisable()
    {

    }

    /*private void Register() 
    {
        ActionManager.Instance.UsernameAvaliable += UsernameAvaliable;
        ActionManager.Instance.UsernameNotAvaliable += UsernameNotAvaliable;
    }*/

    private void OnClickAddListener()
    {
        btn_GoToMainMenu.onClick.AddListener(OpenMainMenu);
        btn_SignUp.onClick.AddListener(SignUp);
        btn_GoToSignIn.onClick.AddListener(GoToSignIn);
    }

    private void OnValueChangedAddListener()
    {
        inputField_Username.onValueChanged.AddListener(ControlUsername);
        inputField_Email.onEndEdit.AddListener(ControlEmail);
        inputField_ConfirmEmail.onValueChanged.AddListener(ControlConfirmEmail);
        inputField_Password.onValueChanged.AddListener(ControlPassword);
        inputField_ConfirmPassword.onValueChanged.AddListener(ControlConfirmPassword);
    }



    #region OnEditInputField

    #region Username

    private void ControlUsername(string arg0)
    {
        // Debug.Log("1: " + arg0.Length);
        //// arg0.Replace("\u200B", "");
        // Debug.Log("2: " + arg0.Length);

        StartCoroutine(EventManager.Instance.ControlIsUsernameExist(arg0, UsernameAvaliable, UsernameNotAvaliable));
    }

    private void UsernameAvaliable()
    {
        isUsernameUnique = true;
        inputField_Username.GetComponent<Outline>().effectColor = color_Green;
        text_UsernameWarning.gameObject.SetActive(false);
        SetInteractableSignUpButton();
    }

    private void UsernameNotAvaliable()
    {
        isUsernameUnique = false;
        inputField_Username.GetComponent<Outline>().effectColor = color_Red;
        text_UsernameWarning.gameObject.SetActive(true);
        SetInteractableSignUpButton();
    }

    #endregion

    #region Email

    private void ControlEmail(string arg0)
    {
        /* arg0.Replace("\u200B", "");

         if (arg0 == MatchEmailPattern)
         {
             isEmailFormatCorrect = true;
         }
         else
         {
             isEmailFormatCorrect = false;
         }
         */
        SetInteractableSignUpButton();
    }

    private void ControlConfirmEmail(string arg0)
    {
        Debug.Log("1: " + arg0.Length);
        arg0.Replace("\u200B", "");
        Debug.Log("2: " + arg0.Length);

        if (arg0 == inputField_Email.text)
        {
            isConfirmPasswordSamePassword = true;
            inputField_ConfirmEmail.GetComponent<Outline>().effectColor = color_Green;
            text_ConfirmEmailWarning.gameObject.SetActive(false);
        }
        else
        {
            isConfirmPasswordSamePassword = false;
            inputField_ConfirmEmail.GetComponent<Outline>().effectColor = color_Red;
            text_ConfirmEmailWarning.gameObject.SetActive(true);
        }


        SetInteractableSignUpButton();
    }

    #endregion

    #region Password

    private void ControlPassword(string arg0)
    {
        arg0.Replace("\u200B", "");

        if (arg0.Length >= passwordMinLenght && arg0.Length <= passwordMaxLenght)
        {
            isPasswordFormatCorrect = true;
            inputField_Password.GetComponent<Outline>().effectColor = color_Green;
            text_PasswordWarning.gameObject.SetActive(false);
        }
        else
        {
            isPasswordFormatCorrect = false;
            inputField_Password.GetComponent<Outline>().effectColor = color_Red;
            text_PasswordWarning.gameObject.SetActive(true);
        }

        SetInteractableSignUpButton();
    }

    private void ControlConfirmPassword(string arg0)
    {
        Debug.Log("1: " + arg0.Length);
        arg0.Replace("\u200B", "");
        Debug.Log("2: " + arg0.Length);

        if (arg0 == inputField_Password.text)
        {
            isConfirmPasswordSamePassword = true;
            inputField_ConfirmPassword.GetComponent<Outline>().effectColor = color_Green;
            text_ConfirmPasswordWarning.gameObject.SetActive(false);
        }
        else
        {
            isConfirmPasswordSamePassword = false;
            inputField_ConfirmPassword.GetComponent<Outline>().effectColor = color_Red;
            text_ConfirmPasswordWarning.gameObject.SetActive(true);
        }


        SetInteractableSignUpButton();
    }

    #endregion

    #endregion

    public void SetInteractableSignUpButton()
    {
        if (isUsernameUnique && /*isUsernameFormatCorrect &&*/ isEmailFormatCorrect && isPasswordFormatCorrect && isConfirmPasswordSamePassword)
        {
            btn_SignUp.interactable = true;
        }
        else
        {
            btn_SignUp.interactable = false;
        }
    }

    private void SignUp()
    {
        SignUpStruct signUpStruct = new SignUpStruct();

        signUpStruct.Username = inputField_Username.textComponent.text.Replace("\u200B", "");
        signUpStruct.Email = inputField_Email.textComponent.text.Replace("\u200B", "");
        signUpStruct.Password = inputField_Password.textComponent.text.Replace("\u200B", "");
        signUpStruct.ConfirmPassword = inputField_ConfirmPassword.textComponent.text.Replace("\u200B", "");

        if (toggle_EnglishLanguage.isOn)
        {
            signUpStruct.Language = text_EnglishToggle.text;
        }
        else if (toggle_TurkishLanguage.isOn)
        {
            signUpStruct.Language = text_TurkishToggle.text;
        }

        Debug.Log(signUpStruct.Language);

        StartCoroutine(EventManager.Instance.SignUpWithEmailPassword(signUpStruct, SignUpWithEmailPasswordSuccesful, SignUpWithEmailPasswordFailed));
    }

    private void SignUpWithEmailPasswordSuccesful()
    {
        // NativeUI.AlertPopup alertPopup = NativeUI.Alert(AuthenticationsDebugs.SignUpPaths.SignUpSuccessful, AuthenticationsDebugs.SignUpPaths.SignUpSuccessfulDetail);
        // NativeUI.ShowToast($"{AuthenticationsDebugs.SignUpPaths.SignUpSuccessful} {AuthenticationsDebugs.SignUpPaths.SignUpSuccessfulDetail}");
    }

    private void SignUpWithEmailPasswordFailed()
    {
        //NativeUI.AlertPopup alertPopup = NativeUI.Alert(AuthenticationsDebugs.SignUpPaths.SignUpFailed, AuthenticationsDebugs.SignUpPaths.SignUpFailedDetail);
    }

    private void GoToSignIn()
    {
        UIManager.Instance.ShowSignInPanel();

        //UIManager.Instance.PanelOpener();

        /* Sequence panelSeq = DOTween.Sequence();
         panelSeq.Append(panel_Parent.DOAnchorPosX(0, 0.3f))
                 .Append(panel_Parent.DOAnchorPosY(0, 0.3f))
                 .OnComplete(() => UIManager.Instance.ShowSignInPanel());*/
    }

    private void OpenMainMenu()
    {
        UIManager.Instance.ShowMainMenuPanel();
    }
}
