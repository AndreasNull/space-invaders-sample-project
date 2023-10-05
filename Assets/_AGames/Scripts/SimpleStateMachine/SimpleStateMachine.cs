using System;
using System.Collections.Generic;
using Zenject;

namespace AGames.StateMachine
{
    public class SimpleStateMachine : ITickable, IFixedTickable
    {
        Enum m_FirstStateId = null;
        Enum m_CurrentStateId;
        IState m_CurrentState;

        // states
        Dictionary<Enum, IState> m_States = new Dictionary<Enum, IState>();
        // transitions
        Dictionary<Enum, List<Transition>> m_TransitionsTable = new Dictionary<Enum, List<Transition>>();
        // internal trigger parameters (in this simple implementation we support only trigger parametes)
        Dictionary<int, TriggerParameter> m_TriggerParametersTable = new Dictionary<int, StateMachine.TriggerParameter>();
        // use this list to reset very fast the parameter values
        List<TriggerParameter> m_TrigerParametersList = new List<StateMachine.TriggerParameter>();

        #region Exposed to child
        protected void StartStateMachine() => ChangeState(m_FirstStateId);
        protected void RegisterTriggerParameter(int id)
        {
            TriggerParameter parameter = new StateMachine.TriggerParameter(id);
            m_TriggerParametersTable.Add(id, parameter);
            m_TrigerParametersList.Add(parameter);
        }

        protected void SetTrigger(int id) => m_TriggerParametersTable[id].value = true;

        protected void AddState(Enum stateId, IState state)
        {
            // cache first state
            if (m_FirstStateId == null)
                m_FirstStateId = stateId;

            m_States.Add(stateId, state);
        }

        // if condition is null, then the condition is always TRUE
        protected void AddTransition(Enum from, Enum to, ICondition condition = null)
        {
            // if transition does not exists create transition list
            if (!m_TransitionsTable.ContainsKey(from))
                m_TransitionsTable.Add(from, new List<Transition>());
            // add transition
            m_TransitionsTable[from].Add(new Transition(from, to, condition));
        }

        protected void ChangeState(Enum stateId)
        {
            //UnityEngine.Debug.Log($"Change state {stateId}");

            if (!m_States.ContainsKey(stateId))
                throw new System.Exception($"State {stateId} is missing!");

            // if same state id
            if (m_CurrentStateId == stateId)
                return;

            m_CurrentStateId = stateId;
            IState state = m_States[stateId];

            // if same state do nothing
            if (m_CurrentState == state)
                return;

            // if prev state is not null call OnExit
            if (m_CurrentState != null)
                m_CurrentState.OnExit();

            // set current state
            m_CurrentState = state;

            // invoke OnEnter
            m_CurrentState.OnEnter();
        }
        #endregion

        private void ResetTriggerParameters()
        {
            for (int i = 0; i < m_TrigerParametersList.Count; i++)
                m_TrigerParametersList[i].value = false;
        }

        internal bool GetTriggerValue(int paramId) => m_TriggerParametersTable[paramId].value;

        private void EvaluateTransitions()
        {
            //UnityEngine.Debug.Log("EvaluateTransitions");

            if (m_CurrentStateId == null)
                return;

            if (m_TransitionsTable.ContainsKey(m_CurrentStateId))   
            {
                //UnityEngine.Debug.Log($"Check {m_CurrentStateId}");

                for (int i = 0; i < m_TransitionsTable[m_CurrentStateId].Count; i++)
                {
                    //UnityEngine.Debug.Log($"Check {m_CurrentStateId}.{i} => {m_TransitionsTable[m_CurrentStateId][i].ConditionIsTrue()}");

                    if (m_TransitionsTable[m_CurrentStateId][i].Evaluate(this))
                    {
                        ChangeState(m_TransitionsTable[m_CurrentStateId][i].to);
                        return;
                    }
                }
            }
        }

        #region Implement ITickable
        public virtual void Tick()
        {
            // evaluate transitions
            EvaluateTransitions();
            // reset trigger parameters
            ResetTriggerParameters();
            // invoke current state update
            m_CurrentState?.OnUpdate();
        }
        #endregion

        #region Implement IFixedTickable
        public virtual void FixedTick() => m_CurrentState?.OnFixedUpdate();
        #endregion
    }
}