using UnityEngine;

/// <summary>
/// 角色基类（玩家、敌人都继承这个）
/// 作用：统一管理角色血量、受伤、死亡等核心逻辑
/// </summary>
public class BaseCharacter : MonoBehaviour
{
    [Header("角色基础属性")]
    [Header("角色最大生命值")]
    public int maxHp ;      // 最大生命值
    [Header("角色当前生命值")]
    public int currentHp;  // 当前生命值

    protected Animator anim; // 角色动画组件

    /// <summary>
    /// 初始化角色组件
    /// </summary>
    protected virtual void Awake()
    {
        if (anim == null)
        {
            anim = GetComponentInChildren<Animator>(); // 获取子物体上的动画组件
        }
    }

    /// <summary>
    /// 初始化角色血量
    /// </summary>
    protected virtual void Start()
    {
        currentHp = maxHp; // 初始血量设为最大血量
    }

    /// <summary>
    /// 角色受伤方法（子类可重写）
    /// </summary>
    /// <param name="damage">受到的伤害值</param>
    public virtual void TakeDamage(int damage)
    {

    }

    /// <summary>
    /// 角色死亡方法（子类重写实现具体逻辑）
    /// </summary>
    protected virtual void Die()
    {

    }
}