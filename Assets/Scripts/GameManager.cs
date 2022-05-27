using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject blockPrefab;
    public GameObject ballPrefab;
    GameObject block;
    GameObject ball;
    GameObject[,] blocks; //ブロック用2次元配列 
    public int Columns=5; //ブロックの横の数
    public int Rows=5; //ブロックの縦の数 
    Vector3 blockSize; //ブロッックのサイズ 
    Vector3 origin; //ブロック配置の原点

    GameObject lifeText;
    GameObject statusText;
    GameObject title;
    GameObject again;
    int life=3; //ライフ
    int blockCount; //残ブロック数を覚えておく変数
    bool playing = true;





    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 1f;
        title = GameObject.Find("TITLE");
        again = GameObject.Find("AGAIN");
        title.SetActive(false);
        again.SetActive(false);
        // Columns = Random.Range(3,8);
        // Rows = Random.Range(4,15);
        // block = Instantiate (blockPrefab, new Vector3(0f, 0.5f, 2f), Quaternion.identity) as GameObject;
        GameObject ground = GameObject.Find("Ground"); //床オブジェクトを探す 
        Vector3 g_size = ground.transform.localScale; //⻑いので短い変数に入れ直し 
        blockSize.x = g_size.x / Columns; //床の横サイズ÷ブロックの列数 
        blockSize.y = blockPrefab.transform.localScale.y;
        blockSize.z = blockPrefab.transform.localScale.z;
        origin = new Vector3 (-g_size.x/2, blockSize.y/2, g_size.z/2); //原点
        GenerateBlocks();

        // ball = Instantiate (ballPrefab, transform.position, Quaternion.identity) as GameObject;
        GenerateBall();

        blockCount = Columns * Rows; //残ブロック数を初期化 
        //LIFEの表示
        lifeText = GameObject.Find("LIFE"); 
        this.lifeText.GetComponent<Text> ().text = "LIFE : " + life;
        //GameTextの設定
        statusText = GameObject.Find("STATUS"); 
        this.statusText.GetComponent<Text> ().text = "PRESS Space\n to START";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey (KeyCode.Space)) {
            if (ball != null) {
                this.statusText.SetActive (false);
                ball.GetComponent<BallControl> ().Strike ();
            } else { //ボールの作り直し 
                GenerateBall();
            }
        }
    }

    void GenerateBlocks(){
        blocks = new GameObject[Rows, Columns]; //ブロック用配列の生成 
        for (int row = 0; row < blocks.GetLength(0); row++) {
            //行の色の生成
            // Color32 color = new Color32(255, (byte)((row+1)*255 / blocks.GetLength(0)), (byte)((row+1)*255 / blocks.GetLength(0)), 1);
            Color32 color = Color.HSVToRGB((float)(row+1) / blocks.GetLength(0),0.7f,1f);
            // Debug.Log(color);

            for (int col = 0; col < blocks.GetLength(1); col++) {
                Vector3 v = origin + new Vector3(blockSize.x/2+blockSize.x*col, 0f,
                    -blockSize.z/2-blockSize.z*row);
                blocks [row, col] = Instantiate (blockPrefab, v, Quaternion.identity) as GameObject;
                blocks [row, col].transform.localScale = blockSize*0.9f; //みっちり敷き詰めると境 界が見えないので，少し小さく
                blocks [row, col].GetComponent<Renderer>().material.color = color;
            } 
        }
    }

    //ボールジェネレータ 
    void GenerateBall(){
        if(playing){
            //バーの位置を探す
        GameObject bar = GameObject.Find("Bar"); //バーオブジェクトを探す
        float r = ballPrefab.transform.localScale.x/2; //球の半径
        float bar_thickness = bar.transform.localScale.z / 2; //バーの厚みの半分
        //球がバーの上にちょうど来るように配置
        Vector3 p = bar.transform.position + new Vector3 (0f,0f,bar_thickness+r); 
        ball = Instantiate (ballPrefab, p, Quaternion.identity) as GameObject;
        }
        
    }

    public void DecreaseLife(){
        life--;
        this.lifeText.GetComponent<Text> ().text = "LIFE : " + life; 
        if (life == 0) {
            GameOver ();
        }else{
            GenerateBall();
        }
    }

    public void GameOver(){ //テキスト表示
        Debug.Log("Game Over!"); 
        statusText.GetComponent<Text> ().text = "GAME OVER!!"; 
        statusText.SetActive (true);
        title.SetActive(true);
        again.SetActive(true);
        Time.timeScale = 0f;
        playing = false;
        // UnityEditor.EditorApplication.isPlaying = false;
        
    }

    public bool DecreaseBlock(){
        blockCount--;
        if(blockCount == 0){
            statusText.GetComponent<Text> ().text = "GAME CLEAR!!"; 
            statusText.SetActive (true);
            title.SetActive(true);
            again.SetActive(true);
            Time.timeScale = 0f;
            playing = false;
            // UnityEditor.EditorApplication.isPlaying = false;
            return true;
        }
        return false;
    }
    
}
