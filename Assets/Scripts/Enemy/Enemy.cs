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
    public float stunnedDuration;//stunned����ʱ��
    public Vector2 stunnedDirection;//stunned�ı����ٶ�
    protected bool canBeStunned;
    [SerializeField] protected GameObject counterImage;

    [Header("Move Info")]
    public float moveSpeed;
    public float idleTime;
    public float battleTime;//����ܴ�battle״̬���˳���
    [Header("Attack Info")]
    public float attackDistance;
    public float attackCooldown;//������ȴ
    [HideInInspector] public float lastTimeAttacked;//���һ�ι�����ʱ��


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

        //Debug.Log(IsPlayerDetected().collider.gameObject.name + "I see");//�⴮����ᱨ������ʹ�汾�����壬��Ϊ��û���ҵ�Player��ʱ�������ǿյģ�NULL�����������ڿ���̨����ʾ�ͱ�����
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

    public virtual void AnimationFinishTrigger() => stateMachine.currentState.AnimationFinishTrigger();//�������ʱ���õĺ�������Player��ͬ
    public virtual RaycastHit2D IsPlayerDetected() => Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, 7, whatIsPlayer);//���ڴ�����Ͷ���ȡ��Ϣ�Ľṹ��
                                                                                                                                        //�ú����ķ���ֵ���Ա䣬����ֻ����bool��Ҳ�����������Ľṹ

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();

        Gizmos.color = Color.yellow;//���߸ĳɻ�ɫ
        Gizmos.DrawLine(transform.position, new Vector3(transform.position.x + attackDistance * facingDir, transform.position.y));//�����б��Ƿ����attackState����
    }
}
