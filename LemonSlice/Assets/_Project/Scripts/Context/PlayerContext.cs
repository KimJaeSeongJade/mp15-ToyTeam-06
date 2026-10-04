using UnityEngine;

public class PlayerContext : IContext
{
	public MonsterDetection monsterDetection;
	public PlayerAnimHandler animHandler;
	public PlayerInput input;

	public bool isLockOn;
	public int lockOnIndex;
	public Rigidbody rigidbody;
	public PlayerStat stat;
	public Transform transform;
}
