using Unity.Entities;
using Unity.Mathematics;
using Unity.Rendering;
using UnityEngine;

namespace RTS.Runtime.Component
{
    public class ChangeBaseColorAuthoring : MonoBehaviour
    {
        public Color color = Color.white;

        private class ChangeBaseColorAuthoringBaker : Baker<ChangeBaseColorAuthoring>
        {
            public override void Bake(ChangeBaseColorAuthoring authoring)
            {
                var entity = GetEntity(TransformUsageFlags.Dynamic);
                AddComponent(entity, new ChangeBaseColor()
                {
                    Color = new float4(authoring.color.r, authoring.color.g, authoring.color.b, authoring.color.a),
                });
            }
        }
    }

    [MaterialProperty("_BaseColor")]
    public struct ChangeBaseColor : IComponentData
    {
        public float4 Color;
    }
}