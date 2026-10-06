using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUIController : MonoBehaviour
{
	[SerializeField] private GameObject _pausePanel;
	[SerializeField] private Button _countinueButton;
	[SerializeField] private Button _quitButton;
	[SerializeField] private Button _titleButton;

	private void Start() => BindButtonEvents();

	private void OnEnable() => BindButtonEvents();

	private void OnDisable() =>  UnBindButtonEvents();

	private void BindButtonEvents()
	{
		_countinueButton.onClick.AddListener(CountinueGame);
		_quitButton.onClick.AddListener(QuitGame);
		_titleButton.onClick.AddListener(TitleGame);
	}

	private void UnBindButtonEvents()
	{
		_countinueButton.onClick.RemoveListener(CountinueGame);
		_quitButton.onClick.RemoveListener(QuitGame);
		_titleButton.onClick.RemoveListener(TitleGame);
	}
	private void CountinueGame()
	{
		GameManager.Instance.ChangeState(GameState.Playing);
	}

	private void QuitGame()
	{

	}

	private void TitleGame()
	{
		GameManager.Instance.ChangeState(GameState.Title);
	}

	public void OnPausePanel()
	{
		_pausePanel.SetActive(true);
	}
	public void OffPausePanel()
	{
		_pausePanel.SetActive(false);
	}

}
