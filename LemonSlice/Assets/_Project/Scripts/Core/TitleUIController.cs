using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUIController : MonoBehaviour
{
	[SerializeField] private Button _startButton;
	[SerializeField] private Button _exitButton;
	[SerializeField] private Button _creditButton;

	private void OnEnable() => BindButtonEvents();

	private void OnDisable() => UnBindButtonEvents();

	private void BindButtonEvents()
	{
		_startButton.onClick.AddListener(LoadMainGame);
	}

	private void UnBindButtonEvents()
	{
		_startButton.onClick.RemoveListener(LoadMainGame);
	}

	private void LoadMainGame()
	{
		SceneManager.LoadScene("MainGame");
	}

	private void OnCreditPanel()
	{

	}

	private void QuitGame()
	{

	}
}
