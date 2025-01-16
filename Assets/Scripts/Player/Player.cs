using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // 场景管理
public class Player : Entity
{
    [Header("Attack Details")]
    public Vector2[] attackMovement;//每个攻击时获得的速度组
    
    public bool isBusy { get; private set; }//防止在攻击间隔中进入move

    [Header("Move Info")]
    public float moveSpeed = 12f;
    public float jumpForce;

    [Header("Dash Info")]
    [SerializeField] private float dashCooldown;
    private float dashUsageTimer;
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    #region States
    public PlayerStateMachine stateMachine { get; private set; }

    public PlayerIdleState idleState { get; private set; }

    public PlayerMoveState moveState { get; private set; }

    public PlayerJumpState jumpState { get; private set; }

    public PlayerAirState airState { get; private set; }

    public PlayerDashState dashState { get; private set; }

    public PlayerWallSlideState wallSlide { get; private set; }

    public PlayerWallJumpState wallJump { get; private set; }

    public PlayerPrimaryAttackState primaryAttack { get; private set; }

    public PlayerDeadState deadState { get; private set; }

    #endregion

    protected override void Awake()
    {
        base.Awake();

        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");//wallJump也是Jump动画
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        deadState = new PlayerDeadState(this, stateMachine, "Die");
        //this 就是 Player这个类本身
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        
        stateMachine.currentState.Update();

        if (myTrans.position.y < -11)
        {
            FallDamage();
            Die();
        }

        else if (myTrans.position.x > 285)
        {
            SceneManager.LoadScene("SuccessfulScene"); // 结算画面
        }
    }
    public IEnumerator BusyFor(float _seconds)//https://www.zhihu.com/tardis/bd/art/504607545?source_id=1001
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;

    }//p39 4.防止在攻击间隔中进入move,通过设置busy值，在使用某些状态时，使其为busy为true，抑制其进入其他state
     //IEnumertor本质就是将一个函数分块执行，只有满足某些条件才能执行下一段代码，此函数有StartCoroutine调用

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    //从当前状态拿到AnimationTrigger进行调用的函数

    public void CheckForDashInput()
    {

        dashUsageTimer -= Time.deltaTime;//给dash上冷却时间
        if (IsWallDetected())
        {
            return;
        }//修复在wallslide可以dash的BUG
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {

            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");//设置一个值，可以将dash的方向改为你想要的方向而不是你的朝向
            if (dashDir == 0)
            {
                dashDir = facingDir;//只有当玩家没有控制方向时才使用默认朝向
            }
            stateMachine.ChangeState(dashState);
        }

    }//将Dash切换设置成一个函数，使其在所以情况下都能使用

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }

}
