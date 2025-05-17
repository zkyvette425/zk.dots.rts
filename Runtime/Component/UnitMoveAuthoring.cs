using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace RTS.Runtime.Component
{
    public class UnitMoveAuthoring : MonoBehaviour
    {
        public float moveSpeed = 10;
        public float rotateSpeed = 5;
        
        private class MoveSpeedAuthoringBaker : Baker<UnitMoveAuthoring>
        {
            public override void Bake(UnitMoveAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,new UnitMove()
                {
                    MoveSpeed = authoring.moveSpeed,
                    RotateSpeed = authoring.rotateSpeed,
                });
                AddComponent(entity,new TargetPosition()
                {
                    Position = new float2(authoring.transform.position.x,authoring.transform.position.z)
                });
                SetComponentEnabled<TargetPosition>(entity,true);
            }
        }
    }

    public struct UnitMove : IComponentData
    {
        public float MoveSpeed;
        public float RotateSpeed;
    }
    
    public struct TargetPosition : IComponentData,IEnableableComponent
    {
        public float2 Position;
    }
}