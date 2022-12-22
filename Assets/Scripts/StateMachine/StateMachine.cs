using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachine : MonoBehaviour
{
    public enum states
    {
        global,
        move,
        jump
    }

    [SerializeField] ScriptableObject m_Data;

    private List<states> m_CurrStates;
    protected Dictionary<states, State> m_States;

    private List<states> m_StatesToAdd;
    private List<states> m_StatesToDelete;

    private void Start()
    {
        m_CurrStates = new List<states>();
        m_States = new Dictionary<states, State>();
        m_StatesToAdd = new List<states>();
        m_StatesToDelete = new List<states>();

        InitAllStates();
        AddInitialsStates();
    }

    private void FixedUpdate()
    {
        CallStart();
        CallFixedUpdate();
    }

    private void Update()
    {
        CallUpdate();
    }

    private void LateUpdate()
    {
        CallLateUpdate();
        CallEnd();
    }

    // appel l'ordre d'execution des state active --------------------------------------------------------------------------------
    private void CallStart()
    {
        // ajoute les state a ajouter et appel leur Awake 
        foreach (states state in m_StatesToAdd)
        {
            if (!m_CurrStates.Contains(state))
            {
                m_CurrStates.Add(state);
                m_States[state].Start();
            }
        }
        m_StatesToAdd.Clear();
    }

    private void CallFixedUpdate()
    {
        foreach (states state in m_CurrStates)
        {
            m_States[state].FixedUpdate();
        }
    }

    private void CallUpdate()
    {
        foreach (states state in m_CurrStates)
        {
            m_States[state].Update();
        }
    }

    private void CallLateUpdate()
    {
        foreach (states state in m_CurrStates)
        {
            m_States[state].LateUpdate();
        }
    }

    private void CallEnd()
    {
        foreach (states state in m_StatesToDelete)
        {
            if (m_CurrStates.Contains(state))
            {
                m_States[state].End();
                m_CurrStates.Remove(state);
            }
        }
        m_StatesToDelete.Clear();
    }
    //-----------------------------------------------------------------------------------------------------

    // gestion des states ---------------------------------------------------------------------------------

    // initialise tout les state possible
    public abstract void InitAllStates();
    //{
    //    m_States.Add(states.moving, new StatePlayerMove(this));
    //}

    public abstract void AddInitialsStates();

    // ajoute une state dans la state courrant
    public void AddCurrState(states state)
    {
        if(m_States.ContainsKey(state) && !m_CurrStates.Contains(state))
        {
            m_StatesToAdd.Add(state);
        }
    }

    // pop une state des states courrant
    public void PopCurrState(states state)
    {
        if (m_States.ContainsKey(state) && m_CurrStates.Contains(state))
        {
            m_StatesToDelete.Add(state);
        }    
    }
    // -------------------------------------------------------------------------------------------------

    // appel les collision sur les state active ---------------------------------------------------------------------------------------
    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (states state in m_CurrStates)
        {
            m_States[state].OnCollisionEnter2D(collision);
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        foreach (states state in m_CurrStates)
        {
            m_States[state].OnCollisionStay2D(collision);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        foreach (states state in m_CurrStates)
        {
            m_States[state].OnCollisionExit2D(collision);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach (states state in m_CurrStates)
        {
            m_States[state].OnTriggerEnter2D(collision);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        foreach (states state in m_CurrStates)
        {
            m_States[state].OnTriggerStay2D(collision);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        foreach (states state in m_CurrStates)
        {
            m_States[state].OnTriggerExit2D(collision);
        }
    }
    // -------------------------------------------------------------------------------------------------------

    public ScriptableObject GetData()
    {
        return m_Data;
    }
}
