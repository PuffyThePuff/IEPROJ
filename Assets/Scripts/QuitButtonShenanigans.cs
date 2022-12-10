using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QuitButtonShenanigans : MonoBehaviour
{
    [SerializeField] private GameObject quitMenu;

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.CompareTag("QuitButton") && Input.GetMouseButton(0) /*&& !(quitMenu.activeInHierarchy)*/) {
                //quitMenu.SetActive(true);
                Debug.Log("Hehe");
            }

            /*
            if(hit.collider.CompareTag("QuitMenuConfirm") && Input.GetMouseButton(0))
            {
                // Exit scene.
            }

            if(hit.collider.CompareTag("QuitMenuCancel") && Input.GetMouseButton(0))
            {
                quitMenu.SetActive(false);
            }
            */
        }

    }
}