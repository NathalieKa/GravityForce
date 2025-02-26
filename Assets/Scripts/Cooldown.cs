using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cooldown : MonoBehaviour
{

    //  QUELLE: https://www.youtube.com/watch?v=5r6RmddoR80


    private float cooldownTime = 0.6f;
    private float nextFireTime;

    public bool isCoolingDown => Time.time < nextFireTime;
    public void StartCoolDown() => nextFireTime = Time.time + cooldownTime;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
