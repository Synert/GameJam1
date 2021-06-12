using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TwitchLib.Client.Models;
using TwitchLib.Unity;
using TwitchLib.Client.Events;
using System;

public class TwitchClient : MonoBehaviour
{
    public Client client;
    private string channel_name = "Insert channel name here";
    public GameObject player;
    public float topScore = 0;
    public Text topScoreText;
    public float score = 0;
    public Text ScoreText;
    public AbilityData[] ab;
    
    public Dictionary<string, playerData> pd = new Dictionary<string, playerData>();

    public class playerData
    {

        public string playerName;
        public float points;

        public playerData(string _playerName, int _points)
        {
            playerName = _playerName;
            points = _points;
        }

    }

    void Awake()
	{
        //check if another of this type exists, if so copy over manual items and delete self
        if (GameObject.FindObjectsOfType<TwitchClient>().Length > 1)
        {
            TwitchClient[] clients = GameObject.FindObjectsOfType<TwitchClient>();
            for (int a = 0; a < clients.Length; a++)
            {
                if (clients[a] != this)
                {
                    clients[a].ScoreText = ScoreText;
                    clients[a].topScoreText = topScoreText;
                    clients[a].player = player;
                }
            }
		    GameObject.Destroy(this.gameObject);
        }
        else
        {
            GameObject.DontDestroyOnLoad (this.gameObject);
        }
	}

    // Start is called before the first frame update
    void Start()
    {
        //make sure app runs in background for consistent API responses
        Application.runInBackground = true;

        //setup bot
        ConnectionCredentials credentials = new ConnectionCredentials("Insert Bot Name here", Secrets.bot_access_token);
        client = new Client();
        client.Initialize(credentials, channel_name);

        //setup message listening and connect to channel
        client.OnMessageReceived += clientRecievedMessage;
        client.Connect();
    }

    void Update()
    {
        //add points to viewers for watching every second
        foreach(KeyValuePair<string, playerData> _pd in pd)
        {
            _pd.Value.points += Time.deltaTime;
        }

        //display current player's score
        score = player.transform.position.x;
        ScoreText.text = "Current score: " + ((int)score).ToString();

        if (score > topScore)
        {
            topScore = score;
        }

        topScoreText.text = "Top score: " + ((int)topScore).ToString();


    }

    //handle messages read from bot
    void clientRecievedMessage(object sender, OnMessageReceivedArgs e)
    {
        if (e.ChatMessage.Message == "!join")
        {
            //if message is join and player doesn't exist, add player and respond in chat
            playerData _pd;
            if (pd.TryGetValue(e.ChatMessage.DisplayName, out _pd) == false)
            {
                pd.Add(e.ChatMessage.DisplayName, new playerData(e.ChatMessage.DisplayName, 0));
                client.SendMessage(client.JoinedChannels[0], "User: " + e.ChatMessage.DisplayName + " has joined the game");
            }
        }
        else if (e.ChatMessage.Message == "!points")
        {
            //if message is points and player exists, respond in chat with points otherwise ask them to say !join
            playerData _pd;
            if (pd.TryGetValue(e.ChatMessage.DisplayName, out _pd))
            {
                client.SendMessage(client.JoinedChannels[0], e.ChatMessage.DisplayName + " You have " + Mathf.FloorToInt(_pd.points) + " points");
            }
            else
            {
                client.SendMessage(client.JoinedChannels[0], e.ChatMessage.DisplayName + " Use !join to start playing");
            }
        }
        else
        {
            for (var a = 0; a < ab.Length; a++)
            {
                //loop through each ability data and check if message recieve is a call for it
                if (e.ChatMessage.Message.Substring(0,2) == "!" + (a + 1).ToString())
                {
                    //parse the viewer's delay for spawning
                    int delay = int.Parse(e.ChatMessage.Message.Substring(2,e.ChatMessage.Message.Length - 2));

                    playerData _pd;
                    if (pd.TryGetValue(e.ChatMessage.DisplayName, out _pd))
                    {
                        if (_pd.points >= ab[a].pointCost)
                        {
                            //if viewer exists and has enough points, remove points and spawn object, then reply to viewer in chat
                            _pd.points -= ab[a].pointCost;

                            ab[a].activate(player.transform.position
                                + new Vector3(ab[a].initialOffset, 0, 0)
                                + new Vector3(Mathf.Abs (player.GetComponent<Rigidbody2D> ().velocity.x) * delay, 0, ab[a].layer));

                            client.SendMessage(client.JoinedChannels[0], e.ChatMessage.DisplayName + " You have spawned a " + ab[a].name +  " You have " + Mathf.FloorToInt(_pd.points) + " points left");
                        }
                        else
                        {

                            client.SendMessage(client.JoinedChannels[0], e.ChatMessage.DisplayName + " You do not have enough points: " + Mathf.FloorToInt(_pd.points));

                        }
                    }
                    else
                    {
                        client.SendMessage(client.JoinedChannels[0], e.ChatMessage.DisplayName + " Use !join to start playing");
                    }
                }
            }
        }
    }
}
