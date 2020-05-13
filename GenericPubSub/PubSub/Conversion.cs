﻿using System;
using System.Collections.Generic;

namespace GenericPubSub.PubSub
{
    public static class ConversionExtensions
    {
        public static Dictionary<string, dynamic> ToDictionnary<T>(this T obj) where T : class
        {
            var dict = new Dictionary<string, dynamic>();
            var fields = typeof(T).GetFields();
            foreach (var fieldInfo in fields)
            {
                if (fieldInfo.FieldType.IsEnum)
                {
                    dict[fieldInfo.Name] = fieldInfo.GetValue(obj).ToString();
                }
                else
                {
                    dict[fieldInfo.Name] = fieldInfo.GetValue(obj);
                }
            }

            return dict;
        }

        public static T ToObject<T>(this Dictionary<string, dynamic> dict) where T : class, new()
        {
            var model = new T();
            var fields = typeof(T).GetFields();
            foreach (var fieldInfo in fields)
            {
                if (dict.ContainsKey(fieldInfo.Name))
                {
                    if (fieldInfo.FieldType.IsEnum)
                    {
                        fieldInfo.SetValue(model, Enum.Parse(fieldInfo.FieldType, (string)dict[fieldInfo.Name], true));
                    }
                    else
                    {
                        fieldInfo.SetValue(model, dict[fieldInfo.Name]);
                    }
                }
                else
                {
                    return null;
                }
            }
            return model;
        }
    }
}