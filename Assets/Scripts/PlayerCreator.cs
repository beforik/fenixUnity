using UnityEngine;

public class PlayerCreator : MonoBehaviour
{
    [SerializeField] private Player _playerPrefab;

    public Player CreatePlayer(Transform fieldTransform, PlayerInfo playerInfo)
    {
        var goPlayer = Instantiate(_playerPrefab, fieldTransform);
        goPlayer.Initialize(playerInfo); 
        return goPlayer;
    }
}
