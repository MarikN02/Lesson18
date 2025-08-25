using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMovement))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator animator;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private bool hasAnimator;

    // ����� ����������
    private const string isRunningParam = "isRunning"; // Int
    private const string isJumpingParam = "isJumping"; // Trigger
    private const string isGroundedParam = "isGrounded"; // Bool
    private const string isAttackParam = "isAttack"; // Trigger ��� �����

    private void Awake()
    {
        playerMovement = GetComponent<PlayerMovement>();
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        hasAnimator = animator != null && animator.runtimeAnimatorController != null;

        if (!hasAnimator)
        {
            Debug.LogWarning("Animator not found or not configured!");
        }
    }

    private void Update()
    {
        if (hasAnimator)
        {
            UpdateAnimatorParameters();
        }
    }

    public void TriggerJumpAnimation()
    {
        if (hasAnimator && HasParameter(isJumpingParam))
        {
            animator.SetTrigger(isJumpingParam);
        }
    }

    // ����� ����� ��� �������� �����
    public void TriggerAttackAnimation()
    {
        if (hasAnimator && HasParameter(isAttackParam))
        {
            animator.SetTrigger(isAttackParam);
            Debug.Log("Attack animation triggered!");
        }
    }

    private void UpdateAnimatorParameters()
    {
        bool isGrounded = playerMovement.IsGrounded;
        float horizontalSpeed = Mathf.Abs(rb.velocity.x);

        // isRunning - Int �������� (0 - �����, 1 - �����)
        int runningState = (horizontalSpeed > 0.1f && isGrounded) ? 1 : 0;
        animator.SetInteger(isRunningParam, runningState);

        // isGrounded - Bool ��������
        animator.SetBool(isGroundedParam, isGrounded);
    }

    // ���������� �������� ������������� ���������
    private bool HasParameter(string paramName)
    {
        foreach (AnimatorControllerParameter param in animator.parameters)
        {
            if (param.name == paramName) return true;
        }
        return false;
    }
}