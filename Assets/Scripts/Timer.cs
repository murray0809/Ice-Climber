using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] float time;
    [SerializeField] int minutes;
    [SerializeField] int second;
    bool isWorking;
    [SerializeField] Text timerText;
    void Start()
    {
        isWorking = true;
    }

    void Update()
    {
        if (isWorking)
        {
            time -= Time.deltaTime;

            minutes = (int)time / 60;
            second = (int)time % 60;

            timerText.text = minutes+ ":" + second;
          
            if (time <= 0)
            {
                isWorking = false;
            }
        }
    }
}
