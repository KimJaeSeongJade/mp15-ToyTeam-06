using UnityEngine;

public class PlayerContext : IContext
{
	public PlayerDetection playerDetection;
	public PlayerAnimHandler animHandler;
	public PlayerInput input;
	public AttackHitBox hitBox;

	public Rigidbody rigidbody;
	public PlayerStat stat;
	public Transform transform;
	public Vector3 hitDirection;
	public Transform paladin;

	public bool isLockOn;
	public LockOnController lockOnController;
}
