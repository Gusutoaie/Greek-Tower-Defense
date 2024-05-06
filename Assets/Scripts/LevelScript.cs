using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.GraphicsBuffer;

public class LevelScript : MonoBehaviour
{

    [SerializeField] public LayerMask enemyMask;
    [SerializeField] public GameObject bulletPrefab;
    [SerializeField] public Transform firingPoint;

    public Text GoldPanel;
    public Text livesPanel;
    public Text GameStatusPanel;

    public GameObject towerPositions;
    public GameObject towerShopPanel;
    public GameObject towerUpgradePanel;
    public GameObject MessagesPanel;
    public GameObject InfoPanel;
    public GameObject WavePanel;

    public Transform startPoint;

    // create a list of towers that the player can buy
    [SerializeField] public Button[] towerButtons;

    [SerializeField] public Transform[] _enemyPaths;
    private Transform target;
    private int pathIndex = 0;


    private int _towerButtons = 0;


    public static LevelScript main;

    private void Awake()
    {
        main = this;
    }

    // private Dictionary<Vector3, Tower> _towers = new Dictionary<Vector3, Tower>();

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current);
        eventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        foreach (RaycastResult result in results)
        {
            if (result.gameObject == towerShopPanel)
                return true; // Click was on the panel
        }
        return false; // Click was outside the panel
    }



    private void DisplayTowerShopPanel(Vector3 position)
    {
        towerShopPanel.SetActive(true);
        towerShopPanel.transform.position = position;

        foreach (Transform child in towerShopPanel.transform)
        {
            



            var button = child.GetComponent<UnityEngine.UI.Button>();
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    var tempTower = new Tower(position, button.GetComponent<UnityEngine.UI.Image>().sprite);
                    BuyTower(tempTower);
                });
            }
        }
    }

    private void DisplayTowerUpgradePanel(Tower tower)
    {
       tower.Upgrade(towerUpgradePanel, towerPositions);
    }
    public void ConfigureButton(Button button, Tower tower)
    {
        button.GetComponent<Image>().sprite = tower.Sprite;
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(() => DisplayTowerUpgradePanel(tower));
    }
    private void BuyTower(Tower tower)
    {
        // _towers.Add(tower.Position, tower);

        for (int i = 0; i < _towerButtons; i++)
        {
            if (towerPositions.transform.GetChild(i).transform.position == tower.Position)
            {

                foreach (Button button in towerButtons)
                {
                    if (button.GetComponent<Image>().name == "PoseidonTowerButtonDefault")
                    {
                        towerPositions.transform.GetChild(i).GetComponent<Image>().sprite = button.GetComponent<Image>().sprite;
                        towerPositions.transform.GetChild(i).GetComponent<Button>().colors = ColorBlock.defaultColorBlock;
                        towerPositions.transform.GetChild(i).GetComponent<Image>().color = new Color(1, 1, 1, 1);

                        var currentX = towerPositions.transform.GetChild(i).transform.position.x;
                        var currentY = towerPositions.transform.GetChild(i).transform.position.y;
                        towerPositions.transform.GetChild(i).transform.position = new Vector3(currentX, currentY + 0.8f, 0);
                        tower.Position = new Vector3(currentX, currentY + 0.8f, 0);

                        towerPositions.transform.GetChild(i).GetComponent<RectTransform>().sizeDelta = tower.Rect.size;
                        towerPositions.transform.GetChild(i).GetComponent<Button>().onClick.RemoveAllListeners();
                        towerPositions.transform.GetChild(i).GetComponent<Button>().onClick.AddListener(() =>
                        {
                            DisplayTowerUpgradePanel(tower);
                        });
                        TowerScript towerScript = towerPositions.transform.GetChild(i).GetComponent<TowerScript>();
                        if (towerScript == null)
                        {
                            towerScript = towerPositions.transform.GetChild(i).gameObject.AddComponent<TowerScript>();
                        }
                        Transform foundFiringPoint = towerPositions.transform.GetChild(i).Find("FiringPoint");
                        if (foundFiringPoint != null)
                        {
                            towerScript.firingPoint = foundFiringPoint;
                        }
                        else
                        {
                            Debug.LogError("Firing point not found for tower at " + towerPositions.transform.GetChild(i).Find("FiringPoint").name);
                        }
                        break;
                    }
                }


            }
        }

        towerShopPanel.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        foreach (Transform child in towerPositions.transform)
        {
            var button = child.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    // Debug.Log(button.name + " clicked");
                    DisplayTowerShopPanel(button.transform.position);
                });

                _towerButtons++;
            }
        }
        target = _enemyPaths[pathIndex];

        foreach (Transform child in MessagesPanel.transform)
        {
            var button = child.GetComponent<UnityEngine.UI.Button>();
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    if (button.name == "PlayAgain")
                    {
                        Time.timeScale = 1;
                        UnityEngine.SceneManagement.SceneManager.LoadScene("Level1Scene");
                    }
                    else if (button.name == "NextLevel")
                    {
                        Application.Quit();
                    }
                });
            }
        }




    }
    // if is outside of panel then close panel
    private void ClosePanel()
    {
        towerShopPanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // Checks if the left mouse button was pressed
        {
            if (!IsPointerOverUIObject())
                ClosePanel();
        }

        // Continuously update the gold display in the UI
        if (GoldPanel != null)
            GoldPanel.text = "Gold: " + Tower.Gold.ToString();


        if (Vector2.Distance(target.position, transform.position) < 0.1f)
        {
            pathIndex++;
            if (pathIndex == _enemyPaths.Length)
            {
                Destroy(gameObject);
                return;
            }
        }

    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;

    }

}
