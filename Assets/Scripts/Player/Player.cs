using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; // ��������
public class Player : Entity
{
    [Header("Attack Details")]
    public Vector2[] attackMovement;//ÿ������ʱ��õ��ٶ���
    
    public bool isBusy { get; private set; }//��ֹ�ڹ�������н���move

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
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");//wallJumpҲ��Jump����
        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        deadState = new PlayerDeadState(this, stateMachine, "Die");
        //this ���� Player����౾��
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
            SceneManager.LoadScene("SuccessfulScene"); // ���㻭��
        }
    }
    public IEnumerator BusyFor(float _seconds)//https://www.zhihu.com/tardis/bd/art/504607545?source_id=1001
    {
        isBusy = true;
        yield return new WaitForSeconds(_seconds);
        isBusy = false;

    }//p39 4.��ֹ�ڹ�������н���move,ͨ������busyֵ����ʹ��ĳЩ״̬ʱ��ʹ��ΪbusyΪtrue���������������state
     //IEnumertor���ʾ��ǽ�һ�������ֿ�ִ�У�ֻ������ĳЩ��������ִ����һ�δ��룬�˺�����StartCoroutine����

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();
    //�ӵ�ǰ״̬�õ�AnimationTrigger���е��õĺ���

    public void CheckForDashInput()
    {

        dashUsageTimer -= Time.deltaTime;//��dash����ȴʱ��
        if (IsWallDetected())
        {
            return;
        }//�޸���wallslide����dash��BUG
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUsageTimer < 0)
        {

            dashUsageTimer = dashCooldown;
            dashDir = Input.GetAxisRaw("Horizontal");//����һ��ֵ�����Խ�dash�ķ����Ϊ����Ҫ�ķ����������ĳ���
            if (dashDir == 0)
            {
                dashDir = facingDir;//ֻ�е����û�п��Ʒ���ʱ��ʹ��Ĭ�ϳ���
            }
            stateMachine.ChangeState(dashState);
        }

    }//��Dash�л����ó�һ��������ʹ������������¶���ʹ��

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }

}
