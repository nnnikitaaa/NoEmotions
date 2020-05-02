using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimationManager : MonoBehaviour
{
    Animator animator;
    int idleTrigger;
    int runTrigger;
    int landTrigger;
    int runBool;

    float direction;
    private void Awake()
    {
        #region Getting

        animator = GetComponent<Animator>();

        #endregion
    }

    private void Start()
    {
        idleTrigger = Animator.StringToHash("Idle");
        runTrigger = Animator.StringToHash("Run");
        landTrigger = Animator.StringToHash("Land");
        runBool = Animator.StringToHash("RunBool");
    }
    public void Move(float dir)
    {
        direction = dir;
        if (direction != 0)
        {
            animator.SetTrigger(runTrigger);
        }
        else
        {
            animator.SetTrigger(idleTrigger);
        }

        // handle flip
        if (transform.localScale.x > 0 && direction < 0)
        {
            Flip();
        }
        else if (transform.localScale.x < 0 && direction > 0)
        {
            Flip();
        }
    }
    public void Land()
    {
        animator.SetTrigger(landTrigger);
        if (direction != 0)
        {
            animator.SetBool(runBool, true);
        }
        else
        {
            animator.SetBool(runBool, false);
        }
    }
    void Flip()
    {
        Vector2 scale = transform.localScale;
        scale.x *= -1;
        transform.localScale = scale;
    }
}
