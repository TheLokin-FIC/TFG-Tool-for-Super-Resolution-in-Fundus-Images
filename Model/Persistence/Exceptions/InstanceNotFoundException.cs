namespace Model.Persistence.Exceptions
{
    public class InstanceNotFoundException : InstanceException
    {
        public InstanceNotFoundException(object key, string className) : base("Instance not found", key, className)
        {
        }
    }
}