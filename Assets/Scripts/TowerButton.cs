using UnityEngine;
using UnityEngine.UI;

public class TowerButton : MonoBehaviour
{
    [SerializeField] public TowerData towerData; // Ensure this is public or marked with [SerializeField]
    [SerializeField] private GameObject towerPosition;  // Reference to the actual tower position
    public Vector3 savedPosition;

    public Text descriptionText;
    private Image buttonImage; // The Image component on this button

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage == null)
        {
            Debug.LogError("Missing Image component on " + gameObject.name);
        }

        Button button = GetComponent<Button>();
        if (button != null)
        {

            button.onClick.AddListener(OnClick);
        }
        else
        {
            Debug.LogError("Missing Button component on " + gameObject.name);
        }

        // For Debugging: Log to ensure the data is assigned
        if (towerData == null)
        {
            Debug.LogError("TowerData not assigned to " + gameObject.name);
        }
       
    }

    /* private void UpdateButtonSprite()
    {
        if (towerData != null && towerData.towerPrefab != null)
        {
            SpriteRenderer prefabSprite = towerData.towerPrefab.GetComponent<SpriteRenderer>();
            if (prefabSprite != null)
            {
                buttonImage.sprite = prefabSprite.sprite;
            }
            else
            {
                Image prefabImage = towerData.towerPrefab.GetComponent<Image>();
                if (prefabImage != null)
                {
                    buttonImage.sprite = prefabImage.sprite;
                }
                else
                {
                    Debug.LogError("No SpriteRenderer or Image component found on tower prefab for " + gameObject.name);
                }
            }
        }
        else
        {
            Debug.LogError("TowerData or towerPrefab is missing for " + gameObject.name);
        }
    }*/

    private void OnClick()
    {
        if (towerData == null)
        {
            Debug.LogError("TowerData is null when button clicked");
            return;
        }
       

        TowerManager.Instance.DisplayTowerShopPanel(transform.position);

       // TowerManager.Instance.ProcessTowerButton(towerData, GetComponent<Button>());
    }
}
