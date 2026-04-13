using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public GameObject pointA;
    public GameObject pointB;
    private Rigidbody2D rb;
    private Animator anim;
    private Transform currPoint;
    public float speed;
    private SpriteRenderer sp;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currPoint = pointB.transform;
        anim.SetBool("isWalking", true);
        sp = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 point = currPoint.position - transform.position;
        if (currPoint == pointB.transform)
        {
            rb.linearVelocity = new Vector2(speed, 0);
        }
        else
        {
            rb.linearVelocity = new Vector2(-speed, 0);
        }

        if(Vector2.Distance(transform.position, currPoint.position) < 0.5f && currPoint == pointB.transform)
        {
            currPoint = pointA.transform;
            sp.flipX = true;
        }
        if (Vector2.Distance(transform.position, currPoint.position) < 0.5f && currPoint == pointA.transform)
        {
            currPoint = pointB.transform;
            sp.flipX = false;
        }
    }
}
