using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;//����SR���������Ҫ�õ����
    [Header("Flash FX")]
    [SerializeField] private Material hitMat;//Ҫ�ĳɵĲ���
    [SerializeField] private float flashDuration;//�����ʱ��
    private Material originalMat;//ԭ���Ĳ���

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();//����������õ�SR���
        originalMat = sr.material;//�õ�ԭ���Ĳ���

    }
    private IEnumerator FlashFX()//�����ô����ĺ���
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(flashDuration);
        sr.material = originalMat;
    } //IEnumertor���ʾ��ǽ�һ�������ֿ�ִ�У�ֻ������ĳЩ��������ִ����һ�δ��룬�˺�����StartCoroutine����
    //https://www.zhihu.com/tardis/bd/art/504607545?source_id=1001
    private void RedColorBlink()//ʹ��ɫ��˸�ĺ���
    {
        if (sr.color != Color.white)
        {
            sr.color = Color.white;
        }
        else
        {
            sr.color = Color.red;
        }
    }
    private void CancelRedBlink()//ʹ��ɫֹͣ��˸�ĺ���
    {
        CancelInvoke();//ȡ���� MonoBehaviour �ϵ����� Invoke ���á�
        //https://docs.unity3d.com/cn/current/ScriptReference/MonoBehaviour.CancelInvoke.html
        sr.color = Color.white;
    }
}
