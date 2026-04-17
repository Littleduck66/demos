using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{
    [Header("箭矢移动+旋转速度")]
    public float moveSpeed =10f; // 移动速度
    private Vector3 move ;
    // Update is called once per frame
    private void Start()
    {
        move = transform.forward * moveSpeed;
        Destroy(gameObject,3f);
    }
    void Update()
    {
        Shoot();
    }
    void Shoot()
    {
        transform.position += move * Time.deltaTime;//子弹飞行
        transform.Rotate(0,0,180 * Time.deltaTime);//子弹飞行
    }

    // 碰撞检测：箭矢碰到敌人时触发受伤
    private void OnTriggerEnter(Collider other)
    {
        // 检测碰撞对象是否有 EnemyController (脚本)组件（需给敌人加Tag：Enemy，或直接判断组件）
        EnemyController enemy = other.GetComponent<EnemyController>();
        if (other.CompareTag("Enemy"))
        {
            // 调用敌人的受伤方法，传入伤害值
            enemy.TakeDamage(35);
            Destroy(this.gameObject); // 箭矢命中后销毁
        }
    }
}
