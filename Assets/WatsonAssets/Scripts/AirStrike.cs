using UnityEngine;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;

public class AirStrike : MonoBehaviour
{
    //  Overlap sphere radius.
    private int BlastRadius = 15;
    //  Maximum amount of damage at the detonation point.
    private int MaxDamage = 100;
    //  Enemy layer mask.
    int shootableMask;
    //  Has the Airstrike been detonated?
    private bool isDetonated = false;

    void OnEnable()
    {
        shootableMask = LayerMask.GetMask("Shootable");
    }

    void OnCollisionEnter(Collision collision)
    {
        if (!isDetonated)
            AirstrikeDamage(gameObject.transform.position);
    }

    public void AirstrikeDamage(Vector3 detonationPoint)
    {
        //  Dispatch event to flash
        EventManager.Instance.SendEvent("OnAirstrikeCollide");

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
