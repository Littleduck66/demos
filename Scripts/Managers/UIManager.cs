using System;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// UI总管理类
/// 继承 BaseManager
/// 负责：面板显示/隐藏、血条更新、背包UI刷新等
/// </summary>
public class UIManager : BaseManager
{
    [Header("角色生命值血条Slider")]
    public Slider playerslider;

    public static UIManager instance; // 单例实例
    /// <summary>
    /// 初始化单例
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        if (instance == null)
        {
            instance = this;
        }
        
    }

    /// <summary>
    /// 显示指定面板
    /// </summary>
    /// <param name="panel">要显示的面板GameObject</param>
    public void ShowMe(GameObject panel)    
    {
        panel.SetActive(true);
    }

    /// <summary>
    /// 隐藏指定面板
    /// </summary>
    /// <param name="panel">要隐藏的面板GameObject</param>
    public void HideMe(GameObject panel)
    {
        panel.SetActive(false);
    }

    /// <summary>
    /// 更新玩家血条显示
    /// </summary>
    /// <param name="ratio">血量百分比（当前血量/最大血量）</param>
    public void UpdatePlayerHpSlider(float ratio)
    {
        // 对接血条Slider组件，设置value = ratio
        playerslider.value = ratio;
    }
}