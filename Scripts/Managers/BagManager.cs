using System.Collections.Generic;
using UnityEngine;

public class BagManager : BaseManager
{
    
    [Header("背包管理器")]
    [Header("背包单个格子预制体")]
    public BagSlot slotPrefab; // 格子预制体
    [Header("单个格子生成的位置（Content）")]
    public Transform content; // ScrollRect 里的 Content

    public List<BagSlot> slotList = new List<BagSlot>();

    public static BagManager instance;
    protected override void Awake()
    {
        base.Awake();
        instance = this;
    }

    // 添加物品（核心逻辑：叠10个，满了新开格子）
    public void AddItem(Item newItem)
    {
        // 先找有没有同一种、且没叠满的格子
        foreach (BagSlot slot in slotList)
        {
            Item item = slot.item;

            if (item.itemName == newItem.itemName && item.count < item.maxStack)
            {
                // 还能放
                item.count++;
                slot.RefreshCount();
                return;
            }
        }

        // 没有空位 → 创建新格子
        CreateNewSlot(newItem);
    }

    // 创建新格子
    void CreateNewSlot(Item item)
    {
        BagSlot newSlot = Instantiate(slotPrefab, content);
        newSlot.SetItem(item);
        slotList.Add(newSlot);
    }

    // 用完物品删除格子
    public void RemoveSlot(BagSlot slot)
    {
        slotList.Remove(slot);
        Destroy(slot.gameObject);
    }
}