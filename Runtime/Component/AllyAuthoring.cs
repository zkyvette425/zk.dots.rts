using Unity.Entities;
using UnityEngine;

namespace RTS.Runtime.Component
{
    public class AllyAuthoring : MonoBehaviour
    {
        private class AllyAuthoringBaker : Baker<AllyAuthoring>
        {
            public override void Bake(AllyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<Ally>(entity);
            }
        }
    }

    public struct Ally : IComponentData
    {
        
    }
}