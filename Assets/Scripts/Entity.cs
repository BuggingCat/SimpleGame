using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;//���������ٶ���Ϣ
    [SerializeField] protected float knockbackDuration;//�������ʱ��
    protected bool isKnocked;//��ֵͨ����סSetVelocity�����ķ�ʽ������ֹ��һ����ɫ������ʱ�����Ҷ������

    [Header("Collision Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;


    #region ����Unity���
    public Animator anim { get; private set; }//��������������õ��Լ����ϵ�animator�Ŀ���Ȩ
    public Rigidbody2D rb { get; private set; }//����õ����ϵ�Rigidbody2D�������Ȩ
    public EntityFX fx { get; private set; }//�õ�EntityFX
    public CharacterStats stats { get; private set; }
    public CapsuleCollider2D cd { get; private set; }

    public Transform myTrans { get; private set; }
    #endregion

    public int facingDir { get; private set; } = 1;
    protected bool facingRight = true;

    public System.Action onflipped;

    protected virtual void Awake()
    {
        fx = GetComponentInChildren<EntityFX>();
        anim = GetComponentInChildren<Animator>();//�õ��Լ���������ϵ�animator�Ŀ���Ȩ
        rb = GetComponent<Rigidbody2D>();
        stats = GetComponent<CharacterStats>();
        cd = GetComponent<CapsuleCollider2D>();
        myTrans = GetComponent<Transform>();
    }

    protected virtual void Start()
    {
        anim = GetComponentInChildren<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    protected virtual void Update()
    {

    }

    public virtual void Damage()
    {
        fx.StartCoroutine("FlashFX");//IEnumertor���ʾ��ǽ�һ�������ֿ�ִ�У�ֻ������ĳЩ��������ִ����һ�δ��룬�˺�����StartCoroutine����
                                     //https://www.zhihu.com/tardis/bd/art/504607545?source_id=1001
        StartCoroutine("HitKnockback");//���ñ�������������Ч���ĺ���

        Debug.Log(gameObject.name + " was damaged!");
    }

    public virtual void FallDamage()
    {
        Debug.Log(gameObject.name + " falling down and died!");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;//��ֵͨ����סSetVelocity�����ķ�ʽ������ֹ��һ����ɫ������ʱ�����Ҷ������
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }
    //��������������Ч���ĺ���

    public virtual void Die()
    {

    }

    #region Velocity
    public void SetZeroVelocity()
    {
        rb.velocity = new Vector2(0, 0);
    }
    public void setVelocity(float _xvelocity, float _yvelocity)
    {
        if (isKnocked)
            return;

        rb.velocity = new Vector2(_xvelocity, _yvelocity);
        FlipController(_xvelocity);
    }
    #endregion

    #region Collision
    public virtual bool IsGroundDetected()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, groundCheckDistance, whatIsGround);
    }

    public virtual bool IsWallDetected()
    {
        return Physics2D.Raycast(wallCheck.position, Vector2.right * facingDir, wallCheckDistance, whatIsGround);
    }

    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheck.position, new Vector3(groundCheck.position.x, groundCheck.position.y - groundCheckDistance));
        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance * facingDir, wallCheck.position.y));
        Gizmos.DrawWireSphere(attackCheck.position, attackCheckRadius);
    }
    #endregion

    #region Flip
    public virtual void Flip()
    {
        facingDir *= -1;
        facingRight = !facingRight; // �л�����
        transform.Rotate(0, 180, 0); // �൱�����ҷ�ת

        if(onflipped != null)
            onflipped();
    }

    public virtual void FlipController(float _x)
    {
        if (_x > 0 && !facingRight)
            Flip();
        else if (_x < 0 && facingRight)
            Flip();
    }

    public virtual void SetupDefaultfacingDir(int _direction)
    {
        facingDir = _direction;
        if (facingDir == -1)
            facingRight = false;
    }
    #endregion
}
