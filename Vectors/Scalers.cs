﻿using System;
using System.Runtime.CompilerServices;
using CodeHelpers.ObjectPooling;
using UnityEngine;

namespace CodeHelpers.Vectors
{
	public static class Scalars
	{
		public const float Epsilon = 0.00001f;

		public const double RadianToDegreeDouble = 180d / Math.PI;
		public const double DegreeToRadianDouble = Math.PI / 180d;

		public const float RadianToDegree = (float)RadianToDegreeDouble;
		public const float DegreeToRadian = (float)DegreeToRadianDouble;

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Lerp(float left, float right, float value) => (right - left) * value + left;
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static int Lerp(int left, int right, int value) => (right - left) * value + left;

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float InverseLerp(float left, float right, float value) => AlmostEquals(left, right) ? 0f : (value - left) / (right - left);
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static int InverseLerp(int left, int right, int value) => AlmostEquals(left, right) ? 0 : (value - left) / (right - left);

		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Clamp(this float value, float min, float max) => value < min ? min : value > max ? max : value;
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float Clamp(this int value, float min, float max) => value < min ? min : value > max ? max : value;
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static int Clamp(this int value, int min, int max) => value < min ? min : value > max ? max : value;

		/// <summary>
		/// Convert <paramref name="value"/> to an angle between -180f (Exclusive) and 180f (Inclusive) with the same rotational value as input.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float ToSignedAngle(this float value) => -(180f - value).Repeat(360f) + 180f;

		/// <summary>
		/// Convert <paramref name="value"/> to an angle between -179 and 180 with the same rotational value as input.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static int ToSignedAngle(this int value) => -(180 - value).Repeat(360) + 180;

		/// <summary>
		/// Convert <paramref name="value"/> to an angle between 0f (Inclusive) and 360f (Exclusive) with the same rotational value as input.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float ToUnsignedAngle(this float value) => value.Repeat(360f);

		/// <summary>
		/// Convert <paramref name="value"/> to an angle between 0 and 359 with the same rotational value as input.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static int ToUnsignedAngle(this int value) => value.Repeat(360);

		/// <summary>
		/// Converts <paramref name="value"/>, a number from zero to positive one. Into a range from negative one to positive one.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float To1To1(this float value) => value * 2f - 1f;

		/// <summary>
		/// Converts <paramref name="value"/>, a number from negative one to positive one. Into a range from zero to positive one.
		/// </summary>
		[MethodImpl(MethodImplOptions.AggressiveInlining)] public static float To0To1(this float value) => (value + 1f) / 2f;

		public static bool IsPowerOfTwo(this int value) => (value & -value) == value;  //Or (value & (value - 1)) == 0;
		public static bool IsPowerOfTwo(this long value) => (value & -value) == value; //Or (value & (value - 1)) == 0;

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AlmostEquals(float left, float right, float epsilon = 0.00001f)
		{
			if (left == right) return true;                  //Handles absolute equals and degenerate cases
			const float Normal = (1L << 23) * float.Epsilon; //The smallest positive (non-zero) normal value that can be stored in a float

			float difference = Math.Abs(left - right);

			//If too close to zero to use relative comparison
			if (left == 0f || right == 0f || difference < Normal) return difference < epsilon * Normal;

			//Relative comparison
			float sum = Math.Abs(left) + Math.Abs(right);
			return difference < epsilon * Math.Min(sum, float.MaxValue);
		}

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AlmostEquals(double left, double right, double epsilon = 0.00000001d)
		{
			if (left == right) return true;                    //Handles absolute equals and degenerate cases
			const double Normal = (1L << 52) * double.Epsilon; //The smallest positive (non-zero) normal value that can be stored in a double

			double difference = Math.Abs(left - right);

			//If too close to zero to use relative comparison
			if (left == 0d || right == 0d || difference < Normal) return difference < epsilon * Normal;

			//Relative comparison
			double sum = Math.Abs(left) + Math.Abs(right);
			return difference < epsilon * Math.Min(sum, double.MaxValue);
		}

		public static int Sign(this float value) => AlmostEquals(value, 0f) ? 0 : Math.Sign(value);
		public static int Sign(this int value) => Math.Sign(value);

		/// <summary>Another implementation of Unity's Mathf.Repeat method. Slightly faster.</summary>
		public static float Repeat(this float value, float length)
		{
			float result = value % length;
			return result < 0f ? result + length : result;
		}

