using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{

    [Header("References")]
    [SerializeField] private Rigidbody2D rb;

    [Header("Attributes")]
    [SerializeField] private static float moveSpeed = 2f;
 
    private Transform target;
    private int pathIndex = 0;
    private static int defaultLives = 3;

    // Start is called before the first frame update
    void Start()
    {
       target = LevelScript.main._enemyPaths[pathIndex];

    }

    public static EnemyMovement main;
    //do a static references to movespeed get and setter

    public static float MoveSpeed
    {
        get { return moveSpeed; }
        set { moveSpeed = value; }
    }


    // Update is called once per frame
    void Update()
    {
        if(Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;
            if(pathIndex >= LevelScript.main._enemyPaths.Length)
            {
                LevelScript.main.isEnemyDestroyedByTower = true;
                EnemySpawner.onEnemyDestroy.Invoke();
             
                
                defaultLives--;
                LevelScript.main.livesPanel.text =  defaultLives.ToString();
                
                Destroy(gameObject);

                if (defaultLives <= 0)
                {
                    //stop the game
                    Time.timeScale = 0;
                    LevelScript.main.GameStatusPanel.text = "Game Over!";
                    LevelScript.main.GameStatusPanel.color = Color.red;
                    LevelScript.main.MessagesPanel.SetActive(true);
                    LevelScript.main.InfoPanel.SetActive(false);

                    return;
                }
                return;
            }
            else
            {
                target = LevelScript.main._enemyPaths[pathIndex];

            }
        }
       
        
    }

    private void FixedUpdate()
    {
        if (target != null)
        {
            //check if gameobject is not destroyed before moving
            if (rb == null)
            {
                return;
            }


            // Move towards the target
            float step = moveSpeed * Time.fixedDeltaTime;
            rb.position = Vector2.MoveTowards(rb.position, target.position, step);

            // Calculate the direction to the target
            Vector2 targetDirection = (target.position - transform.position).normalized;

            // Calculate the angle to the target
            float angle = Mathf.Atan2(targetDirection.y, targetDirection.x) * Mathf.Rad2Deg;

            // Set the rotation of the enemy
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle )); // Subtract 90 if forward is the y-axis

           
        }

    }
}
