using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "New TowerData", menuName = "Tower Defense/Tower Data")]

public class TowerData : ScriptableObject
{
    public string towerName;
    public List<GameObject> towerPrefabs; // List to hold different levels or upgrades of the tower
    public int cost;
    public float damage;
    public float range;
    public string description;  // For UI tooltips or descriptions
    public Vector3 DataTowerPosition;
}
