using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class PlayerPrimaryAttackState : PlayerState
{
    //p38 2.��ground����

    private int comboCounter;

    private float lastTimeAttacked;//������һ�ι�����ʱ��
    private float comboWindow = 2;//���Լ����ʱ��
    public PlayerPrimaryAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();

        xInput = 0;//�޸�������ת������

        if (comboCounter > 2 || Time.time > comboWindow + lastTimeAttacked)//������������2�ͼ��ʱ�����windowʱ�������һ����������
        {
            comboCounter = 0;
        }


        player.anim.SetInteger("comboCounter", comboCounter);//����animtor���comboCounter

        #region ѡ�񹥻�����
        float attackDir = player.facingDir;

        if (xInput != 0)
        {
            attackDir = xInput;
        }
        #endregion
        //ʹ���ܸı乥������
        player.setVelocity(player.attackMovement[comboCounter].x * attackDir, player.attackMovement[comboCounter].y);//����ɫ���ٶȣ��ý�ɫ�ڹ�������ʱ�ƶ�һ��

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
        }//1.�޸��ƶ�ʱ����ʱ������ƶ���BUG
        //2.�����˵�ʱ��ģ����Կ��Զ�һ��
        if (triggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

}
