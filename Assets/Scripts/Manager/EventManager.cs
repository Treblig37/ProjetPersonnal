using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager
{
    // dictionary d'evenement
    private Dictionary<string, Action<Dictionary<string, object>>> m_EventDictionary;

    private static EventManager m_EventManager;

    public static EventManager m_Instance
    {
        get
        {
            // initialise l'instance si elle n'est pas initialisé
            if (m_EventManager == null)
            {
                Init();
            }
            return m_EventManager;
        }
    }

    private static void Init()
    {
        m_EventManager = new EventManager();
        m_EventManager.m_EventDictionary = new Dictionary<string, Action<Dictionary<string, object>>>();
    }

    // ajoute une action au dictionnaire a l'index eventName
    public static void StartListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        Action<Dictionary<string, object>> currEvent;
        if (m_Instance.m_EventDictionary.TryGetValue(eventName, out currEvent))
        {
            currEvent += listener;
            m_Instance.m_EventDictionary[eventName] = currEvent;
        }
        else
        {
            m_Instance.m_EventDictionary.Add(eventName, listener);
        }
    }

    // retire une action au dictionnaire a l'index eventName
    public static void StopListening(string eventName, Action<Dictionary<string, object>> listener)
    {
        Action<Dictionary<string, object>> currEvent;
        if (m_Instance.m_EventDictionary.TryGetValue(eventName, out currEvent))
        {
            currEvent -= listener;
            m_Instance.m_EventDictionary[eventName] = currEvent;
        }
    }

    // fait jouer l'action a l'index eventName et retourn les valeur
    public static Dictionary<string, object> TriggerEvent(string eventName, Dictionary<string, object> parametre)
    {
        Action<Dictionary<string, object>> currEvent;
        if (m_Instance.m_EventDictionary.TryGetValue(eventName, out currEvent))
        {
            currEvent.Invoke(parametre);
        }
        return null;
    }
}
