using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerControler : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 4;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool crouch;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    protected override void ComputeVelocity()
    {
        Vector2 move = Vector2.zero;

        move.x = Input.GetAxis("Horizontal");

        if (Input.GetButtonDown ("Jump") && grounded)
        {
            velocity.y = jumpTakeOffSpeed;
        }
        else if(Input.GetButtonUp("Jump"))
        {
            if (velocity.y > 0) velocity.y = velocity.y * .5f;
        }

        if(Input.GetKey("down"))
        {
            crouch = true;
            velocity.x = velocity.x / 2f;
        }
        else
        {
            crouch = false;
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("speed", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetBool("crouch", crouch);

        targetVelocity = move * maxSpeed;
    }
}
