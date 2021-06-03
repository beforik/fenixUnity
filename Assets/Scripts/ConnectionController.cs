using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Fenix;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ConnectionController : MonoBehaviour
{
    
    public static string token = "token";

    private CommandInvoker _commandInvoker;
    private JoystickController _joystickController;
    private Socket _socket;
    private IChannel _channel;

    private const string  host = "";
    private Dictionary<string, PlayerInfo> _playerInfos = new Dictionary<string, PlayerInfo>();

    
    private void Awake()
    {
        _commandInvoker = FindObjectOfType<CommandInvoker>();
        _joystickController = FindObjectOfType<JoystickController>();
        
        token =  SystemInfo.deviceModel;
        var settings = new Settings()
        {
            HeartbeatTimeout = TimeSpan.FromSeconds(1)
        };
        _socket = new Socket(settings);
        _socket.Connected += (sender, args) => { settings.Logger.Info("CONNECTED"); };
        
        _joystickController.OnDoMove += JoystickControllerOnOnDoMove;
        print("[ConnectionController]");
    }
    
    private void Update()
    {
        _commandInvoker.OnConnection(_playerInfos);
    }

    private void JoystickControllerOnOnDoMove(MoveTypes moveType)
    {
        SendMove(moveType);
    }

    private void SendMove(MoveTypes moveType)
    {
        switch (moveType)
        {
            case MoveTypes.Up:
                _channel.SendAsync(Commands.controll, new {x = 0, y = 1});
                break;
            case MoveTypes.Down:
                _channel.SendAsync(Commands.controll, new {x = 0, y = -1});
                break;
            case MoveTypes.Left:
                _channel.SendAsync(Commands.controll, new {x = -1, y = 0});
                break;
            case MoveTypes.Right:
                _channel.SendAsync(Commands.controll, new {x = 1, y = 0});
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(moveType), moveType, null);
        }
    }

    private void Start()
    {
        print("[Start]");
        Connect();
        StartCoroutine(WaitAndJoin());
    }

    IEnumerator WaitAndJoin(float waitTime = 2f)
    {
        yield return new WaitForSeconds(waitTime);
        print("[WaitAndJoin] time " + Time.time);
        JoinLobby();
    }
    
    private async void Connect()
    {
        try
        {
            print("tryConnected token=" + token);

            var uri = new Uri("ws://4f23a112.ngrok.io/socket/websocket");
            await _socket.ConnectAsync(uri, new[] {("token", token)});
            print("Connected!!!!!!!!!!");
        }
        catch (Exception ex)
        {
            print($"Error: [{ex.GetType().FullName}] \"{ex.Message}\"");
        }
    }

    private async void JoinLobby()
    {
        _channel = _socket.Channel("room:lobby", new {NickName = "Timotije"});

        
        _channel.Subscribe(ChannelEvents.Close,
            (ch, response) =>
            {
                print(ChannelEvents.Close);

            });

        _channel.Subscribe(Commands.onConnect, OnConnectCallback);
           

        _channel.Subscribe(Commands.playerJoined,
            (ch, response) =>
            {
                print(Commands.playerJoined + response);

                var playerInfo = response.ToObject<PlayerInfo>();
                _playerInfos[playerInfo.id] = playerInfo;
            });

        _channel.Subscribe(Commands.movement, (ch, response) =>
            {
                print(Commands.movement + response);
                var playerInfo = response.ToObject<PlayerInfo>();
                _playerInfos[playerInfo.id] = playerInfo;
                //_commandInvoker.SetPosition(playerInfo);
            }
        );
            
        try
        {
            var result = await _channel.JoinAsync();
            _socket.Settings.Logger.Info($"JOIN COMPLETED: status = {result.Status}, response: {result.Response}");
        }
        catch (Exception ex)
        {
            _socket.Settings.Logger.Error(ex);
        }
    }
    private void OnConnectCallback(IChannel arg1, JObject response)
    {
        print(Commands.onConnect + response);
                
        JArray a = (JArray)response["players"];
        IList<PlayerInfo> players = a.ToObject<IList<PlayerInfo>>();
        _playerInfos = players.ToDictionary(info => info.id);
        _commandInvoker.OnConnection(_playerInfos);
    }

 
}