		/// <summary>Another implementation of Unity's Mathf.Repeat method. Slightly faster.</summary>
		public static int Repeat(this int value, int length)
		{
			int result = value % length;
			return result < 0 ? result + length : result;
		}

		/// <summary>Another implementation of Unity's Mathf.Repeat method. Slightly faster.</summary>
		public static double Repeat(this double value, double length)
		{
			double result = value % length;
			return result < 0d ? result + length : result;
		}

		/// <summary>Another implementation of Unity's Mathf.Repeat method. Slightly faster.</summary>
		public static long Repeat(this long value, long length)
		{
			long result = value % length;
			return result < 0L ? result + length : result;
		}

		public static int Floor(this float value) => (int)Math.Floor(value);
		public static int Ceil(this float value) => (int)Math.Ceiling(value);
		public static int Round(this float value) => (int)Math.Round(value);

		public static int FlooredDivide(this int value, int divisor) => value / divisor - Convert.ToInt32((value < 0) ^ (divisor < 0) && value % divisor != 0);
		public static long FlooredDivide(this long value, long divisor) => value / divisor - Convert.ToInt64((value < 0) ^ (divisor < 0) && value % divisor != 0);

		public static int CeilDivide(this int value, int divider) => (value - 1) / divider + 1;

		public static float Remap(this float value, float fromLow, float fromHigh, float toLow, float toHigh) => (value - fromLow) * (toHigh - toLow) / (fromHigh - fromLow) + toLow;

		public static int SingleToInt32Bits(float value)
		{
#if UNSAFE_CODE_ENABLED
			unsafe
			{
				return *(int*)&value;
			}
#else
			return new FloatIntConverter(value).intValue;
#endif
		}

		public static float Int32BitsToSingle(int value)
		{
#if UNSAFE_CODE_ENABLED
			unsafe
			{
				return *(float*)&value;
			}
#else
			return new FloatIntConverter(value).floatValue;
#endif
		}

#if !UNSAFE_CODE_ENABLED
		[System.Runtime.InteropServices.StructLayout(System.Runtime.InteropServices.LayoutKind.Explicit)]
		readonly struct FloatIntConverter
		{
			public FloatIntConverter(float floatValue) : this() => this.floatValue = floatValue;
			public FloatIntConverter(int intValue) : this() => this.intValue = intValue;

			[System.Runtime.InteropServices.FieldOffset(0)] public readonly float floatValue;
			[System.Runtime.InteropServices.FieldOffset(0)] public readonly int intValue;
		}
#endif

		public static float Damp(float current, float target, ref float velocity, float smoothTime, float deltaTime)
		{
			//Implementation based on Game Programming Gems 4 Chapter 1.10

			float omega = 2f / smoothTime;  //The smooth coefficient
			float delta = current - target; //Change in position/value

			float exp = ApproximateExp(omega * deltaTime);
			float number = (velocity + omega * delta) * deltaTime;

			velocity = (velocity - omega * number) * exp;
			return target + (delta + number) * exp;

			//Uses Taylor Polynomials to approximate 1/exp; acceptable accuracy when domain: 0 < x < 1
			float ApproximateExp(float value) => 1f / (1f + value + 0.48f * value * value + 0.235f * value * value * value);
		}

		/// <summary>
		/// Format the integer to their abbreviations using metric suffixes
		/// The returned string will always be shorter or equals to 4 characters
		/// </summary>
		public static string ToKiloFormatString(this int value)
		{
			if (value < 0) throw ExceptionHelper.Invalid(nameof(value), value, "cannot be negative.");

			if (value >= 1000000000) return Format(1000000000, 'B');
			if (value >= 1000000) return Format(1000000, 'M');
			if (value >= 1000) return Format(1000, 'K');

			return value.ToString();

			string Format(int level, char suffix)
			{
				int integer = value / level;
				int floating = value / (level / 1000) - integer * 1000;

				var builder = CommonPooler.stringBuilder.GetObject();

				builder.Append(integer);
				builder.Append('.');
				builder.Append(floating.ToString("D3"));

				if (builder.Length > 3) builder.Remove(3, builder.Length - 3);
				if (builder[builder.Length - 1] == '.') builder.Remove(builder.Length - 1, 1);

				builder.Append(suffix);

				string result = builder.ToString();
				CommonPooler.stringBuilder.ReleaseObject(builder);

				return result;
			}
		}
	}
}