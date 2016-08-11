using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;
using IBM.Watson.DeveloperCloud.Debug;
using IBM.Watson.DeveloperCloud.Services.NaturalLanguageClassifier.v1;

public class WatsonEnabled : MonoBehaviour
{
    [SerializeField]
    private GameObject m_AirStrikePrefab;

    [SerializeField]
    private Transform m_PlayerTransform;

    void Start()
    {
        LogSystem.InstallDefaultReactors();
    }

    void OnEnable()
    {
        EventManager.Instance.RegisterEventReceiver("OnAirSupportRequest", HandleAirSupportRequest);
        EventManager.Instance.RegisterEventReceiver("OnAirSupportRequestFromKeyboard", HandleAirSupportRequestFromKeyboard);
    }

    void OnDisable()
    {
        EventManager.Instance.UnregisterEventReceiver("OnAirSupportRequest", HandleAirSupportRequest);
        EventManager.Instance.UnregisterEventReceiver("OnAirSupportRequestFromKeyboard", HandleAirSupportRequestFromKeyboard);
    }

    private void HandleAirSupportRequest(object[] args)
    {
        Instantiate(m_AirStrikePrefab, m_PlayerTransform.localPosition + new Vector3(0f, 10f, 0f), Quaternion.identity);
    }

    private void HandleAirSupportRequestFromKeyboard(object[] args)
    {
        Instantiate(m_AirStrikePrefab, m_PlayerTransform.localPosition + new Vector3(0f, 10f, 0f), Quaternion.identity);
    }
}
