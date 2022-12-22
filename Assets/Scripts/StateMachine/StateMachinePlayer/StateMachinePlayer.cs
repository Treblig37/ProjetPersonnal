using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachinePlayer : StateMachine
{
    public override void AddInitialsStates()
    {
        AddCurrState(states.move);
        AddCurrState(states.jump);
    }

    public override void InitAllStates()
    {
        m_States.Add(states.global, new StatePlayerGlobal(this));
        m_States.Add(states.move, new StatePlayerMove(this));
        m_States.Add(states.jump, new StatePlayerJump(this));
    }
}
