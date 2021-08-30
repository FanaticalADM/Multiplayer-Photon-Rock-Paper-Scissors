using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowPlayer : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] public GameObject player;
    private bool isFollowing = false;

    private void Start()
    {
        StartFollowing();
    }

    void LateUpdate()
    {
        if(isFollowing)
        transform.position = player.transform.position + offset;
    }
    
    public void StartFollowing()
    {
        isFollowing = true;
    }

    public void StopFollowing()
    {
        isFollowing = false;
    }

}
