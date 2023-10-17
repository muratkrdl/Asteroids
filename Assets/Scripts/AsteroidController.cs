using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class AsteroidController : MonoBehaviour
{
    [SerializeField] Rigidbody2D myRigid;
    Collider2D collider;

    float splitSpeed;

    void Awake() 
    {
        collider = GetComponent<Collider2D>();
        IEnumerator DO()
        {
            yield return new WaitForSeconds(0.3f);
            collider.enabled = true;
        }  
        StartCoroutine(DO());  
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
                var AsteroidController = Instantiate(prefab, transform.position, Quaternion.identity);
                AsteroidController.transform.localScale = transform.localScale/2;

                var direction = myRigid.velocity.normalized + new Vector2(Random.value, Random.value);
                AsteroidController.AsteroidMove(direction, splitSpeed*4);
            }
        }

        Destroy(gameObject);
    }

}
