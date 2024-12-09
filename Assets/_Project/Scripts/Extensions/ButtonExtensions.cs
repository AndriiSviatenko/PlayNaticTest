using UnityEngine.Events;
using UnityEngine.UI;

namespace Assets._Project.Scripts.Extensions
{
    public static class ButtonExtensions
    {
        public static void Subscribe(this Button button, UnityAction action) => 
            button.onClick.AddListener(action);
        public static void UnSubscribe(this Button button, UnityAction action) =>
            button.onClick.RemoveListener(action);
    }
}
