using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigid;

    Collider2D colliderr;

    float splitSpeed;
    float rotateSpeed;

    int chosenIndex;
    Vector3 chosenDirection = Vector3.back;

    public float RotateSpeed
    {
        set
        {
            rotateSpeed = value;
        }
    }

    void Awake() 
    {
        colliderr = GetComponent<Collider2D>();
        IEnumerator DO()
        {
            yield return new WaitForSeconds(0.3f);
            colliderr.enabled = true;
        }  
        StartCoroutine(DO());

        chosenIndex = Random.Range(0,2);
        if(chosenIndex == 1)
        {
            chosenDirection *= -1;
        }
    }

    void Update() 
    {
        transform.Rotate(chosenDirection * rotateSpeed);    
    }

    public void AsteroidMove(Vector3 direction, float speed)
    {
        splitSpeed = speed;
        myRigid.AddForce(direction * speed);
    }

    public void Split()
    {
        var targetScaale = transform.localScale.x / 2;
        if(targetScaale > 0.6)
        {
            var prefab = AsteroidManager.Instance.asteroidPrefab;

            for(int i = 0; i < 2; i++)
            {
                var asteroid = Instantiate(prefab, transform.position, Quaternion.identity);
                asteroid.transform.localScale = transform.localScale/2;
                asteroid.RotateSpeed = this.rotateSpeed;

                var direction = myRigid.velocity.normalized + new Vector2(Random.value, Random.value);
                asteroid.AsteroidMove(direction, splitSpeed*4);
            }
        }

        if(gameObject.transform.localScale.x > 2)
            GameManager.Instance.IncreaseScore(1);
        else if(gameObject.transform.localScale.x > 1)
            GameManager.Instance.IncreaseScore(2);
        else
            GameManager.Instance.IncreaseScore(3);

        var playerController = FindObjectOfType<PlayerController>();
        playerController.ShootDelay = playerController.ShootDelay /100*99;

        Destroy(gameObject);
    }

    void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("DeathArea"))
        {
            Destroy(gameObject);
        }    
    }

}
