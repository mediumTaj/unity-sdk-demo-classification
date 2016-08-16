using UnityEngine;
using IBM.Watson.DeveloperCloud.Logging;
using IBM.Watson.DeveloperCloud.Utilities;
using UnityEngine.UI;

public class WatsonEnabled : MonoBehaviour
{
    [SerializeField]
    private GameObject m_AirStrikePrefab;
    [SerializeField]
    private GameObject m_PizzaPrefab;

    [SerializeField]
    private Transform m_PlayerTransform;
    [SerializeField]
    private GameObject m_PizzaPanel;
    [SerializeField]
    private PauseManager m_PauseManager;
    [SerializeField]
    private PizzaUIManager m_PizzaUIManager;

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
        if (m_AirstrikeDetonated)
        {
            m_FlashImage.color = m_FlashColor;
        }
        else
        {
            m_FlashImage.color = Color.Lerp(m_FlashImage.color, Color.clear, m_FlashSpeed * Time.deltaTime);
        }
        
        m_AirstrikeDetonated = false;
    }

    void OnEnable()
    {
        EventManager.Instance.RegisterEventReceiver("OnAirSupportRequest", HandleAirSupportRequest);
        EventManager.Instance.RegisterEventReceiver("OnAirSupportRequestFromKeyboard", HandleAirSupportRequestFromKeyboard);
        EventManager.Instance.RegisterEventReceiver("OnPizzaRequest", HandlePizzaRequest);
        EventManager.Instance.RegisterEventReceiver("OnPizzaRequestFromKeyboard", HandlePizzaRequestFromKeyboard);
        EventManager.Instance.RegisterEventReceiver("OnAirstrikeCollide", HandleAirstrikeCollide);
        EventManager.Instance.RegisterEventReceiver("OnPizzaCollected", HandlePizzaCollected);
    }

    void OnDisable()
    {
        EventManager.Instance.UnregisterEventReceiver("OnAirSupportRequest", HandleAirSupportRequest);
        EventManager.Instance.UnregisterEventReceiver("OnAirSupportRequestFromKeyboard", HandleAirSupportRequestFromKeyboard);
        EventManager.Instance.UnregisterEventReceiver("OnPizzaRequest", HandlePizzaRequest);
        EventManager.Instance.UnregisterEventReceiver("OnPizzaRequestFromKeyboard", HandlePizzaRequestFromKeyboard);
        EventManager.Instance.UnregisterEventReceiver("OnAirstrikeCollide", HandleAirstrikeCollide);
        EventManager.Instance.UnregisterEventReceiver("OnPizzaCollected", HandlePizzaCollected);
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

    private void HandlePizzaRequest(object[] args)
    {
        Instantiate(m_PizzaPrefab, m_PlayerTransform.localPosition + new Vector3(0f, 10f, 0f), Quaternion.identity);
    }

    private void HandlePizzaRequestFromKeyboard(object[] args)
    {
        Instantiate(m_PizzaPrefab, m_PlayerTransform.localPosition + new Vector3(0f, 10f, 0f), Quaternion.identity);
    }

    private void HandlePizzaCollected(object[] args)
    {
        m_PizzaUIManager.IsPizzaPanelVisible = true;
        m_PauseManager.Pause();
    }
}
