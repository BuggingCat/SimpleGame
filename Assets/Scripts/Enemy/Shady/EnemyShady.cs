using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShady : Enemy
{

    [Header("Shady Specifics")]
    public float battleStateMoveSpeed;
    [SerializeField] private GameObject explosivePreFab;
    [SerializeField] private float growSpeed;
    [SerializeField] private float maxSize;

    #region State
    public ShadyIdleState idleState { get; private set; }
    public ShadyMoveState moveState { get; private set; }
    public ShadyBattleState battleState { get; private set; }

    public ShadyStunnedState stunnedState { get; private set; }

    public ShadyAttackState attackState { get; private set; }
    public ShadyDeadState deadState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();

        idleState = new ShadyIdleState(this, stateMachine, "Idle", this);
        moveState = new ShadyMoveState(this, stateMachine, "Move", this);
        battleState = new ShadyBattleState(this, stateMachine, "MoveFast", this); // Shady独有的状态
        stunnedState = new ShadyStunnedState(this, stateMachine, "Stunned", this);
        attackState = new ShadyAttackState(this, stateMachine, "Attack", this);
        deadState = new ShadyDeadState(this, stateMachine, "Dead", this);
    }

    protected override void Start()
    {
        base.Start();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }

    public override void Die()
    {
        base.Die();

        stateMachine.ChangeState(deadState);
    }

    public override void SpecialAttackTrigger()
    {
        GameObject newExplosive = Instantiate(explosivePreFab, attackCheck.position, Quaternion.identity);
        newExplosive.GetComponent<ExplosiveController>().SetupExplosion(stats, growSpeed, maxSize, attackCheckRadius);

        cd.enabled = false;
        rb.gravityScale = 0; //  不会在地面上折腾了
    }

    public void SelfDestroy()
    {
        Destroy(gameObject);
    }
}
