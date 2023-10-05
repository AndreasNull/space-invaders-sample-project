namespace AGames.StateMachine
{
    public struct Transition
    {
        public Transition(System.Enum from, System.Enum to, ICondition condition)
        {
            this.from = from;
            this.to = to;
            this.condition = condition;
        }

        public System.Enum from { get; set; }
        public System.Enum to { get; set; }
        ICondition condition { get; set; }

        public bool Evaluate(SimpleStateMachine stateMachine)
        {
            if (condition == null)
                return true;

            return condition.Evaluate(stateMachine);
        }
        
    }
}