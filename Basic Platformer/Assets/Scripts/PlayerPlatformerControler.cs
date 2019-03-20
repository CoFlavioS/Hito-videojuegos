using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPlatformerControler : PhysicsObject
{
    public float maxSpeed = 7;
    public float jumpTakeOffSpeed = 4;
    public CapsuleCollider2D colider;
    public bool cellingCheck;
    public bool crouch;

    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private Vector2 originS;
    private Vector2 crouchS;
    private Vector2 originO;
    private Vector2 crouchO;

    void Awake()
    {
        originS.Set(0.9458637f, 1.75528f);
        crouchS.Set(0.9458637f, 0.9458637f);
        originO.Set(-0.02999997f, -0.1402209f);
        crouchO.Set(-0.02999997f, -0.5427848f);
        colider.size = originS;
        colider.offset = originO;
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

        if(Input.GetKey(KeyCode.DownArrow))
        {
            crouch = true;
            colider.size = crouchS;
            colider.offset = crouchO;
        }
        else if (crouch && !cellingCheck)
        {
            crouch = false;
            colider.size = originS;
            colider.offset = originO;
        }

        bool flipSprite = (spriteRenderer.flipX ? (move.x > 0.01f) : (move.x < 0.01f));
        if (flipSprite)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        animator.SetBool("grounded", grounded);
        animator.SetFloat("speed", Mathf.Abs(velocity.x) / maxSpeed);
        animator.SetBool("crouch", crouch);


        if (crouch == true)
        {

            targetVelocity = move * (maxSpeed / 2f);
        }
        else
        {
            targetVelocity = move * maxSpeed;
        }
    }
}
