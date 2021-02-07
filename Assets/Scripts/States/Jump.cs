using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : State
{
    private float _JumpHeight = 1;
    private int _JumpCount = 0;

    private BoxCollider2D _BoxCollider;
    private Transform _Transform;
    private Vector2 _Velocity;

    public Jump(StateMachine stateMachine, float jumpHeight) : base(stateMachine)
    {
        _JumpHeight = jumpHeight;
    }

    public override void OnEnter()
    {
        _JumpCount = 0;

        _BoxCollider = GetGameObject().GetComponent<BoxCollider2D>();
        _Transform = GetGameObject().transform;
    }

    public override void OnExit()
    {

    }

    public override void Update()
    {
        bool grounded = false;
        Collider2D[] hits = Physics2D.OverlapBoxAll(_Transform.position, _BoxCollider.size, LayerMask.NameToLayer("Ground"));

        foreach (Collider2D hit in hits)
        {
            if (hit == _BoxCollider)
                continue;

            ColliderDistance2D colliderDistance = hit.Distance(_BoxCollider);

            if (colliderDistance.isOverlapped)
            {
                _Transform.Translate(colliderDistance.pointA - colliderDistance.pointB);

                if (Vector2.Angle(colliderDistance.normal, Vector2.up) < 90 && _Velocity.y < 0)
                {
                    grounded = true;
                }
            }
        }

        if (grounded)
        {
            ++_JumpCount;
            _Velocity.y = 0;
            _Velocity.y = Mathf.Sqrt(2 * _JumpHeight * Mathf.Abs(Physics2D.gravity.y));
        }
        else
        {
            // In this example this isn't needed but I'm leaving it in.
            _Velocity.y += Physics2D.gravity.y * Time.deltaTime;
        }
        
        _Transform.Translate(_Velocity * Time.deltaTime);
    }

    public int GetJumpCount()
    {
        return _JumpCount;
    }
}
