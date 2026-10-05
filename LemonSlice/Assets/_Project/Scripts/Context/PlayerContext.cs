using UnityEngine;

public class PlayerContext : IContext
{
	public PlayerDetection playerDetection;
	public PlayerAnimHandler animHandler;
	public PlayerInput input;
	public AttackHitBox hitBox;

	public bool isLockOn;
	public int lockOnIndex;
	public Rigidbody rigidbody;
	public PlayerStat stat;
	public Transform transform;
}
