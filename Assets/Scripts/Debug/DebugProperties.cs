using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugProperties : MonoBehaviour
{
    public struct TVector3
    {
        public int x;
        public int y;
        public int z;
    }



    public Vector3 Temp { get; set; }
    // Use this for initialization
    void Start()
    {
        /*
        Temp = new TVector3();

        Temp.x = 4;
        */
    }

    // Update is called once per frame
    void Update()
    {

    }
}
