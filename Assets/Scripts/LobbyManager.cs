using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Realtime;
using Photon.Pun;
using ExitGames.Client.Photon;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LobbyManager : MonoBehaviourPunCallbacks
{

    // Start is called before the first frame update
    void Start()
    {
        ConnectPhoton();
    }

    void ConnectPhoton()
    {
        if (!PhotonNetwork.IsConnected)
        {
            PhotonNetwork.GameVersion = "1.0";
            PhotonNetwork.ConnectUsingSettings();
        }
    }

    /// <summary>
    /// ロビーに入る
    /// </summary>
    private void JoinLobby()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
    }

    //void OnReceivedRoomListUpdate()
    //{
    //    //ルーム一覧を取る
    //    RoomInfo[] rooms = PhotonNetwork.GetCustomRoomList(TypedLobby.Default, sqlLobbyFilter: "where");
    //    if (rooms.Length == 0)
    //    {
    //        Debug.Log("ルームが一つもありません");
    //    }
    //    else
    //    {
    //        //ルームが1件以上ある時ループでRoomInfo情報をログ出力
    //        for (int i = 0; i < rooms.Length; i++)
    //        {
    //            Debug.Log("RoomName:" + rooms[i].Name);
    //            Debug.Log("userName:" + rooms[i].CustomProperties["userName"]);
    //            Debug.Log("userId:" + rooms[i].CustomProperties["userId"]);
    //            GameObject.Find("StatusText").GetComponent<Text>().text = rooms[i].Name;
    //        }
    //    }
    //}

    /// <summary>
    /// ランダムな名前のルームを作って参加する
    /// </summary>
    public void CreateRandomRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            RoomOptions roomOptions = new RoomOptions
            {
                IsVisible = true,   // 誰でも参加できるようにする
                MaxPlayers = 2
            };
            /* **************************************************
             * spawPositions の配列長を最大プレイ人数とする。
             * 無料版では最大20まで指定できる。
             * MaxPlayers の型は byte なのでキャストしている。
             * MaxPlayers の型が byte である理由はおそらく1ルームのプレイ人数を255人に制限したいためでしょう。
             * **************************************************/
            //roomOptions.MaxPlayers = (byte)m_spawnPositions.Length;
            PhotonNetwork.CreateRoom(null, roomOptions); // ルーム名に null を指定するとランダムなルーム名を付ける
            GameStart();
        }
    }

    /// <summary>
    /// 既に存在する部屋に参加する
    /// </summary>
    public void JoinExistingRoom()
    {
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinRandomRoom();
            GameStart();
        }
        
    }

    private void GameStart()
    {
        SceneManager.LoadScene("Main");
    }

    /// <summary>Photon に接続した時</summary>
    public override void OnConnected()
    {
        Debug.Log("OnConnected");
        //SetMyNickName(System.Environment.UserName + "@" + System.Environment.MachineName);
    }

    /// <summary>マスターサーバーに接続した時</summary>
    public override void OnConnectedToMaster()
    {
        Debug.Log("OnConnectedToMaster");
        JoinLobby();
    }

    /// <summary>Photon との接続が切れた時</summary>
    public override void OnDisconnected(DisconnectCause cause)
    {
        Debug.Log("OnDisconnected");
    }

    public override void OnJoinedLobby()
    {
        Debug.Log("OnJoinedLobby");
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (var item in buttons)
        {
            item.interactable = true;
        }
    }

    /// <summary>ロビーから出た時</summary>
    public override void OnLeftLobby()
    {
        Debug.Log("OnLeftLobby");
        Button[] buttons = FindObjectsOfType<Button>();
        foreach (var item in buttons)
        {
            item.interactable = false;
        }
    }

    /// <summary>部屋を作成した時</summary>
    public override void OnCreatedRoom()
    {
        Debug.Log("OnCreatedRoom");
    }

    /// <summary>部屋の作成に失敗した時</summary>
    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnCreateRoomFailed: " + message);
    }

    /// <summary>部屋に入室した時</summary>
    public override void OnJoinedRoom()
    {
        Debug.Log("OnJoinedRoom");
    }

    /// <summary>指定した部屋への入室に失敗した時</summary>
    public override void OnJoinRoomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRoomFailed: " + message);
    }

    /// <summary>ランダムな部屋への入室に失敗した時</summary>
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        Debug.Log("OnJoinRandomFailed: " + message);
        CreateRandomRoom();
    }

    /// <summary>部屋から退室した時</summary>
    public override void OnLeftRoom()
    {
        Debug.Log("OnLeftRoom");
    }

    /// <summary>自分のいる部屋に他のプレイヤーが入室してきた時</summary>
    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        Debug.Log("OnPlayerEnteredRoom: " + newPlayer.NickName);
    }

    /// <summary>自分のいる部屋から他のプレイヤーが退室した時</summary>
    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Debug.Log("OnPlayerLeftRoom: " + otherPlayer.NickName);
    }

    /// <summary>マスタークライアントが変わった時</summary>
    public override void OnMasterClientSwitched(Player newMasterClient)
    {
        Debug.Log("OnMasterClientSwitched to: " + newMasterClient.NickName);
    }

    /// <summary>ロビー情報に更新があった時</summary>
    public override void OnLobbyStatisticsUpdate(List<TypedLobbyInfo> lobbyStatistics)
    {
        Debug.Log("OnLobbyStatisticsUpdate");
    }

    /// <summary>ルームリストに更新があった時</summary>
    public override void OnRoomListUpdate(List<RoomInfo> roomList)
    {
        Debug.Log("OnRoomListUpdate");
    }

    /// <summary>ルームプロパティが更新された時</summary>
    public override void OnRoomPropertiesUpdate(ExitGames.Client.Photon.Hashtable propertiesThatChanged)
    {
        Debug.Log("OnRoomPropertiesUpdate");
    }

    /// <summary>プレイヤープロパティが更新された時</summary>
    public override void OnPlayerPropertiesUpdate(Player target, ExitGames.Client.Photon.Hashtable changedProps)
    {
        Debug.Log("OnPlayerPropertiesUpdate");
    }

    /// <summary>フレンドリストに更新があった時</summary>
    public override void OnFriendListUpdate(List<FriendInfo> friendList)
    {
        Debug.Log("OnFriendListUpdate");
    }

    /// <summary>地域リストを受け取った時</summary>
    public override void OnRegionListReceived(RegionHandler regionHandler)
    {
        Debug.Log("OnRegionListReceived");
    }

    /// <summary>WebRpcのレスポンスがあった時</summary>
    public override void OnWebRpcResponse(OperationResponse response)
    {
        Debug.Log("OnWebRpcResponse");
    }

    /// <summary>カスタム認証のレスポンスがあった時</summary>
    public override void OnCustomAuthenticationResponse(Dictionary<string, object> data)
    {
        Debug.Log("OnCustomAuthenticationResponse");
    }

    /// <summary>カスタム認証が失敗した時</summary>
    public override void OnCustomAuthenticationFailed(string debugMessage)
    {
        Debug.Log("OnCustomAuthenticationFailed");
    }
}
