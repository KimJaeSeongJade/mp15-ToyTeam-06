using UnityEngine;

public interface ILockonable
{
	public GameObject GameObject { get; }

	public Transform GetLockonPosition();
}
