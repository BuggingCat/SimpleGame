using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;//定义SR组件来保持要用的组件
    [Header("Flash FX")]
    [SerializeField] private Material hitMat;//要改成的材料
    [SerializeField] private float flashDuration;//闪光的时间
    private Material originalMat;//原来的材料

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();//从子组件中拿到SR组件
        originalMat = sr.material;//拿到原来的材料

    }
    private IEnumerator FlashFX()//被打后该触发的函数
    {
        sr.material = hitMat;
        yield return new WaitForSeconds(flashDuration);
        sr.material = originalMat;
    } //IEnumertor本质就是将一个函数分块执行，只有满足某些条件才能执行下一段代码，此函数有StartCoroutine调用
    //https://www.zhihu.com/tardis/bd/art/504607545?source_id=1001
    private void RedColorBlink()//使角色闪烁的函数
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
    private void CancelRedBlink()//使角色停止闪烁的函数
    {
        CancelInvoke();//取消该 MonoBehaviour 上的所有 Invoke 调用。
        //https://docs.unity3d.com/cn/current/ScriptReference/MonoBehaviour.CancelInvoke.html
        sr.color = Color.white;
    }
}
