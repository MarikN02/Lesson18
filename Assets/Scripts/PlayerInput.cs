using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Shooter shooter; // ��������� ������ �� Shooter
    [SerializeField] private PlayerAnimator playerAnimator;

    private void Awake()
    {
        // ������������� ������� ���������� ���� �� ��������� � ����������
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
        if (shooter == null) shooter = GetComponent<Shooter>();
        if (playerAnimator == null) playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        // ��������
        float horizontalDirection = Input.GetAxis("Horizontal");
        bool isJumpButtonPressed = Input.GetButtonDown("Jump");

        playerMovement.Move(horizontalDirection, isJumpButtonPressed);

        // �������� � �������� �����
        // �������� ����� - ������ ������ ��������
        if (Input.GetButtonDown("Fire1"))
        {
            playerAnimator.TriggerAttackAnimation(); // ������ ��������
                                                     // shooter.Shoot() ������ �� �������� �����!
        }
    }
}