using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 场景管理

public class PlayerDeadState : PlayerState
{
    public PlayerDeadState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void AnimationFinishTrigger()
    {
        base.AnimationFinishTrigger();
    }

    public override void Enter()
    {
        base.Enter();

        SceneManager.LoadScene("DeadScene"); // 跳转到死亡画面
        
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();
        
    }
    public override void Exit()
    {
        base.Exit();

    }
}
