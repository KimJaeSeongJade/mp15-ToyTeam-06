using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUIController : MonoBehaviour
{
	[SerializeField] private Image _playerHeart; // 하트 UI 프리팹 참조
	[SerializeField] private Sprite _filledHeart;
	[SerializeField] private Sprite _blankHeart; // 빈하트 UI 프리팹 참조
	[SerializeField] private GameObject _heartPosition; // 하트 배치 기준점
	[SerializeField] private Image _playerStamina; // 스테미나 UI 참조
	[SerializeField] private float _recoveryStamina; // 1초에 스테미나 차오르는 양

	[SerializeField] private int _heartCount; // 생성할 하트 수

	private Coroutine addStaminaCoroutine;

	private int currentHeart; // 현재 하트 수
	private List<Image> playerHearts = new List<Image>(); // 하트 배열

	private void Start() => Init();

	private void Update()
	{
		DiscountHeart();
		AddcountHeart();
		DiscountStamina();
		AddStamina();
	}

	private void Init()
	{
		_playerStamina.gameObject.SetActive(false);

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

	private void DiscountHeart()
	{
		if (Input.GetKeyDown(KeyCode.E) && currentHeart > 0)
		{
			currentHeart--;
			playerHearts[currentHeart].sprite = _blankHeart;
		}
	}

	private void AddcountHeart()
	{
		if (Input.GetKeyDown(KeyCode.F) && currentHeart < _heartCount)
		{
			playerHearts[currentHeart].sprite = _filledHeart;
			currentHeart++;
		}
	}

	private void DiscountStamina()
	{
		if (_playerStamina.fillAmount == 1f && Input.GetKeyDown(KeyCode.Space))
		{
			_playerStamina.fillAmount -= 0.125f;
			StopCoroutine(addStaminaCoroutine);
		}
		else if (_playerStamina.fillAmount != 1f && Input.GetKeyDown(KeyCode.Space) &&
		         _playerStamina.fillAmount > 0.125f)
		{
			_playerStamina.fillAmount -= 0.125f;
			StopCoroutine(addStaminaCoroutine);
		}
	}

	private IEnumerator AddStaminaRoutine()
	{
		yield return new WaitForSeconds(0.5f);
		_playerStamina.fillAmount += Time.deltaTime * (0.1f * _recoveryStamina);
	}

	private void AddStamina()
	{
		if (_playerStamina.fillAmount < 1f)
		{
			_playerStamina.gameObject.SetActive(true);
			addStaminaCoroutine = StartCoroutine(AddStaminaRoutine());
		}
		else if (_playerStamina.fillAmount == 1f)
		{
			StopCoroutine(addStaminaCoroutine);
			_playerStamina.gameObject.SetActive(false);
		}
	}
}
