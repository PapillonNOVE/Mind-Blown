using System;
using System.Collections.Generic;

[Serializable]
public struct Question
{
	public string QuestionText;
	public List<Option> OptionList;
	public Options Options;
	public string QuestionLevel;
	public string QuestionLanguage;
	public string QuestionCategory;
	public string SenderPlayerID;
}

[Serializable]
public struct Options
{
	public Option CorrectOption;
	public Option WrongOption1;
	public Option WrongOption2;
	public Option WrongOption3;
}

[Serializable]
public struct Option
{
	public string OptionText;
	public bool IsCorrectOption;
}