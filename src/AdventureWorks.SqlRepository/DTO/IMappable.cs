namespace AdventureWorks.SqlRepository.DTO;

internal interface IMappable<Dto, Entity>
{
    static abstract Dto FromEntity(Entity entity);
    Entity ToEntity();
}