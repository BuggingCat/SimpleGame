using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    [Header("Knockback info")]
    [SerializeField] protected Vector2 knockbackDirection;//被击打后的速度信息
    [SerializeField] protected float knockbackDuration;//被击打的时间
    protected bool isKnocked;//此值通过卡住SetVelocity函数的方式用来阻止当一个角色被攻击时，会乱动的情况

    [Header("Collision Info")]
    public Transform attackCheck;
    public float attackCheckRadius;
    [SerializeField] protected Transform groundCheck;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected Transform wallCheck;
    [SerializeField] protected float wallCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;


    #region 定义Unity组件
    public Animator anim { get; private set; }//这样才能配合着拿到自己身上的animator的控制权
    public Rigidbody2D rb { get; private set; }//配合拿到身上的Rigidbody2D组件控制权
    public EntityFX fx { get; private set; }//拿到EntityFX
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
        anim = GetComponentInChildren<Animator>();//拿到自己子组件身上的animator的控制权
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
        fx.StartCoroutine("FlashFX");//IEnumertor本质就是将一个函数分块执行，只有满足某些条件才能执行下一段代码，此函数有StartCoroutine调用
                                     //https://www.zhihu.com/tardis/bd/art/504607545?source_id=1001
        StartCoroutine("HitKnockback");//调用被击打后产生后退效果的函数

        Debug.Log(gameObject.name + " was damaged!");
    }

    public virtual void FallDamage()
    {
        Debug.Log(gameObject.name + " falling down and died!");
    }

    protected virtual IEnumerator HitKnockback()
    {
        isKnocked = true;//此值通过卡住SetVelocity函数的方式用来阻止当一个角色被攻击时，会乱动的情况
        rb.velocity = new Vector2(knockbackDirection.x * -facingDir, knockbackDirection.y);
        yield return new WaitForSeconds(knockbackDuration);
        isKnocked = false;
    }
    //被击打后产生后退效果的函数

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
        facingRight = !facingRight; // 切换朝向
        transform.Rotate(0, 180, 0); // 相当于左右翻转

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
