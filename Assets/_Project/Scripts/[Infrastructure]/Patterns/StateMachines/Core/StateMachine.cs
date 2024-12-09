using System;
using System.Linq;
using UnityEngine;
using VContainer.Unity;

public class StateMachine : ITickable
{
    private IState _current;
    private readonly IState[] _states;
    private readonly ITransition[] _transitions;

    public StateMachine(IState[] states, ITransition[] transitions)
    {
        Debug.Log($"OriginState : {states[0].GetType().Name}");
        Debug.Log("StateMachine Initialized");
        _current = states[0];
        _states = states;
        Debug.Log("Registered states: " + string.Join(", ", states.Select(s => s.GetType().Name)));
        Debug.Log("Concat Registered states: " + string.Join(", ", states.Select(s => s.GetType().Name)));
        _transitions = transitions;

        if(_current is IEnterState enterState)
        {
            Debug.Log($"Entering Initial State: {_current.GetType().Name}");
            enterState.Enter();
        }
    }


    public void Tick()
    {
        Debug.Log("StateMachine Tick");
        Update();
    }

    public void Update()
    {
        foreach (var transition in _transitions)
        {
            if (transition.CanTransition(_current))
            {
                Translate(transition);
            }
        }

        if (_current is IUpdateState updateState)
        {
            updateState.Update();
        }
    }

    private void Translate(ITransition transition)
    {
        Debug.Log($"Translate from {_current} to {transition.To}");

        if (_current is IExitState exitState)
            exitState.Exit();

        if (_states == null || !_states.Any())
            throw new InvalidOperationException("States collection is null or empty.");

        _current = _states.FirstOrDefault(x => x.GetType() == transition.To);

        if (_current == null)
            throw new InvalidOperationException($"State of type {transition.To} not found in states collection.");

        if (_current is IEnterState enterState)
            enterState.Enter();
    }
}
