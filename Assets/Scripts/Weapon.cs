using UnityEngine;

public class Weapon : MonoBehaviour
{
    public string weaponName = "";
    public float damage;

    private Collider2D weaponCollider;
    private Vector2 playerRightAttackOffset;

    void Start()
    {
        playerRightAttackOffset = transform.position;
        weaponCollider = GetComponent<Collider2D>();
    }

    public void MoveSwordHitBoxRight()
    {
        weaponCollider.enabled = true;
        transform.localPosition = playerRightAttackOffset;
    }

    public void MoveSwordHitBoxLeft()    
    {
        weaponCollider.enabled = true;
        transform.localPosition = new Vector3(playerRightAttackOffset.x * -1, playerRightAttackOffset.y);
    }

    public void StopAttack()
    {
        weaponCollider.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D otherObjectWithCollider)
    {
        if (otherObjectWithCollider.tag == "Enemy")
        {
            Enemy enemy = otherObjectWithCollider.GetComponent<Enemy>();

            if (enemy != null)
                enemy.enemyHealth -= damage;
        }
    }
}
