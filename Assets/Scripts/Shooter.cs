using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private GameObject bullet;
    [SerializeField] private float fireSpeed;
    [SerializeField] private Transform firePoint;

    // Метод который будет вызываться из анимации
    public void OnAnimationShootEvent()
    {
        PerformShoot();
        Debug.Log("Выстрел из анимации!");
    }

    // Основной метод выстрела
    private void PerformShoot()
    {
        GameObject currentBullet = Instantiate(bullet, firePoint.position, Quaternion.identity);
        Rigidbody2D currentBulletVelocity = currentBullet.GetComponent<Rigidbody2D>();

        float direction = Mathf.Sign(transform.localScale.x);

        currentBulletVelocity.velocity = new Vector2(fireSpeed * direction, currentBulletVelocity.velocity.y);

        if (direction < 0)
        {
            FlipBullet(currentBullet);
        }
    }

    private void FlipBullet(GameObject bulletObject)
    {
        Vector3 scale = bulletObject.transform.localScale;
        scale.x *= -1;
        bulletObject.transform.localScale = scale;
    }

    // Старый метод для обратной совместимости
    public void Shoot()
    {
        // Можно оставить пустым или вызвать анимацию
        Debug.Log("Запуск анимации атаки...");
    }
}