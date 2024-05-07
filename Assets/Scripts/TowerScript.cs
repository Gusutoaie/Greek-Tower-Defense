using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEditor;
using UnityEngine;

public class TowerScript : MonoBehaviour
{



    [Header("Attribute")]
    [SerializeField] private float targetingRange = 5f;


    [Header("References")]
    public Transform firingPoint; // Public or serialized for assignment


    [SerializeField] private float bps = 1f;  // Bullets per second





  

    private float timeUntilFire;


    private Transform target;

    private void Update()
    {




        if (target == null)
        {

            FindTarget();
            return;
        }

        if (!CheckTargetIsInRange())
        {
            target = null;

        }
        else
        {
            timeUntilFire += Time.deltaTime;
            if (timeUntilFire >= 1f / bps)
            {
                Shoot();
                timeUntilFire = 0f;

            }
        }
        
       
    }

    private void Shoot()
    {
        if (firingPoint == null)
        {
            return;
        }
        GameObject bulletObj = Instantiate( LevelScript.main.bulletPrefab, firingPoint.position, Quaternion.identity);
        Bullet bulletScript = bulletObj.GetComponent<Bullet>();
        bulletScript.SetTarget(target);


    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
        
  
    }


    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, LevelScript.main.enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }

    }

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position,transform.forward ,targetingRange);
    }
}


