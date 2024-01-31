using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Transform Player;
    [SerializeField] float BulletSpeed;
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
        Destroy(gameObject);
    }
}
