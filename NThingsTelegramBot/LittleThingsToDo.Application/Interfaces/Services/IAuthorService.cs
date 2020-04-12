using System;
using System.Threading.Tasks;

namespace LittleThingsToDo.Application.Interfaces.Services
{
    public interface IAuthorService
    {
        Task Add(Guid id);
    }
}