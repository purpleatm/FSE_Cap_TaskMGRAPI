using System;
using System.Reflection;
using System.Collections.Generic;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace TaskManager.FSD.Core.ApiHelper.Resolver
{
    public class JsonResolver : DefaultContractResolver
    {
        protected readonly Dictionary<Type, HashSet<string>> Ignores;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        public JsonResolver()
        {
            Ignores = new Dictionary<Type, HashSet<string>>();
        }

        /// <summary>
        /// Explicitly ignore the given property(s) for the given type</summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyNames">
        /// One or more properties to ignore. Leave empty to ignore the type entirely.
        /// </param>
        public void Ignore(Type type, params string[] propertyNames)
        {
            if (!Ignores.ContainsKey(type))
                Ignores[type] = new HashSet<string>();

            foreach (var prop in propertyNames)
                Ignores[type].Add(prop);
        }

        /// <summary>
        /// Is the given property for the given type ignored?
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="propertyName">Name of the property.</param>
        /// <returns></returns>
        public bool IsIgnored(Type type, string propertyName)
        {
            if (!Ignores.ContainsKey(type))
                return false;

            if (Ignores[type].Count == 0)
                return true;

            return Ignores[type].Contains(propertyName);
        }

        /// <summary>
        /// The decision logic goes here
        /// </summary>
        /// <param name="member">The member to create a <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for.</param>
        /// <param name="memberSerialization">The member's parent <see cref="T:Newtonsoft.Json.MemberSerialization" />.</param>
        /// <returns>A created <see cref="T:Newtonsoft.Json.Serialization.JsonProperty" /> for the given <see cref="T:System.Reflection.MemberInfo" />.</returns>
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            if (IsIgnored(property.DeclaringType, property.PropertyName))
                property.ShouldSerialize = instance => false;

            return property;
        }
    }
}