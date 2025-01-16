using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWallSlideState : PlayerState
{
    //在空中碰墙时切换为滑墙状态
    public PlayerWallSlideState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
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

        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJump);
            return; //由于都是从wallSlide进入，光摁Space会进入其他State，故由return控制
        }

        if (xInput != 0 && player.facingDir != xInput)//这样做是为了保证在没有接触地面的时候也可以切换回idleState，使控制更加灵活
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (player.IsGroundDetected())//接触地面切回idleState
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);//玩家可以控制是否加速滑墙
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);//玩家没控制时在墙上滑行时速度减慢一点
    }
}
