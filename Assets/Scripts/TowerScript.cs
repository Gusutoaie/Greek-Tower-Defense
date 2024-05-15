using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TowerScript : MonoBehaviour
{
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private float maxDamage = 5f;
    [SerializeField] private float maxRange = 5f;
    [SerializeField] public LayerMask enemyMask;

    private float currentSpeed = 20f;
    private float currentDamage = 15f;
    private float currentRange = 3f;
    private float targetingRange = 4f;
    private float bps = 5f;  // Bullets per second

    public Transform firingPoint;
    public GameObject bulletPrefab; // Assign this through the inspector

    private int upgradeLevel = 1;
    private int upgradeCost = 100;
    private int maxUpgradeLevel = 5;
    private int speedLevel = 1;
    private int attackLevel = 1;
    private int rangeLevel = 1;
    private const int maxAbilityLevel = 5;
    private float timeUntilFire;
    private Transform target;

    public int UpgradeLevel => upgradeLevel;
    public int UpgradeCost => upgradeCost;
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        bool isOverUI = false;
        foreach (RaycastResult result in results)
        {
            foreach (GameObject panel in TowerManager.Instance.upgrateTowersPanels)
            {
                if (result.gameObject == panel)
                {
                    isOverUI = true;
                    break;
                }

            }
        }
        if (isOverUI)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
    private void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
        else if (!IsTargetInRange())
        {
            target = null;
        }
        else
        {
            HandleFiring();
        }
        if (Input.GetMouseButtonDown(0)) // Checks if the left mouse button was pressed
        {
            if (!IsPointerOverUIObject())
            {
                foreach (GameObject panel in TowerManager.Instance.upgrateTowersPanels)
                {
                    if (panel.activeSelf)
                    {
                        panel.SetActive(false);
                    }
                }
            }
        }
    }

    private void HandleFiring()
    {
        timeUntilFire += Time.deltaTime;
        if (timeUntilFire >= 1f / bps)
        {
            Shoot();
            timeUntilFire = 0;
        }
    }

    private void Shoot()
    {

        if (firingPoint != null)
        {
            GameObject bullet = Instantiate(bulletPrefab, firingPoint.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            if (bulletScript != null)
            {
                bulletScript.Initialize(currentSpeed, currentDamage);
                bulletScript.SetTarget(target);
            }
        }
    }

    private bool IsTargetInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
        }

    }



    public void UpgradeAttribute(TowerManager.TowerUpgradeType upgradeType, Transform CurrentTower, GameObject panel)
    {
        if (GameManager.Instance.SpendGold(upgradeCost))
        {
            bool isUpgradeSuccessful = false;
            /* if (CurrentTower.transform.GetComponent<Image>().sprite.name == "AphroditaTower1")
             {
                 switch (upgradeType)
                 {
                     case TowerManager.TowerUpgradeType.Speed:
                         if (speedLevel < maxAbilityLevel)
                         {
                             currentSpeed += 0.2f;
                             speedLevel++;
                             isUpgradeSuccessful = true;
                         }
                         break;
                     case TowerManager.TowerUpgradeType.Damage:
                         if (attackLevel < maxAbilityLevel)
                         {
                             currentDamage += 1f;
                             attackLevel++;
                             isUpgradeSuccessful = true;
                         }
                         break;
                     case TowerManager.TowerUpgradeType.Range:
                         if (rangeLevel < maxAbilityLevel)
                         {
                             currentRange += 0.5f;
                             targetingRange += 0.5f;
                             rangeLevel++;
                             isUpgradeSuccessful = true;
                         }
                         break;
                 }
             }
             else if (CurrentTower.transform.GetComponent<Image>().sprite.name == "DefaultTower")
             {
                 switch (upgradeType)
                 {
                     case TowerManager.TowerUpgradeType.Speed:
                         if (speedLevel < maxAbilityLevel)
                         {
                             currentSpeed += 0.2f;
                             speedLevel++;
                             isUpgradeSuccessful = true;
                         }
                         break;
                     case TowerManager.TowerUpgradeType.Damage:
                         if (attackLevel < maxAbilityLevel)
                         {
                             currentDamage += 1f;
                             attackLevel++;
                             isUpgradeSuccessful = true;
                         }
                         break;
                     case TowerManager.TowerUpgradeType.Range:
                         if (rangeLevel < maxAbilityLevel)
                         {
                             currentRange += 0.5f;
                             targetingRange += 0.5f;
                             rangeLevel++;
                             isUpgradeSuccessful = true;
                         }
                         break;
                 }
             }
             else if (CurrentTower.transform.GetComponent<Image>().sprite.name == "TowerHades1")
             {
                 switch (upgradeType)
                 {
                     case TowerManager.TowerUpgradeType.Speed:
                         if (speedLevel < maxAbilityLevel)
                         {
                             currentSpeed += 0.2f;
                             speedLevel++;
                             isUpgradeSuccessful = true;
                         }
                         break;
                     case TowerManager.TowerUpgradeType.Damage:
                         if (attackLevel < maxAbilityLevel)
                         {
                             currentDamage += 1f;
                             attackLevel++;
                             isUpgradeSuccessful = true;
                         }
                         break;
                     case TowerManager.TowerUpgradeType.Range:
                         if (rangeLevel < maxAbilityLevel)
                         {
                             currentRange += 0.5f;
                             targetingRange += 0.5f;
                             rangeLevel++;
                             isUpgradeSuccessful = true;
                         }
                         break;
                 }
             }   */
            switch (upgradeType)
            {
                case TowerManager.TowerUpgradeType.Speed:
                    if (speedLevel < maxAbilityLevel)
                    {
                        currentSpeed += 0.2f;
                        speedLevel++;
                        isUpgradeSuccessful = true;
                    }
                    break;
                case TowerManager.TowerUpgradeType.Damage:
                    if (attackLevel < maxAbilityLevel)
                    {
                        currentDamage += 1f;
                        attackLevel++;
                        isUpgradeSuccessful = true;
                    }
                    break;
                case TowerManager.TowerUpgradeType.Range:
                    if (rangeLevel < maxAbilityLevel)
                    {
                        currentRange += 0.5f;
                        targetingRange += 0.5f;
                        rangeLevel++;
                        isUpgradeSuccessful = true;
                    }
                    break;
            }
            panel.SetActive(false);
            if (isUpgradeSuccessful)
            {
                upgradeCost += 50;  // Increase the cost for the next upgrade
                Debug.Log($"Tower upgraded: {upgradeType}. New stats: Speed {currentSpeed}, Damage {currentDamage}, Range {currentRange}, Levels: Speed {speedLevel}, Attack {attackLevel}, Range {rangeLevel}");
                Debug.Log($"upgradeCost=: {upgradeCost}");
                Transform infoPanel = CurrentTower.transform.Find("InfoPanel");
                infoPanel.Find("Speed").GetComponent<Text>().text = $"Speed: {currentSpeed}";
                infoPanel.Find("Range").GetComponent<Text>().text = $"Range: {currentRange}";
                infoPanel.Find("Damage").GetComponent<Text>().text = $"Damage: {currentDamage}";
                CheckMaxUpgrade(CurrentTower.transform.position);
            }
            else
            {
                Debug.Log("Ability already at max level or not enough gold.");
            }
        }
        else
        {
            Debug.Log("Not enough gold to upgrade.");
        }
    }

    private void CheckMaxUpgrade(Vector3 position)
    {
        if (speedLevel == maxAbilityLevel && attackLevel == maxAbilityLevel && rangeLevel == maxAbilityLevel)
        {
            TowerManager.Instance.TowerMaximumUpgraded(position);
        }
    }
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.red;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}