using Infrastructure;
using Infrastructure.CoroutineRunner;
using System;
using System.Collections;
using UnityEngine;

public class BootStrap : IEnterState, IExitState
{
    private Action _resourcesLoaded;
    private readonly LoadingCurtain _loadingCurtain;
    private readonly ICoroutineRunner _coroutineRunner;

    public BootStrap(ICoroutineRunner coroutineRunner, LoadingCurtain loadingCurtain, Action resorcesLoad)
    {
        Debug.Log("Initialize Boot");
        _coroutineRunner = coroutineRunner;
        _loadingCurtain = loadingCurtain;
        _resourcesLoaded = resorcesLoad;
    }

    public void Enter()
    {
        Debug.Log("BootStrap Enter");
        _loadingCurtain.Show();
        _coroutineRunner.StartCoroutine(LoadResources());
    }

    public void Exit()
    {
        Debug.Log("BootStrap Exit");
        _loadingCurtain.Hide();
    }

    private IEnumerator LoadResources()
    {
        yield return new WaitForSeconds(2f);
        Debug.Log("Resources loaded");
        _resourcesLoaded.Invoke();
    }
}