using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonControll : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonPressed(){
        Debug.Log ("Start Button is pressed");
        SceneManager.LoadScene ("SampleScene");
    }

    public void Go2Title(){
        SceneManager.LoadScene ("New Scene");
    }

    public void Exit(){
        UnityEditor.EditorApplication.isPlaying = false;
    }


}
