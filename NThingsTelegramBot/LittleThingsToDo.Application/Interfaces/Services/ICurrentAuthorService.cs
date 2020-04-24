using System;

namespace LittleThingsToDo.Application.Interfaces.Services
{
    public interface ICurrentAuthorService
    {
        Guid CurrentAuthorId { get; set; }
    }
}