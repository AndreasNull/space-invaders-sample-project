namespace AGames.StateMachine
{
    internal class TriggerParameter
    {
        public TriggerParameter(int id)
        {
            this.id = id;
            value = false;
        }

        internal int id { get; private set; }
        internal bool value { get; set; }
    }
}