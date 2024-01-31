using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public float PlayerLife = 10;
    public bool isPlayerDead = false;

    public static GameManager instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        UiManager.instance.Life.text = PlayerLife.ToString();
        if(PlayerLife <= 0)
        {
            
            Debug.Log("Player is Dead");
            isPlayerDead = true;
           
        }
    }
}
