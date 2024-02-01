using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaivior : MonoBehaviour
{
    Transform Player;
    
    [SerializeField] float EnemySpeed;
    [SerializeField] float distanceCheck;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletspawnpoint;
    [SerializeField] GameObject[] wanderingPoints;

    //public EnemySO Enemy;

    private bool ishooot = true;
    private bool isWandering = true;
    int movePoint = 0;
    private Vector3 travelPoint;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
         movePoint = Random.Range(0, wanderingPoints.Length);
        travelPoint = wanderingPoints[movePoint].transform.position;
        //EnemySpeed = Enemy.Speed;
    }

    // Update is called once per frame
    void Update()
    {
        
        if(Vector3.Distance(transform.position, Player.position)< distanceCheck)
        {
            transform.LookAt(Player);
            //transform.position += transform.forward * EnemySpeed * 3 * Time.deltaTime;
            if(ishooot)
            {

                StartCoroutine(shoot());
            }
        }
        else 
        {
            
            if(isWandering && wanderingPoints.Length > 0)
            {
                Debug.Log("enemyWander " + movePoint);
                //StartCoroutine(enemyWander());
                if(Vector3.Distance(transform.position, travelPoint) > 1)
                {
                    this.transform.position = Vector3.MoveTowards(transform.position, wanderingPoints[movePoint].transform.position, EnemySpeed * Time.deltaTime);
                 //   transform.LookAt(wanderingPoints[movePoint].transform.position);
                }
                else
                {
                    movePoint = Random.Range(0, wanderingPoints.Length);
                    travelPoint = wanderingPoints[movePoint].transform.position;
                }

            }
        }
    }

    IEnumerator shoot()
    {
        Debug.Log("EnnemyShoot");
        ishooot = false;
        GameObject bulletInst = Instantiate(bullet, bulletspawnpoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        ishooot = true;

    }

    IEnumerator enemyWander() {
        isWandering = false;
        int movePoint = Random.Range(0, wanderingPoints.Length);
        /*
        if(this.transform.position != wanderingPoints[movePoint].transform.position)
        {
            Debug.Log("Move to " + movePoint);
            transform.LookAt(wanderingPoints[movePoint].transform.position);
            transform.position += transform.forward * EnemySpeed * Time.deltaTime;
        }
        else
        {
            isWandering = true;
        }
        
         while(this.transform.position != wanderingPoints[movePoint].transform.position)
        {
            
            transform.position += transform.forward * EnemySpeed * Time.deltaTime;
            yield return null;
        }
         
         */
        Debug.Log("Move to " + movePoint);
        transform.LookAt(wanderingPoints[movePoint].transform.position);
        // transform.position += transform.forward * EnemySpeed * Time.deltaTime;
        while (this.transform.position != wanderingPoints[movePoint].transform.position)
        {

            this.transform.position = Vector3.MoveTowards(transform.position, wanderingPoints[movePoint].transform.position, EnemySpeed * Time.deltaTime);
            yield return null;
        }
        yield return new WaitForSeconds(5);
        isWandering = true;
    }
}
