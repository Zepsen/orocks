using MongoDB.Bson;
using outdoor.rocks.Filters;

namespace outdoor.rocks.Classes.Mongo
{
    public class DbMongoHelpers
    {
        public static ObjectId TryParseObjectId(string id)
        {
            ObjectId objId;
            var res = ObjectId.TryParse(id, out objId);

            if (!res) throw new IdFormatException("Bad format for ObjectId");

            return objId;
        }
    }
}