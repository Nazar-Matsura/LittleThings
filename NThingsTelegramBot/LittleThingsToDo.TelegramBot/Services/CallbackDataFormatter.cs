using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LittleThingsToDo.Infrastructure.Extensions;
using MediatR;
using Newtonsoft.Json;

namespace LittleThingsToDo.TelegramBot.Services
{
    public class CallbackDataFormatter : ICallbackDataFormatter
    {
        protected const int MaxCallbackDataLength = 64;
        protected readonly ConcurrentDictionary<int, Type> _typesById;
        protected readonly ConcurrentDictionary<Type, int> _idsByType;

        public CallbackDataFormatter()
        {
            var idsAndTypes = Assembly
                .GetExecutingAssembly()
                .DefinedTypes
                .Where(t => t.Implements<IRequest>())
                .Select((t, i) => new KeyValuePair<int, Type>(i, t))
                .ToArray();

            _typesById = new ConcurrentDictionary<int, Type>(idsAndTypes);

            var typesAndIds = idsAndTypes.Select(p => new KeyValuePair<Type, int>(p.Value, p.Key));

            _idsByType = new ConcurrentDictionary<Type, int>(typesAndIds);
        }


        public string ToString(IRequest request)
        {
            var requestJson = JsonConvert.SerializeObject(request);
            var result = $"{_idsByType[request.GetType()]}_{requestJson}";

            if(result.Length > MaxCallbackDataLength)
                throw new CallbackDataTooLongException();

            return result;
        }

        public IRequest FromString(string request)
        {
            var typeAndData = request.Split('_');
            if (typeAndData.Length != 2)
                throw new BadCallbackDataFormatException();

            if(!int.TryParse(typeAndData[0], out var typeId))
                throw new BadCallbackDataFormatException();

            if(!_typesById.TryGetValue(typeId, out var type))
                throw new BadCallbackDataFormatException();

            return (IRequest) JsonConvert.DeserializeObject(typeAndData[1], type);
        }

        public class CallbackDataTooLongException : Exception
        {
        }

        public class BadCallbackDataFormatException : Exception
        {
        }
    }

}