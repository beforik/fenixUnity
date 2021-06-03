using System;
using UnityEngine;

public class CommandInvokerTestFromJoyStick : MonoBehaviour
{

   private GameField _gameField;
   private JoystickController _joystickController;
   
   private void Awake()
   {
      _gameField = FindObjectOfType<GameField>();
      _joystickController = FindObjectOfType<JoystickController>();
      _joystickController.OnDoMove += JoystickControllerOnOnDoMove;
      
      //print(Vector2.down.ToString());
   }
   
   private void DoMove(MoveTypes moveType)
   {
      _gameField.DoMovePlayer("0", moveType);
   }

   private void JoystickControllerOnOnDoMove(MoveTypes moveType)
   {
      DoMove(moveType);
   }

   private void OnDestroy()
   {
      _joystickController.OnDoMove -= JoystickControllerOnOnDoMove;
   }
}
