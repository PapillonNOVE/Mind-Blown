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
    public bool isTrueOption;

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

    private void OnEnable()
    {
        Subscribe();
    }

    private void Start()
    {
        OnClickAddListener();
    }

	private void OnDisable()
    {
        GeneralControls.ControlQuit(Unsubscribe);
    }

    private void Subscribe()
    {
        ActionManager.Instance.UpdateOptionButton += UpdateButton;
    }

	private void Unsubscribe()
	{
		ActionManager.Instance.UpdateOptionButton -= UpdateButton;
	}

	private void OnClickAddListener()
    {
        _optionButton.onClick.AddListener(ClickOption);
    }

    private void ClickOption() 
    {
        ActionManager.Instance.ControlAnswer(isTrueOption, this);
    }

    public void UpdateButton(string optionText, ButtonCode buttonCode = 0, bool isCorrectOption = false)
    {
        //if (buttonCode == _buttonCode)
        //{
        //    _optionBackgroundImage.texture = _optionDefaultBackground;
        //    _chooseIconImage.texture = _defaultOptionIcon;
        //    _optionText.SetText(optionText);
        //    isTrueOption = isCorrectOption;
        //}

        _optionBackgroundImage.texture = _optionDefaultBackground;
        _optionText.SetText(optionText);
        _chooseIconImage.texture = _defaultOptionIcon;

		if (isCorrectOption)
		{
            isTrueOption = true;
        }
		else
		{
            isTrueOption = false;
        }
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
