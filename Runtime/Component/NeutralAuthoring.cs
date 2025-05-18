using Unity.Entities;
using UnityEngine;

namespace RTS.Runtime.Component
{
    public class NeutralAuthoring : MonoBehaviour
    {
        private class NeutralAuthoringBaker : Baker<NeutralAuthoring>
        {
            public override void Bake(NeutralAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<Neutral>(entity);
            }
        }
    }
    
    public struct Neutral : IComponentData { }
}