using System;
using UnityEngine;

namespace RTS.Runtime.Manager
{
    public class Selector : MonoSingleton<Selector>
    {
        private Vector2 _startPosition;
        private const int MultipleArea = 50;
        
        public bool IsCheckBox { get; private set; }
        
        public Rect SelectRect { get;private set; }

        public float Area => SelectRect.width * SelectRect.height; 
        
        
        public bool Contains(Vector2 screenPoint) => SelectRect.Contains(screenPoint);
        
        public bool Contains(Vector3 worldPoint,Camera viewCamera) => SelectRect.Contains(viewCamera.WorldToScreenPoint(worldPoint));
        
        public SelectState State { get; private set; }
        
        public event EventHandler<Vector2> ClickDown;
        
        public event EventHandler ClickUp;
        
        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _startPosition = Input.mousePosition;
                IsCheckBox = true;
                State = SelectState.SingleSelect;
                ClickDown?.Invoke(this, _startPosition);
            }

            if (Input.GetMouseButtonUp(0))
            {
                IsCheckBox = false;
                State = SelectState.Move;
                ClickUp?.Invoke(this, EventArgs.Empty);
            }

            if (IsCheckBox)
            {
                var endPosition = Input.mousePosition;
                var lowerLeftPoint = new Vector2(Mathf.Min(_startPosition.x, endPosition.x), Mathf.Min(_startPosition.y, endPosition.y));
                var upperRightPoint = new Vector2(Mathf.Max(_startPosition.x, endPosition.x), Mathf.Max(_startPosition.y, endPosition.y));
                SelectRect = new Rect(lowerLeftPoint.x,lowerLeftPoint.y,upperRightPoint.x-lowerLeftPoint.x,upperRightPoint.y-lowerLeftPoint.y);
                if (Area > MultipleArea)
                {
                    State = SelectState.MultipleSelect;
                }
            }
        }
    }
    
    public enum SelectState
    {
        Move,
        SingleSelect,
        MultipleSelect,
    }
}