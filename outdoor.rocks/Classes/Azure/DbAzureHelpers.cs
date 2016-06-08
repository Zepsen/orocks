using System;
using outdoor.rocks.Filters;

namespace outdoor.rocks.Classes.Azure
{
    public class DbAzureHelpers
    {
        public static Guid TryParseIdToGuid(string id)
        {
            var guid = Guid.Empty;

            try
            {
                guid = Guid.Parse(id);
            }
            catch (FormatException)
            {
                throw new IdFormatException("Bad format for GUID");
            }

            return guid;
        }
    }
}