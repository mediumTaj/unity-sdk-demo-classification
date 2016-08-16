using UnityEngine;
using System.Collections;

public class PizzaUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject m_PizzaOrderPanel;

    private bool m_IsPizzaPanelVisible = false;
    public bool IsPizzaPanelVisible
    {
        get { return m_IsPizzaPanelVisible; }
        set
        {
            m_IsPizzaPanelVisible = value;
            SetPizzaPanelVisibility();
        }
    }

    private void SetPizzaPanelVisibility()
    {
        m_PizzaOrderPanel.SetActive(IsPizzaPanelVisible);
    }
}
