using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    //由Ground进入
    public PlayerDashState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;


    }

    public override void Exit()
    {
        base.Exit();
        player.setVelocity(0, rb.velocity.y);//当退出时使速度为0防止动作结束后速度不变导致的持续移动

    }

    public override void Update()
    {
        base.Update();
        if (!player.IsGroundDetected() && player.IsWallDetected())//修复无法在空中dash直接进入wallSlide,修复在wallslide可以dash
                                                                  //因为一旦在wallSlide里dash，就会直接进wallSlide，当然还有更简单的方法
        {
            stateMachine.ChangeState(player.wallSlide);
        }
        player.setVelocity(player.dashSpeed * player.dashDir, 0);//这个写在Update里，防止在x轴方向减速了，同时Y轴写成0，以防止dash还没有完成就从空中掉下去了
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
