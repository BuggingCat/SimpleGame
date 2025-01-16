using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerWallSlideState : PlayerState
{
    //�ڿ�����ǽʱ�л�Ϊ��ǽ״̬
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
            return; //���ڶ��Ǵ�wallSlide���룬����Space���������State������return����
        }

        if (xInput != 0 && player.facingDir != xInput)//��������Ϊ�˱�֤��û�нӴ������ʱ��Ҳ�����л���idleState��ʹ���Ƹ������
        {
            stateMachine.ChangeState(player.idleState);
        }
        if (player.IsGroundDetected())//�Ӵ������л�idleState
        {
            stateMachine.ChangeState(player.idleState);
        }

        if (yInput < 0)
            rb.velocity = new Vector2(0, rb.velocity.y);//��ҿ��Կ����Ƿ���ٻ�ǽ
        else
            rb.velocity = new Vector2(0, rb.velocity.y * .7f);//���û����ʱ��ǽ�ϻ���ʱ�ٶȼ���һ��
    }
}
