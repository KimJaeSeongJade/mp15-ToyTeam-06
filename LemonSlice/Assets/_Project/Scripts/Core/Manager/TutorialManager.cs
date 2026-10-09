using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : SingletonBehaviour<TutorialManager>
{
	[SerializeField] private GameObject[] TutorialUI;


	private void Awake()
	{
		for(int i = 0; i < TutorialUI.Length; i++)
		{
			TutorialUI[i].SetActive(false);
		}
	}

	public void ShowTutorialUI(int index)
	{
		TutorialUI[index].SetActive(true);
	}
}
