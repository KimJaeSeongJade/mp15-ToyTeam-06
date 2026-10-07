using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class TitleUIController : MonoBehaviour
{
	[SerializeField] private Button _startButton;
	[SerializeField] private Button _exitButton;
	[SerializeField] private Button _creditButton;
	[SerializeField] private Button _closeCreditButton;
	[SerializeField] private GameObject _creditPanel;

	private void Start() => OffCreditPanel();

	private void OnEnable() => BindButtonEvents();

	private void OnDisable() => UnBindButtonEvents();

	private void BindButtonEvents()
	{
		_startButton.onClick.AddListener(LoadMainGame);
		_creditButton.onClick.AddListener(OnCreditPanel);
		_closeCreditButton.onClick.AddListener(OffCreditPanel);
		_exitButton.onClick.AddListener(QuitGame);
	}

	private void UnBindButtonEvents()
	{
		_startButton.onClick.RemoveListener(LoadMainGame);
		_creditButton.onClick.RemoveListener(OnCreditPanel);
		_closeCreditButton.onClick.RemoveListener(OffCreditPanel);
		_exitButton.onClick.RemoveListener(QuitGame);
	}

	private void LoadMainGame()
	{
		SceneManager.LoadScene("MainGame");
	}

	private void OnCreditPanel()
	{
		_creditPanel.SetActive(true);
	}

	private void OffCreditPanel()
	{
		_creditPanel.SetActive(false);
	}

	private void QuitGame()
	{
		Application.Quit();
	}
}
