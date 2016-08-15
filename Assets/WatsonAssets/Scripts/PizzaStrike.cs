using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;

public class PizzaStrike : MonoBehaviour
{
    private SphereCollider m_TriggerCollider;
    private bool m_IsCollected = false;

    void Awake()
    {
        m_TriggerCollider = gameObject.GetComponent<SphereCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
            if (!m_IsCollected)
                CollectPizza();
    }

    private void CollectPizza()
    {
        m_IsCollected = true;
        EventManager.Instance.SendEvent("OnPizzaCollected");
        Destroy(gameObject);
    }
}
