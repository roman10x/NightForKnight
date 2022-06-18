using CodeBase.Infrastructure.States;
using CodeBase.Logic;
using UnityEngine;

namespace CodeBase.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
    {
       private Game m_game;

       [SerializeField]
       private LoadingCurtain m_curtain;
     
       private void Awake()
        {
            m_game = new Game(this, m_curtain);
            m_game.StateMachine.Enter<BootStrapState>();

            DontDestroyOnLoad(this);
        }
    }
}