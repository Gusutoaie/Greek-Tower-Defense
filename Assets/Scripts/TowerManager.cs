using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class TowerManager : MonoBehaviour
{
    public static TowerManager Instance;
    public GameObject towerShopPanel; // Reference to the Tower Shop Panel
    public GameObject PanelFromTowers;
    public GameObject[] upgrateTowersPanels;
    public List<GameObject> TowersCopy; // List to hold different levels or upgrades of the tower

    public enum TowerUpgradeType
    {
        Damage,
        Range,
        Speed
    }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
  
    public void ProcessTowerButton(TowerData data, Button towerButton)
    {
        if (data == null)
        {
            Debug.LogError("TowerData is null");
            return;
        }

        if (GameManager.Instance == null)
        {
            Debug.LogError("GameManager instance is null");
            return;
        }

        
        else if (GameManager.Instance.Gold >= data.cost)
        {
            PlaceNewTower(data, towerButton);
        }
        else
        {
            Debug.Log("Not enough gold!");
        }
    }

    private TowerScript CheckForExistingTower(Vector3 position)
    {
        // Improved raycast to check directly on the button's position
        RaycastHit2D hit = Physics2D.Raycast(position, Vector2.zero);
        if (hit.collider != null)
        {
            return hit.collider.GetComponent<TowerScript>();
        }
        return null;
    }

    private void PlaceNewTower(TowerData data, Button towerButton)
    {
        TowersCopy = data.towerPrefabs;
        GameManager.Instance.RemoveGold(data.cost);
        float tolerance = 0.1f; // Tolerance level for position comparison

        foreach (Transform child in PanelFromTowers.transform)
        {
            Debug.Log("Data Position = " + data.DataTowerPosition);
            Debug.Log("Button Position = " + child.transform.position);

            if (data.DataTowerPosition == child.transform.position)
            {
                Debug.Log("tower was clicked = "  +data.towerName);

                if (data.towerName == "PoseidonTowerButton")
                {
                    foreach (GameObject towerPrefab in data.towerPrefabs)
                    {

                        if(towerPrefab.GetComponent<Button>().name == "PoseidonTowerDefault")
                        {

                            child.transform.GetComponent<Image>().sprite = towerPrefab.transform.GetComponent<Image>().sprite;
                            child.transform.GetComponent<Button>().colors = ColorBlock.defaultColorBlock;
                            child.transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                            var currentX = child.transform.position.x;
                            var currentY = child.transform.position.y;
                            child.transform.position = new Vector3(currentX, currentY + 0.6f, 0);

                            child.transform.GetComponent<RectTransform>().sizeDelta = new Vector3(200, 200, 50);

                            // bring towerscript from towerprefab to my chid object 
                            TowerScript towerScript = towerPrefab.GetComponent<TowerScript>();
                            // now set to child object 
                            child.gameObject.AddComponent<TowerScript>();
                            child.gameObject.GetComponent<TowerScript>().bulletPrefab = towerPrefab.GetComponent<TowerScript>().bulletPrefab;
                            child.gameObject.GetComponent<TowerScript>().firingPoint = child.transform.Find("FiringPoint").transform;
                            child.gameObject.GetComponent<TowerScript>().enemyMask = towerPrefab.GetComponent<TowerScript>().enemyMask;



                            Transform infoPanel = child.transform.Find("InfoPanel");
                            infoPanel.Find("Speed").GetComponent<Text>().text = $"Speed: 3";
                            infoPanel.Find("Range").GetComponent<Text>().text = $"Range: 4";
                            infoPanel.Find("Damage").GetComponent<Text>().text = $"Damage: 3";
                            if (child.transform.GetComponent<Button>().name == "TowerPosition2")
                            {
                                infoPanel.position = new Vector3(currentX - 1.5f, currentY, 0);

                            }
                            else
                            {
                                infoPanel.position = new Vector3(currentX + 1.5f, currentY, 0);

                            }
                            if (infoPanel != null)
                            {
                                infoPanel.gameObject.SetActive(true);
                            }
                            child.transform.GetComponent<Button>().onClick.RemoveAllListeners();

                            child.transform.GetComponent<Button>().onClick.AddListener(() =>
                            {
                                DisplayUpgradeShopPanel(child, "PoseidonTowerUpgradePanel", child.gameObject.GetComponent<TowerScript>());
                            });
                        }
                    }

                }
                else if (data.towerName == "HadesTowerButton")
                { 

                        foreach (GameObject towerPrefab in data.towerPrefabs)
                        {

                            if (towerPrefab.GetComponent<Button>().name == "HadesTowerButtonDefault")
                            {

                                child.transform.GetComponent<Image>().sprite = towerPrefab.transform.GetComponent<Image>().sprite;
                                child.transform.GetComponent<Button>().colors = ColorBlock.defaultColorBlock;
                                child.transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                                var currentX = child.transform.position.x;
                                var currentY = child.transform.position.y;
                                child.transform.position = new Vector3(currentX, currentY + 0.6f, 0);


                            child.transform.GetComponent<RectTransform>().sizeDelta = new Vector3(200, 200, 50);



                            TowerScript towerScript = towerPrefab.GetComponent<TowerScript>();
                                // now set to child object 
                                child.gameObject.AddComponent<TowerScript>();
                                child.gameObject.GetComponent<TowerScript>().bulletPrefab = towerPrefab.GetComponent<TowerScript>().bulletPrefab;
                                child.gameObject.GetComponent<TowerScript>().firingPoint = child.transform.Find("FiringPoint").transform;
                                child.gameObject.GetComponent<TowerScript>().enemyMask = towerPrefab.GetComponent<TowerScript>().enemyMask;


                            Transform infoPanel = child.transform.Find("InfoPanel");
                            infoPanel.Find("Speed").GetComponent<Text>().text = $"Speed: 3";
                            infoPanel.Find("Range").GetComponent<Text>().text = $"Range: 4";
                            infoPanel.Find("Damage").GetComponent<Text>().text = $"Damage: 3";
                            if (child.transform.GetComponent<Button>().name == "TowerPosition2")
                            {
                                infoPanel.position = new Vector3(currentX - 1.5f, currentY, 0);

                            }
                            else
                            {
                                infoPanel.position = new Vector3(currentX + 1.5f, currentY, 0);

                            }
                            if (infoPanel != null)
                            {
                                infoPanel.gameObject.SetActive(true);
                            }

                            child.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                                child.transform.GetComponent<Button>().onClick.AddListener(() =>
                                {
                                    DisplayUpgradeShopPanel(child, "HadesTowerUpgradePanel", child.gameObject.GetComponent<TowerScript>());
                                });
                        }
                        }
                    

                }
                else if(data.towerName == "AphroditaTowerButton")
                {
                    foreach (GameObject towerPrefab in data.towerPrefabs)
                        {
                        if (towerPrefab.GetComponent<Button>().name == "AphroditaTowerDefault")
                            {

                                child.transform.GetComponent<Image>().sprite = towerPrefab.transform.GetComponent<Image>().sprite;
                                child.transform.GetComponent<Button>().colors = ColorBlock.defaultColorBlock;
                                child.transform.GetComponent<Image>().color = new Color(1, 1, 1, 1);
                                var currentX = child.transform.position.x;
                                var currentY = child.transform.position.y;
                                child.transform.position = new Vector3(currentX, currentY + 0.6f, 0);


                            child.transform.GetComponent<RectTransform>().sizeDelta = new Vector3(200, 200, 50);

                            TowerScript towerScript = towerPrefab.GetComponent<TowerScript>();
                                // now set to child object 
                                child.gameObject.AddComponent<TowerScript>();
                                child.gameObject.GetComponent<TowerScript>().bulletPrefab = towerPrefab.GetComponent<TowerScript>().bulletPrefab;
                                child.gameObject.GetComponent<TowerScript>().firingPoint = child.transform.Find("FiringPoint").transform;
                                child.gameObject.GetComponent<TowerScript>().enemyMask = towerPrefab.GetComponent<TowerScript>().enemyMask;


                            Transform infoPanel = child.transform.Find("InfoPanel");
                            infoPanel.Find("Speed").GetComponent<Text>().text = $"Speed: 3";
                            infoPanel.Find("Range").GetComponent<Text>().text = $"Range: 4";
                            infoPanel.Find("Damage").GetComponent<Text>().text = $"Damage: 3";
                            if (child.transform.GetComponent<Button>().name == "TowerPosition2")
                            {
                                infoPanel.position = new Vector3(currentX - 1.5f, currentY, 0);

                            }
                            else
                            {
                                infoPanel.position = new Vector3(currentX + 1.5f, currentY, 0);

                            }
                            if (infoPanel != null)
                            {
                                infoPanel.gameObject.SetActive(true);
                            }

                            child.transform.GetComponent<Button>().onClick.RemoveAllListeners();
                                child.transform.GetComponent<Button>().onClick.AddListener(() =>
                                {
                                    DisplayUpgradeShopPanel(child, "AphroditaTowerUpgradePanel", child.gameObject.GetComponent<TowerScript>());
                                });
                        }
                        }
                    

                }

            }
            towerShopPanel.SetActive(false);
        }



            Debug.Log("Tower placed!");

        // Optionally close the shop panel
        //if (towerShopPanel != null) towerShopPanel.SetActive(false);
    }

    public void DisplayTowerShopPanel(Vector3 position)
    {
        if (towerShopPanel != null)
        {
            towerShopPanel.SetActive(true);
            towerShopPanel.transform.position = position;

            foreach (Transform child in towerShopPanel.transform)
            {
                if (child.GetComponent<Button>() != null)
                {
                    Button button = child.GetComponent<Button>();
                    button.onClick.RemoveAllListeners();
                    button.onClick.AddListener(() =>
                    {
                        TowerData data = button.GetComponent<TowerButton>().towerData;
                        data.DataTowerPosition = position;
                        data.towerName = button.GetComponent<Button>().transform.name;
                        ProcessTowerButton(data, button);
                    });
                }
            }

        }


    }

    public void DisplayUpgradeShopPanel(Transform CurentTower, string name, TowerScript towerScript)
    {
        foreach (GameObject panel in upgrateTowersPanels)
        {
            if (panel.name == name)
            {
                panel.SetActive(true);
                panel.transform.position = CurentTower.transform.position;
                // Assuming the upgrade buttons are the first three children in order
                panel.transform.GetChild(0).GetComponent<Button>().onClick.AddListener(() => towerScript.UpgradeAttribute(TowerUpgradeType.Damage, CurentTower, panel));
                Debug.Log("CurentTower = " + panel.transform.GetChild(0).GetComponent<Button>().name);
                panel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => towerScript.UpgradeAttribute(TowerUpgradeType.Range, CurentTower, panel));
                Debug.Log("CurentTower = " + panel.transform.GetChild(1).GetComponent<Button>().name);
                panel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => towerScript.UpgradeAttribute(TowerUpgradeType.Speed, CurentTower, panel));
                Debug.Log("CurentTower = " + panel.transform.GetChild(2).GetComponent<Button>().name);





                // Remove existing listeners to avoid stacking them
                //panel.transform.GetChild(0).GetComponent<Button>().onClick.RemoveAllListeners();
                //panel.transform.GetChild(1).GetComponent<Button>().onClick.RemoveAllListeners();
                //panel.transform.GetChild(2).GetComponent<Button>().onClick.RemoveAllListeners();
            }
        }
    }

    public void TowerMaximumUpgraded(Vector3 position)
    {
        foreach (Transform child in PanelFromTowers.transform)
        {
            if (child.transform.position == position)
            {
                if(child.GetComponent<Image>().sprite.name == "TowerHades1")
                {
                    foreach (GameObject towerPrefab in TowersCopy)
                    {
                        if (towerPrefab.transform.GetComponent<Image>().sprite.name == "TowerHades2")
                        {
                            child.transform.GetComponent<Image>().sprite = towerPrefab.transform.GetComponent<Image>().sprite;
                            child.gameObject.GetComponent<TowerScript>().bulletPrefab = towerPrefab.GetComponent<TowerScript>().bulletPrefab;



                        }
                    }


                }

            }
        }



    }





}
