using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 背包UI显示层（适配ScrollView）
/// 负责生成格子、绑定物品数据、刷新UI
/// </summary>
public class BagPanel : MonoBehaviour
{
    //public static BagPanel instance;

    [Header("背包面板")]
    [Header("关闭背包面板按钮")]
    public Button backBt;            // 关闭背包面板
    [Header("生成物品按钮")]
    public Button getBt;            // 生成物品按钮
    [Header("生成的物品")]
    public GameObject hpslot;        // 生成物品按钮
    [Header("生成的物品的生成位置")]
    public Transform slotTransform; //生成 的预制体位置


    /// <summary>
    /// 初始化背包格子
    /// </summary>
    void Start()
    {
        backBt.onClick.AddListener(HideBag);
        getBt.onClick.AddListener(getslot);
    }

    /// <summary>
    /// 初始化背包格子
    /// </summary>
    public void HideBag()
    {
        UIManager.instance.HideMe(gameObject);// 关闭背包面板
        AudioManager.instance.ShowSfxClip();//播放音效
    }
    /// <summary>
    /// 初始化背包格子
    /// </summary>
    /// 
    public void getslot()
    {
        Instantiate(hpslot, slotTransform.position, slotTransform.rotation);
        AudioManager.instance.ShowSfxClip();//播放音效
    }
}