using LittleThingsToDo.Domain.Entities;

namespace LittleThingsToDo.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        void Add();
        bool Exists();
        Author Current();
    }
}