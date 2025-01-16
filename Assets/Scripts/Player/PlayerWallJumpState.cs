using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWallJumpState : PlayerState
{
    //wallJUmp��Ȼ��wallSlide����
    public PlayerWallJumpState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {

    }

    public override void Enter()
    {
        base.Enter();

        stateTimer = 1f;//��ʱ������һ��ʱ������idle
        player.setVelocity(5 * -player.facingDir, player.jumpForce);//�����򷴷�����
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (player.IsGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);//����wallslideʱ����ʱ��ȴ���ʱ�䳤���������ӳ�
        }
    }
}
