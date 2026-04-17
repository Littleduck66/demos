using UnityEngine;
using UnityEngine.UI;


public class BagSlot : MonoBehaviour
{
    [Header("背包单个格子预制体")]
    [Header("使用脚本的按钮")]
    public Button useButton;
    [Header("物品信息")]
    public Item item;
    [Header("物品图片显示区域")]
    public Image icon;
    [Header("物品数量显示区域")]
    public Text countText;

    private void Start()
    {
        useButton.onClick.AddListener(OnUseItem);
    }

    // 给格子设置物品
    public void SetItem(Item item)
    {
        this.item = item;
        icon.sprite = item.icon;
        icon.enabled = true;
        countText.text = item.count.ToString();
    }

    // 点击使用物品
    public void OnUseItem()
    {
        if (item == null || item.count <= 0) return;
        AudioManager.instance.ShowSfxClip();//播放音效
        // 玩家回血
        PlayerController.instance.AddHp(item.addHp);

        PromptPanel.instance.ShowPanel("成功使用" + item.itemName + "恢复"+ item.addHp);
        // 用掉1个
        item.count--;

        // 用完了就删除这个格子
        if (item.count <= 0)
        {
            BagManager.instance.RemoveSlot(this);
        }
        else
        {
            RefreshCount();
        }
    }

    // 更新数量显示
    public void RefreshCount()
    {
        countText.text = item.count.ToString();
    }
}