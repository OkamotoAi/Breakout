using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemControll : MonoBehaviour
{
    Color32 color;

    //0=バーが伸びる，1＝バーが短くなる
    public int status;

    // Start is called before the first frame update
    void Start()
    {
        status = 0;
        // GetComponent<Renderer>().material.color = color;
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x,this.transform.position.y,this.transform.position.z-0.1f);
    }
}
