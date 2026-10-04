public class MonsterDieState : StateBase<MonsterContext>
{
	public MonsterDieState(MonsterContext context, StateMachine<MonsterContext> stateMachine) : base(context,
		stateMachine)
	{
	}

	public override void Enter()
	{
		_ctx.animHandler.PlayDieAnim();

		// TODO
		// 아이템 드랍 실행
		// 몬스터를 풀로 반환
	}
}
