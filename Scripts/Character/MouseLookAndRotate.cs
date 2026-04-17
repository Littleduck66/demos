using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLookAndRotate : MonoBehaviour
{
    [Header("视角灵敏度")]
    public float mouseSensitivity = 2f;
    [Header("上下视角限制")]
    public float minUpDownAngle = -50f;    // 最多低头多少度
    public float maxUpDownAngle = 50f;     // 最多抬头多少度
    private float cameraPitch = 0f;        // 摄像机上下角度
    private bool isCursorHidden = false;   // 记录鼠标是否隐藏
    public GameObject Player;
    public GameObject Camera;
    public static MouseLookAndRotate instance;
 
    private void Awake()
    {
        instance = this;
    }

    /// <summary>
    /// 公开给角色脚本判断：是否可以攻击
    /// </summary
    public bool CanAttack()
    {
        return isCursorHidden; // 鼠标隐藏 → 能攻击
    }

    /// <summary>
    /// 玩家玩家视角跟随鼠标转动逻辑
    /// </summary>
    public void PlayerLookAndRotate()
    {
        // ========== 右键切换鼠标显示/隐藏 ==========
        if (Input.GetMouseButtonDown(1)) // 右键按下
        {
            isCursorHidden = !isCursorHidden;
            if (isCursorHidden)
            {
                Cursor.lockState = CursorLockMode.Locked; // 锁住鼠标
                Cursor.visible = false;                   // 隐藏
                
            }
            else
            {
                Cursor.lockState = CursorLockMode.None;   // 解锁
                Cursor.visible = true;                    // 显示
            }
        }
        // ========== 只有鼠标隐藏时才跟随旋转 ==========
        if (isCursorHidden)
        {
            // 获取鼠标左右移动的值
            float mouseX = Input.GetAxis("Mouse X");
            // 获取鼠标左右移动的值
            float mouseY = Input.GetAxis("Mouse Y");

            // 计算旋转量
            float rotateAmountX = mouseX * mouseSensitivity;
            float rotateAmountY = mouseY * mouseSensitivity;

            // 让角色左右旋转
            //PlayerController instance.anim.SetFloat("IsRun", Mathf.Abs(horizontal) + Mathf.Abs(vertical));
            Player.transform.Rotate(0, rotateAmountX, 0);
            // 让摄像头上下旋转
            // 摄像机上下转（带角度限制）
            cameraPitch -= mouseY * mouseSensitivity;
            cameraPitch = Mathf.Clamp(cameraPitch, minUpDownAngle, maxUpDownAngle);

            // 直接设置角度，不会乱飘
            Camera.transform.localEulerAngles = new Vector3(cameraPitch, 0, 0);
       
           
        }
    }
}
