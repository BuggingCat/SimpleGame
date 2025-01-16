using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAirState : PlayerState
{
    public PlayerAirState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }
    public override void Update()
    {
        base.Update();

        if (player.IsGroundDetected())//触地时切换为默认状态
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (xInput != 0)//使角色能在空中时改变速度
        {
            player.setVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
        }
        if (player.IsWallDetected())//在空中碰墙时切换为滑墙状态
        {
            stateMachine.ChangeState(player.wallSlide);
        }
        
    }
}
