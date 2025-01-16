using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_AnimationTriggers : MonoBehaviour
{
    private Enemy enemy => GetComponentInParent<Enemy>();//�õ�enemyʵ��
    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();//����ʵ���ϵĺ�����ʹtriggerCalledΪtrue��
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackCheckRadius);//����һ����ײ���飬��������Ȧ����������ײ��
        //https://docs.unity3d.com/2022.3/Documentation/ScriptReference/Physics2D.OverlapCircleAll.html
        foreach (var hit in colliders)//https://blog.csdn.net/m0_52358030/article/details/121722077
        {
            if (hit.GetComponent<Player>() != null)
            {
                PlayerStats target = hit.GetComponent<PlayerStats>();
                enemy.stats.DoDamage(target);
                //hit.GetComponent<Player>().Damage();
            }
        }
    }

    private void SpecialAttackTrigger()
    {
        enemy.SpecialAttackTrigger();
    }

    private void OpenCounterWindow()
    {
        enemy.OpenCounterAttackWindow();
    }

    private void CloseCounterWindow()
    {
        enemy.CloseCounterAttackWindow();
    }
}
