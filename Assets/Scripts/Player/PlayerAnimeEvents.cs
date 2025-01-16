using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimeEvents : MonoBehaviour
{
    private Player player;
    // Start is called before the first frame update
    void Start()
    {
        player = GetComponentInParent<Player>(); // 从父类获得player对象
    }

    private void AnimationTrigger()
    {
        //player.AttackOver(); // 调用AttackOver函数，使IsAttacking参数更新为false
    }
}
