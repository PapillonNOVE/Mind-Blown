using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public struct CategoryToggleState
{
	public string name;
	public CategoryType categoryType;
	public bool isActive;
}

[Serializable]
public struct CategoryStateHolder
{
	public List<CategoryToggleState> CategoryToggleStates;
}

public class CategoryInfo : MonoBehaviour
{
	public CategoryType category;
}
