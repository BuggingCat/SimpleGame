using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimeEvents : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>(); // �Ӹ�����player����
    }

    private void AnimationTrigger()
    {
        //player.AttackOver(); // ����AttackOver������ʹIsAttacking��������Ϊfalse
    }
}
