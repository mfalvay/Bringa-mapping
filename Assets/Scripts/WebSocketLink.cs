using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System;
using WebSocketSharp;
using WebSocketSharp.Server;

using UnityEngine.Events;


public class MessageRelay
{
    public delegate void RelayCallback(string s);
    public event RelayCallback callbacks;

    public void Relay(string s) {
        callbacks(s);
    }
}

public class WSService : WebSocketBehavior {

    public MessageRelay relay;

    protected override void OnMessage(MessageEventArgs e)
    {
        relay.Relay(e.Data);
    }

    protected override void OnOpen()
    {
    }
}

public class WebSocketLink : MonoBehaviour
{
    static WebSocketServer server = null;

    public static Dictionary<string, MessageRelay> channels = new Dictionary<string, MessageRelay>();

    public BicycleRack bicycleRack;

    public string channel = "test";
    public UnityEvent testEvent;

    string msg;
    bool flag;

    void Awake()
    {
        if (server != null)
        {
            server.Stop();
            server = null;
        }
        channels.Clear();


    }

    private void Start()
    {
        if (server == null)
        {
            server = new WebSocketServer(81);
            server.Start();
        }
        if (!channels.ContainsKey(channel))
        {
            channels.Add(channel, new MessageRelay());
            server.AddWebSocketService<WSService>($"/{channel}", () => new WSService {relay = channels[channel]});
        }
        channels[channel].callbacks += OnMessage;

    }
    private void Update()
    {
        if (flag) {
            flag = !flag;
            Debug.Log(msg);

            bicycleRack.Raise();
        }
        
    }

    private void OnMessage(string s)
    {
        //Debug.Log(name);
        //Debug.Log("Maybe wored: " + s);
        //testEvent.Invoke();
        flag = true;
        msg = s;


    }

    private void OnDisable()
    {
        if (server != null)
        {
            server.Stop();
            server = null;
        }
    }

    static public void TestPrint(string s)
    {
        Debug.Log(s);
    }

}
