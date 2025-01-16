using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyBattleState : EnemyState
{
    private Transform player;//���ڸ�Player��λ�����ж���ô������
    private EnemyShady enemy;
    private int moveDir;

    private float defaultSpeed;
    public ShadyBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, EnemyShady _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

        defaultSpeed = enemy.moveSpeed;
        enemy.moveSpeed = enemy.battleStateMoveSpeed; // ����ٶ�

        player = GameObject.Find("Player").transform;//ȫ����Playerλ��
    }

    public override void Exit()
    {
        base.Exit();

        enemy.moveSpeed = defaultSpeed; // �ص�ԭ�����ٶ�
    }

    public override void Update()
    {
        base.Update();
        //�˳���״̬�ķ�ʽ
        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)//������С�ڹ������룬��Ϊ����״̬
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
                //stateMachine.ChangeState(enemy.deadState);
                // enemy.stats.killEntity();
               
            }
        }
        else//��û�п���player�󣬲Ż����û�п�����ʱ����ʹ���˳�battle״̬
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)//���ݾ������ж��Ƿ����battle״̬
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }


        //����Ϊ�ƶ���������
        if (player.position.x > enemy.transform.position.x)//���ң������ƶ�
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x)//���������ƶ�
        {
            moveDir = -1;
        }

        if (Vector2.Distance(player.transform.position, enemy.transform.position) > 1)
            enemy.setVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
        else
        {
            enemy.SetZeroVelocity();
        }//���Լ�������һ�����˽ӽ�һ�������ͣ���������ã���ֹ���ֵ����һε����
    }

    private bool CanAttack()
    {
        if (Time.time > enemy.lastTimeAttacked + enemy.attackCooldown)
        {
            enemy.lastTimeAttacked = Time.time;
            return true;
        }

        return false;
    }


}
