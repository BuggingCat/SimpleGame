using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
[RequireComponent(typeof(EnemyStats))]
[RequireComponent(typeof(EntityFX))]
public class Enemy : Entity
{
    [SerializeField] protected LayerMask whatIsPlayer;

    [Header("Stun Info")]
    public float stunnedDuration;//stunned持续时间
    public Vector2 stunnedDirection;//stunned改变后的速度
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;//多久能从battle状态中退出来
    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldown;//攻击冷却
    [HideInInspector] public float lastTimeAttacked;//最后一次攻击的时间


    public EnemyStateMachine stateMachine;
    public string lastAnimBoolName { get; private set; }
    

    protected override void Awake()
    {
        base.Awake();
        stateMachine = new EnemyStateMachine();

    }
    protected override void Start()
    {
        base.Start();

    }


    protected override void Update()
    {
        base.Update();

        stateMachine.currentState.Update();

        //Debug.Log(IsPlayerDetected().collider.gameObject.name + "I see");//这串代码会报错，可能使版本的物体，因为在没有找到Player的时候物体是空的，NULL，你想让他在控制台上显示就报错了
    }

    public virtual void AssignLastAnimName(string _animBoolName)
    {
        lastAnimBoolName = _animBoolName;
    }

    public virtual void OpenCounterAttackWindow()
    {
        canBeStunned = true;
        counterImage.SetActive(true);
    }

    public virtual void CloseCounterAttackWindow()
    {
        canBeStunned = false;
        counterImage.SetActive(false);
    }

    public virtual void SpecialAttackTrigger()
    {

    }

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();//动画完成时调用的函数，与Player相同
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 7, whatIsPlayer);//用于从射线投射获取信息的结构。
                                                                                                                                        //该函数的返回值可以变，可以只返回bool，也可以是碰到的结构

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;//把线改成黄色
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));//用来判别是否进入attackState的线
    }
}
