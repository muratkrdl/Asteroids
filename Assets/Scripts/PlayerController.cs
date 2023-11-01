using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] BulletController bulletPrefab;
    [SerializeField] Transform bulletParent;
    [SerializeField] Rigidbody2D myRigid;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float rotateSpeed = 5f;
    [SerializeField] float shootDelay = 1f;

    bool isPressFront => Input.GetKey(KeyCode.W);

    bool isPressLeft => Input.GetKey(KeyCode.A);
    bool isPressRight => Input.GetKey(KeyCode.D);

    bool isPressShoot => Input.GetMouseButton(0);
    bool isAllowShoot = true;

    BoxCollider2D boxCollider2D;

    public float ShootDelay
    {
        get
        {
            return shootDelay;
        }
        set
        {
            shootDelay = value;
        }
    }

    void Awake() 
    {
        boxCollider2D = GetComponent<BoxCollider2D>();    
    }

    void Update() 
    {
        Move();
        Shoot();
    }

    void Move()
    {
        if(isPressFront)
        {
            myRigid.AddForce(transform.up * Time.deltaTime * moveSpeed);
        }

        if(isPressRight || isPressLeft)
        {
            var currentRotateSpeed = (isPressLeft ? rotateSpeed : -rotateSpeed);
            transform.Rotate(Vector3.forward * Time.deltaTime * currentRotateSpeed);
        }
    }

    void Shoot()
    {
        if(isPressShoot && isAllowShoot)
        {
            var bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity, bulletParent);
            bullet.BulletMove(transform.up);
            
            isAllowShoot = false;
        
            StartCoroutine(WaitReload());
        }
    }

    IEnumerator WaitReload()
    {
        yield return new WaitForSeconds(shootDelay);
        isAllowShoot = true;
    }

    void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Asteroid"))
        {
            GameManager.Instance.ShowGameOver();
        }
        else if(other.gameObject.CompareTag("Border"))
        {
            boxCollider2D.enabled = false;
        }
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.gameObject.CompareTag("Border"))
        {
            boxCollider2D.enabled = true;
        }   
    }

}
