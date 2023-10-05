using System;

namespace AGames.StateMachine
{
    public interface ICondition
    {
        bool Evaluate(SimpleStateMachine stateMachine);
    }

    public struct TriggerCondition : ICondition
    {
        private int m_ParamId;

        public TriggerCondition(int paramId) => m_ParamId = paramId;

        public bool Evaluate(SimpleStateMachine stateMachine) => stateMachine.GetTriggerValue(m_ParamId);
    }

    public struct FuncCondition : ICondition
    {
        private Func<bool> m_Evaluation;

        public FuncCondition(Func<bool> evaluation) => m_Evaluation = evaluation;

        public bool Evaluate(SimpleStateMachine stateMachine) => m_Evaluation.Invoke();
    }
}
