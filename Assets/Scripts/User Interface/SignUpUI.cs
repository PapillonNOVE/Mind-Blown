using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class SignUpUI : Singleton<SignUpUI>
{
    //[SerializeField] private TextMeshProUGUI txt_SignUp_Header;

    [Header("Text")]
    [SerializeField] private TextMeshProUGUI _text_EnglishToggle;
    [SerializeField] private TextMeshProUGUI _text_TurkishToggle;

    [Header("Input Field")]
    [SerializeField] private TMP_InputField _inputField_Username;
    [SerializeField] private TMP_InputField _inputField_Email;
    [SerializeField] private TMP_InputField _inputField_Password;
    [SerializeField] private TMP_InputField _inputField_ConfirmPassword;

    [Header("Toggle")]
    [SerializeField] private ToggleGroup _toggleGroup_LanguageToggle;
    [SerializeField] private Toggle _toggle_EnglishLanguage;
    [SerializeField] private Toggle _toggle_TurkishLanguage;

    [Header("Button")]
    [SerializeField] private Button _btn_Home;
    [SerializeField] private Button _btn_SignUp;
    [SerializeField] private Button _btn_SignIn;

    private void OnEnable()
    {
        OnClickAddListener();
    }

    private void OnDisable()
    {

    }

    private void OnClickAddListener()
    {
        _btn_Home.onClick.AddListener(UIManager.Instance.ShowMenuPanel);
        _btn_SignIn.onClick.AddListener(UIManager.Instance.ShowSignInPanel);
        _btn_SignUp.onClick.AddListener(SignUp);
    }

    private void SignUp()
    {
        SignUpStruct signUpStruct = new SignUpStruct();

        signUpStruct.Username = _inputField_Username.textComponent.text.Replace("\u200B", "");
        signUpStruct.Email = _inputField_Email.textComponent.text.Replace("\u200B", "");
        signUpStruct.Password = _inputField_Password.textComponent.text.Replace("\u200B", "");
        signUpStruct.ConfirmPassword = _inputField_ConfirmPassword.textComponent.text.Replace("\u200B", "");//.Replace("\u200B", "");

        if (_toggle_EnglishLanguage.isOn)
        {
            signUpStruct.Language = _text_EnglishToggle.text;
        }
        else if (_toggle_TurkishLanguage.isOn)
        {
            signUpStruct.Language = _text_TurkishToggle.text;
        }

        Debug.Log(signUpStruct.Language);

        //ActionManager.Instance.SignUpWithEmailPassword(signUpStruct);
    }
}
