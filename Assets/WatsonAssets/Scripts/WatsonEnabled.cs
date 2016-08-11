using UnityEngine;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;
using UnityEngine.UI;

public class WatsonEnabled : MonoBehaviour
{
    [SerializeField]
    private GameObject m_AirStrikePrefab;

    [SerializeField]
    private Transform m_PlayerTransform;

    [SerializeField]
    private Image m_FlashImage;

    private bool m_AirstrikeDetonated = false;
    private Color m_FlashColor = new Color(1f, 1f, 1f, 1f);
    private float m_FlashSpeed = 0.01f;

    void Start()
    {
        LogSystem.InstallDefaultReactors();
    }

    void Update()
    {
        //If airstrike has gone off
        if (m_AirstrikeDetonated)
        {
            // ... set the colour of the damageimage to the flash colour.
            m_FlashImage.color = m_FlashColor;
        }
        // otherwise...
        else
        {
            // ... transition the colour back to clear.
            m_FlashImage.color = Color.Lerp(m_FlashImage.color, Color.clear, m_FlashSpeed * Time.deltaTime);
        }

        // reset the airstrikeDetonated flag.
        m_AirstrikeDetonated = false;
    }

    void OnEnable()
    {
        EventManager.Instance.RegisterEventReceiver("OnAirSupportRequest", HandleAirSupportRequest);
        EventManager.Instance.RegisterEventReceiver("OnAirSupportRequestFromKeyboard", HandleAirSupportRequestFromKeyboard);
        EventManager.Instance.RegisterEventReceiver("OnAirstrikeCollide", HandleAirstrikeCollide);
    }

    void OnDisable()
    {
        EventManager.Instance.UnregisterEventReceiver("OnAirSupportRequest", HandleAirSupportRequest);
        EventManager.Instance.UnregisterEventReceiver("OnAirSupportRequestFromKeyboard", HandleAirSupportRequestFromKeyboard);
        EventManager.Instance.UnregisterEventReceiver("OnAirstrikeCollide", HandleAirstrikeCollide);
    }

    private void HandleAirSupportRequest(object[] args)
    {
        Instantiate(m_AirStrikePrefab, m_PlayerTransform.localPosition + new Vector3(0f, 10f, 0f), Quaternion.identity);
    }

    private void HandleAirSupportRequestFromKeyboard(object[] args)
    {
        Instantiate(m_AirStrikePrefab, m_PlayerTransform.localPosition + new Vector3(0f, 10f, 0f), Quaternion.identity);
    }

    private void HandleAirstrikeCollide(object[] args)
    {
        m_AirstrikeDetonated = true;
    }
}
