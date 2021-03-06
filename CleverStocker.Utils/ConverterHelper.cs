﻿using System;

namespace CleverStocker.Utils
{
    /// <summary>
    /// 转换器助手
    /// </summary>
    public static class ConverterHelper
    {
        /// <summary>
        /// String => Double
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double StringToDouble(string value, double defaultValue = 0D)
            => double.TryParse(value, out double result) ? result : defaultValue;

        /// <summary>
        /// String => Long
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long StringToLong(string value, long defaultValue = 0L)
            => long.TryParse(value, out long result) ? result : defaultValue;

        /// <summary>
        /// String => Float
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static float StringToFloat(string value, float defaultValue = float.NaN)
            => float.TryParse(value, out float result) ? result : defaultValue;

        /// <summary>
        /// String => Int
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static int StringToInt(string value, int defaultValue = 0)
            => int.TryParse(value, out int result) ? result : defaultValue;

        /// <summary>
        /// String=> DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string value)
            => StringToDateTime(value, DateTime.Now);

        /// <summary>
        /// String=> DateTime
        /// </summary>
        /// <param name="value"></param>
        /// <param name="defaultDateTime"></param>
        /// <returns></returns>
        public static DateTime StringToDateTime(string value, DateTime defaultDateTime)
            => DateTime.TryParse(value, out DateTime result) ? result : defaultDateTime;

        /// <summary>
        /// 字符串转枚举
        /// </summary>
        /// <typeparam name="TEnum">枚举类型</typeparam>
        /// <param name="name"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TEnum StringToEnum<TEnum>(string name, TEnum defaultValue)
            where TEnum : struct
            => Enum.TryParse(name, out TEnum value) ? value : defaultValue;
    }
}
