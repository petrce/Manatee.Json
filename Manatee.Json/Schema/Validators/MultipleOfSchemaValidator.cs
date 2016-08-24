﻿/***************************************************************************************

	Copyright 2016 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		MultipleOfSchemaValidator.cs
	Namespace:		Manatee.Json.Schema.Validators
	Class Name:		MultipleOfSchemaValidator
	Purpose:		Validates schemas with a "multipleOf" property.

***************************************************************************************/
namespace Manatee.Json.Schema.Validators
{
	internal class MultipleOfSchemaValidator : IJsonSchemaPropertyValidator
	{
		public bool Applies(JsonSchema schema)
		{
			return schema.MultipleOf.HasValue;
		}
		public SchemaValidationResults Validate(JsonSchema schema, JsonValue json, JsonValue root)
		{
			if (json.Type == JsonValueType.Number && (decimal) json.Number%(decimal) schema.MultipleOf.Value != 0)
				return new SchemaValidationResults(string.Empty, $"Expected: {json.Number}%{schema.MultipleOf}=0; Actual: {json.Number%schema.MultipleOf}.");
			return new SchemaValidationResults();
		}
	}
}
