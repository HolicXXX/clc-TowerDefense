using UnityEngine;

public class TargetPoint : MonoBehaviour
{
  public Enemy Enemy { get; private set; }

  public Vector3 Position => transform.position;

  const int enemyLayerMask = 1 << 9;

  static Collider[] buffer = new Collider[100];

  public static int BufferedCount { get; private set; }

  public static TargetPoint RandomBuffered => GetBuffered(Random.Range(0, BufferedCount));

  private void Awake()
  {
    Enemy = transform.root.GetComponent<Enemy>();
    Enemy.TargetPointCollider = GetComponent<Collider>();
  }

  public static bool FillBuffer(Vector3 position, float range)
  {
    Vector3 top = position;
    top.y += 3f;
    BufferedCount = Physics.OverlapCapsuleNonAlloc(position, top, range, buffer, enemyLayerMask);
    return BufferedCount > 0;
  }

  public static TargetPoint GetBuffered(int index)
  {
    var target = buffer[index].GetComponent<TargetPoint>();
    return target;
  }
}
