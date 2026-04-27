using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health;
    public int maxHealth = 10;

    public SpriteRenderer spriteRenderer;
    public PlayerMovement playerMovement;
    public Transform spawnPoint;
    Rigidbody2D rb;
    [SerializeField] private Animator _animator;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    // Update is called once per frame
    public void TakeDamage(int amount)
    {
        health -= amount;
        playerMovement.enabled = false;
        if(health <= 0)
        {
            GetComponent<BoxCollider2D>().enabled = false;
            rb.gravityScale = 0;
            _animator.SetBool("isDead", true);
            rb.linearVelocityY = 1;
            rb.linearVelocityX = 1;
            return;
        }
        _animator.SetBool("isDamaged", true);
        
        
        rb.linearVelocityY = 10;
        rb.linearVelocityX = 10;
    }
    public void DamageEnd()
    {
        _animator.SetBool("isDamaged", false);
        playerMovement.enabled = true;
    }
    private IEnumerator Waiter()
    {
        spriteRenderer.enabled = false;
        yield return new WaitForSeconds(0.2f);
        transform.position = spawnPoint.position;
        yield return new WaitForSeconds(1);
        spriteRenderer.enabled = true;
        playerMovement.enabled = true;
        GetComponent<BoxCollider2D>().enabled = true;
        rb.gravityScale = 2.99f;
        _animator.SetBool("isDead", false);
        health = maxHealth;
    }
    public void Respawn()
    {
        StartCoroutine(Waiter());

    }
}
