using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LittleThingsToDo.TelegramBot.Common
{
    public class CallbackData<T>
    {
        public CallbackCommand Command { get; set; }

        public T Data { get; set; }

        public CallbackData(CallbackCommand command, T data)
        {
            Command = command;
            Data = data;
        }

        public virtual string ToJson()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static CallbackCommand GetCommand(string json)
        {
            var jObject = JObject.Parse(json);
            return jObject[nameof(Command)].Value<CallbackCommand>();
        }

        public static CallbackData<TData> FromJson<TData>(string json)
        {
            return JsonConvert.DeserializeObject<CallbackData<TData>>(json);
        }
    }
}
