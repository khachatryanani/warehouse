namespace Warehouse.Infrastructure.Attributes
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class CollectionAttribute : Attribute
    {
        private string _collectionName = string.Empty;
        public CollectionAttribute(string collectionName)
        {
            _collectionName = collectionName;
        }
        public string Name => _collectionName;
    }
}
