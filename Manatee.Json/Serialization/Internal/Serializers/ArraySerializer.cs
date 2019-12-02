using System;

namespace Manatee.Json.Serialization.Internal.Serializers
{
	internal class ArraySerializer : GenericTypeSerializerBase
	{
		public override bool Handles(SerializationContextBase context)
		{
			return context.InferredType.IsArray;
		}

		protected override Type[] GetTypeArguments(Type type)
		{
			return new[] { type.GetElementType() };
		}

		private static JsonValue _Encode<T>(SerializationContext context)
		{
			var array = (T[]) context.Source;
			var values = new JsonValue[array.Length];
			for (int i = 0; i < array.Length; i++)
			{
				context.Push(array[i]?.GetType() ?? typeof(T), typeof(T), i.ToString(), array[i]);

				values[i] = context.RootSerializer.Serialize(context);
			}
			return new JsonArray(values);
		}
		private static T[] _Decode<T>(DeserializationContext context)
		{
			var array = context.LocalValue.Array;
			var values = new T[array.Count];
			for (int i = 0; i < array.Count; i++)
			{
				context.Push(typeof(T), typeof(T), i.ToString(), array[i]);

				values[i] = (T) context.RootSerializer.Deserialize(context);

				context.Pop();
			}
			return values;
		}
	}
}