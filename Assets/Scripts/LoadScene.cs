using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadScene : MonoBehaviour
{
    [SerializeField] float fadeTime;
    public void Load(string loadScene)
    {
        Initiate.Fade(loadScene, Color.black, fadeTime);
    } 
}
