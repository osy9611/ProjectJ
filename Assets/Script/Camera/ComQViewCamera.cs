using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[RequireComponent(typeof(Camera))]
public class ComQViewCamera : MonoBehaviour
{
    [SerializeField]
    bool isFollowPlayer = true;

    [SerializeField]
    Vector3 distanceFromPlayer = new Vector3();

    [SerializeField]
    Quaternion rotation;

    private Transform target;
    private bool isStopHeight = false;
    private Camera camera;
    public Camera Camera => camera;


    // Start is called before the first frame update
    void Awake()
    {
       camera= GetComponent<Camera>();
    }

    public void SetTarget(Transform target)
    {
        this.target = target;
        transform.position = target.position - distanceFromPlayer;
        transform.rotation = rotation;
    }

    private void FollowPlayer()
    {
        if (target == null || !isFollowPlayer)
            return;
        if(isStopHeight)
        {
            Vector3 pos = target.position - distanceFromPlayer;
            pos.y = transform.position.y;
            transform.position = pos;
        }
        else
        {
            Vector3 pos = target.transform.position - distanceFromPlayer;
            transform.position = pos;
        }
    }

    void LateUpdate()
    {
        FollowPlayer();
    }

#if UNITY_EDITOR
    public void SetDistanceEditor(GameObject source)
    {
        distanceFromPlayer = source.transform.position - transform.position;
        rotation = transform.rotation;
    }
#endif
}
