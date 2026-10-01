using UnityEngine;

public class PlayerContext : IContext
{
	public Animator animator;
	public PlayerInput input;
	public Rigidbody rigidbody;
	public PlayerStat stat;
	public Transform transform;
}
