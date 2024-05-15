using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private AudioSource audioSource;  // Reference to the AudioSource component


    [Header("Attributes")]
    [SerializeField] private  float bulletSpeed = 4f;
    [SerializeField] private  float bulletDamage = 1;
    private Transform target;
    

    public void SetTarget(Transform _target)
    {
        target = _target;
    }

    public float BulletSpeed
    {
        get { return bulletSpeed; }
        set { bulletSpeed = value; }
    }

    public float BulletDamage
    {
        get { return bulletDamage; }
        set { bulletDamage = value; }
    }

    public void Initialize(float speed, float damage)
    {
        bulletSpeed = speed;
        bulletDamage = damage;
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
        if (audioSource != null)
        {
            audioSource.Play();
        }


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
