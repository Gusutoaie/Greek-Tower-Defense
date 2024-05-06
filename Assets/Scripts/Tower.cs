using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tower
{
    public Vector3 Position { get; set; }
    public Sprite Sprite { get; }


    public Rect Rect
    {
        get { return new Rect(Position.x, Position.y, 150, 220); }
    }
    public int Level { get; set; }

    public int NeedMoney = 100;

    private static int _gold = 100;  // Initial gold value

    public static int Gold
    {
        get { return _gold; }
        set { _gold = value; }
    }



    public static Tower Instance { get; set; }



    public Tower(Vector3 position, Sprite sprite)
    {
        Position = position;
        Sprite = sprite;
        Level = 1;
    }

    // Upgrade the tower if the player has enough money


    public void Upgrade(GameObject panel, GameObject towerPositions)
    {
        panel.SetActive(true);
        panel.transform.position = Position;

        foreach (Transform child in panel.transform)
        {
            var button = child.GetComponent<UnityEngine.UI.Button>();
            if (button != null)
            {
                button.onClick.AddListener(() =>
                {
                    for (int i = 0; i < towerPositions.transform.childCount; i++)
                    {
                        // check if the user has enough money to upgrade the tower  
                        if (NeedMoney > Gold)
                        {
                            Debug.Log("NeedMoney = " + NeedMoney + "Cost = " + Gold);
                            Debug.Log("Not enough money to upgrade the tower");
                            panel.SetActive(false);
                            
                            return;
                        }else
                     


                        if (towerPositions.transform.GetChild(i).position == Position)
                        {

                            if (button.GetComponent<Image>().name == "PoseidonHeadUpgrade1")
                            {
                                foreach(Button button in LevelScript.main.towerButtons)
                                {
                                    if (button.GetComponent<Image>().name == "PoseidonTowerButton1")
                                    {
                                        towerPositions.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite = button.GetComponent<UnityEngine.UI.Image>().sprite;
                                        //Bring from button audio source to tower audio source
                                        towerPositions.transform.GetChild(i).GetComponent<AudioSource>().clip = button.GetComponent<AudioSource>().clip;
                                        AudioSource audio = towerPositions.transform.GetChild(i).GetComponent<AudioSource>();
                                        audio.volume = 1;
                                        audio.Play();
                                        Level++;
                                        Gold -= NeedMoney; // Deduct the money after successful upgrade
                                        Bullet.SetDamage(2);
                                        Bullet.ChangebulletSpeed(5f);
                                        
                                        break;
                                    }
                                }
                            }else if (button.GetComponent<Image>().name == "PoseidonHeadUpgrade2")
                            {

                                foreach (Button button in LevelScript.main.towerButtons)
                                {
                                    if (button.GetComponent<Image>().name == "PoseidonTowerButton2")
                                    {
                                        towerPositions.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite = button.GetComponent<UnityEngine.UI.Image>().sprite;
                                        towerPositions.transform.GetChild(i).GetComponent<AudioSource>().clip = button.GetComponent<AudioSource>().clip;
                                        AudioSource audio = towerPositions.transform.GetChild(i).GetComponent<AudioSource>();
                                        audio.volume = 1;
                                        audio.Play();
                                        Level++;
                                        Gold -= NeedMoney; // Deduct the money after successful upgrade
                                        Bullet.SetDamage(1);
                                        Bullet.ChangebulletSpeed(7f);
                                        
                                        break;
                                    }
                                }

                            }else if (button.GetComponent<Image>().name == "PoseidonHeadUpgrade3")
                            {

                                foreach (Button button in LevelScript.main.towerButtons)
                                {
                                    if (button.GetComponent<Image>().name == "PoseidonTowerButton3")
                                    {
                                        towerPositions.transform.GetChild(i).GetComponent<UnityEngine.UI.Image>().sprite = button.GetComponent<UnityEngine.UI.Image>().sprite;
                                        towerPositions.transform.GetChild(i).GetComponent<AudioSource>().clip = button.GetComponent<AudioSource>().clip;
                                        AudioSource audio = towerPositions.transform.GetChild(i).GetComponent<AudioSource>();
                                        audio.volume = 1;
                                        audio.Play();
                                        Level++;
                                        Gold -= NeedMoney; // Deduct the money after successful upgrade
                                        Bullet.SetDamage(1.2f);
                                        Bullet.ChangebulletSpeed(6f);
                                       
                                        break;
                                    }
                                }
                            }
                            






                            break;
                        }

                    }
                    panel.SetActive(false);
                });
            }
        }


   
        Debug.Log("Tower upgraded to level " + Level);
    }
  
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
