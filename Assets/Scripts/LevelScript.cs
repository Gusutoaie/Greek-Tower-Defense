using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firingPoint;
    [SerializeField] public Transform[] _enemyPaths;
    [SerializeField] private GameObject[] TowerUpgradePanels;

    public Text GoldPanel;
    public Text livesPanel;
    public Text GameStatusPanel;
    public GameObject towerPositions;
    public GameObject towerShopPanel;
    public GameObject MessagesPanel;
    public GameObject InfoPanel;
    public GameObject WavePanel;

    public Transform startPoint;

    private Transform target;
    private int pathIndex = 0;
    private int _towerButtons = 0;

    public static LevelScript main;

    private void Awake()
    {
        main = this;
    }

    private void Start()
    {
        // Setup enemy paths or any initial settings for the level
        target = _enemyPaths[pathIndex];
       // InitializeTowerButtons();
    }

    private void InitializeTowerButtons()
    {
        // Assuming each child of 'towerPositions' is a button for a tower
        foreach (Transform child in towerPositions.transform)
        {
            var button = child.GetComponent<Button>();
            if (button != null)
            {
                button.onClick.AddListener(() => {
                    Vector3 position = button.transform.position;
                    // Assuming you have a method in TowerManager to handle this
                    TowerManager.Instance.DisplayTowerShopPanel(position);
                });
                _towerButtons++;
            }
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            ClosePanel();

        // Continuously update the gold display in the UI
        if (GameManager.Instance != null && GoldPanel != null)
            GoldPanel.text = "Gold: " + GameManager.Instance.Gold;

        if (Vector2.Distance(target.position, transform.position) < 0.1f)
        {
            pathIndex++;
            if (pathIndex == _enemyPaths.Length)
            {
                Destroy(gameObject);
                return;
            }
            target = _enemyPaths[pathIndex];
        }
    }

    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
    }

    private bool IsPointerOverUIObject()
    {
        PointerEventData eventData = new PointerEventData(EventSystem.current) { position = Input.mousePosition };
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventData, results);
        return results.Exists(result => result.gameObject == towerShopPanel);
    }

    private void ClosePanel()
    {
        towerShopPanel.SetActive(false);
    }
}
