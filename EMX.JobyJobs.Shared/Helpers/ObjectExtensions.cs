using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace EMX.JobyJobs.Shared.Helpers
{
  public static class ObjectExtensions
  {
    /// <summary>
    /// Unsafely casts the current object to the specifies type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T ToType<T>(this object obj)
    {
      return (T)obj;
    }

    /// <summary>
    /// Safely casts the current object to the specifies type.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T SafeToType<T>(this object obj) where T : class
    {
      return obj as T;
    }

    /// <summary>
    /// Deep clones the current object using binary formatting.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static T DeepClone<T>(this T obj)
    {
      using (var ms = new MemoryStream())
      {
        var formatter = new BinaryFormatter();
        formatter.Serialize(ms, obj);
        ms.Position = 0;

        return (T)formatter.Deserialize(ms);
      }
    }


    /// <summary>
    /// Deep clones the current object to a similar type using xml serialization.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="obj"></param>
    /// <returns></returns>
    public static TResult DeepCloneSimilar<TResult>(this object obj)
    {
      using (var ms = new MemoryStream())
      {
        var serializer = new XmlSerializer(obj.GetType());
        serializer.Serialize(ms, obj);
        ms.Position = 0;

        return (TResult)serializer.Deserialize(ms);
      }
    }


    public static int IntTryParse(this string valueNotNull)
    {
      int res;
      bool isOK = int.TryParse(valueNotNull, out res);

      return res;
    }
    public static int IntTryParse(this string valueNotNull, int defaultValue)
    {
      int res;
      bool isOK = int.TryParse(valueNotNull, out res);
      if (isOK)
      {
        return res;
      }
      else
      {
        return defaultValue;
      }
    }

    public static decimal DecimalTryParse(this string valueNotNull)
    {
      decimal res;
      bool isOK = decimal.TryParse(valueNotNull, out res);

      return res;
    }
    public static decimal DecimalTryParse(this string valueNotNull, decimal defaultValue)
    {
      decimal res;
      bool isOK = decimal.TryParse(valueNotNull, out res);
      if (isOK)
      {
        return res;
      }
      else
      {
        return defaultValue;
      }

    }

    public static bool BoolTryParse(this string valueNotNull)
    {
      bool res;
      bool isOK = bool.TryParse(valueNotNull, out res);

      return res;
    }
    public static bool BoolTryParse(this string valueNotNull, bool defaultValue)
    {
      bool res;
      bool isOK = bool.TryParse(valueNotNull, out res);
      if (isOK)
      {
        return res;
      }
      else
      {
        return defaultValue;
      }
    }


    public static DateTime DateTimeTryParse(this string valueNotNull)
    {
      DateTime res;
      bool isOK = DateTime.TryParse(valueNotNull, out res);

      return res;
    }
    public static DateTime DateTimeTryParse(this string valueNotNull, DateTime defaultValue)
    {
      DateTime res;
      bool isOK = DateTime.TryParse(valueNotNull, out res);
      if (isOK)
      {
        return res;
      }
      else
      {
        return defaultValue;
      }
    }


    public static TimeSpan TimeSpanTryParse(this string valueNotNull)
    {
      TimeSpan res;
      bool isOK = TimeSpan.TryParse(valueNotNull, out res);

      return res;
    }
    public static TimeSpan TimeSpanTryParse(this string valueNotNull, TimeSpan defaultValue)
    {
      TimeSpan res;
      bool isOK = TimeSpan.TryParse(valueNotNull, out res);
      if (isOK)
      {
        return res;
      }
      else
      {
        return defaultValue;
      }
    }

    /// <summary>
    /// Note: ignores case.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="val"></param>
    /// <returns></returns>
    public static TEnum EnumTryParse<TEnum>(this string val) where TEnum : struct
    {
      TEnum result = default(TEnum); ;
      bool isOK = Enum.TryParse<TEnum>(val, true, out result);
      return result;
    }

    /// <summary>
    /// Note: ignores case.
    /// </summary>
    /// <typeparam name="TEnum"></typeparam>
    /// <param name="val"></param>
    /// <param name="defaultValue"></param>
    /// <returns></returns>
    public static TEnum EnumTryParse<TEnum>(this string val, TEnum defaultValue) where TEnum : struct
    {
      TEnum result;
      bool isOK = Enum.TryParse<TEnum>(val, true, out result);
      if (isOK)
      {
        return result;
      }
      else
      {
        return defaultValue;
      }
    }

  }
}
