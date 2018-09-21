using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;

namespace Manatee.Json.Serialization.Internal.Serializers
{
	internal class ObjectSerializer : GenericTypeSerializerBase
	{
		public override bool Handles(SerializationContext context)
		{
			return context.InferredType == typeof(object);
		}

		private static JsonValue _Encode(SerializationContext<object> context)
		{
			throw new NotImplementedException();
		}
		private static object _Decode(SerializationContext<JsonValue> context)
		{
			switch (context.Source.Type)
			{
				case JsonValueType.Number:
					return context.Source.Number;
				case JsonValueType.String:
					return context.Source.String;
				case JsonValueType.Boolean:
					return context.Source.Boolean;
				case JsonValueType.Array:
					return context.Source.Array.Select(context.RootSerializer.Deserialize<object>).ToList();
				case JsonValueType.Object:
					var result = new ExpandoObject() as IDictionary<string, object>;
					foreach (var kvp in context.Source.Object)
					{
						result[kvp.Key] = context.RootSerializer.Deserialize<object>(kvp.Value);
					}
					return result;
				case JsonValueType.Null:
					return null;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}
	}
}