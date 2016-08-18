using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Utilities;
using UnityEngine.UI;
using IBM.Watson.DeveloperCloud.Logging;

public class PizzaPanel : MonoBehaviour
{
    [SerializeField]
    private InputField m_NameField;
    [SerializeField]
    private InputField m_AddressField1;
    [SerializeField]
    private InputField m_AddressField2;
    [SerializeField]
    private InputField m_PhoneNumberField;
    [SerializeField]
    private InputField m_CreditCardField;
    [SerializeField]
    private InputField m_ExperiationDateField;
    [SerializeField]
    private Button m_SubmitButton;
    [SerializeField]
    private Button m_OkButton;
    [SerializeField]
    private Text m_ThankYouText;
    [SerializeField]
    private Text m_ResultText;

    private string m_Name;
    private string m_Address1;
    private string m_Address2;
    private string m_DeliverTime = "20 Minutes";
    private string m_ResultString = "It will arrive in \n{0} \n\n{1}\n{2}\n{3}";

    [SerializeField]
    private PauseManager m_PauseManager;
    [SerializeField]
    private PizzaUIManager m_PizzaUIManager;

    public void OnSubmit()
    {
        m_Name = m_NameField.text;
        m_Address1 = m_AddressField1.text;
        m_Address2 = m_AddressField2.text;

        m_ResultText.text = string.Format(m_ResultString, m_DeliverTime, m_Name, m_Address1, m_Address2);
        SetFieldVisibility(false);
        SetResultVisibility(true);
    }

    public void OnOK()
    {
        ClearData();
        SetFieldVisibility(true);
        SetResultVisibility(false);

        m_PizzaUIManager.IsPizzaPanelVisible = false;
        m_PauseManager.Pause();
    }

    private void SetFieldVisibility(bool isVisible)
    {
        m_NameField.gameObject.SetActive(isVisible);
        m_AddressField1.gameObject.SetActive(isVisible);
        m_AddressField2.gameObject.SetActive(isVisible);
        m_PhoneNumberField.gameObject.SetActive(isVisible);
        m_CreditCardField.gameObject.SetActive(isVisible);
        m_ExperiationDateField.gameObject.SetActive(isVisible);
        m_SubmitButton.gameObject.SetActive(isVisible);
    }

    private void SetResultVisibility(bool isVisible)
    {
        m_ThankYouText.gameObject.SetActive(isVisible);
        m_ResultText.gameObject.SetActive(isVisible);
        m_OkButton.gameObject.SetActive(isVisible);
    }

    private void ClearData()
    {
        m_Name = "";
        m_Address1 = "";
        m_Address2 = "";

        m_ResultText.text = "";
        m_NameField.text = "Taj Santiago";
        m_AddressField1.text = "11501 Burnet Rd";
        m_AddressField2.text = "Austin, TX 78758";
        m_PhoneNumberField.text = "310-555-5555";
        m_CreditCardField.text = "123456789012";
        m_ExperiationDateField.text = "02/18";
    }

    void OnEnable()
    {
		EventManager.Instance.RegisterEventReceiver("OnSubmitButtonPressed", HandleSubmitPressed);
		EventManager.Instance.RegisterEventReceiver("OnOKButtonPressed", HandleOKPressed);

        ClearData();
        SetFieldVisibility(true);
        SetResultVisibility(false);

    }

	void OnDisable()
	{
		EventManager.Instance.UnregisterEventReceiver("OnSubmitButtonPressed", HandleSubmitPressed);
		EventManager.Instance.UnregisterEventReceiver("OnOKButtonPressed", HandleOKPressed);
	}

	private void HandleOKPressed(object[] args)
	{
		OnOK();
	}

	private void HandleSubmitPressed(object[] args)
	{
		OnSubmit();
	}

	void Update()
	{
		if(Input.GetButton("OK"))
			EventManager.Instance.SendEvent("OnOKButtonPressed");
		if(Input.GetButton("Submit"))
			EventManager.Instance.SendEvent("OnSubmitButtonPressed");
	}
}
