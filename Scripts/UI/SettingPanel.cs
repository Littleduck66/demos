using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 设置面板逻辑
/// 负责：背景音乐音量、音效音量调节，关闭设置面板
/// </summary>
public class SettingPanel : MonoBehaviour
{
    [Header("设置面板组件")]
    [Header("背景音乐音量滑块")]
    public Slider bgmSlider;  // 背景音乐音量滑块
    [Header("音效音量滑块")]
    public Slider sfxSlider;  // 音效音量滑块
    [Header("关闭按钮")]
    public Button closeBtn;   // 关闭按钮

    /// <summary>
    /// 初始化滑块监听
    /// </summary>
    void Start()
    {
        InitAudioVolume();
        AddSliderListener();
        closeBtn.onClick.AddListener(ClosePanel); // 关闭按钮监听
    }

    /// <summary>
    /// 用于初始化音乐大小
    /// </summary>
    void InitAudioVolume()
    {
        float bgmVolume = bgmSlider.value;
        float sfxVolume = sfxSlider.value;

        AudioManager.instance.bgmSource.volume = bgmVolume;
        AudioManager.instance.sfxSource.volume = sfxVolume;

    }

    /// <summary>
    /// 为音量滑块添加值改变监听
    /// </summary>
    void AddSliderListener()
    {
        bgmSlider.onValueChanged.AddListener(OnBgmValueChanged);
        sfxSlider.onValueChanged.AddListener(OnSfxValueChanged);
    }

    /// <summary>
    /// 背景音乐音量改变事件
    /// </summary>
    /// <param name="value">音量值（0-1）</param>
    public void OnBgmValueChanged(float value)
    {
        AudioManager.instance.SetBgmVolume(value); // 调用音频管理器设置音量
        //AudioManager.instance.ShowSfxClip();//播放音效
    }

    /// <summary>
    /// 音效音量改变事件
    /// </summary>
    /// <param name="value">音量值（0-1）</param>
    public void OnSfxValueChanged(float value)
    {
        AudioManager.instance.SetSfxVolume(value); // 调用音频管理器设置音量
        //AudioManager.instance.ShowSfxClip();//播放音效
    }

    /// <summary>
    /// 关闭设置面板
    /// </summary>
    public void ClosePanel()
    {
        UIManager.instance.HideMe(this.gameObject); // 隐藏当前面板
        AudioManager.instance.ShowSfxClip();//播放音效
    }
}