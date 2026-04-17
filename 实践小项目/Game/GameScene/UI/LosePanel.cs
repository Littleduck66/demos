using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LosePanel : BasePanel<LosePanel>
{
    public CustomGUIButton btnBack;
    public CustomGUIButton btnGoOn;

    // Start is called before the first frame update
    void Start()
    {
        btnBack.clickEvent += () =>
        {
            //取消暂停
            Time.timeScale = 1;
            //切换场景
            SceneManager.LoadScene("BeginScene");
        };

        btnGoOn.clickEvent += () =>
        {
            //取消暂停
            Time.timeScale = 1;
            //再次切换到 游戏场景 就可以 达到所有内容重新加载 重头开始的 目的
            SceneManager.LoadScene("GameScene");
        };
        //一开始 隐藏自己
        HideMe();
    }

}
