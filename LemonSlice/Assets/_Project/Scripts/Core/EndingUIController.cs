using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndingUIController : MonoBehaviour
{
	[SerializeField] private Button _titleButton; // 타이틀
	[SerializeField] private Button _creditButton; // 크레딧
	[SerializeField] private Button _exitCreditButton;
	[SerializeField] private GameObject _creditPanel; // 크레딧 판넬

	private void Start()
	{
		CloseCredit();
		BindButtonEvents();
	}

	private void BindButtonEvents()
	{
		_titleButton.onClick.AddListener(LoadTitle);
		_creditButton.onClick.AddListener(OpenCredit);
		_exitCreditButton.onClick.AddListener(CloseCredit);
	}

	private void OnDisable()
	{
		UnBindButtonEvents();
	}

	private void UnBindButtonEvents()
	{
		_titleButton.onClick.RemoveListener(LoadTitle);
		_creditButton.onClick.RemoveListener(OpenCredit);
		_exitCreditButton.onClick.RemoveListener(CloseCredit);
	}

	private void LoadTitle() // 타이틀 씬 전환
	{
		SceneManager.LoadScene("Title");
	}

	private void OpenCredit() // 크레딧 창 활성화
	{
		_creditPanel.SetActive(true);
	}

	private void CloseCredit() // 크레딧 창 비활성화
	{
		_creditPanel.SetActive(false);
	}
}
