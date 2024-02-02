using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaivior : MonoBehaviour
{
    Transform Player;
    
    [SerializeField] float EnemySpeed;
    [SerializeField] float distanceCheck;
    [SerializeField] float StopPursuitDistance;
    [SerializeField] GameObject bullet;
    [SerializeField] GameObject bulletspawnpoint;
    [SerializeField] GameObject[] wanderingPoints;
    [SerializeField] AudioSource EnemyShootSound;

    //public EnemySO Enemy;

    private bool ishooot = true;
    private bool isWandering = true;
    int movePoint = 0;
    private Vector3 travelPoint;
    private Vector3 PlayerVector3;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player").transform;
        movePoint = Random.Range(0, wanderingPoints.Length);
        travelPoint = new Vector3(wanderingPoints[movePoint].transform.position.x , 0f, wanderingPoints[movePoint].transform.position.z);
        PlayerVector3 = new Vector3(Player.position.x,0f,Player.position.z);
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
            if(Vector3.Distance(transform.position, Player.position) > StopPursuitDistance)
            {
                this.transform.position = Vector3.MoveTowards(transform.position, PlayerVector3, EnemySpeed * Time.deltaTime);
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
                    this.transform.position = Vector3.MoveTowards(transform.position, travelPoint, EnemySpeed * Time.deltaTime);
                    transform.LookAt(travelPoint);
                }
                else
                {
                    movePoint = Random.Range(0, wanderingPoints.Length);
                    travelPoint = new Vector3(wanderingPoints[movePoint].transform.position.x, 0f, wanderingPoints[movePoint].transform.position.z);
                }

            }
        }
    }

    IEnumerator shoot()
    {
        Debug.Log("EnnemyShoot");
        ishooot = false;
        EnemyShootSound.Play();
        GameObject bulletInst = Instantiate(bullet, bulletspawnpoint.transform.position, Quaternion.identity);
        yield return new WaitForSeconds(2);
        ishooot = true;

    }

   
}
