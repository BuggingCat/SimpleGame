using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    //void Start()
    //{
    //    
    //}

    // 背景Castle的移动速度
    public float speed = 1.2f;

    // Update is called once per frame
    void Update()
    {
        // 在每一帧内，遍历子物体
        foreach(Transform tran in transform)
        {
            // 获取子物体的位置
            Vector3 pos = tran.position;
            // 按照速度向左侧移动
            pos.x -= speed * Time.deltaTime;
            // 判断是否出了屏幕，若是，则移动Castle1至最右边
            if (pos.x < -23.2)
            {
                pos.x = pos.x + 23.224f * 2;
            }
            // 将修改后的位置赋值给子物体
            tran.position = pos;
        }
    }
}
