using System;

public interface ITransition
{
    Type To { get; }
    bool CanTransition(IState from);
}
