using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_AI : MonoBehaviour
{
    public static Monster_AI instance;

    public float speed;
    public float detectionRadius = 5f; // 감지 범위
    public Rigidbody2D target;
    public bool isLive = true;
    Rigidbody2D rigid;
    SpriteRenderer spriter;
    private Vector2 randomDirection;
    private float changeDirectionTime = 2f;
    private float changeDirectionTimer;
    public float maxHealth = 1000f;
    public float health;
    public bool isPlayerInRange;

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
        health = maxHealth;
    }

    void FixedUpdate()
    {
        if (!isLive)
            return;

        float distanceToPlayer = Vector2.Distance(rigid.position, target.position);

        if (distanceToPlayer < detectionRadius)
        {
            Vector2 dirVec = target.position - rigid.position;
            Vector2 nextVec = dirVec.normalized * speed * Time.fixedDeltaTime;
            rigid.MovePosition(rigid.position + nextVec);
            isPlayerInRange = true;
        }
        else
        {
            isPlayerInRange = false;
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
        if (!isLive)
            return;
        spriter.flipX = target.position.x < rigid.position.x;
    }

    void SetRandomDirection()
    {
        randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        changeDirectionTimer = changeDirectionTime;
    }

    public void TakeDamage(float damage)
    {
        if (!isLive)
            return;

        health -= damage;
        if (health <= 0)
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
