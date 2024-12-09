using System;
using UnityEngine;

namespace States
{
    public class GameLoop : IEnterState, IUpdateState, IExitState
    {
        private GameController _gameController;

        public GameLoop(GameController gameController, Action winCallBack, Action loseCallBack)
        {
            _gameController = gameController;
            Debug.Log("INIt");
            _gameController.Win += () => winCallBack?.Invoke();
            _gameController.Lose += () => loseCallBack?.Invoke();
        }
        public void Enter()
        {
            Debug.Log("Start Game");
            _gameController.StartGame();
        }
        public void Update()
        {
            _gameController.Tick();
        }

        public void Exit()
        {
        }
    }

}
