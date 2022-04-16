using System;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Hero
{
  public class HeroAnimator : MonoBehaviour, IAnimationStateReader
  {
    [SerializeField]
    private Animator m_animator;
    [SerializeField]
    private CharacterController m_characterController;
    
    private static readonly int MoveHash = Animator.StringToHash("Walking");
    private static readonly int AttackHash = Animator.StringToHash("AttackNormal");
    private static readonly int HitHash = Animator.StringToHash("Hit");
    private static readonly int DieHash = Animator.StringToHash("Die");

    private readonly int m_idleStateHash = Animator.StringToHash("Idle");
    private readonly int m_idleStateFullHash = Animator.StringToHash("Base Layer.Idle");
    private readonly int m_attackStateHash = Animator.StringToHash("Attack Normal");
    private readonly int m_walkingStateHash = Animator.StringToHash("Run");
    private readonly int m_deathStateHash = Animator.StringToHash("Die");
    
    public event Action<AnimatorState> StateEntered;
    public event Action<AnimatorState> StateExited;
   
    public AnimatorState State { get; private set; }
    
    

    private void Update()
    {
      m_animator.SetFloat(MoveHash, m_characterController.velocity.magnitude, 0.1f, Time.deltaTime);
    }

    public bool IsAttacking => State == AnimatorState.Attack;
    

    public void PlayHit() => m_animator.SetTrigger(HitHash);
    
    public void PlayAttack() => m_animator.SetTrigger(AttackHash);

    public void PlayDeath() =>  m_animator.SetTrigger(DieHash);

    public void ResetToIdle() => m_animator.Play(m_idleStateHash, -1);
    
    public void EnteredState(int stateHash)
    {
      State = StateFor(stateHash);
      StateEntered?.Invoke(State);
    }

    public void ExitedState(int stateHash) =>
      StateExited?.Invoke(StateFor(stateHash));
    
    private AnimatorState StateFor(int stateHash)
    {
      AnimatorState state;
      if (stateHash == m_idleStateHash)
        state = AnimatorState.Idle;
      else if (stateHash == m_attackStateHash)
        state = AnimatorState.Attack;
      else if (stateHash == m_walkingStateHash)
        state = AnimatorState.Walking;
      else if (stateHash == m_deathStateHash)
        state = AnimatorState.Died;
      else
        state = AnimatorState.Unknown;
      
      return state;
    }
  }
}