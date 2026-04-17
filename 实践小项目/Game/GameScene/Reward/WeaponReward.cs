using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponReward : MonoBehaviour
{
    //有多个用于随机的 武器预设体
    public GameObject[] weaponObj;

    //获取特效
    public GameObject getEff;

    private void OnTriggerEnter(Collider other)
    {
        if( other.CompareTag("Player") )
        {
            //让玩家 切换武器
            int index = Random.Range(0, weaponObj.Length);
            //得到撞到的玩家身上挂在的 脚本 命令它切换武器
            PlayerObj player = other.GetComponent<PlayerObj>();
            player.ChangeWeapon(weaponObj[index]);

            //播放一个 奖励特效
            GameObject eff = Instantiate(getEff, this.transform.position, this.transform.rotation);
            //控制 获取音效
            AudioSource audioS = eff.GetComponent<AudioSource>();
            //大小和开启状态
            audioS.volume = GameDataMgr.Instance.musicData.soundValue;
            audioS.mute = !GameDataMgr.Instance.musicData.isOpenSound;

            //获取到自己后 移除自己
            Destroy(this.gameObject);
        }
    }
}
