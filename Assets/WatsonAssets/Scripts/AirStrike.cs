using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Services.NaturalLanguageClassifier.v1;

public class AirStrike : MonoBehaviour
{
    //  Overlap sphere radius.
    public int BlastRadius = 50;
    //  Maximum amount of damage at the detonation point.
    public int MaxDamage = 100;

    //  Enemy layer mask.
    int shootableMask;

    public void CallAirstrike()
    {
        AirstrikeDamage(gameObject.transform.localPosition);
    }

    void OnEnable()
    {
        EventManager.Instance.RegisterEventReceiver("OnAirSupportRequest", HandleAirSupportRequest);

        // Create a layer mask for the Shootable layer.
        shootableMask = LayerMask.GetMask("Shootable");
    }

    void OnDisable()
    {
        EventManager.Instance.UnregisterEventReceiver("OnAirSupportRequest", HandleAirSupportRequest);
    }

    private void HandleAirSupportRequest(object[] args)
    {
        EventManager.Instance.SendEvent("OnDebugMessage", (args[0] as ClassifyResult).top_class + ", " + (args[0] as ClassifyResult).topConfidence);
        Log.Debug("WatsonEnabled", "AirSupport Event received!");

        CallAirstrike();
    }

    public void AirstrikeDamage(Vector3 detonationPoint)
    {
        //  Get colliders in overlap sphere.
        Collider[] hitColliders = Physics.OverlapSphere(detonationPoint, BlastRadius, shootableMask);
        
        foreach(Collider hitCollider in hitColliders)
        {
            //  get enenmy health component.
            CompleteProject.EnemyHealth enemyHealth = hitCollider.gameObject.GetComponentInChildren<CompleteProject.EnemyHealth>();
            if (enemyHealth != null)
            {

                //  find distance.
                float distance = Vector3.Distance(detonationPoint, hitCollider.transform.localPosition);

                //  find damage.
                int damage = -Mathf.RoundToInt(Mathf.Pow(distance / BlastRadius, 2)) + MaxDamage;

                //  find hit point.
                Ray damageRay = new Ray();
                damageRay.origin = detonationPoint;
                damageRay.direction = detonationPoint - hitCollider.transform.position;

                RaycastHit damageHit;
                Vector3 hitPoint = Vector3.zero;
                if (Physics.Raycast(damageRay, out damageHit, BlastRadius, shootableMask))
                {
                    hitPoint = damageHit.point;
                }

                Log.Debug("AirStrike", "damage: {0}, hitPoint: {1}, distance: {2}", damage, hitPoint, distance);
                //  deal damage. 
                enemyHealth.TakeDamage(damage, hitPoint);
            }
        }
    }
}
