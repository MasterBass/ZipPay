using TestProject.Common.Exception;

namespace TestProject.Storage.DAL.Exceptions
{
    public class EntityNotFoundException<TKey> : TestProjectException
    {
        public EntityNotFoundException(string entityName, TKey entityKey)
        {
            EntityName = entityName;
            EntityKey = entityKey;
            Message = $"Entity of type '{entityName}' and key {EntityKey} not found in the current context.";
        }

        private string EntityName { get; }

        public TKey EntityKey { get; }

        public override string Message { get; }
    }
}