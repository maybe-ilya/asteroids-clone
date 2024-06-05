using System;

namespace MIG.API
{
    public interface IGameInputHandler
    {
        event Action<float> OnMove;
        event Action<float> OnTurn;
        event Action OnPrimaryAttackStart;
        event Action OnPrimaryAttackFinish;
        event Action OnSecondaryAttackStart;
        event Action OnSecondaryAttackFinish;
    }
}