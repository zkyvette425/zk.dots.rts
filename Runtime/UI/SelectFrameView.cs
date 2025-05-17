using RTS.Runtime.Manager;
using UnityEngine;

namespace RTS.Runtime.UI
{
    public class SelectFrameView : MonoBehaviour
    {
        [SerializeField] private RectTransform frame;
        [SerializeField] private Canvas canvas;
        
        private void OnEnable()
        {
            frame.sizeDelta = Vector2.zero;
            frame.localScale = Vector3.one;
        }

        private void Update()
        {
            if (Selector.Instance.IsCheckBox)
            {
                var rect = Selector.Instance.SelectRect;
                var canvasScale = canvas.transform.localScale.x;
                frame.anchoredPosition = rect.position / canvasScale;
                frame.sizeDelta = rect.size / canvasScale;
            }
            else
            {
                frame.sizeDelta = Vector2.zero;
            }
        }
    }
}