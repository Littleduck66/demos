using UnityEngine;

/// <summary>
/// 玩家控制器
/// 继承 BaseCharacter
/// 负责：玩家移动、攻击、拾取物品、打开背包等交互逻辑
/// </summary>
public class PlayerController : BaseCharacter
{
    [Header("玩家移动设置")]
    [Header("玩家移动速度")]
    public float moveSpeed = 5f; // 移动速度
    [Header("玩家跳跃高度")]
    public float jumpHeight = 2f; //跳跃高度
    [Header("重力大小")]
    public float gravity = -19.6f;// 重力相关
    private Vector3 velocity;

    [Header("生成 的预制体箭矢")]
    public GameObject Arrow; //生成 的预制体箭矢
    [Header("箭矢生成位置")]
    public Transform arrowTransform; //生成 的预制体箭矢位置
                                     
    private bool isDead = false;     // 玩家是否死亡
   

    private CharacterController cc; // 角色控制器组件
    private bool isGrounded; // 地面检测
    public static PlayerController instance;

    /// <summary>
    /// 初始化玩家组件
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        instance = this;
        cc = GetComponent<CharacterController>(); // 获取角色控制器
    }

    /// <summary>
    /// 初始化玩家状态
    /// </summary>
    protected override void Start()
    {
        base.Start();
    }

    /// <summary>
    /// 每帧检测玩家输入
    /// </summary>
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            TakeDamage(5);
        }

        // 先检测是否在地面
        isGrounded = cc.isGrounded;

        if (isDead)
        {
            return; // 死亡后：直接停止所有逻辑
        }
        else
        {

            Move();             // 处理移动
            Jump();             // 处理跳跃
            ApplyGravityAndMove(); // 统一应用移动 + 重力 + 跳跃（关键修复）
            CheckAttackInput(); // 检测攻击输入
            //CheckBagInput();    // 检测打开背包输入
        }
    }

    /// <summary>
    /// 玩家移动逻辑
    /// </summary>
    void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal"); // 水平输入（A/D/左右箭头）
        float vertical = Input.GetAxisRaw("Vertical");     // 垂直输入（W/S/上下箭头）
        Vector3 moveDir = transform.right * horizontal + transform.forward * vertical;

        anim.SetFloat("IsRun", Mathf.Abs(horizontal) + Mathf.Abs(vertical));

        // 只计算方向，不执行 Move
        moveDir.Normalize();
        moveDir *= moveSpeed;

        // 统一移动
        cc.Move(moveDir * Time.deltaTime);

        MouseLookAndRotate.instance.PlayerLookAndRotate();//鼠标控制角色转动
    }

    /// <summary>
    /// 玩家跳跃逻辑
    /// </summary>
    void Jump()
    {
        // 落地时重置下落速度
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // 小负数，让角色贴紧地面
        }

        // 跳跃（只有地面才能跳）
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
    }

    /// <summary>
    /// 应用重力 + 跳跃（必须单独一次 Move）
    /// </summary>
    void ApplyGravityAndMove()
    {
        // 重力
        velocity.y += gravity * Time.deltaTime;

        // 应用下落和跳跃
        cc.Move(velocity * Time.deltaTime);
    }

    /// <summary>
    /// 检测攻击输入
    /// </summary>
    void CheckAttackInput()
    {
        // 只有鼠标锁定视角时，左键才攻击
        if (MouseLookAndRotate.instance != null &&
            MouseLookAndRotate.instance.CanAttack() &&
            Input.GetMouseButtonDown(0))
        {
            Attack();
        }
    }

    /// <summary>
    /// 玩家攻击逻辑
    /// </summary>
    void Attack()
    {
        anim.SetTrigger("IsAttack"); // 触发攻击动画
        Instantiate(Arrow, arrowTransform.position, arrowTransform.rotation);
        //后续添加攻击检测、伤害计算逻辑（加到Arrow上了 不需要了）
    }

    /// <summary>
    /// 玩家吃药加血（背包系统调用）
    /// </summary>
    public void AddHp(int value)
    {
        currentHp += value;

        // 血量不能超过最大值
        if (currentHp > maxHp)
            currentHp = maxHp;
        // 直接用你现有的血条更新！
        UIManager.instance.UpdatePlayerHpSlider((float)currentHp / maxHp);
    }

    /// <summary>
    /// 玩家受伤重写
    /// </summary>
    /// <param name="damage">伤害值</param>
    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (currentHp > 0)
        {
            currentHp -= damage; // 扣除血量
            UIManager.instance.UpdatePlayerHpSlider((float)currentHp / maxHp); // 更新血条
            AudioManager.instance.PlayerClip();//播放怪物受伤音效
        }
        if (currentHp <= 0)
        {
            Die();   // 血量为0触发死亡
        }
        
    }

    /// <summary>
    /// 玩家死亡重写
    /// </summary>
    protected override void Die()
    {
        base.Die();

        anim.SetBool("IsDead", true); // 触发死亡动画
        enabled = false; // 关闭控制器
        cc.enabled = false; // 关闭角色控制器

        isDead = true;        //  标记死亡
      
    }
}