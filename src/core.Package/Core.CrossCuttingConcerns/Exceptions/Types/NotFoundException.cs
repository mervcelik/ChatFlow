namespace Core.CrossCuttingConcerns.Exceptions.Types;

public class NotFoundException : Exception
{
    public NotFoundException(string entity, Guid id)
        : base($"{entity} bulunamadı. Id: {id}") { }
}
