namespace Model.Persistence.Exceptions
{
    public class DuplicateInstanceException : InstanceException
    {
        public DuplicateInstanceException(object key, string className) : base("Duplicate instance", key, className)
        {
        }
    }
}