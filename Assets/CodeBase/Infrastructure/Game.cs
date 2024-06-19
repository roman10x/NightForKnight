using CodeBase.Infrastructure.Services;
using CodeBase.Infrastructure.States;
using CodeBase.Logic;

namespace CodeBase.Infrastructure
{
  public class Game
  {
    public ApplicationStateMachine StateMachine;

    public Game(ICoroutineRunner coroutineRunner, LoadingCurtain curtain)
    {
      StateMachine = new ApplicationStateMachine(new SceneLoader(coroutineRunner), curtain, AllServices.Container);
    }
  }
}