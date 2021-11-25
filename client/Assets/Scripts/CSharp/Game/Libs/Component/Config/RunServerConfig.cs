using MongoDB.Bson.Serialization.Attributes;

namespace MetaGame
{
	[BsonIgnoreExtraElements]
	public class RunServerConfig: AConfigComponent
	{
		public string IP = "";
	}
}