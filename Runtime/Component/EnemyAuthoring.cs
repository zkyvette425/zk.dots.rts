using Unity.Entities;
using UnityEngine;

namespace RTS.Runtime.Component
{
    public class EnemyAuthoring : MonoBehaviour
    {
        private class EnemyAuthoringBaker : Baker<EnemyAuthoring>
        {
            public override void Bake(EnemyAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent<Enemy>(entity);
            }
        }
    }
    
    public struct Enemy : IComponentData { }
}