using Unity.Entities;
using UnityEngine;

namespace RTS.Runtime.Component
{
    public class UnitPreparedSelectAuthoring : MonoBehaviour
    {
        public GameObject effectTarget;

        public float scale = 2;
        
        private class UnitPreparedSelectAuthoringBaker : Baker<UnitPreparedSelectAuthoring>
        {
            public override void Bake(UnitPreparedSelectAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,new UnitPreparedSelect()
                {
                    EffectTarget = GetEntity(authoring.effectTarget, TransformUsageFlags.Dynamic),
                    Scale = authoring.scale,
                });
                SetComponentEnabled<UnitPreparedSelect>(entity,false);
            }
        }
    }

    public struct UnitPreparedSelect : IComponentData,IEnableableComponent
    {
        public Entity EffectTarget;

        public float Scale;
    }
}