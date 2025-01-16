using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadyBattleState : EnemyState
{
    private Transform player;//用于给Player定位，好判断怎么跟上他
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
        enemy.moveSpeed = enemy.battleStateMoveSpeed; // 提高速度

        player = GameObject.Find("Player").transform;//全局找Player位置
    }

    public override void Exit()
    {
        base.Exit();

        enemy.moveSpeed = defaultSpeed; // 回到原来的速度
    }

    public override void Update()
    {
        base.Update();
        //退出此状态的方式
        if (enemy.IsPlayerDetected())
        {
            stateTimer = enemy.battleTime;
            if (enemy.IsPlayerDetected().distance < enemy.attackDistance)//当距离小于攻击距离，变为攻击状态
            {
                if (CanAttack())
                    stateMachine.ChangeState(enemy.attackState);
                //stateMachine.ChangeState(enemy.deadState);
                // enemy.stats.killEntity();
               
            }
        }
        else//当没有看见player后，才会根据没有看到的时间来使其退出battle状态
        {
            if (stateTimer < 0 || Vector2.Distance(player.transform.position, enemy.transform.position) > 7)//根据距离来判断是否结束battle状态
            {
                stateMachine.ChangeState(enemy.idleState);
            }
        }


        //下面为移动方向设置
        if (player.position.x > enemy.transform.position.x)//在右，向右移动
        {
            moveDir = 1;
        }
        else if (player.position.x < enemy.transform.position.x)//在左，向左移动
        {
            moveDir = -1;
        }

        if (Vector2.Distance(player.transform.position, enemy.transform.position) > 1)
            enemy.setVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
        else
        {
            enemy.SetZeroVelocity();
        }//我自己设置了一个敌人接近一定距离就停下来的设置，防止出现敌人乱晃的情况
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
