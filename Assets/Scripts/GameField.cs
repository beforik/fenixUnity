using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GameField : MonoBehaviour
{
    [SerializeField] private PlayerCreator _playerCreator;

    [SerializeField] private Transform _field;

    [SerializeField] private List<Player> _players;

    private void Awake()
    {
        //CreatePlayer("0", Vector2.down); //todo remove
    }

    public void UpdatePlayerPosition(string id, Vector2 pos)
    {
        var player = _players.FirstOrDefault(x => x.Id == id);
        if(player != null)
            player.SetPosition(pos);
    }
    
    public void DoMovePlayer(string id, MoveTypes moveType)
    {
        var player = _players.FirstOrDefault(x => x.Id == id);
        if(player != null)
            player.DoMove(moveType);
    }

    //todo get start post from server 
    public void CreatePlayerOrUpdate(PlayerInfo playerInfo)
    {
        //print("[GameField] CreatePlayer id=" + playerInfo.id);
        Player player;
        player = _players.FirstOrDefault(x => x.Id == playerInfo.id);

        if (player == null)
        {
            player = _playerCreator.CreatePlayer(_field, playerInfo);
            _players.Add(player);
        }
        else
        {
            player.SetPosition(new Vector2(playerInfo.x, playerInfo.y));
        }
        
    }

    public void RemovePlayer(string id)
    {
        var player = _players.FirstOrDefault(x => x.Id == id);
        if (player != null)
            _players.Remove(player);
    }
    
    
    
}
