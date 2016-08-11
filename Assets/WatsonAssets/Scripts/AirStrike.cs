using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.NaturalLanguageClassifier.v1;

public class AirStrike : MonoBehaviour
{
    //  Overlap sphere radius.
    private int BlastRadius = 10;
    //  Maximum amount of damage at the detonation point.
    private int MaxDamage = 100;
    //  Enemy layer mask.
    int shootableMask;
    //  AirStrike target
    private Vector3 airStrikeTarget;
    //  Has the Airstrike been detonated?
    private bool isDetonated = false;

    void OnEnable()
    {
        shootableMask = LayerMask.GetMask("Shootable");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isDetonated)
        {
            foreach (ContactPoint contact in collision.contacts)
            {
                Log.Debug("AirStrike", "CONTACT! {0}", contact.otherCollider.gameObject.name);
            }

            AirstrikeDamage(gameObject.transform.position);
        }
    }

    public void AirstrikeDamage(Vector3 detonationPoint)
    {
        //  Get colliders in overlap sphere.
        Collider[] hitColliders = Physics.OverlapSphere(detonationPoint, BlastRadius, shootableMask);
        
        //  Iterate through colliders
        foreach(Collider hitCollider in hitColliders)
        {
            //  get enemy health component.
            CompleteProject.EnemyHealth enemyHealth = hitCollider.gameObject.GetComponentInChildren<CompleteProject.EnemyHealth>();
            if (enemyHealth != null)
            {
                if (Vector3.Distance(detonationPoint, hitCollider.transform.position) < BlastRadius)
                {
                    //  find distance.
                    float distance = Vector3.Distance(detonationPoint, hitCollider.transform.position);

                    //  find damage.
                    int damage = -Mathf.RoundToInt(Mathf.Pow(distance / BlastRadius, 2)) + MaxDamage;

                    //  raycast to find the point where the blast hits.
                    RaycastHit damageHit;
                    Physics.Raycast(detonationPoint, (hitCollider.transform.position - detonationPoint), out damageHit);
                    Vector3 hitPoint = damageHit.point;
                    
                    //Debug.DrawRay(detonationPoint, hitCollider.transform.position - detonationPoint, Color.red, Mathf.Infinity);

                    Log.Debug("AirStrike", "damage: {0}, hitPoint: {1}, distance: {2}", damage, hitPoint, distance);
                    //  deal damage. 
                    enemyHealth.TakeDamage(damage, hitPoint);
                }
            }
        }

        isDetonated = true;

        Destroy(gameObject);
    }
}
