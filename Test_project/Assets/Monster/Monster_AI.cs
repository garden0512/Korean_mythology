using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_AI : MonoBehaviour
{
    public static Monster_AI instance;
    [Tooltip("몬스터의 움직임에 관한 변수들입니다.")]
    [Header("Monster Move Info")]
    public float speed;
    [Tooltip("몬스터의 플레이어 감지에 관련된 변수들입니다.")]
    [Header("Recognition Info")]
    public float detectionRadius = 5f; // 감지 범위
    public Rigidbody2D target;
    private bool _isPlayerInRange;
    public bool isPlayerInRange
    {
        get => _isPlayerInRange;
        set => _isPlayerInRange = value;
    }
    [Tooltip("몬스터의 체력에 대한 변수들입니다.")]
    [Header("Monster HP Info")]
    public float maxHealth = 1000f;
    private float _health;
    public float health
    {
        get => _health;
        set => _health = value;
    }
    private bool isLive = true;
    public bool IsLive
    {
        get => isLive;
        set => isLive = value;
    }
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    private Vector2 randomDirection;
    private float changeDirectionTime = 2f;
    private float changeDirectionTimer;
    private float attackInterval = 3f;
    private float attackTimer;
    private bool isAttacking = false;
    private Vector2 initialAttackPosition;
    
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
        rigid = GetComponent<Rigidbody2D>();
        spriter = GetComponent<SpriteRenderer>();
        SetRandomDirection();
        _health = maxHealth;
    }

    void FixedUpdate()
    {
        if (!isLive || isAttacking)
            return;

        float distanceToPlayer = Vector2.Distance(rigid.position, target.position);

        if (distanceToPlayer < detectionRadius)
        {
            if (distanceToPlayer <= 5f)
            {
                AttackPlayer();
                rigid.velocity = Vector2.zero;
                return;
            }
            else
            {
                Vector2 dirVec = target.position - rigid.position;
                Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
                rigid.MovePosition(rigid.position + nextVec);
            }
            _isPlayerInRange = true;
            Debug.Log("isPlayerInRange = true");
        }
        else
        {
            _isPlayerInRange = false;
            Debug.Log("isPlayerInRange = false");
            changeDirectionTimer -= Time.fixedDeltaTime;
            if (changeDirectionTimer <= 0)
            {
                SetRandomDirection();
            }
            Vector2 nextVec = randomDirection * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
        }

        rigid.velocity = Vector2.zero;
    }

    void LateUpdate()
    {
        if (!isLive || isAttacking)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void SetRandomDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        changeDirectionTimer = changeDirectionTime;
    }

    void AttackPlayer()
    {
        if(isAttacking)
            return;

        attackTimer -= Time.deltaTime;
        if (attackTimer <= 0)
        {
            StartCoroutine(PerformAttack());
        }
    }

    IEnumerator PerformAttack()
    {
        isAttacking = true;
        attackTimer = attackInterval;
        initialAttackPosition = rigid.position;

        yield return new WaitForSeconds(0.5f);

        Player_Health playerHealth = target.GetComponent<Player_Health>();
        if(playerHealth != null)
        {
            playerHealth.TakeDamage(10f);
        }

        yield return new WaitForSeconds(0.5f);

        isAttacking = false;
    }


    public void TakeDamage(float damage)
    {
        if (!isLive)
            return;

        _health -= damage;
        if (_health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isLive = false;
        Debug.Log("Monster died");
        Destroy(gameObject);
    }
}