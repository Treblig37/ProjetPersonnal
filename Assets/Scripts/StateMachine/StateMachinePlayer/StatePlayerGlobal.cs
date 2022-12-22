using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatePlayerGlobal : State
{
    DataPlayer m_Data;

    public StatePlayerGlobal(StateMachine stateMachine) : base(stateMachine)
    {
        m_Data = (DataPlayer)m_StateMachine.GetData();
    }

    public override void Update()
    {
        CheckInput();
    }

    private void CheckInput()
    {
        if(Input.GetKey(m_Data.leftKey) || Input.GetKey(m_Data.rightKey))
        {
            m_StateMachine.AddCurrState(StateMachine.states.move);
        }

        if(Input.GetKey(m_Data.primarySlotKey))
        {
            
        }
    }
}
