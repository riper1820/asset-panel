using System;

namespace RiperBool.AssetPanel.Editor.Domain.ValueObjects
{
    /// <summary>
    /// A value object which describes a url of an item of BOOTH
    /// </summary>
    public record BoothUrl
    {
        private static readonly string ValidPathPattern = @"/.*/items/\d+";
        private static readonly string ValidHost = "booth.pm";

        public Uri Value { get; init; }
        
        public BoothUrl(Uri value)
        {
            if(value == null) throw new ArgumentNullException(nameof(value));
            if (!System.Text.RegularExpressions.Regex.IsMatch(value.AbsolutePath, ValidPathPattern))
            {
                throw new ArgumentException("Invalid BOOTH URL format.", nameof(value));
            }
            if (!value.Host.Contains(ValidHost))
            {
                throw new ArgumentException("URL must be from booth.pm domain.", nameof(value));
            }
            
            Value = value;
        }
    }
}