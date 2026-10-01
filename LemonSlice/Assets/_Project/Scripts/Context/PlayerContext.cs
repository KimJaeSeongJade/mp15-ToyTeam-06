using UnityEngine;

public class PlayerContext : IContext
{
	public Animator animator;
	public PlayerInput input;
	public Rigidbody rigidbody;
	public PlayerBase stat;
	public Transform transform;
}
