using System;

namespace Abp.Extensions
{
    public static class GuidExtensions
    {
        public const string Guid0String = "00000000-0000-0000-0000-000000000000";
        public const string Guid1String = "00000000-0000-0000-0000-000000000001";
        public const string Guid2String = "00000000-0000-0000-0000-000000000002";
        public const string Guid3String = "00000000-0000-0000-0000-000000000003";
        public const string Guid4String = "00000000-0000-0000-0000-000000000004";
        public const string Guid5String = "00000000-0000-0000-0000-000000000005";
        public const string Guid6String = "00000000-0000-0000-0000-000000000006";
        public const string Guid7String = "00000000-0000-0000-0000-000000000007";
        public const string Guid42String = "00000000-0000-0000-0000-000000000042";
        public const string Guid43String = "00000000-0000-0000-0000-000000000043";
        public const string Guid266String = "00000000-0000-0000-0000-000000000266";
        public const string Guid999String = "00000000-0000-0000-0000-000000000999";
        public const string Guid999999String = "00000000-0000-0000-0000-000000999999";
        public const string Guid65910381String = "00000000-0000-0000-0000-000065910381";

        public static Guid Guid0 { get; } = Guid.Parse(Guid0String);
        public static Guid Guid1 { get; } = Guid.Parse(Guid1String);
        public static Guid Guid2 { get; } = Guid.Parse(Guid2String); 
        public static Guid Guid3 { get; } = Guid.Parse(Guid3String); 
        public static Guid Guid4 { get; } = Guid.Parse(Guid4String); 
        public static Guid Guid5 { get; } = Guid.Parse(Guid5String);
        public static Guid Guid6 { get; } = Guid.Parse(Guid6String);
        public static Guid Guid7 { get; } = Guid.Parse(Guid7String);
        public static Guid Guid42 { get; } = Guid.Parse(Guid42String);
        public static Guid Guid43 { get; } = Guid.Parse(Guid43String);
        public static Guid Guid266 { get; } = Guid.Parse(Guid266String);
        public static Guid Guid999 { get; } = Guid.Parse(Guid999String);
        public static Guid Guid999999 { get; } = Guid.Parse(Guid999999String);
        public static Guid Guid65910381 { get; } = Guid.Parse(Guid65910381String);
    }
}
