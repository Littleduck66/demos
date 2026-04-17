using UnityEditor;
using UnityEngine;

/// <summary>
/// 音频管理类
/// 继承 BaseManager
/// 负责：背景音乐播放、音效播放、音量调节
/// </summary>
public class AudioManager : BaseManager
{
    public static AudioManager instance; // 单例实例

    [Header("音频源组件")]
    [Header("背景音乐音源")]
    public AudioSource bgmSource;  // 背景音乐音源
    [Header("音效音源")]
    public AudioSource sfxSource;  // 音效音源
    [Header("单击音效内容")]
    public AudioClip sfxClip;  // 音效音源
    [Header("怪物受伤音效内容")]
    public AudioClip enemeyClip;  // 音效音源
    [Header("玩家受伤音效内容")]
    public AudioClip playerClip;  // 音效音源
    [Header("玩家捡到物品音效内容")]
    public AudioClip playerSlotClip;  // 音效音源

    /// <summary>
    /// 初始化单例
    /// </summary>
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    /// <summary>
    /// 设置背景音乐音量
    /// </summary>
    /// <param name="value">音量值（0-1）</param>
    public void SetBgmVolume(float value)
    {
        bgmSource.volume = value;
    }

    /// <summary>
    /// 设置音效音量
    /// </summary>
    /// <param name="value">音量值（0-1）</param>
    public void SetSfxVolume(float value)
    {
        sfxSource.volume = value;
    }

    /// <summary>
    /// 播放按钮单击音效音乐
    /// </summary>
    public void ShowSfxClip()
    {
        sfxSource.PlayOneShot(sfxClip);
    }

    /// <summary>
    /// 播放怪物受伤音效
    /// </summary>
    public void EnemyClip()
    {
        sfxSource.PlayOneShot(enemeyClip);
    }

    /// <summary>
    /// 播放玩家受伤音效
    /// </summary>
    public void PlayerClip()
    {
        sfxSource.PlayOneShot(playerClip);
    }
    /// <summary>
    /// 播放玩家捡到物品音效
    /// </summary>
    public void PlayeSlotClip()
    {
        sfxSource.PlayOneShot(playerSlotClip);
    }

}