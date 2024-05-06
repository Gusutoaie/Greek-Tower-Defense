using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;


    [Header("Attributes")]
    [SerializeField] private static float bulletSpeed = 5f;
    [SerializeField] private static float bulletDamage = 1;
    private Transform target;

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public static void ChangebulletSpeed(float _bulletSpeed)
    {
        bulletSpeed = _bulletSpeed;
    }

    //change bullet damage from another script 

    public static void SetDamage(float _damage)
    {
        bulletDamage = _damage;
    }


    private void FixedUpdate()
    {
        if (target == null || rb == null)
        {
            return;  // Early exit if no target or Rigidbody2D is not set
        }

        // Move directly towards the target
        float step = bulletSpeed * Time.fixedDeltaTime;
        Vector2 direction = (target.position - transform.position).normalized;
        rb.position = Vector2.MoveTowards(rb.position, target.position, step);

        // Optional: Update rotation each frame to keep the bullet oriented correctly
        OrientTowardsTarget();
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        other.gameObject.GetComponent<HealthScript>().TakeDamage(bulletDamage);
       

        Destroy(gameObject);  // Destroy the bullet on any collision
    }

    private void OrientTowardsTarget()
    {
        if (target != null)
        {
            Vector2 targetDirection = (target.position - transform.position).normalized;
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg - 90f; // Adjusting -90 degrees if your bullet's 'forward' is up
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }
}
