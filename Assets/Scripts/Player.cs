using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _spriteRenderer;

    public Vector2 position;

    public string Id;

    public void Initialize(PlayerInfo playerInfo)
    {
        Color color = Color.white;
        ColorUtility.TryParseHtmlString(playerInfo.color, out color);
        _spriteRenderer.color = color;
        Id = playerInfo.id;
        position = new Vector2(playerInfo.x, playerInfo.y);
        UpdatePosition();
    }

    public void SetPosition(Vector2 pos)
    {
        position = pos;
        UpdatePosition();
    }
    

    public void DoMove(MoveTypes moveType)
    {
        switch (moveType)
        {
            case MoveTypes.Up:
                position.y++;
                break;
            case MoveTypes.Down:
                position.y--;
                break;
            case MoveTypes.Left:
                position.x--;
                break;
            case MoveTypes.Right:
                position.x++;
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(moveType), moveType, null);
        }
        UpdatePosition();
    }

    private void UpdatePosition()
    {
        transform.position = position;
    }
}
