using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_Attack : MonoBehaviour
{
    public static Player_Attack instance;

    public float attackDamage = 100f; // 플레이어의 공격력을 100으로 설정
    public Monster_AI monster; // 공격할 몬스터를 참조

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // 마우스 좌클릭 입력을 체크
        {
            Attack(); // 공격 메서드 호출
        }
    }

    void Attack()
    {
        if (monster != null) // 몬스터가 존재하는지 확인
        {
            monster.TakeDamage(attackDamage); // 몬스터의 체력을 깎음
        }
    }
}
