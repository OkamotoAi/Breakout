using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallControl : MonoBehaviour
{
    public float speed = 20;
    GameObject gm; //ゲームマネージャ
    private bool gameEnd = false;
    bool isActive = false;
    // Start is called before the first frame update
    void Start()
    {
        // Vector3 v = new Vector3(-1,0,1);
        // GetComponent<Rigidbody> ().AddForce (v*speed, ForceMode.VelocityChange);
        gm = GameObject.Find ("GameManager");
    }

    // Update is called once per frame
    void Update()
    {
        if(!isActive){
            GameObject bar = GameObject.Find("Bar"); //バーオブジェクトを探す
            float r = this.transform.localScale.x/2; //球の半径
            float bar_thickness = bar.transform.localScale.z / 2; //バーの厚みの半分
            //球がバーの上にちょうど来るように配置
            this.transform.position = bar.transform.position + new Vector3 (0f,0f,bar_thickness+r); 
        }
    }

    void OnCollisionEnter(Collision obj){ 
        if (obj.gameObject.tag == "Block")
        {
            gameEnd = gm.GetComponent<GameManager>().DecreaseBlock();
            Destroy (obj.gameObject, 0f);
            if(gameEnd) {
                Destroy (this.gameObject, 0f);
            }
        }else if (obj.gameObject.tag == "Wall") { //”Wall”に当たったら
            Debug.Log("hit the wall");
            Destroy (this.gameObject, 0f); //球(自分自身)を壊す
            gm.GetComponent<GameManager>().DecreaseLife();
            
        }
    }

    public void Strike(){
        if(!isActive){
            Vector3 v = new Vector3 (Random.Range(-1f,1f), 0f, Random.Range(0f,1f)); 
        v = v.normalized;
        // Debug.Log(v);
        GetComponent<Rigidbody> ().AddForce (v*speed, ForceMode.VelocityChange);
        }
        isActive = true;
        
        
    }
    
    
}
