using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    //��Ground����
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
        player.setVelocity(0, rb.velocity.y);//���˳�ʱʹ�ٶ�Ϊ0��ֹ�����������ٶȲ��䵼�µĳ����ƶ�

    }

    public override void Update()
    {
        base.Update();
        if (!player.IsGroundDetected() && player.IsWallDetected())//�޸��޷��ڿ���dashֱ�ӽ���wallSlide,�޸���wallslide����dash
                                                                  //��Ϊһ����wallSlide��dash���ͻ�ֱ�ӽ�wallSlide����Ȼ���и��򵥵ķ���
        {
            stateMachine.ChangeState(player.wallSlide);
        }
        player.setVelocity(player.dashSpeed * player.dashDir, 0);//���д��Update���ֹ��x�᷽������ˣ�ͬʱY��д��0���Է�ֹdash��û����ɾʹӿ��е���ȥ��
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }
}
