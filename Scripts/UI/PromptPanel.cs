using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 提示面板逻辑
/// 负责：打开提示面板，关闭提示面板
/// </summary>
public class PromptPanel : MonoBehaviour
{
    [Header("提示面板信息")]

    [Header("设置面板对象")]
    public GameObject promptPanel; // 设置面板对象
    [Header("提示文本框")]
    public Text promp;              // 提示文本框

    public static PromptPanel instance; // 单例实例

    /// <summary>
    /// 初始化单例
    /// </summary>
    protected void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 协程：等待2秒自动关闭面板
    /// </summary>
    private IEnumerator AutoHideCoroutine()
    {
        // 等待2秒
        yield return new WaitForSeconds(1.5f);
        // 时间到，隐藏面板
        ClosePanel();
    }

    /// <summary>
    /// 关闭设置面板
    /// </summary>
    private void ClosePanel()
    {
        // 【修复】安全判断：如果 UIManager 存在才调用
        if (UIManager.instance != null)
        {
            UIManager.instance.HideMe(promptPanel); // 隐藏当前面板
        }
        else
        {
            gameObject.SetActive(false); // 找不到UI管理器，直接隐藏
        }
    }

    /// <summary>
    /// 打开设置面板，传入提示信息
    /// </summary>
    public void ShowPanel(string text)
    {

        if (promp != null)
        {
            promp.text = text;

            UIManager.instance.ShowMe(promptPanel); // 显示当前面板
        }

        StartCoroutine(AutoHideCoroutine());
    }
}