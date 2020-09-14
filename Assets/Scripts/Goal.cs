using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon;  // EventData を使うため

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject text;
    private Text goal;
    private bool isGoal = false;

    private void Start()
    {
        goal = text.GetComponent<Text>();
    }

    /// <summary>オブジェクトが有効になった時にイベントにメソッドを登録する</summary>
    private void OnEnable()
    {
        PhotonNetwork.NetworkingClient.EventReceived += EventReceived;
    }

    /// <summary>オブジェクトが無効になった時にイベントからメソッドを解除する</summary>
    private void OnDisable()
    {
        PhotonNetwork.NetworkingClient.EventReceived -= EventReceived;
    }

    /// <summary>
    /// イベントデータとして渡された内容をログに出力する
    /// </summary>
    /// <param name="e">イベントデータ</param>
    void EventReceived(EventData e)
    {
        if ((int)e.Code < 1)  // 200 以上はシステムで使われているので処理しない
        {
            isGoal = true;
            goal.text = e.CustomData + "P Goal!!";
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && !isGoal)
        {
            PlayerController playerController = collision.GetComponent<PlayerController>();
            playerController.Raise();
        }
    }
}
