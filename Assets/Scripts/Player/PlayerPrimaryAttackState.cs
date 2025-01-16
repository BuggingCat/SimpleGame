using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerPrimaryAttackState : PlayerState
{
    //p38 2.从ground进入

    private int comboCounter;

    private float lastTimeAttacked;//距离上一次攻击的时间
    private float comboWindow = 2;//可以间隔的时间
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        xInput = 0;//修复攻击乱转的问题

        if (comboCounter > 2 || Time.time > comboWindow + lastTimeAttacked)//当计数器超过2和间隔时间大于window时，进入第一个攻击动作
        {
            comboCounter = 0;
        }


        player.anim.SetInteger("comboCounter", comboCounter);//设置animtor里的comboCounter

        #region 选择攻击方向
        float attackDir = player.facingDir;

        if (xInput != 0)
        {
            attackDir = xInput;
        }
        #endregion
        //使其能改变攻击方向
        player.setVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);//给角色初速度，让角色在攻击触发时移动一点

        stateTimer = .1f;
    }

    public override void Exit()
    {
        base.Exit();
        player.StartCoroutine("BusyFor", .15f);
        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            player.SetZeroVelocity();
        }//1.修改移动时攻击时后可以移动的BUG
        //2.但给了点时间模拟惯性可以动一点
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}
