using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float time;
    //[SerializeField] int minutes;
    //[SerializeField] int second;
    [SerializeField] GameObject load;
    [SerializeField] string sceneName = "GameOver";
    LoadScene loadScene;
    bool isWorking;
    [SerializeField] Text timerText;

    void Start()
    {
        isWorking = true;
        loadScene = load.GetComponent<LoadScene>();
    }

    void Update()
    {
        if (isWorking)
        {
            time -= Time.deltaTime;

            //minutes = (int)time / 60;
            //second = (int)time % 60;

            //timerText.text = minutes+ ":" + second;

            TimeFormatTest(time);

            if (time <= 0)
            {
                isWorking = false;
                loadScene.Load(sceneName);
            }
        }
    }

    public void TimeFormatTest(float seconds)
    {
        TimeSpan ts = new TimeSpan(0, 0, (int)seconds);
        string time = ts.ToString(@"mm\:ss");
        timerText.text = time;
        //Debug.LogFormat("{0} 秒は {1} です。", seconds.ToString(), time);
    }

}
