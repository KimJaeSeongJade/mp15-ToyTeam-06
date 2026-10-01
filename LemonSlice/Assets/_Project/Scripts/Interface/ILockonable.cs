using UnityEngine;

public interface ILockonable
{
	public GameObject GameObject { get; }

	public void SetLockonPosition(Transform lockonPosition);
}
