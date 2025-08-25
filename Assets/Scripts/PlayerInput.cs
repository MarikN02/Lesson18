using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Shooter shooter; // Добавляем ссылку на Shooter
    [SerializeField] private PlayerAnimator playerAnimator;

    private void Awake()
    {
        // Автоматически находим компоненты если не назначены в инспекторе
        if (playerMovement == null) playerMovement = GetComponent<PlayerMovement>();
        if (shooter == null) shooter = GetComponent<Shooter>();
        if (playerAnimator == null) playerAnimator = GetComponent<PlayerAnimator>();
    }

    private void Update()
    {
        // Движение
        float horizontalDirection = Input.GetAxis("Horizontal");
        bool isJumpButtonPressed = Input.GetButtonDown("Jump");

        playerMovement.Move(horizontalDirection, isJumpButtonPressed);

        // Стрельба и анимация атаки
        // Упрощаем вызов - только запуск анимации
        if (Input.GetButtonDown("Fire1"))
        {
            playerAnimator.TriggerAttackAnimation(); // Только анимация
                                                     // shooter.Shoot() больше не вызываем здесь!
        }
    }
}