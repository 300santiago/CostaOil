using System.Collections;
using System.Collections.Generic;
using Nakama;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NakamaSessionHandler : MonoBehaviour
{

    public static IClient _client;
    public static ISocket _socket;
    public static ISession _session;
    public static NakamaSessionHandler instance;

    public GameObject socketCanvas;
    public GameObject loadingPanel;


    private void Awake() {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _socket.Closed += () => ShowConnectionError();
        _socket.Connected += () => Debug.Log("Socket connected.");
        _socket.ReceivedError += e => Debug.Log("Socket error: " + e.Message);
    }

    // Update is called once per frame
    void Update()
    {
        //print(_socket?.IsConnected);
    }

    public void AssignRefrences(IClient tempClient, ISocket tempSocket)
    {
        _socket = tempSocket;
        _client = tempClient;
    }

    public void ShowConnectionError()
    {
        if (SceneManager.GetActiveScene().name == "MainMenu") { return; }
        socketCanvas.SetActive(true);
    }
}
