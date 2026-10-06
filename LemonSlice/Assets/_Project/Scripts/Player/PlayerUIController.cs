using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
	/*
	[SerializeField] private Image _playerHeart; // 하트 UI 프리팹 참조
	[SerializeField] private Sprite _filledHeart;
	[SerializeField] private Sprite _blankHeart; // 빈하트 UI 프리팹 참조
	[SerializeField] private GameObject _heartPosition; // 하트 배치 기준점
	[SerializeField] private Image _playerStamina; // 스테미나 UI 참조
	[SerializeField] private float _fillStaminaDelay; // 스테미나 차오르는 딜레이

	[SerializeField] private int _heartCount; // 생성할 하트 수

	private Coroutine fillStaminaCoroutine; // 스테미나 회복 코루틴

	private int currentHeart; // 현재 하트 수
	private List<Image> playerHearts = new List<Image>(); // 하트 배열
	private bool isStaminaActive; // 스테미나 꺼짐 켜짐 여부
	private bool isActionRunning; // 플레이어가 행동 중인가
	private WaitForSeconds fillStaminaDelay;

	private void Start()
	{
		SetPlayerHeart();
		SetStamina();
	}

	private void Update()
	{
		DecreaseHeart();
		IncreaseHeart();
		DecreaseStamina();
		FillStamina();
	}

	private void SetPlayerHeart()
	{
		currentHeart = _heartCount;

		for (int i = 0; i < _heartCount; i++)
		{
			float x = 100f;
			float y = 100f;
			if (i < 10)
			{
				x *= i;
				playerHearts.Add(Instantiate(_playerHeart, _heartPosition.transform));
				playerHearts[i].rectTransform.anchoredPosition = new Vector2(x, 0f);
			}
			else if (i < 20)
			{
				x *= (i - 10);
				playerHearts.Add(Instantiate(_playerHeart, _heartPosition.transform));
				playerHearts[i].rectTransform.anchoredPosition = new Vector2(x, -y);
			}
		}
	}

	private void DecreaseHeart()
	{
		if (Input.GetKeyDown(KeyCode.E) && currentHeart > 0)
		{
			currentHeart--;
			playerHearts[currentHeart].sprite = _blankHeart;
		}
	}

	private void IncreaseHeart()
	{
		if (Input.GetKeyDown(KeyCode.F) && currentHeart < _heartCount)
		{
			playerHearts[currentHeart].sprite = _filledHeart;
			currentHeart++;
		}
	}

	private void SetStamina() // 스테미나 초기화
	{
		fillStaminaDelay = new WaitForSeconds(_fillStaminaDelay);
		HideStamina();
	}

	private void HideStamina() // 스테미나 UI 숨기기
	{
		_playerStamina.gameObject.SetActive(false);
		isStaminaActive = false;
	}

	private void ShowStamina() // 스테미나 UI 보이기
	{
		if (!isStaminaActive)
		{
			_playerStamina.gameObject.SetActive(true);
			isStaminaActive = true;
		}
	}

	private void DecreaseStamina() // 스테미나 사용
	{
		if (Input.GetKeyDown(KeyCode.Space) && _playerStamina.fillAmount == 1) // 스테미나 꽉 찼을 때
		{
			ShowStamina();
			_playerStamina.fillAmount -= 0.125f;
			isActionRunning = true;
			fillStaminaCoroutine = StartCoroutine(FillStaminaRoutine()); // 스테미나 회복 딜레이의 첫 발생
		}
		else if (_playerStamina.fillAmount < 1 && _playerStamina.fillAmount >= 0.125f && Input.GetKeyDown(KeyCode.Space))
		{
			StopCoroutine(fillStaminaCoroutine);// 스테미나 회복 딜레이를 멈춤
			_playerStamina.fillAmount -= 0.125f;
			isActionRunning = true;
			fillStaminaCoroutine = StartCoroutine(FillStaminaRoutine());// 스테미나 회복 딜레이의 갱신
		}
	}

	private IEnumerator FillStaminaRoutine() // 스테미나 회복 코루틴
	{
		yield return fillStaminaDelay;
		isActionRunning = false;
	}

	private void FillStamina()
	{
		if (_playerStamina.fillAmount < 1 && !isActionRunning)
		{
			_playerStamina.fillAmount += Time.deltaTime * 0.125f;
		}
		else if (_playerStamina.fillAmount == 1)
		{
			HideStamina();
		}
	}
	*/
}
