using UnityEngine;

public class LaserTower : Tower
{
  [SerializeField]
  Transform turrent = default, laserBeam = default;

  TargetPoint target;

  const int enemyLayerMask = 1 << 9;

  Vector3 laserBeamScale;

  [SerializeField, Range(1f, 100f)]
  float demagePerSecond = 10f;

  public override TowerType TowerType => TowerType.Laser;

  private void Awake()
  {
    laserBeamScale = laserBeam.localScale;
  }

  public override void GameUpdate()
  {
    if (TrackTarget(ref target) || AcquireTarget(out target))
    {
      Shoot();
    }
    else
    {
      laserBeam.localScale = Vector3.zero;
    }
  }

  void Shoot()
  {
    Vector3 point = target.Position;
    turrent.LookAt(point);
    laserBeam.localRotation = turrent.localRotation;

    float d = Vector3.Distance(turrent.position, point);
    laserBeamScale.z = d;
    laserBeam.localScale = laserBeamScale;
    laserBeam.localPosition = turrent.localPosition + 0.5f * d * laserBeam.forward;

    target.Enemy.ApplyDemage(demagePerSecond * Time.deltaTime);
  }
}
