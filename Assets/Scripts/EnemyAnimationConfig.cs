using UnityEngine;

[CreateAssetMenu]
public class EnemyAnimationConfig : ScriptableObject
{
  [SerializeField]
  AnimationClip move = default;

  [SerializeField]
  AnimationClip intro = default;

  [SerializeField]
  AnimationClip outro = default;

  [SerializeField]
  AnimationClip dying = default;

  public AnimationClip Move => move;

  public AnimationClip Intro => intro;

  public AnimationClip Outro => outro;

  public AnimationClip Dying => dying;
}
