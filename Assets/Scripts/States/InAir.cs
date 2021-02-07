using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InAir : State
{
    private BoxCollider2D _BoxCollider;
    private Transform _Transform;
    private bool _Grounded = false;
    private Vector2 _Velocity;

    public InAir(StateMachine stateMachine) : base(stateMachine)
    {

    }

    public override void OnEnter()
    {
        _BoxCollider = GetGameObject().GetComponent<BoxCollider2D>();
        _Transform = GetGameObject().transform;
    }

    public override void Update()
    {
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
                    _Grounded = true;
                }
            }
        }

        if (!_Grounded)
        {
            _Velocity.y += Physics2D.gravity.y * Time.deltaTime;
        }
        
        _Transform.Translate(_Velocity * Time.deltaTime);
    }

    public bool IsGrounded()
    {
        return _Grounded;
    }
}
