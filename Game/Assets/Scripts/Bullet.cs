using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform Player;
    [SerializeField] float BulletSpeed;
    bool canPlayerBeHit = true;
    void Start()
    {
        Player = GameManager.instance.Player.transform;
        transform.LookAt(Player.transform);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * BulletSpeed * Time.deltaTime;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player" && canPlayerBeHit)
        {
            Debug.Log("PlayerHit");
            GameManager.instance.PlayerLife -= 1;
            canPlayerBeHit=false;
        }
        Destroy(gameObject);
    }

  
}
