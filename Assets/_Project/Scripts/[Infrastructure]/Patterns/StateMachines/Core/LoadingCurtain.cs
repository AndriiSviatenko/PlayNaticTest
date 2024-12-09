using DG.Tweening;
using System.Collections;
using TMPro;
using UnityEngine;

namespace Infrastructure
{
    public class LoadingCurtain : MonoBehaviour
    {
        [SerializeField] private CanvasGroup curtain;
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private float pointsChangeTime = 0.3f;

        public IEnumerator TextChanger()
        {
            while (true)
            {
                text.text = "Loading";
                yield return new WaitForSeconds(pointsChangeTime);
                text.text = "Loading.";
                yield return new WaitForSeconds(pointsChangeTime);
                text.text = "Loading..";
                yield return new WaitForSeconds(pointsChangeTime);
                text.text = "Loading...";
                yield return new WaitForSeconds(pointsChangeTime);
            }
        }

        public void Show() => 
            curtain.Show(1, 0.2f);

        public void Hide() => 
            curtain.Hide(0, 0.2f);
    }
}