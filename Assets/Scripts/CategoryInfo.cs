using System.Collections.Generic;
using UnityEngine;

public struct Category
{
	public List<string> categories;
}

public class CategoryInfo : MonoBehaviour
{
	public enum Category
	{
		Anime,
		Book,
		Documentary,
		History,
		Language,
		MartialArt,
		Math,
		Movie,
		Music,
		Religion,
		Science,
		Technology,
		VideoGame
	}

	public Category category;
}
