using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 1f;
    public float collisionOffset = 0.05f;
    public SwordAttack swordAttack;

    private readonly string moveAnimationParameter = "move";
    private readonly string attackAnimationParameter = "swordAttack";
    private bool playerCanMove = true;

    private Vector2 movementInput;
    private ContactFilter2D movementFilter;

    private Rigidbody2D rb;
    private List<RaycastHit2D> castCollisions = new List<RaycastHit2D>();
    private Animator playerAnimator;
    private SpriteRenderer spriteRenderer;
  
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void FixedUpdate()
    {
        PlayerMovement();
        AdjustAnimationMovementDirection();
    }

    public void SwordAttack()
    {
        playerCanMove = false;

        if (spriteRenderer.flipX == true)
            swordAttack.MoveSwordHitBoxLeft();
        else
            swordAttack.MoveSwordHitBoxRight();
    }

    public void EndSwordAttack()
    {
        UnlockMovement();
        swordAttack.StopAttack();
    }

    public void UnlockMovement()
    {
        playerCanMove = true;
    }

    private void PlayerMovement()
    {
        if(playerCanMove)
            if (movementInput != Vector2.zero)
            {
                bool success = TryMove(movementInput);

                if (!success && movementInput.x > 0)
                {
                    success = TryMove(new Vector2(movementInput.x, 0));
                }

                if (!success && movementInput.y > 0)
                {
                    success = TryMove(new Vector2(0, movementInput.y));
                }

                playerAnimator.SetBool(moveAnimationParameter, success);
            }
            else
            {
                playerAnimator.SetBool(moveAnimationParameter, false);
            }
    }

    private void AdjustAnimationMovementDirection()
    {
        if(movementInput.x < 0)
        {
            spriteRenderer.flipX = true;
        }else if(movementInput.x > 0)
        {
            spriteRenderer.flipX = false;
        }
    }

    private bool TryMove(Vector2 direction)
    {
        int count = rb.Cast(
                direction,
                movementFilter,
                castCollisions,
                moveSpeed * Time.fixedDeltaTime + collisionOffset);

        if (count == 0)
        {
            rb.MovePosition(rb.position + direction * moveSpeed * Time.fixedDeltaTime);
            return true;
        }
        else
        {
            return false;
        }
    }

    private void OnMove(InputValue movementValue)
    {
        movementInput = movementValue.Get<Vector2>();
    }

    private void OnFire()
    {
        playerAnimator.SetTrigger(attackAnimationParameter);
    }
}
