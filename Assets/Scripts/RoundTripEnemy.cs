using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoundTripEnemy : MonoBehaviour
{

    private int count;
    public int a = 1;
    public int limitCount = 200;

    void Update()
    {
        count += 1;
        transform.Translate(a * Vector2.left * Time.deltaTime);

        if (count == limitCount)
        {
            count = 0;
            // 反転テクニック・・・＞-1を掛ける
            //a = a * -1;
            a *= -1;
        }
    }
}
