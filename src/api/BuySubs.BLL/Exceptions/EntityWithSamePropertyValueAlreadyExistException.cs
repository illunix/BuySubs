namespace BuySubs.BLL.Exceptions;

public sealed class EntityWithSamePropertyValueAlreadyExistException : Exception
{
    public EntityWithSamePropertyValueAlreadyExistException(
        string entityName, 
        string propertyName
    ) : base($"There is already {entityName} associated with this {propertyName}.") { }
}