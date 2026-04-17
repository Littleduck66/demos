using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 开始界面逻辑
/// 负责：开始游戏、打开设置、退出游戏按钮交互
/// </summary>
public class StartPanel : MonoBehaviour
{
    [Header("开始界面按钮")]
    [Header("开始游戏按钮")]
    public Button startBtn;     // 开始游戏按钮
    [Header("设置按钮")]
    public Button settingBtn;   // 设置按钮
    [Header("退出按钮")]
    public Button exitBtn;      // 退出按钮
    [Header("设置面板对象")]
    public GameObject settingPanel; // 设置面板对象

    /// <summary>
    /// 初始化按钮监听
    /// </summary>
    void Start()
    {
        UIManager.instance.HideMe(settingPanel);
        AddButtonListener();
    }

    /// <summary>
    /// 为按钮添加点击监听
    /// </summary>
    void AddButtonListener()
    {
        startBtn.onClick.AddListener(OnStartClick);
        settingBtn.onClick.AddListener(OnSettingClick);
        exitBtn.onClick.AddListener(OnExitClick);
    }

    /// <summary>
    /// 开始游戏按钮点击事件
    /// </summary>
    public void OnStartClick()
    {
        GameManager.instance.StartGame(); // 调用游戏管理器开始游戏
        AudioManager.instance.ShowSfxClip();//播放音效
    }

    /// <summary>
    /// 设置按钮点击事件
    /// </summary>
    public void OnSettingClick()
    {
        UIManager.instance.ShowMe(settingPanel); // 显示设置面板
        AudioManager.instance.ShowSfxClip();//播放音效
    }

    /// <summary>
    /// 退出游戏按钮点击事件
    /// </summary>
    public void OnExitClick()
    {
        GameManager.instance.ExitGame(); // 调用游戏管理器退出游戏
        AudioManager.instance.ShowSfxClip();//播放音效
    }
}