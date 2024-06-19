using System;
using System.Collections.Generic;

namespace CodeBase.Infrastructure.States
{
    public interface IState : IExitableState
    {
        void Enter()
        {
            
        }
    }

    public interface IPayloadedState<TPayload> : IExitableState
    {
        void Enter(TPayload payload);
    }
    
    public interface IExitableState
    {
        void Exit()
        {
            
        }
    }
    
    public interface IStrictState
    {
        List<Type> AvailableStates();
    }
}
