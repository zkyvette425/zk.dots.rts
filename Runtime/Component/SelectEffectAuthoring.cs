using Unity.Entities;
using UnityEngine;

namespace RTS.Runtime.Component
{
    public class SelectEffectAuthoring : MonoBehaviour
    {
        public float radius = 1f;
        public GameObject preparedSelectTarget;
        public GameObject selectTarget;
        
        private class SelectEffectAuthoringBaker : Baker<SelectEffectAuthoring>
        {
            public override void Bake(SelectEffectAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new SelectEffect()
                {
                    Radius = authoring.radius,
                    PreparedSelectTarget = GetEntity(authoring.preparedSelectTarget, TransformUsageFlags.Dynamic),
                    SelectTarget = GetEntity(authoring.selectTarget, TransformUsageFlags.Dynamic)
                });
            }
        }
    }
    
    public struct SelectEffect : IComponentData
    {
        public float Radius;
        
        public Entity PreparedSelectTarget;

        public Entity SelectTarget;
    }
}