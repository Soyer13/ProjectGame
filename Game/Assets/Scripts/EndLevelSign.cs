using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLevelSign : MonoBehaviour
{
    private BoxCollider myColider;
    // Start is called before the first frame update
    void Start()
    {
        myColider = GetComponent<BoxCollider>();
    }

   
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }
}
