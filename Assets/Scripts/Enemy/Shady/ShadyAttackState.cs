using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyAttackState : EnemyState
{
    private EnemyShady enemy;
    public ShadyAttackState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyShady _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();

        enemy.lastTimeAttacked = Time.time;//��¼���������ʱ���Ա����ж��Ƿ��ܹ����빥��״̬
    }

    public override void Update()
    {
        base.Update();

        enemy.SetZeroVelocity();//ʹ�ٶ�Ϊ0


        if (triggerCalled)
        {
            stateMachine.ChangeState(enemy.battleState);
        }//���˳��������ڶ������֮��
    }
}
