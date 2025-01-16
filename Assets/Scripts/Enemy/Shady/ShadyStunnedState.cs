using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyStunnedState : EnemyState
{
    private EnemyShady enemy;
    public ShadyStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyShady _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }
    public override void Enter()
    {
        base.Enter();
        enemy.fx.InvokeRepeating("RedColorBlink", 0, .1f);//在 time 秒后调用 methodName 方法，然后每 repeatRate 秒调用一次。
                                                          //https://docs.unity3d.com/cn/current/ScriptReference/MonoBehaviour.InvokeRepeating.html
        stateTimer = enemy.stunnedDuration;//stunned持续时间
        rb.velocity = new Vector2(-enemy.facingDir * enemy.stunnedDirection.x, enemy.stunnedDirection.y);//stunned改变后的速度，由于SetVelocity有FlipCheck，所有这个用rb.velocity设置速度

    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelRedBlink", 0); //在 time 秒后调用 methodName 方法。
        //https://docs.unity3d.com/cn/current/ScriptReference/MonoBehaviour.Invoke.html
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
