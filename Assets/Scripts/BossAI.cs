using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem.UI;

public class BossAI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float hp;
    public GameObject Player;
    public GameObject Rocks;
    public Collider2D bossHitbox;
    private Rigidbody2D rb;
    private Animator animator;
    private Collider2D attackHitbox;
    //private Collider2D modelHitbox;
    public float speed;
    private float hitAnimTiming= 0.4f;
    private float attackSpeed = 3f;
    private float distance;
    private bool isAttacking = false;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        attackHitbox = transform.Find("BasicAttackHitbox").GetComponent<Collider2D>();
        //modelHitbox = transform.Find("Boss").GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (!isAlive())
        {
            Debug.Log("Deadge");
            animator.SetBool("Death", true);
            attackHitbox.enabled = false;
            bossHitbox.enabled = false;
            //modelHitbox.enabled = false;
            return;
        }
        distance = Vector2.Distance(transform.position, Player.transform.position);
        speed = 0f;

        if (!isAttacking) { 
            if (distance < 6)
            {
                speed = 1f;
                transform.position = Vector2.MoveTowards(this.transform.position, Player.transform.position, speed * Time.deltaTime);
            }
            if (distance < 2.5f)
            {
                StartCoroutine(Attack(attackSpeed));
            }
            HandleFacingDirection();
        }
        animator.SetFloat("Speed", speed);
    }

    private bool isAlive()
    {
        return hp > 0;
    }

    private IEnumerator Attack(float seconds)
    {
        if (!isAttacking) { 
            isAttacking = true;
            Debug.Log("attacked");
            animator.SetTrigger("Attack");
            yield return new WaitForSeconds(hitAnimTiming);
            DisableAttackHitbox();
            yield return new WaitForSeconds(seconds - hitAnimTiming);
            isAttacking = false;
        }
    }

    private void HandleFacingDirection()
    {
        float direction = Player.transform.position.x - transform.position.x;

        if (direction < 0) // player is to the left
            transform.localScale = new Vector3(3, 3, 1);
        else if (direction > 0) // player is to the right
            transform.localScale = new Vector3(-3, 3, 1);
    }
    public void TakeDamage(float damage)
    {
        Debug.Log("damaged");
        Debug.Log(hp);
        hp -= damage;
        animator.SetTrigger("Damage");
    }

    public void EnableAttackHitbox()
    {
        attackHitbox.enabled = true;
        isAttacking = true;
    }

    public void DisableAttackHitbox()
    {
        attackHitbox.enabled = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player"))
        {

            PlayerHealth player = other.GetComponent<PlayerHealth>();
            if (player!= null) {
                player.TakeDamage();
            }
        }
    }

    private void OnDeathAnimationEnd()
    {
        Destroy(gameObject);
        Destroy(Rocks);
    }
}
