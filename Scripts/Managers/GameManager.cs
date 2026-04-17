using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// 游戏全局管理类
/// 继承 BaseManager
/// 负责：场景切换、游戏状态控制、全局流程管理
/// </summary>
public class GameManager : BaseManager
{

    public static GameManager instance; // 单例实例，方便全局调用
    /// <summary>
    /// 初始化单例
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        instance = this; // 赋值单例
    }

    /// <summary>
    /// 开始游戏（从开始界面进入游戏场景）
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("GameScene"); // 加载游戏场景（需提前创建并命名）
    }
    /// <summary>
    /// 返回主界面
    /// </summary>
    public void BackGame()
    {
        SceneManager.LoadScene("StartScene"); // 加载游戏场景（需提前创建并命名）
    }

    /// <summary>
    /// 退出游戏
    /// </summary>
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // 编辑器模式下停止运行
#else
        Application.Quit(); // 打包后退出程序
#endif
    }
}