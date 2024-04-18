using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="BulletTrailData", menuName = "Custom/Bullet Trail Data")]
public class BulletTrailData : ScriptableObject
{
    public AnimationCurve widthCurve;
    public float time = 0.5f;
    public float minVertexDistance = 0.1f;
    public Gradient colorGradient;
    public Material material;
    public int cornerVertecies;
    public int endCapVertecies;

    public void SetUpTrail(TrailRenderer trailRenderer)
    {
        trailRenderer.widthCurve = widthCurve;
        trailRenderer.time = time;
        trailRenderer.minVertexDistance = minVertexDistance;
        trailRenderer.colorGradient = colorGradient;
        trailRenderer.sharedMaterial = material;
        trailRenderer.numCornerVertices = cornerVertecies;
        trailRenderer.numCapVertices = endCapVertecies;
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
