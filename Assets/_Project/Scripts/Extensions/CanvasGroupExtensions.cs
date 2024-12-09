using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public static class CanvasGroupExtensions
{
    public static void Show(this CanvasGroup canvasGroup, float endValue, float duration)
    {
        canvasGroup.DOFade(endValue, duration);
        canvasGroup.interactable = true;
        canvasGroup.blocksRaycasts = true;
        canvasGroup.ignoreParentGroups = true;
    }
    public static void Hide(this CanvasGroup canvasGroup, float endValue, float duration)
    {
        canvasGroup.DOFade(endValue, duration);
        canvasGroup.interactable = false;
        canvasGroup.blocksRaycasts = false;
        canvasGroup.ignoreParentGroups = false;
    }
}
