using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NativeSizeManager : MonoBehaviour
{
    [SerializeField] private float widhtMultiplier;
    [SerializeField] private float heightMultiplier;

	private void Awake()
	{
		gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width * widhtMultiplier, Screen.height * heightMultiplier);
	}
}
