using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public InputSystem_Actions actions;
    private float xPosLastFrame;
    public float speed;
    public float jumpForce;
    public float maxSpeed;
    float move;
    Rigidbody2D rb;
    public Transform groundCheckTransform;
    public float groundCheckRadius;
    public LayerMask groundLayer;
    bool isGrounded;
    SpriteRenderer spriteRenderer;
    [SerializeField] private Animator _animator;
    private void Awake()
    {
        xPosLastFrame = transform.position.x;
        actions = new InputSystem_Actions();
    }

    private void OnEnable()
    {
        actions.Player.Enable();
        actions.Player.Move.performed += Movement;
        actions.Player.Jump.performed += Jumping;
        actions.Player.Sprint.performed += Running;

        actions.Player.Move.canceled += Movement;
        actions.Player.Jump.canceled += Jumping;
    }
    private void OnDisable()
    {
        actions.Player.Disable();
        actions.Player.Move.performed -= Movement;
        actions.Player.Jump.performed -= Jumping;
    }

    void Running (InputAction.CallbackContext ctx)
    {
        if (ctx.performed && isGrounded)
        {
            
        }
        else if (!isGrounded)
        {
            
        }
    }
    void Movement(InputAction.CallbackContext ctx)
    {
        move = ctx.ReadValue<Vector2>().x;
        if (move != 0)
        {
            _animator.SetBool("isWalking", true);
        }
        else
        {
            _animator.SetBool("isWalking", false);
        }
    }

    void Jumping(InputAction.CallbackContext ctx)
    {
        if(ctx.performed && isGrounded)
        {
            rb.linearVelocityY = jumpForce;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayer);
        if (transform.position.x > xPosLastFrame)
        {
            spriteRenderer.flipX = false;
        }
        else if (transform.position.x < xPosLastFrame)
        {
            spriteRenderer.flipX = true;
        }
        xPosLastFrame = transform.position.x;
    }
    private void FixedUpdate()
    {
        Run();
    }
    private void Run()
    {
        var velocity = move * speed;
        rb.linearVelocityX = velocity;
        

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
    }
}
