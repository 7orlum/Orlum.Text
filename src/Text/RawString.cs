namespace Orlum.Text
{
    /// <summary>
    /// If you call a method with FormattableString argument, the C# compiler invokes the method overload with String parameter instead of the the one with FormattableString parameter
    /// because the compiler prefers the conversion of interpolated strings to string over the conversion to FormattableString.
    /// This class fixes that the compiler behavior!
    /// If you call the method with FormattableString argument, there will be invoked the method overload with with FormattableString parameter.
    /// And if you call the method with String argument, there will be invoked the method overload with RawString parameter.
    /// Change your method String parameter to RawString to help the compiler to choose the right method overload.
    /// </summary>
    public class RawString
    {
        public string Value { get; }

        private RawString(string s)
        {
            Value = s;
        }

        public static implicit operator RawString(string s)
        {
            return new RawString(s);
        }

        public static implicit operator RawString(FormattableString formattable)
        {
            return new RawString(formattable.ToString());
        }
    }
}
