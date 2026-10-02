using UnityEngine;

public class PlayerContext : IContext
{
	public PlayerAnimationHandler animHandler;
	public PlayerInput input;
	public Rigidbody rigidbody;
	public PlayerStat stat;
	public Transform transform;
}
