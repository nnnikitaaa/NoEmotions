using UnityEngine;

[RequireComponent(typeof(AnimationManager))]
[RequireComponent(typeof(RigidbodyMovement))]
[RequireComponent(typeof(ChangeManager))]
public class PlayerBrain : MonoBehaviour
{
    [Range(0, 1f)] [SerializeField] float radius;
    [SerializeField] float groundTime;

    Transform groundCheck;
    InputActions inputActions;
    RigidbodyMovement rbMove;
    AnimationManager animManager;
    ChangeManager changeManager;

    float waitTime;
    bool isGrounded;

    private void Awake()
    {
        #region Getting

        rbMove = GetComponent<RigidbodyMovement>();
        animManager = GetComponent<AnimationManager>();
        changeManager = GetComponent<ChangeManager>();

        groundCheck = transform.Find("Ground Check");
        #endregion
    }
    private void Start()
    {
        // handle input
        inputActions = new InputActions();
        inputActions.Enable();
        inputActions.Player.Move.performed += ctx => SetVelocity(ctx.ReadValue<float>());
        inputActions.Player.Jump.performed += ctx => Jump();
        inputActions.Player.Change.performed += ctx => changeManager.Change();
    }
    private void Update()
    {
        if (!Ground() && isGrounded)
        {
            if (waitTime <= 0)
                isGrounded = false;
            else
                waitTime -= Time.deltaTime;
        }
        else if (Ground() && !isGrounded)
        {
            // land
            isGrounded = true;
            waitTime = groundTime;
            animManager.Land();
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rbMove.Jump();
            waitTime = 0;
            SoudManager.instance.Play("Jump");
        }
    }
    bool Ground()
    {
        return Physics2D.OverlapCircle(groundCheck.position, radius, GameManager.GROUND_LAYERMASK);
    }
    void SetVelocity(float velocity)
    {
        rbMove.velocity = velocity;
        animManager.Move(velocity);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Friend"))
        {
            changeManager.RestoreHealth();
        }
        else if (other.CompareTag("Hurt"))
        {
            SoudManager.instance.Play("Hurt");
            Die();
        }
        else if (other.CompareTag("Portal"))
        {
            LevelLoader.instance.LoadNextLevel(1f);
            SoudManager.instance.Play("Succed");
            Destroy(other);
        }
    }
    void Die()
    {
        LevelLoader.instance.ReloadLevel(2f);
        changeManager.Die();
        animManager.Die();
        rbMove.StopForever();
        Destroy(this);
    }
    private void OnDisable()
    {
        inputActions?.Disable();
    }
    private void OnDrawGizmos()
    {
        if (groundCheck)
            Gizmos.DrawWireSphere(groundCheck.position, radius);
    }
}
