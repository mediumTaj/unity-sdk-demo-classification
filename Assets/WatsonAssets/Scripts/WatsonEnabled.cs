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
        
		if(Input.GetAxis("Fire2") == 1f)
			EventManager.Instance.SendEvent("OnAirSupportRequestFromKeyboard");

		if(Input.GetAxis("Fire3") == 1f)
			EventManager.Instance.SendEvent("OnPizzaRequestFromKeyboard");

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
        EventManager.Instance.RegisterEventReceiver("OnPauseRequest", HandlePauseRequest);
		EventManager.Instance.RegisterEventReceiver("OnUnpuaseRequest", HandleUnpauseRequest);
		EventManager.Instance.RegisterEventReceiver("OnHelpRequest", HandleHelpRequest);
		EventManager.Instance.RegisterEventReceiver("OnPauseRequestFromKeyboard", HandlePauseRequestFromKeyboard);
		EventManager.Instance.RegisterEventReceiver("OnUnpauseRequestFromKeyboard", HandleUnpauseRequestFromKeyboard);
		EventManager.Instance.RegisterEventReceiver("OnHelpRequestFromKeyboard", HandleHelpRequestFromKeyboard);
	}

	void OnDisable()
    {
        EventManager.Instance.UnregisterEventReceiver("OnAirSupportRequest", HandleAirSupportRequest);
        EventManager.Instance.UnregisterEventReceiver("OnAirSupportRequestFromKeyboard", HandleAirSupportRequestFromKeyboard);
        EventManager.Instance.UnregisterEventReceiver("OnPizzaRequest", HandlePizzaRequest);
        EventManager.Instance.UnregisterEventReceiver("OnPizzaRequestFromKeyboard", HandlePizzaRequestFromKeyboard);
        EventManager.Instance.UnregisterEventReceiver("OnAirstrikeCollide", HandleAirstrikeCollide);
        EventManager.Instance.UnregisterEventReceiver("OnPizzaCollected", HandlePizzaCollected);
		EventManager.Instance.UnregisterEventReceiver("OnPauseRequest", HandlePauseRequest);
		EventManager.Instance.UnregisterEventReceiver("OnUnpuaseRequest", HandleUnpauseRequest);
		EventManager.Instance.UnregisterEventReceiver("OnHelpRequest", HandleHelpRequest);
		EventManager.Instance.UnregisterEventReceiver("OnPauseRequestFromKeyboard", HandlePauseRequestFromKeyboard);
		EventManager.Instance.UnregisterEventReceiver("OnUnpauseRequestFromKeyboard", HandleUnpauseRequestFromKeyboard);
		EventManager.Instance.UnregisterEventReceiver("OnHelpRequestFromKeyboard", HandleHelpRequestFromKeyboard);
	}

    private void HandleAirSupportRequest(object[] args)
    {
        Instantiate(m_AirStrikePrefab, m_PlayerTransform.localPosition + new Vector3(0f, 8f, 0f) + (m_PlayerTransform.forward * 4), Quaternion.identity);
    }

    private void HandleAirSupportRequestFromKeyboard(object[] args)
    {
        Instantiate(m_AirStrikePrefab, m_PlayerTransform.localPosition + new Vector3(0f, 8f, 0f) + (m_PlayerTransform.forward * 4), Quaternion.identity);
    }

    private void HandleAirstrikeCollide(object[] args)
    {
        m_AirstrikeDetonated = true;
    }

    private void HandlePizzaRequest(object[] args)
    {
        Instantiate(m_PizzaPrefab, m_PlayerTransform.localPosition + new Vector3(0f, 10f, 0f) + (m_PlayerTransform.forward * 5), Quaternion.Euler(0.0f, Random.Range(0f, 360f), 0.0f));
    }

    private void HandlePizzaRequestFromKeyboard(object[] args)
    {
        Instantiate(m_PizzaPrefab, m_PlayerTransform.localPosition + new Vector3(0f, 10f, 0f) + (m_PlayerTransform.forward * 5), Quaternion.Euler(0.0f, Random.Range(0f, 360f), 0.0f));
    }

    private void HandlePizzaCollected(object[] args)
    {
        m_PizzaUIManager.IsPizzaPanelVisible = true;
        m_PauseManager.Pause();
    }

	private void HandlePauseRequest(object[] args)
	{
		Log.Debug("WatsonEnabled", "HandlePauseRequest");
	}

	private void HandleUnpauseRequest(object[] args)
	{
		Log.Debug("WatsonEnabled", "HandleUnpauseRequest");
	}

	private void HandleHelpRequest(object[] args)
	{
		Log.Debug("WatsonEnabled", "HandleHelpRequest");
	}

	private void HandlePauseRequestFromKeyboard(object[] args)
	{
		Log.Debug("WatsonEnabled", "HandlePauseRequestFromKeyboard");
	}

	private void HandleUnpauseRequestFromKeyboard(object[] args)
	{
		Log.Debug("WatsonEnabled", "HandleUnpauseRequestFromKeyboard");
	}

	private void HandleHelpRequestFromKeyboard(object[] args)
	{
		Log.Debug("WatsonEnabled", "HandleHelpRequestFromKeyboard");
	}
}
