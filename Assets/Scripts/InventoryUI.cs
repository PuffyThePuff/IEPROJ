using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    [SerializeField] private GameObject InventoryPanel;

    public void open_close_inv()
    {
        if (InventoryPanel.activeInHierarchy)
        {
            InventoryPanel.SetActive(false);
        }
        else
        {
            InventoryPanel.SetActive(true);
        }
    }
}
