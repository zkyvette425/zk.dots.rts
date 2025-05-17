using Unity.Entities;
using UnityEngine;

namespace RTS.Runtime.Component
{
    public class UnitSelectAuthoring : MonoBehaviour
    {
        public GameObject effectTarget;

        public float scale = 2;
        
        private class UnitSelectAuthoringBaker : Baker<UnitSelectAuthoring>
        {
            public override void Bake(UnitSelectAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity,new UnitSelect()
                {
                    EffectTarget = GetEntity(authoring.effectTarget, TransformUsageFlags.Dynamic),
                    Scale = authoring.scale,
                });
                SetComponentEnabled<UnitSelect>(entity,false);
            }
        }
    }

    public struct UnitSelect : IComponentData, IEnableableComponent
    {
        public Entity EffectTarget;

        public float Scale;
    }
}