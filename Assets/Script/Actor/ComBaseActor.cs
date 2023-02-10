using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComBaseActor : MonoBehaviour
{
    SkillAgent skillAgent;
    // Start is called before the first frame update
    void Start()
    {
        skillAgent = new SkillAgent();
        skillAgent.Init();
        skillAgent.AddBuff(1);   
    }

    // Update is called once per frame
    void Update()
    {
        skillAgent.Execute();
    }
}
