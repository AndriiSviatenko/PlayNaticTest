using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    
}
public interface IEnterState : IState
{
    void Enter();
}
public interface IUpdateState : IState
{
    void Update();
}
public interface IExitState : IState
{
    void Exit();
}
