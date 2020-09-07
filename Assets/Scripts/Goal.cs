﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class Goal : MonoBehaviour
{
    [SerializeField] private GameObject text;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Text goal = text.GetComponent<Text>();
            goal.text = PhotonNetwork.LocalPlayer.ActorNumber + "P Goal!!";
        }
    }
}
