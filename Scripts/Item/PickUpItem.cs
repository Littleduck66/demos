using UnityEngine;

public class PickUpItem : MonoBehaviour
{
    public Item item; // 这个必须在Inspector面板赋值！
    [Header("旋转速度")]
    public float rotateSpeed = 90f;

    void Update()
    {
        transform.Rotate(0, rotateSpeed * Time.deltaTime, 0);
    }

    void OnTriggerEnter(Collider other)
    {
        // 1. 必须先判断是不是玩家
        if (!other.CompareTag("Player")) return;

        // 2. 必须判断物品有没有赋值（没赋值就报错）
        if (item == null)
        {
            Debug.LogError("物品没赋值！去面板把Item填好！");
            return;
        }

        // 3. 安全创建物品
        Item copy = new Item();
        copy.itemName = item.itemName;
        copy.icon = item.icon;
        copy.addHp = item.addHp;
        copy.count = 1;

        // 4. 安全播放音效（加判断，防止报错）
        if (AudioManager.instance != null)
        {
            AudioManager.instance.PlayeSlotClip();
        }

        // 5. 安全添加进背包
        if (BagManager.instance != null)
        {
            BagManager.instance.AddItem(copy);
        }

        // 6. 安全提示面板
        if (PromptPanel.instance != null)
        {
            PromptPanel.instance.ShowPanel("成功获取：" + item.itemName);
        }

        // 7. 销毁物品
        Destroy(gameObject);
    }
}