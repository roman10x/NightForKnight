using UnityEngine;

namespace CodeBase.Logic
{
  public class AnimatorStateReporter : StateMachineBehaviour
  {
    private IAnimationStateReader m_stateReader;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateEnter(animator, stateInfo, layerIndex);
      FindReader(animator);
     
      m_stateReader.EnteredState(stateInfo.shortNameHash);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
      base.OnStateExit(animator, stateInfo, layerIndex);
      FindReader(animator);
     
      m_stateReader.ExitedState(stateInfo.shortNameHash);
    }

    private void FindReader(Animator animator)
    {
      if (m_stateReader != null)
        return;

      m_stateReader = animator.gameObject.GetComponent<IAnimationStateReader>();
    }
  }
}