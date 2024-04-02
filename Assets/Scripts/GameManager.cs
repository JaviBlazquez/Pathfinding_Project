using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager>
{
    public int coins = 0;
    public bool LavaWalkers = false;
    public bool WaterWalkers = false;
    private int maxcoins = 10;
    public int health = 3;
    public int minutos;
    public int segundos;
    // Start is called before the first frame update
    void Start()
    {
       // DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        /*if(coins == 5)
        {
            Destroy(GameObject.Find("puenteLevadizo"));
        }
        if(coins == maxcoins)*/
        {
            Destroy(GameObject.Find("secretDoor"));
        }
        if(health == 0)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
