using UnityEngine;

[System.Serializable]
public class Item
{
    [Header("每个格子最多叠")]
    [Header("物品名称")]
    public string itemName;
    [Header("物品图片")]
    public Sprite icon;
    [Header("回血物品回血值")]
    public int addHp; // 使用加多少血
    [Header("一个的数量")]
    public int count; // 当前数量
    // 每个格子最多叠10个

    public int maxStack => 10;
}