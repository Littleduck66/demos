using UnityEngine;
using UnityEngine.UI;
using static System.Net.Mime.MediaTypeNames;

/// <summary>
/// 开始界面逻辑
/// 负责：开始游戏、打开设置、退出游戏按钮交互
/// </summary>
public class MainPanel : MonoBehaviour
{
    [Header("游戏界面按钮")]
    [Header("退出游戏按钮")]
    public Button backBtn;           // 退出游戏按钮
    [Header("设置按钮")]
    public Button settingBtn;        // 设置按钮
    [Header("提示按钮")]
    public Button promptBtn;         // 提示按钮
    [Header("背包按钮")]
    public Button bagBtn;            // 背包按钮
    [Header("设置面板对象")]
    public GameObject settingPanel;  // 设置面板对象
    [Header("提示面板对象")]
    public GameObject PromptPanel1;   // 提示面板对象
    [Header("背包面板对象")]
    public GameObject BagPanel;      // 背包面板对象
    //[Header("主界面面板对象")]
    //public GameObject mainPanel;      // 主面板对象

    /// <summary>
    /// 初始化按钮监听
    /// </summary>
    void Start()
    {
        //UIManager.instance.ShowMe(mainPanel);
        UIManager.instance.HideMe(settingPanel);
        UIManager.instance.HideMe(PromptPanel1);
        UIManager.instance.HideMe(BagPanel);
        AddButtonListener();
    }

    /// <summary>
    /// 为按钮添加点击监听
    /// </summary>
    void AddButtonListener()
    {
        backBtn.onClick.AddListener(OnStartClick);
        settingBtn.onClick.AddListener(OnSettingClick);
        promptBtn.onClick.AddListener(OnPromptClick);
        bagBtn.onClick.AddListener(OnBagClick);
    }

    /// <summary>
    /// 返回主界面按钮点击事件
    /// </summary>
    public void OnStartClick()
    {
        GameManager.instance.BackGame(); // 调用游戏管理器返回主界面
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
    /// 显示提示面板按钮点击事件
    /// </summary>
    public void OnPromptClick()
    {
        PromptPanel.instance.ShowPanel("击败所有敌人吧！"); // 显示提示面板
        AudioManager.instance.ShowSfxClip();//播放音效
    }
    /// <summary>
    /// 显示提示面板按钮点击事件
    /// </summary>
    public void OnBagClick()
    {
        UIManager.instance.ShowMe(BagPanel); // 显示提示面板
        AudioManager.instance.ShowSfxClip();//播放音效
    }
}