using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    Rigidbody rb;
    float damage = 10;
    
    void Start()

    {

        rb
            = GetComponent<Rigidbody>();
        
        StartCoroutine(Predict());

    }
    void FixedUpdate()

    {

        StartCoroutine(Predict());
        Destroy(gameObject, 3);

    }

    IEnumerator Predict()

    {

        Vector3 prediction = transform.position + rb.velocity * Time.fixedDeltaTime;

        RaycastHit hit2;

        int layerMask = ~LayerMask.GetMask("Bullet");

        //Debug.DrawLine(transform.position, prediction);




        if (Physics.Linecast(transform.position, prediction, out hit2, layerMask))

        {

            transform.position = hit2.point;

            rb.collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;

            rb.isKinematic = true;

            yield return 0;

            OnTriggerEnterFixed(hit2.collider);
            

        }

    }

    void OnTriggerEnterFixed(Collider other)

    {

        //if (other.CompareTag("Target"))
        
        other.gameObject.GetComponent<IDamageable>()?.TakeDamage(damage);
        Destroy(gameObject);

    }
    

}
