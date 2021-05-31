namespace Fluint.Layer.Tasks
{
    public class TaskArgs
    {
        public object Invoker { get; }
        public object[] Args { get; }

        public TaskArgs()
        {

        }

        public TaskArgs(object[] args)
        {
            Args = args;
        }

        public TaskArgs(object invoker)
        {
            Invoker = invoker;
        }

        public TaskArgs(object[] args, object invoker)
        {
            Args = args;
            Invoker = invoker;
        }
    }
}
