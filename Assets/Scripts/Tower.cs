﻿using UnityEngine;

public abstract class Tower : GameTileContent
{
  [SerializeField, Range(1.5f, 10.5f)]
  protected float targatingRange = 1.5f;

  public abstract TowerType TowerType { get; }

  protected bool AcquireTarget(out TargetPoint target)
  {
    if (TargetPoint.FillBuffer(transform.localPosition, targatingRange))
    {
      target = TargetPoint.RandomBuffered;
      return true;
    }
    target = null;
    return false;
  }

  protected bool TrackTarget(ref TargetPoint target)
  {
    if (target == null || !target.Enemy.IsValidTarget)
    {
      return false;
    }
    Vector3 a = transform.localPosition;
    Vector3 b = target.Position;
    float x = a.x - b.x;
    float z = a.z - b.z;
    float r = targatingRange + 0.125f * target.Enemy.Scale;
    if (x * x + z * z > r * r)
    {
      target = null;
      return false;
    }
    return true;
  }

  private void OnDrawGizmosSelected()
  {
    Gizmos.color = Color.yellow;
    Vector3 position = transform.localPosition;
    position.y += 0.01f;
    Gizmos.DrawWireSphere(position, targatingRange);
  }
}
