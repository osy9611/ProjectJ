using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[ExecuteAlways]
public class LightTest : MonoBehaviour
{
    public GameObject Head;
    public MeshRenderer mat;
    [SerializeField] Vector3 forward = new Vector3(0,1,0);
    [SerializeField] Vector3 right = new Vector3(-1,0,0);
    // Start is called before the first frame update
    void Start()
    {
        Head = this.gameObject;
        //mat = this.gameObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mat != null)
        {
            mat.sharedMaterial.SetVector("_HeadForward", Head.transform.TransformDirection(transform.forward));
            mat.sharedMaterial.SetVector("_HeadRight", Head.transform.TransformDirection(transform.right));
        }
       
    }
}
