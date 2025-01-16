using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlimeStunnedState : EnemyState
{
    private EnemySlime enemy;
    public SlimeStunnedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemySlime _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
        enemy.fx.InvokeRepeating("RedColorBlink", 0, .1f);
        stateTimer = enemy.stunnedDuration;//stunned����ʱ��
        rb.velocity = new Vector2(-enemy.facingDir * enemy.stunnedDirection.x, enemy.stunnedDirection.y);//stunned�ı����ٶȣ�����SetVelocity��FlipCheck�����������rb.velocity�����ٶ�

    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelRedBlink", 0); 
    }

    public override void Update()
    {
        base.Update();

        if (rb.velocity.y < .1f && enemy.IsGroundDetected())
        {
            enemy.fx.Invoke("CancelColorChange", 0);
            enemy.anim.SetTrigger("StunnedFold");
        }

        if (stateTimer < 0)
        {
            stateMachine.ChangeState(enemy.idleState);
        }
    }
}
