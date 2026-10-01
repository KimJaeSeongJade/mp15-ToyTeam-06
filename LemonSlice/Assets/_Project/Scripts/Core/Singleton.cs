using UnityEngine;

public abstract class SingletonBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
	private static T _instance;

	[SerializeField] private bool _isDestroyedOnManager;
	public static T Instance
	{
		get
		{
			if (_instance == null)
			{
				_instance = FindObjectOfType<T>();
			}

			return _instance;
		}
	}

	protected void SetSingleton()
	{
		if (_instance != null && _instance != this)
		{
			Destroy(gameObject);
		}
		else
		{
			_instance = GetComponent<T>();
			if (_isDestroyedOnManager)
			{
				DontDestroyOnLoad(_instance.gameObject);

			}
		}
	}
}
