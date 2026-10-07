using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverUIController : MonoBehaviour
{
	[SerializeField] private Button _restartButton;
	[SerializeField] private Button _backToTitleButton;

	private void Start()
	{
		HidePanel();
	}

	private void OnEnable() => BindButtonEvents();

	private void OnDisable() => UnBindButtonEvents();

	private void BindButtonEvents()
	{
		_restartButton.onClick.AddListener(LoadMain);
		_backToTitleButton.onClick.AddListener(LoadTitle);
	}

	private void UnBindButtonEvents()
	{
		_restartButton.onClick.RemoveListener(LoadMain);
		_backToTitleButton.onClick.RemoveListener(LoadTitle);
	}

	private void HidePanel()
	{
		gameObject.SetActive(false);
	}

	// TODO : ShowPanel은 게임 오버가 되었을 때 밖에서 호출해주어야 합니다

	private void LoadTitle()
	{
		GameFlowManager.Instance.IsPlayerDead = false;
		GameManager.Instance.ChangeState(GameState.Title);
		SceneManager.LoadScene("Title");
	}

	private void LoadMain()
	{
		GameFlowManager.Instance.IsPlayerDead = false;
		GameManager.Instance.ChangeState(GameState.Playing);
		gameObject.SetActive(false);
		SceneManager.LoadScene("MainGame");
	}
}
