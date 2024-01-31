using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyTuretBehaivior : MonoBehaviour
{
    Transform Player;
    [Range(1f,10f)]
    [SerializeField] float distanceCheck;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletspawnpoint;
    Animator animator;

    //public EnemySO Enemy;

    private bool ishooot = true;
    
  
    void Start()
    {
        //Player = GameManager.instance.Player.transform;
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(this.transform.position, Player.position)< distanceCheck)
        {
            animator.SetBool("IsPlayerSpoted", true);
            transform.LookAt(Player);
            if(ishooot)
            {
                StartCoroutine(shoot());
            }
        }
        else
        {
            animator.SetBool("IsPlayerSpoted", false);
        }
        
    }

    IEnumerator shoot()
    {
        ishooot = false;
       GameObject bulletInst = Instantiate(bullet, bulletspawnpoint.transform.position, Quaternion.identity);       
        yield return new WaitForSeconds(2);
        ishooot = true;

    }

    
}
