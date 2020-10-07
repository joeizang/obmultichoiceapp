using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using HotChocolate.Language;
using HotChocolate.Types;

namespace RektaRetailApp.Backend.GraphQL.ScalarTypes
{
    public class DateTimeOffsetType : ScalarType
    {
        public DateTimeOffsetType() : base("DateTimeOffset")
        {
        }

        public override bool IsInstanceOfType(IValueNode literal)
        {
            if (literal == null)
            {
                throw new ArgumentNullException(nameof(literal));
            }

            return literal is StringValueNode || literal is NullValueNode;
        }

        public override object ParseLiteral(IValueNode literal)
        {
            if (literal == null)
            {
                throw new ArgumentNullException(nameof(literal));
            }

            if (literal is NullValueNode)
            {
                return null;
            }

            if (literal is StringValueNode stringLiteral)
            {
                return DateTimeOffset.Parse(stringLiteral.Value, CultureInfo.InvariantCulture);
            }

            throw new ArgumentException(
                "The DateTimeOffsetType can only parse string literals.",
                nameof(literal));
        }

        public override IValueNode ParseValue(object value)
        {
            if (value == null)
            {
                return new NullValueNode(null);
            }

            if (value is DateTimeOffset t)
            {
                return new StringValueNode(null, t.ToString("O", CultureInfo.InvariantCulture), false);
            }

            throw new ArgumentException(
                "The specified value has to be a DateTimeOffset in order to be parsed by the DateTimeOffsetType.");
        }

        public override object Serialize(object value)
        {
            if (value == null)
            {
                return null;
            }

            if (value is DateTimeOffset t)
            {
                return t.ToString("O", CultureInfo.InvariantCulture);
            }

            throw new ArgumentException(
                "The specified value cannot be serialized by the DateTimeOffsetType.");
        }

        public override bool TryDeserialize(object serialized, out object value)
        {
            if (serialized is null)
            {
                value = null;
                return true;
            }

            if (serialized is string s)
            {
                var result = DateTimeOffset.TryParse(s, out var ts);
                value = ts;
                return result;
            }

            value = null;
            return false;
        }

        public override Type ClrType => typeof(DateTimeOffset);
    }
}
