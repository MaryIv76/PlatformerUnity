using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject parent;
    public GameObject Parent
    {
        set
        {
            parent = value;
        }
    }
    private float speed = 10.0F;
    private Vector3 direction;
    private float force = 1.4F;
    public float Force
    {
        set
        {
            force = value;
        }
    }

    private SpriteRenderer sprite;
    public Color Color
    {
        set
        {
            sprite.color = value;
        }
    }

    public Vector3 Direction
    {
        set
        {
            direction = value;
        }
    }

    private void Awake()
    {
        sprite = GetComponent<SpriteRenderer>();
    }

    private void Start()
    {
        Destroy(gameObject, force);
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, transform.position + direction, speed * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        Unit unit = collider.GetComponent<Unit>();
        if(unit && unit.gameObject != parent)
        {
            if (unit is Character)
            {
                unit.ReceiveDamage();
            }
            Destroy(gameObject);
        }
    }
}
