using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterFollower : MonoBehaviour
{
    [SerializeField] private Transform objectToFollow;
    private Vector3 camOffset;

    private void Start()
    {
        camOffset = this.transform.position - objectToFollow.position;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = Vector3.Lerp(this.transform.position, objectToFollow.position + camOffset, Time.smoothDeltaTime);
    }
}
