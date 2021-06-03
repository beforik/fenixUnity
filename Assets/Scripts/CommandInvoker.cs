using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CommandInvoker : MonoBehaviour
{
    private GameField _gameField;

    private void Awake()
    {
        _gameField = FindObjectOfType<GameField>();
    }
    
    public void DoMove(MoveTypes moveType)
    {
        _gameField.DoMovePlayer(ConnectionController.token, moveType);
    }

    public void SetPosition(PlayerInfo playerInfo)
    {
        Vector2 vector2 = new Vector2(playerInfo.x, playerInfo.y);
        
        _gameField.UpdatePlayerPosition(playerInfo.id, vector2);
    }

    public void OnConnection(Dictionary<string, PlayerInfo> playerInfos)
    {
        //print("[CommandInvoker] OnConnection" + playerInfos.Count);
        foreach (var playerInfo in playerInfos)
        {
            CreatePlayer(playerInfo.Value);
        }
    }
    
    public void OnConnection(IList<PlayerInfo> playerInfos)
    {
        print("[CommandInvoker] OnConnection" + playerInfos.Count);
        foreach (var playerInfo in playerInfos)
        {
            CreatePlayer(playerInfo);
        }
    }

    public void CreatePlayer(PlayerInfo playerInfo)
    {
        _gameField.CreatePlayerOrUpdate(playerInfo);
    }
    
    public void RemovePlayer(PlayerInfo playerInfo)
    {
        string id = playerInfo.id;
        _gameField.RemovePlayer(id);
    }
}
