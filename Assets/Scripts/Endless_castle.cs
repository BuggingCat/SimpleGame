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

    // ����Castle���ƶ��ٶ�
    public float speed = 1.2f;

    // Update is called once per frame
    void Update()
    {
        // ��ÿһ֡�ڣ�����������
        foreach(Transform tran in transform)
        {
            // ��ȡ�������λ��
            Vector3 pos = tran.position;
            // �����ٶ�������ƶ�
            pos.x -= speed * Time.deltaTime;
            // �ж��Ƿ������Ļ�����ǣ����ƶ�Castle1�����ұ�
            if (pos.x < -23.2)
            {
                pos.x = pos.x + 23.224f * 2;
            }
            // ���޸ĺ��λ�ø�ֵ��������
            tran.position = pos;
        }
    }
}
