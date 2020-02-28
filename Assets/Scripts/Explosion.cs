using UnityEngine;

public class Explosion : WarEntity
{
  [SerializeField, Range(0f, 1f)]
  float duration = 0.5f;

  float age;

  [SerializeField]
  AnimationCurve opacityCurve = default;

  [SerializeField]
  AnimationCurve scaleCurve = default;

  static int colorPropertyID = Shader.PropertyToID("_Color");

  static MaterialPropertyBlock propertyBlock;

  float scale;

  MeshRenderer meshRenderer;

  private void Awake()
  {
    meshRenderer = GetComponent<MeshRenderer>();
  }

  public void Initialize(Vector3 position, float blastRange, float demage = 0f)
  {
    if (demage > 0f)
    {
      TargetPoint.FillBuffer(position, blastRange);
      for (int i = 0; i < TargetPoint.BufferedCount; i++)
      {
        TargetPoint.GetBuffered(i).Enemy.ApplyDemage(demage);
      }
    }
    transform.localPosition = position;
    scale = 2f * blastRange;
  }

  public override bool GameUpdate()
  {
    age += Time.deltaTime;
    if (age >= duration)
    {
      OriginFactory.Reclaim(this);
      return false;
    }

    if (propertyBlock == null)
    {
      propertyBlock = new MaterialPropertyBlock();
    }
    float t = age / duration;
    Color c = Color.clear;
    c.a = opacityCurve.Evaluate(t);
    propertyBlock.SetColor(colorPropertyID, c);
    meshRenderer.SetPropertyBlock(propertyBlock);
    transform.localScale = Vector3.one * (scale * scaleCurve.Evaluate(t));
    return true;
  }
}
