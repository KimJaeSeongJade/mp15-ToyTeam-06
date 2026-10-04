using UnityEngine;

public class GameObjectBuilder
{
	private GameObject _target;

	public GameObjectBuilder(GameObject target)
	{
		_target = target;
	}

	public GameObjectBuilder SetPosition(Vector3 position)
	{
		_target.transform.position = position;
		return this;
	}

	public GameObjectBuilder SetRotation(Quaternion rotation)
	{
		_target.transform.rotation = rotation;
		return this;
	}

	public GameObject Build()
	{
		_target.SetActive(true);
		return _target;
	}
}
