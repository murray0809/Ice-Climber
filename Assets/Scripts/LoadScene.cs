using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScene : MonoBehaviour
{
    [SerializeField] float fadeTime;
    
    public void Load(string sceneName)
    {
        Debug.Log("Load");
        Initiate.Fade(sceneName, Color.black, fadeTime);
    } 
}
