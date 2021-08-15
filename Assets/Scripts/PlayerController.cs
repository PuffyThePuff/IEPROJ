using UnityEngine;
using UnityEngine.AI;

public class PlayerController : MonoBehaviour
{
    public Camera cam;
    public NavMeshAgent agent;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //shoot a ray from the cam to mouse position
            Ray ray = cam.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            //returns true if ray hits an object
            if (Physics.Raycast(ray, out hit))
            {
                agent.SetDestination(hit.point);
            }
        }
    }
}
