using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// 敌人AI控制器
/// 继承 BaseCharacter
/// 负责：巡逻、追击玩家、攻击玩家等AI逻辑
/// </summary>
public class EnemyController : BaseCharacter
{
    [Header("敌人AI设置")]
    [Header("追击的玩家目标")]
    public Transform player;       // 玩家目标
    [Header("追击范围")]
    public float chaseRange = 10f; // 追击范围
    [Header("攻击伤害")]
    public int attackDamage = 10;  // 攻击伤害
    [Header("攻击范围")]
    public float attackRange = 2f; // 攻击范围

    [Header("攻击冷却")]
    public float attackCooldown = 1.5f; // 攻击间隔

    private NavMeshAgent agent;    // 导航代理组件
    private float distanceToPlayer;// 到玩家的距离（与玩家之间的距离）

    private float attackTimer;     // 冷却计时器
    private bool canAttack = true; // 是否可以攻击

    /// <summary>
    /// 初始化敌人组件
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        agent = GetComponent<NavMeshAgent>(); // 获取导航组件
    }

    /// <summary>
    /// 初始化敌人状态
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// 每帧执行AI逻辑
    /// </summary>
    void Update()
    {
        if (currentHp <= 0) return; // 死亡后停止AI

        CanAttack();

        distanceToPlayer = Vector3.Distance(transform.position, player.position);
        AIBehaviour(); // 执行AI行为
    }

    /// <summary>
    /// 敌人核心AI行为
    /// </summary>
    void AIBehaviour()
    {
        // 玩家死亡 停止所有AI行为
        if (PlayerController.instance != null && PlayerController.instance.enabled == false)
        {
            Idle(); // 回到待机
            return;
        }

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer(); // 进入攻击范围则攻击
        }
        else if (distanceToPlayer <= chaseRange)
        {
            ChasePlayer(); // 进入追击范围则追击
        }
        else
        {
            Idle(); // 超出范围则待机
        }
    }


    /// <summary>
    /// 追击玩家
    /// </summary>
    void ChasePlayer()
    {
        agent.SetDestination(player.position); // 设置导航目标为玩家
        anim.SetBool("IsRun", true);
        anim.SetBool("IsIdle", false);
    }

    /// <summary>
    /// 待机状态
    /// </summary>
    void Idle()
    {
        agent.ResetPath(); // 停止导航
        anim.SetBool("IsIdle", true);
        anim.SetBool("IsRun", false);
    }

    /// <summary>
    /// 攻击玩家
    /// </summary>
    void AttackPlayer()
    {
        // 冷却中不能攻击
        if (!canAttack) return;

        agent.ResetPath(); // 停止移动
        transform.LookAt(player); // 看向玩家

        anim.SetTrigger("IsAttack"); // 触发攻击动画

        anim.SetBool("IsRun", false);
        anim.SetBool("IsIdle", false);

        PlayerController.instance.TakeDamage(attackDamage);

        // 攻击后进入冷却
        canAttack = false;
    }
    /// <summary>
    /// 攻击冷却计时
    /// </summary>
    void CanAttack()
    {
        // 攻击冷却计时
        if (!canAttack)
        {
            attackTimer += Time.deltaTime;
            if (attackTimer >= attackCooldown)
            {
                canAttack = true;
                attackTimer = 0;
            }
        }
    }

    /// <summary>
    /// 敌人受伤重写
    /// </summary>
    /// <param name="damage">伤害值</param>
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (currentHp > 0)
        {
            AudioManager.instance.EnemyClip();//播放怪物受伤音效
            currentHp -= damage;//怪物扣血

        }
        if (currentHp <= 0)
        {
            Die();   //执行怪物死亡
        }
    }

    /// <summary>
    /// 敌人死亡重写
    /// </summary>
    protected override void Die()
    {
        base.Die();
        anim.SetBool("IsDead", true);
        agent.enabled = false;
        enabled = false;
        Destroy(gameObject, 3f);
    }
}