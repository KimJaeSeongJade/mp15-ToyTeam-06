using UnityEngine;

public class BossContext : IContext
{
	public BossAnimationHandler animHandler;
	public int attackIndex;
	public PlayerDetection playerDetection;
	public BossStat stat;
	public Transform transform;
}
