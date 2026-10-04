public class BossDieState : StateBase<BossContext>
{
	public BossDieState(BossContext context, StateMachine<BossContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		// 사망 애니메이션 재생
		// 아이템 드랍 or 게임 클리어
	}
}
