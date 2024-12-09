using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using VContainer;
using VContainer.Unity;

public class FactoryInstaller : LifetimeScope
{
    [SerializeField] private RectTransform content;
    protected override void Configure(IContainerBuilder builder)
    {
        //builder
        //    .Register<FactoryCard>(_factoryCard)
        //    .WithParameter(content)
        //    .Build();
    }
}
