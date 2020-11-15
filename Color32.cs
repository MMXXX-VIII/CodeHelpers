﻿using System;
using CodeHelpers.Vectors;

namespace CodeHelpers
{
	public readonly struct Color32 : IEquatable<Color32>
	{
		public Color32(byte r, byte g, byte b, byte a = byte.MaxValue)
		{
			this.r = r;
			this.g = g;
			this.b = b;
			this.a = a;
		}

		public Color32(float r, float g, float b, float a = 1f) : this(ToInteger(r), ToInteger(g), ToInteger(b), ToInteger(a)) { }

		public readonly byte r;
		public readonly byte g;
		public readonly byte b;
		public readonly byte a;

		public float RFloat => ToDecimal(r);
		public float GFloat => ToDecimal(g);
		public float BFloat => ToDecimal(b);
		public float AFloat => ToDecimal(a);

		public byte this[int index]
		{
			get
			{
#if UNSAFE_CODE_ENABLED
				unsafe
				{
					if (index < 0 || 3 < index) throw ExceptionHelper.Invalid(nameof(index), index, InvalidType.outOfBounds);
					fixed (Color32* pointer = &this) return ((byte*)pointer)[index];
				}
#else
				switch (index)
				{
					case 0: return r;
					case 1: return g;
					case 2: return b;
					case 3: return a;
				}

				throw ExceptionHelper.Invalid(nameof(index), index, InvalidType.outOfBounds);
#endif
			}
		}

		public static readonly Color32 black = new Color32(0, 0, 0);

		static float ToDecimal(byte value) => (float)value / byte.MaxValue;
		static byte ToInteger(float value) => (byte)(value.Clamp(0f, 1f) * byte.MaxValue);

		public static explicit operator Color32(Float3 value) => new Color32(value.x, value.y, value.z);
		public static explicit operator Color32(Int3 value) => new Color32(value.x, value.y, value.z);

		public static explicit operator Float3(Color32 value) => new Float3(value.RFloat, value.GFloat, value.BFloat);
		public static explicit operator Int3(Color32 value) => new Int3(value.r, value.g, value.b);

		public static bool operator ==(Color32 first, Color32 second) => first.Equals(second);
		public static bool operator !=(Color32 first, Color32 second) => !first.Equals(second);

		public bool Equals(Color32 other) => r == other.r && g == other.g && b == other.b && a == other.a;
		public override bool Equals(object obj) => obj is Color32 other && Equals(other);

		public override int GetHashCode() => (r << 24) | (g << 16) | (b << 8) | a;
		public override string ToString() => $"{nameof(r)}: {r}, {nameof(g)}: {g}, {nameof(b)}: {b}, {nameof(a)}: {a}";
	}
}