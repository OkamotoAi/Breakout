using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarControll : MonoBehaviour
{
    GameObject ground;
    GameObject item;
    Vector3 v;
    int status;
    // Start is called before the first frame update
    void Start()
    {
        ground = GameObject.Find ("Ground");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.LeftArrow)) {
            // 壁を突き抜けないように制限
            if(transform.position.x-transform.localScale.x/2 > -ground.transform.localScale.x/2)
                transform.Translate (-0.1f, 0f, 0f); //左(X軸負方向)に少し動かす
        }

        if (Input.GetKey (KeyCode.RightArrow)) {
            // 壁を突き抜けないように制限
            if(transform.position.x+transform.localScale.x/2 < ground.transform.localScale.x/2)
                transform.Translate (0.1f, 0f, 0f); //右(X軸正方向)に少し動かす
        }
    }

    void OnTriggerEnter(Collider obj){ 
        if (obj.gameObject.tag == "Item")
        {
            this.transform.localScale = new Vector3(this.transform.localScale.x * 1.2f,this.transform.localScale.y,this.transform.localScale.z);
            Destroy (obj.gameObject, 0f);
        }
    }
}
