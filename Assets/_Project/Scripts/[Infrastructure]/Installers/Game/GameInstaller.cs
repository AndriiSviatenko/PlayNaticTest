using Infrastructure;
using Infrastructure.CoroutineRunner;
using States;
using System;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class GameInstaller : LifetimeScope
{
    [SerializeField] private LoadingCurtain loadingCurtain;
    [SerializeField] private CoroutineRunner coroutineRunner;
    [SerializeField] private GameController gameController;

    private Func<bool> ResourcesLoadedCallBack;
    private bool _resorcesLoad;
    private Func<bool> WinCallBack;
    private bool _isWin;
    private Func<bool> LoseCallBack;
    private bool _isLose;
    protected override void Configure(IContainerBuilder builder)
    {
        ResourcesLoadedCallBack = () => _resorcesLoad;
        WinCallBack = () => _isWin;
        LoseCallBack = () => _isLose;

        builder.RegisterInstance(loadingCurtain);
        builder
            .RegisterInstance(coroutineRunner)
            .AsImplementedInterfaces();

        RegisterStates(builder);
        RegisterTransitions(builder);
        RegisterStateMachine(builder);
    }

    private void RegisterStates(IContainerBuilder builder)
    {
        var bootStrap = RegisterBootStrap(builder);
        var gameLoop = RegisterGameLoop(builder);
        var win = RegisterWin(builder);
        var lose = RegisterLose(builder);

        IState[] states = new IState[]
        {
            bootStrap,
            gameLoop,
            win,
            lose
        };

        builder
            .RegisterInstance(states)
            .As<IState[]>();
    }

    private Lose RegisterLose(IContainerBuilder builder)
    {
        var lose = new Lose();
        builder
            .RegisterInstance(lose)
            .AsImplementedInterfaces();
        return lose;
    }

    private Win RegisterWin(IContainerBuilder builder)
    {
        var win = new Win();
        builder
            .RegisterInstance(win)
            .AsImplementedInterfaces();
        return win;
    }

    private GameLoop RegisterGameLoop(IContainerBuilder builder)
    {
        var gameLoop = new GameLoop(gameController, () => _isWin = true, () => _isLose = true);
        builder
            .RegisterInstance(gameLoop)
            .AsImplementedInterfaces();
        return gameLoop;
    }

    private BootStrap RegisterBootStrap(IContainerBuilder builder)
    {
        var bootStrap = new BootStrap(coroutineRunner, loadingCurtain, () => _resorcesLoad = true);
        builder
            .RegisterInstance(bootStrap)
            .AsImplementedInterfaces();
        return bootStrap;
    }

    private void RegisterTransitions(IContainerBuilder builder)
    {
        var transitions = new ITransition[]
        {
            new Transition<BootStrap, GameLoop>(ResourcesLoadedCallBack),
            new Transition<GameLoop, Win>(WinCallBack),
            new Transition<GameLoop, Lose>(LoseCallBack)
        };
        builder
            .RegisterInstance(transitions)
            .AsImplementedInterfaces();
    }

    private void RegisterStateMachine(IContainerBuilder builder)
    {
        builder
            .Register<StateMachine>(Lifetime.Singleton)
            .As<ITickable>();
    }
}
