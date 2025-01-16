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

        if (player.IsGroundDetected())//����ʱ�л�ΪĬ��״̬
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (xInput != 0)//ʹ��ɫ���ڿ���ʱ�ı��ٶ�
        {
            player.setVelocity(player.moveSpeed * 0.8f * xInput, rb.velocity.y);
        }
        if (player.IsWallDetected())//�ڿ�����ǽʱ�л�Ϊ��ǽ״̬
        {
            stateMachine.ChangeState(player.wallSlide);
        }
        
    }
}
