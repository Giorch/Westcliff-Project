using UnityEngine;

public class Victory : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Animator _animator;
    public PlayerMovement playerMovement;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnCollisionEnter2D(Collision2D collision)
    {
       if(collision.gameObject.tag == "Player")
        {
            playerMovement.enabled = false;
            _animator.SetBool("isDead", true);
        }

    }
}
