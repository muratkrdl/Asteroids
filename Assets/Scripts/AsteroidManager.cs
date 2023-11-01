using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidManager : MonoBehaviour
{
    public static AsteroidManager Instance { get; private set; }

    public AsteroidController asteroidPrefab;

    [SerializeField] Vector2 spawnDelayRange;
    [SerializeField] Vector2 SpeedRange;
    [SerializeField] Vector2 scaleRange;
    [SerializeField] Vector2 rotateRange;

    [SerializeField] MyPointManager spawnPoint;
    [SerializeField] MyPointManager targetPoint;

    [SerializeField] Transform asteroidsParent;

    void Awake() 
    {
        Instance = this;    
    }

    void Start()
    {
        SpawnAsteroid();
    }

    void SpawnAsteroid()
    {
        IEnumerator Do()
        {
            while(true)
            {
                var delay = Random.Range(spawnDelayRange.x, spawnDelayRange.y);
                yield return new WaitForSeconds(delay);

                var spawnPosition = spawnPoint.GetPosition();

                var current = Instantiate(asteroidPrefab, spawnPosition, Quaternion.identity, asteroidsParent);
                current.transform.localScale = Vector3.one * Random.Range(scaleRange.x, scaleRange.y);

                current.RotateSpeed = Random.Range(rotateRange.x,rotateRange.y);

                var direction = targetPoint.GetPosition() - spawnPosition;
                var speed = Random.Range(SpeedRange.x, SpeedRange.y);
                current.AsteroidMove(direction, speed);
            }
        }
        
        StartCoroutine(Do());
    }

}
