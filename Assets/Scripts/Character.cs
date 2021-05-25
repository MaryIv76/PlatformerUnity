using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class Character : Unit
{
    [SerializeField]
    public int lives = 5;
    [SerializeField]
    private float speed = 3.0F;
    [SerializeField]
    private float jumpForce = 15.0F;
    [SerializeField]
    private LayerMask whatIsLadder;

    new private Rigidbody2D rigidbody;
    private Animator animator;
    private SpriteRenderer sprite;

    public PhysicsMaterial2D noFriction;
    public PhysicsMaterial2D withFriction;

    private bool isGrounded = false;
    private bool isClimbing = false;

    private bool isDead = false;

    [SerializeField]
    private bool hasPick = false;

    public float distance;

    private Bullet bullet;

    private RaycastHit2D hitInfo;

    private GameManager gameManager;

    private AudioSource audioSource;

    public AudioClip audioClip;
    public AudioClip audioClipJump;

    public float JumpForce
    {
        get
        {
            return jumpForce;
        }
        set 
        {
            jumpForce = value;
        }
    }
    public bool HasPick
    {
        get
        {
            return hasPick;
        }
        set
        {
            hasPick = value;
        }
    }

    public int Lives
    {
        get
        {
            return lives;
        }
        set
        {
            if (value <= 0)
            {
                isDead = true;
                Die();
            }
            if (value < 5) lives = value;
            livesBar.Refresh();
        }
    }

    LivesBar livesBar;

    private CharState State
    {
        get
        {
            return (CharState)animator.GetInteger("State");
        }
        set
        {
            animator.SetInteger("State", (int)value);
        }
    }

    private void Awake()
    {
        livesBar = FindObjectOfType<LivesBar>();
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        sprite = GetComponentInChildren<SpriteRenderer>();

        bullet = Resources.Load<Bullet>("Bullet");
        gameManager = FindObjectOfType<GameManager>();
        audioSource = this.GetComponent<AudioSource>();
    }

    private void FixedUpdate()
    {
        if (!isDead)
        {
            CheckGround();
            CheckLadder();

            if (rigidbody.position.y < -10.0f)
            {
                FindObjectOfType<GameManager>().EndGame();
            }
        }
    }

    private void Update()
    {
        if (!isDead)
        {
            if (isGrounded && !isDead)
            {
                State = CharState.Idle;
            }

            if (Input.GetButtonDown("Fire1") && gameManager.levelFinal)
            {
                Shoot();
            }

            if (Input.GetButton("Horizontal"))
            {
                Run();
            }
            else
            {
                rigidbody.sharedMaterial = withFriction;
            }

            if (isGrounded && Input.GetButtonDown("Jump"))
            {
                Jump();
            }

            Climb();
        }
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);

        sprite.flipX = direction.x < 0.0F;

        rigidbody.sharedMaterial = noFriction;

        if (isGrounded && !isDead)
        {
            State = CharState.Run;
        }
    }

    private void Jump()
    {
        if (!isDead)
        {
            State = CharState.Jump;
        }

        rigidbody.AddForce(transform.up * jumpForce, ForceMode2D.Impulse);
        audioSource.PlayOneShot(audioClipJump);
    }

    private void Shoot()
    {
        Vector3 position = transform.position;
        Bullet newBullet = Instantiate(bullet, position, bullet.transform.rotation) as Bullet;

        newBullet.Parent = gameObject;
        newBullet.Direction = newBullet.transform.right * (sprite.flipX ? -1.0F : 1.0F);
    }



    private void Climb()
    {
        if (isClimbing == true && hitInfo.collider != null)
        {
            float inputVertical = Input.GetAxisRaw("Vertical");
            rigidbody.velocity = new Vector2(rigidbody.velocity.x, inputVertical * speed);
            rigidbody.gravityScale = 0;
        }
        else
        {
            rigidbody.gravityScale = 3;
        }
    }

    private void CheckGround()
    {
        Collider2D[] colliders = Physics2D.OverlapAreaAll(new Vector2(transform.position.x - transform.lossyScale.x / 2, transform.position.y - transform.lossyScale.y / 2 - 0.6F), new Vector2(transform.position.x + transform.lossyScale.x / 2, transform.position.y + transform.lossyScale.y / 2));

        foreach (var collider in colliders)
        {
            if (collider.gameObject.CompareTag("Ground"))
            {
                isGrounded = true;
                break;
            }
            else
            {
                isGrounded = false;
            }
        }

        if (!isGrounded)
        {
            if (isClimbing && Input.GetButton("Vertical") && !isDead)
            {
                State = CharState.Jump;
            }
            else if (isClimbing && !isDead)
            {
                State = CharState.Idle;
            }
            else if (!isDead)
            {
                State = CharState.Jump;
            }
        }
    }

    private void CheckLadder()
    {
        hitInfo = Physics2D.Raycast(transform.position, Vector2.up, distance, whatIsLadder);

        if (hitInfo.collider != null)
        {
            isClimbing = true;
        }
        else
        {
            isClimbing = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Coins"))
        {
            Destroy(collision.gameObject);
        }

        if (collision.gameObject.CompareTag("FinishLevel"))
        {
            FindObjectOfType<GameManager>().EndGame();
        }
    }

    public override void ReceiveDamage()
    {
        Lives--;
        audioSource.PlayOneShot(audioClip);
    }

    public override void Die()
    {
        State = CharState.Die;
        Destroy(gameObject.GetComponent<SpriteRenderer>(), 1f);
        FindObjectOfType<GameManager>().EndGame();
    }

    public bool CheckPick()
    {
        return hasPick;
    }

}


public enum CharState
{
    Idle,
    Run,
    Jump,
    Die
}
