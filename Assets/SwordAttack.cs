using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public Collider2D swordCollider;

    private Vector2 playerRightAttackOffset;
    private float damage = 0.5F;

    void Start()
    {
        playerRightAttackOffset = transform.position;
    }

    public void MoveSwordHitBoxRight()
    {
        swordCollider.enabled = true;
        transform.localPosition = playerRightAttackOffset;
    }

    public void MoveSwordHitBoxLeft()    
    {
        swordCollider.enabled = true;
        transform.localPosition = new Vector3(playerRightAttackOffset.x * -1, playerRightAttackOffset.y);
    }

    public void StopAttack()
    {
        swordCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D otherObjectWithCollider)
    {
        if (otherObjectWithCollider.tag == "Enemy")
        {
            Enemy enemy = otherObjectWithCollider.GetComponent<Enemy>();

            if (enemy != null)
                enemy.health -= damage;
        }
    }
}
