using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float Speed = 2;
    public float Acceleration = 1;
    public float JumpHeight = 1;

    private Vector2 _Velocity;
    private bool _Grounded = true;
    private BoxCollider2D _BoxCollider;

    // Start is called before the first frame update
    void Start()
    {
        _BoxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float moveInput = Input.GetAxisRaw("Horizontal");
        if (moveInput != 0)
        {
            _Velocity.x = Mathf.MoveTowards(_Velocity.x, Speed * moveInput, Acceleration * Time.deltaTime);
        }
        else
        {
            _Velocity.x = Mathf.MoveTowards(_Velocity.x, 0, 100 * Time.deltaTime);
        }

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, _BoxCollider.size, 0);

        foreach (Collider2D hit in hits)
        {
            if (hit == _BoxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(_BoxCollider);

            if (colliderDistance.isOverlapped)
            {
                transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && _Velocity.y < 0)
                {
                    _Grounded = true;
                }
            }
        }

        if (_Grounded)
        {
            _Velocity.y = 0;

            if (Input.GetButtonDown("Jump"))
            {
                _Grounded = false;
                _Velocity.y = Mathf.Sqrt(2 * JumpHeight * Mathf.Abs(Physics2D.gravity.y));
            }
        }
        else
        {
            _Velocity.y += Physics2D.gravity.y * Time.deltaTime;
        }
        transform.Translate(_Velocity * Time.deltaTime);
    }
}
