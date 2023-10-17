using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletController : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigid;
    [SerializeField] float bulletSpeed = 100f;
    [SerializeField] float killTime = 5f;

    public void BulletMove(Vector3 direction)
    {
        myRigid.AddForce(bulletSpeed*direction);

        StartCoroutine(KYS());
    }

    IEnumerator KYS()
    {
        yield return new WaitForSeconds(killTime);
        Destroy(gameObject);
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Asteroid"))
        {
            var asteroidController = other.GetComponent<AsteroidController>();
            asteroidController.Split();
            Destroy(gameObject);
        }    
    }


}
