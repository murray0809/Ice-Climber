using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NCMB;

public class AccountName : MonoBehaviour
{
    TextMesh text;
    Transform player;
    Vector3 left = new Vector3(0.1f, 0.1f, 1);
    Vector3 right = new Vector3(-0.1f, 0.1f, 1);
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player(Clone)").GetComponent<Transform>();
        text = GetComponent<TextMesh>();
        text.text = NCMBUser.CurrentUser.UserName;
    }

    private void Update()
    {
        Debug.Log(player.lossyScale);
        if (player.lossyScale.x > 0)
        {
            gameObject.transform.localScale = left;
        }
        else
        {
            gameObject.transform.localScale = right;
        }
    }
}
