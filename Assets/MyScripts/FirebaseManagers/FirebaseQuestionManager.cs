using Constants;
using Firebase.Database;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

public class FirebaseQuestionManager : MonoBehaviour
{
	public static List<string> questionIDs = new List<string>();

	private void OnEnable()
	{
		Subscribe();

		StartCoroutine(GetQuestionIDs());
	}

	private void OnDisable()
	{
		GeneralControls.ControlQuit(Unsubscribe);
	}

	#region Event Subscribe/Unsubscribe

	private void Subscribe()
	{
		EventManager.Instance.GetQuestionIDs += GetQuestionIDs;
		EventManager.Instance.GetQuestion += GetQuestion;
		EventManager.Instance.SendQuestion += SendQuestion;
	}

	private void Unsubscribe()
	{
		EventManager.Instance.GetQuestionIDs -= GetQuestionIDs;
		EventManager.Instance.GetQuestion -= GetQuestion;
		EventManager.Instance.SendQuestion -= SendQuestion;
	}

	#endregion

	private IEnumerator GetQuestionIDs(/*List<string> categories*/)
	{
		questionIDs.Clear();

		while (EventManager.Instance.LoadCategories == null || !LoadingUI.S_IsDatabaseReferencesCreated)
		{
			Debug.Log("off offfff");
			yield return null;
		}

		CategoryStateHolder categoryStateHolder = EventManager.Instance.LoadCategories();

		List<string> categories = new List<string>();

		foreach (CategoryToggleState categoryToggleState in categoryStateHolder.CategoryToggleStates)
		{
			if (categoryToggleState.isActive)
			{
				categories.Add(categoryToggleState.name);
			}
		}

		Debug.Log(FirebaseManager.PublishedQuestionsDatabaseReference);

		Task<DataSnapshot> task = FirebaseManager.PublishedQuestionsDatabaseReference.GetValueAsync();

		yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);

		if (task.IsCanceled)
		{
			Debug.LogWarning(GetDataTaskDebugs.GetData + DebugPaths.IsCanceled);
		}
		else if (task.IsFaulted)
		{
			Debug.LogError(GetDataTaskDebugs.GetData + DebugPaths.IsFaulted);
		}
		else if (task.IsCompleted)
		{
			Debug.Log(GetDataTaskDebugs.GetData + DebugPaths.IsCompleted);

			DataSnapshot dataSnapshot = task.Result;

			foreach (DataSnapshot questionID in dataSnapshot.Children)
			{
				string categoryOfQuestion = questionID.Child(QuestionPaths.QuesitonPath.QuestionCategory).Value.ToString();

				if (categories.Contains(categoryOfQuestion))
				{
					questionIDs.Add(questionID.Key);
				}
			}
		}
	}

	private IEnumerator GetQuestion()
	{
		if (questionIDs.Count > 0)
		{
			string questionID = questionIDs[UnityEngine.Random.Range(0, questionIDs.Count)];

			Task<DataSnapshot> task = FirebaseManager.PublishedQuestionsDatabaseReference.Child(questionID).GetValueAsync();

			yield return new WaitUntil(() => task.IsCanceled || task.IsFaulted || task.IsCompleted);

			if (task.IsCanceled)
			{
				Debug.LogWarning(GetDataTaskDebugs.GetData + DebugPaths.IsCanceled);
			}
			else if (task.IsFaulted)
			{
				Debug.LogError(GetDataTaskDebugs.GetData + DebugPaths.IsFaulted);
			}
			else if (task.IsCompleted)
			{
				Debug.Log(GetDataTaskDebugs.GetData + DebugPaths.IsCompleted);

				DataSnapshot snapshot = task.Result;

				Question question = JsonUtility.FromJson<Question>(snapshot.GetRawJsonValue());

				//Question question = new Question();
				//question.OptionList = new List<Option>();

				//foreach (DataSnapshot option in snapshot.Child(QuestionPaths.QuesitonPath.Options).Children)
				//{
				//	Option newOption = new Option();

				//	newOption.OptionText = option.Child(QuestionPaths.QuesitonPath.OptionPath.OptionText).Value.ToString();

				//	newOption.IsCorrectOption = (bool)option.Child(QuestionPaths.QuesitonPath.OptionPath.IsCorrectOption).Value;

				//	question.OptionList.Add(newOption);
				//}

				//question.QuestionText = snapshot.Child(QuestionPaths.QuesitonPath.QuestionText).Value.ToString();
				//question.QuestionLevel = int.Parse(snapshot.Child(QuestionPaths.QuesitonPath.QuestionLevel).Value.ToString());

				question.OptionList.Add(question.Options.CorrectOption);
				question.OptionList.Add(question.Options.WrongOption1);
				question.OptionList.Add(question.Options.WrongOption2);
				question.OptionList.Add(question.Options.WrongOption3);

				Debug.Log(question.QuestionLevel);
				EventManager.Instance.AskQuestion(question);
			}
		}
		else
		{
			Debug.LogError("Hiç soru bulunamadı");
		}
	}

	private void SendQuestion(Question question)
	{
		string questionID = FirebaseManager.PendingQuestionsDatabaseReference.Push().Key;

		string json = JsonUtility.ToJson(question);

		FirebaseManager.PendingQuestionsDatabaseReference.Child(questionID).SetRawJsonValueAsync(json);
	}
}
