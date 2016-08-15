using UnityEngine;
using System.Collections;
using IBM.Watson.DeveloperCloud.Utilities;

public class PizzaPanel : MonoBehaviour
{
    void OnEnable()
    {
        EventManager.Instance.RegisterEventReceiver("OnClosePizzaPanel", OnClosePizzaPanel);
    }

    void OnDisable()
    {
        EventManager.Instance.UnregisterEventReceiver("OnClosePizzaPanel", OnClosePizzaPanel);
    }

    private void OnClosePizzaPanel(object[] args)
    {
        gameObject.SetActive(false);
    }
}
