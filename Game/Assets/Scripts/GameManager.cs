using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject Player;
    public float PlayerLife = 10;
    public bool isPlayerDead = false;
    [SerializeField] KeyCode PauseKey = KeyCode.Escape;
    public static GameManager instance;

    public bool isGameStoped = false;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        
    }

    // Update is called once per frame
    void Update()
    {
        UiManager.instance.Life.text = PlayerLife.ToString() + " HP";
        if(PlayerLife <= 0 )
        {
            UiManager.instance.PlayerUiPanel.SetActive(false);
            UiManager.instance.DeadPanel.SetActive(true);
            Debug.Log("Player is Dead");
            isPlayerDead = true;
           
        }
        if(Input.GetKeyDown(PauseKey))
        {
            if(isGameStoped == false)
            {
                
                StartCoroutine(PauseGame());
            }
            else
            {
                StartCoroutine(UnPauseGame());
            }
        }
    }

    public void ReloadLevel()
    {

        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);

    }
    IEnumerator PauseGame()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        UiManager.instance.PlayerUiPanel.SetActive(false );
        UiManager.instance.ESCmenu.SetActive(true);
        isGameStoped = true;
        Time.timeScale = 0;
        yield return new WaitForSeconds(1);
    }
    public void UnPauseGameButton()
    {
        StartCoroutine(UnPauseGame());
    }
    IEnumerator UnPauseGame()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
        UiManager.instance.ESCmenu.SetActive(false) ;
        UiManager.instance.PlayerUiPanel.SetActive(true);
        isGameStoped = false;
        Time.timeScale = 1;
        yield return new WaitForSeconds(1);
    }

    public void LoadLevel(int lvlId)
    {
        SceneManager.LoadScene(lvlId);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
