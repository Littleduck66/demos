using UnityEngine;

/// <summary>
/// 所有管理器的基类
/// 作用：提供通用初始化逻辑，被 UIManager、AudioManager、GameManager 继承
/// </summary>
public class BaseManager : MonoBehaviour
{
    /// <summary>
    /// 初始化通用逻辑（子类可重写）
    /// </summary>
    protected virtual void Awake()
    {

    }

    /// <summary>
    /// 启动通用逻辑（子类可重写）
    /// </summary>
    protected virtual void Start()
    {

    }
}