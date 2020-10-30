using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum ButtonCode
{
    Option1,
    Option2,
    Option3,
    Option4
}

public class OptionButton : MonoBehaviour//, IPointerClickHandler
{
    public bool isCorrectOption;

    [Header("Component")]
    public Button _optionButton;
    public TextMeshProUGUI _optionText;
    public RawImage _optionBackgroundImage;
    public RawImage _chooseIconImage;
    
    [Header("Option Background")]
    [SerializeField] private Texture2D _optionDefaultBackground;
    [SerializeField] private Texture2D _optionWrongBackground;
    [SerializeField] private Texture2D _optionCorrectBackground;

    [Header("Choose Icon")]
    [SerializeField] private Texture2D _defaultOptionIcon;
    [SerializeField] private Texture2D _choosenOptionIcon;
    [SerializeField] private Texture2D _correctOptionIcon;

    public ButtonCode _buttonCode;
    private Option _currentOption;
    private Action<bool> _onClick;
    public static Action _showCorrectOption;

    private void Awake()
    {
        OnClickAddListener();
        
    }
    private void OnClickAddListener()
    {
        _optionButton.onClick.AddListener(OnClick);
    }

    public void Init(in Option option, Action<bool> onClick)
    {
        _onClick = onClick;
        _currentOption = option;
        _optionBackgroundImage.texture = _optionDefaultBackground;
        _chooseIconImage.texture = _defaultOptionIcon;

        _optionText.SetText(option.OptionText);

        isCorrectOption = option.IsCorrectOption;

        if (_showCorrectOption != null)
        {
            _showCorrectOption -= ShowCorrectOption;
        }

        if (!isCorrectOption)
        {
            return;
        }
     
        _showCorrectOption = ShowCorrectOption;
    }

    private void OnClick() 
    {
        _onClick?.Invoke(isCorrectOption);

		if (isCorrectOption)
		{
            CorrectOption();
            return;
        }

        WrongOption();
    }

    public void WrongOption() 
    {
        _optionBackgroundImage.texture = _optionWrongBackground;
        _chooseIconImage.texture = _choosenOptionIcon;
    }

    public void CorrectOption()
    {
        _optionBackgroundImage.texture = _optionCorrectBackground;
        _chooseIconImage.texture = _choosenOptionIcon;
    }

    public void ShowCorrectOption() 
    {
        _optionBackgroundImage.texture = _optionCorrectBackground;
        _chooseIconImage.texture = _correctOptionIcon;
    }
}
