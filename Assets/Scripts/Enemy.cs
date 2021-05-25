using UnityEngine;
using System.Collections;

public class Enemy : Monster
{
    public Transform player;
    public float move_speed;
    public Transform enemy;
    private Animator animator;
    private SpriteRenderer sprite;
    private bool isDead = false;
    HealthBar healthBar;
    private Character character;

    private BossState State
    {
        get
        {
            return (BossState)animator.GetInteger("State");
        }
        set
        {
            animator.SetInteger("State", (int)value);
        }
    }

    public bool IsDead
    {
        get
        {
            return isDead;
        }
    }

    protected override void Awake()
    {
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();
        healthBar = FindObjectOfType<HealthBar>();
        character = FindObjectOfType<Character>();
    }

    protected override void Update()
    {
        if (!isDead)
        {
            Vector3 position = player.position;
            position.y += 0.5F;
            sprite.flipX = player.position.x - transform.position.x < 0.0F;
            transform.position = Vector3.Lerp(transform.position, position, move_speed * Time.deltaTime);
        }
        else
        {
            State = BossState.Die;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!isDead)
        {
            if (collision.gameObject.tag == "Bullet")
            {
                ReceiveDamage();
            }

            if (collision.gameObject.tag == "Player")
            {
                character.ReceiveDamage();
            }
        }
    }

    /*protected override void OnTriggerEnter2D(Collider2D collider)
    {
        if (!isDead)
        {
            Bullet bullet = collider.GetComponent<Bullet>();
            if (bullet)
            {
                ReceiveDamage();
            }

            Unit unit = collider.GetComponent<Unit>();
            if (unit && unit is Character)
            {
                unit.ReceiveDamage();
            }
        }
    }*/

    public override void ReceiveDamage()
    {
        healthBar.Health -= 10;
        if (healthBar.Health <= 0)
        {
            isDead = true;
            Destroy(gameObject, 1.2f);
        }
    }
}

public enum BossState
{
    Run,
    Die
}
