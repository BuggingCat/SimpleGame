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
        enemy.fx.InvokeRepeating("RedColorBlink", 0, .1f);//�� time ������ methodName ������Ȼ��ÿ repeatRate �����һ�Ρ�
                                                          //https://docs.unity3d.com/cn/current/ScriptReference/MonoBehaviour.InvokeRepeating.html
        stateTimer = enemy.stunnedDuration;//stunned����ʱ��
        rb.velocity = new Vector2(-enemy.facingDir * enemy.stunnedDirection.x, enemy.stunnedDirection.y);//stunned�ı����ٶȣ�����SetVelocity��FlipCheck�����������rb.velocity�����ٶ�

    }

    public override void Exit()
    {
        base.Exit();
        enemy.fx.Invoke("CancelRedBlink", 0); //�� time ������ methodName ������
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
