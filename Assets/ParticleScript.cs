using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleScript : MonoBehaviour
{
    public float Duration;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Duration = Mathf.Clamp(Duration - Time.deltaTime, 0, Duration);
        if(Duration == 0)
        {
            Destroy(gameObject);
        }
    }
}
