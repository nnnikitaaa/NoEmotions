using UnityEngine;
[RequireComponent(typeof(AnimationManager))]
[RequireComponent(typeof(RigidbodyMovement))]
public class EnemyBrain : MonoBehaviour
{
    [SerializeField] [Range(-1f, 1f)] float direction;
    [SerializeField] float waitTime;
    [SerializeField] float raycastDistance;
    enum State { Idle, Go }
    State state = State.Idle;
    RigidbodyMovement rbMove;
    AnimationManager animManager;
    Transform groundCheck;
    float delay;

    private void Awake()
    {
        #region Getting

        rbMove = GetComponent<RigidbodyMovement>();
        animManager = GetComponent<AnimationManager>();
        groundCheck = transform.Find("Ground Check");

        #endregion
    }
    private void Start()
    {
        delay = 1f;
    }
    private void Update()
    {
        switch (state)
        {
            case State.Go:
                if (!GroundCheck())
                {
                    SetIdle();
                }
                break;
            case State.Idle:
                if (delay <= 0)
                {
                    SetGo();
                }
                else
                    delay -= Time.deltaTime;
                break;
        }
    }
    void SetIdle()
    {
        SetVelocity(0);
        delay = waitTime;
        state = State.Idle;
    }
    void SetGo()
    {
        direction = -direction;
        SetVelocity(direction);
        state = State.Go;
    }
    bool GroundCheck()
    {
        return Physics2D.Raycast(groundCheck.position, Vector2.down, raycastDistance, GameManager.GROUND_LAYERMASK);
    }
    void SetVelocity(float velocity)
    {
        rbMove.velocity = velocity;
        animManager.Move(velocity);
    }
    private void OnDrawGizmos()
    {
        if (groundCheck)
        {
            Gizmos.DrawLine(groundCheck.position, groundCheck.position + Vector3.down * raycastDistance);
        }
    }
}
