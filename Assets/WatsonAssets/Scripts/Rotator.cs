using UnityEngine;
using System.Collections;

public class Rotator : MonoBehaviour
{
    private float m_Speed = 100f;
    //private RigidBody m_RigidBody;
    
    void Awake()
    {
        //m_RigidBody = gameObject.GetComponent<RigidBody>();
    }
	void Update ()
    {
        float v = Input.GetAxis("Vertical") * m_Speed * Time.deltaTime;
        //m_RigidBody.AddTorque(transform.right * v);
    }
}
