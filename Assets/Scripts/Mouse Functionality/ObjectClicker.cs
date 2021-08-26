using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectClicker : MonoBehaviour
{
    [SerializeField] private Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //shoot a ray from the cam to mouse position
        Ray ray = cam.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        //returns true if ray hits an object
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.tag == "Interactable")
            {
                hit.transform.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

                if (Input.GetMouseButtonDown(0))
                {
                    GameObject nodeParser = hit.transform.GetChild(0).gameObject;
                    nodeParser.SetActive(true);
                }
            }
        }

    }
}
