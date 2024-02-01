using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstAid : MonoBehaviour
{
    // Start is called before the first frame update
    BoxCollider myboxCider;
    void Start()
    {
        myboxCider = GetComponent<BoxCollider>();
    }

  
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            GameManager.instance.PlayerLife += 1;
            Destroy(this.gameObject);
        }
    }
}
