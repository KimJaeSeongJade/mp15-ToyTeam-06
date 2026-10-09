using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseUIController : MonoBehaviour
{
	[SerializeField] private Button _continueButton;
	[SerializeField] private Button _quitButton;
	[SerializeField] private Button _titleButton;

	private void Start(){}

	private void OnEnable() => BindButtonEvents();

	private void OnDisable() =>  UnBindButtonEvents();

	private void BindButtonEvents()
	{
		_continueButton.onClick.AddListener(CountinueGame);
		_quitButton.onClick.AddListener(QuitGame);
		_titleButton.onClick.AddListener(TitleGame);
	}

	private void UnBindButtonEvents()
	{
		_continueButton.onClick.RemoveListener(CountinueGame);
		_quitButton.onClick.RemoveListener(QuitGame);
		_titleButton.onClick.RemoveListener(TitleGame);
	}
	private void CountinueGame()
	{
		if (GameManager.Instance.isTutorial)
		{
			GameManager.Instance.ChangeState(GameState.Tutorial);
		}
		else
		{
			GameManager.Instance.ChangeState(GameState.PlayMain);
		}

		gameObject.SetActive(false);
	}

	private void QuitGame()
	{
		Application.Quit();
	}

	private void TitleGame()
	{
		SceneManager.LoadScene("Title");
	}


}
