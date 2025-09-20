using System.Linq;
using UnityEngine;
using System.Collections;

public class PlayerCombat : MonoBehaviour
{

    private enum AttackDirection
    {
        Forward,
        Up,
        Down
    }

    private Animator animator;
    private PlayerInput input;
    private PlayerMovement movement;
    private Collider2D[] attackHitBoxes;
    private bool isAttacking = false;
    private float attackSpeed = 0.4f; // seconds

    public float damage = 1f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        input = GetComponent<PlayerInput>();
        movement= GetComponent<PlayerMovement>();
        animator = GetComponent<Animator>();
        attackHitBoxes = GetComponentsInChildren<Collider2D>()
            .Where(c => c.gameObject != gameObject)
            .ToArray();
    }


    // Update is called once per frame
    private void Update()
    {
        if (!isAttacking && input.AttackPressed)
        {
            isAttacking = true;
            // If mid air
            if (!movement.isGrounded && input.AttackDirectionY < 0)
                    animator.SetTrigger("AttackDown");
            else if (input.AttackDirectionY > 0)
                animator.SetTrigger("AttackUp");
            else
                animator.SetTrigger("Attack");

            StartCoroutine(DelayAttack()); 
        }
    }

    private IEnumerator DelayAttack()
    {
        yield return new WaitForSeconds(attackSpeed);
        isAttacking = false;
    }

    private void EnableAttackHitBox()
    {
        attackHitBoxes[(int)AttackDirection.Forward].enabled = true;
    }

    private void EnableUpSlashHitbox()
    {
        attackHitBoxes[(int)AttackDirection.Up].enabled = true;
    }

    private void EnableDownSlashHitbox()
    {
        Debug.Log("enabling down");
        attackHitBoxes[(int)AttackDirection.Down].enabled = true;
    }

    public void DisableAllHitBoxes()
    {
        Debug.Log("Disabling all");
        foreach (var hitbox in attackHitBoxes)
        {
            hitbox.enabled = false;
        }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Enemy"))
        {
            var downSlash = attackHitBoxes[(int)AttackDirection.Down];
            if (downSlash.enabled && downSlash.IsTouching(other))
            {
                movement.isJumping = false;
                movement.Pogo();
            }
            BossAI enemy = other.GetComponent<BossAI>();
            if (enemy.hp == 0)
                return;
            if (enemy != null) {
                foreach (var hitbox in attackHitBoxes)
                {
                    if (hitbox.enabled && hitbox.IsTouching(other)) { 
                            enemy.TakeDamage(damage);
                    }
                }
            }
        }
    }
}
