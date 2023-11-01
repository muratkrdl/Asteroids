using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis
{
    X,Y
}

public class BorderController : MonoBehaviour
{
    [SerializeField] Axis axis;

    void OnTriggerEnter2D(Collider2D other) 
    {
        var current = other.transform.position;

        if(axis == Axis.X)
        {
            current.x *= -1;
        }
        else
        {
            current.y *= -1;
        }

        other.transform.position = current;
        StartCoroutine(EnterTeleport(other.gameObject));
    }

    IEnumerator EnterTeleport(GameObject other)
    {
        DisableCollider(other);
        yield return new WaitForSeconds(.5f);
        EnableCollider(other);
    }

    void DisableCollider(GameObject other)
    {
        other.GetComponent<Collider2D>().enabled = false;
    }

    void EnableCollider(GameObject other)
    {
        if(other == null) { return; }
        other.GetComponent<Collider2D>().enabled = true;
    }

}
