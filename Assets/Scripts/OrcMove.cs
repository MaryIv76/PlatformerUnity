using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrcMove : Monster
{
    [SerializeField]
    public float speed = 1;
    public bool moveRight;

    private Character character;
    private AudioSource audioSource;
    public AudioClip audioClip;

    protected override void Start()
    {
        character = FindObjectOfType<Character>();
        audioSource = character.GetComponent<AudioSource>();
    }

    // Update is called once per frame
    protected override void Update()
    {
        if (moveRight)
        {
            transform.Translate(2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(0.36f, 0.36f);

        }
        else
        {
            transform.Translate(-2 * Time.deltaTime * speed, 0, 0);
            transform.localScale = new Vector2(-0.36f, 0.36f);
        }
    }

    public int orcsValue = 1;

    protected override void OnTriggerEnter2D(Collider2D trig)
    {
        if (trig.gameObject.CompareTag("Turn"))
        {
            if (moveRight)
            {
                moveRight = false;
            }
            else
            {
                moveRight = true;
            }
        }

        Unit unit = trig.GetComponent<Unit>();

        if (unit && unit is Character)
        {
            if (Mathf.Abs(unit.transform.position.x - transform.position.x) < 0.85F)
            {
                ReceiveDamage();
                audioSource.PlayOneShot(audioClip);
                ScoreOrcsManager.instance.ChangeScore(orcsValue);
            }
            else
            {
                unit.ReceiveDamage();
            }
        }
    }
}
