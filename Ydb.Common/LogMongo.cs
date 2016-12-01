using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Bson;
using MongoDB.Driver;
using log4net;
using log4net.Config;
using Log4Mongo;


namespace Ydb.Common
{
    public class LogMongo
    {

        //private IMongoCollection<BsonDocument> _collection;
        //private IMongoDatabase _db;
        //private const string LogsCollectionName = "logs";

        //private LogMongo()
        //{
        //    XmlConfigurator.Configure();
        //    //Log4Mongo.InternalDebugging = true;

        //    MongoUrl url = new MongoUrl("mongodb://localhost/log4net");
        //    MongoClient client = new MongoClient(url);
        //    _db = client.GetDatabase(url.DatabaseName);
        //    _db.DropCollectionAsync(LogsCollectionName);
        //    _collection = _db.GetCollection<BsonDocument>(LogsCollectionName);
        //}


        //"%date [%thread] %-5level %logger- %message%newline");
        public static void GetConfiguredLog()
        {
            XmlConfigurator.Configure(new System.IO.MemoryStream(Encoding.UTF8.GetBytes(@"
<log4net>
	<appender name='MongoDBAppender' type='Log4Mongo.MongoDBAppender, Log4Mongo'>
		<connectionString value='mongodb://localhost' />
		<field>
			<name value='date' />
			<layout type='log4net.Layout.PatternLayout'  value='%date'/>
		</field>
		<field>
			<name value='level' />
			<layout type='log4net.Layout.PatternLayout' value='%level' />
		</field>
		<field>
			<name value='thread' />
			<layout type='log4net.Layout.PatternLayout' value='%thread' />
		</field>
        <field>
			<name value='logger' />
			<layout type='log4net.Layout.PatternLayout' value='%logger' />
		</field>
        <field>
			<name value='logger1' />
			<layout type='log4net.Layout.PatternLayout' value='%logger{4}' />
		</field>
        <field>
			<name value='logger2' />
			<layout type='log4net.Layout.PatternLayout' value='%logger{5}' />
		</field>
        <field>
			<name value='message' />
			<layout type='log4net.Layout.PatternLayout' value='%message' />
		</field>
		<field>
			<name value='threadContextProperty' />
			<layout type='log4net.Layout.RawPropertyLayout'>
				<key value='threadContextProperty' />
			</layout>
		</field>
		<field>
			<name value='globalContextProperty' />
			<layout type='log4net.Layout.RawPropertyLayout'>
				<key value='globalContextProperty' />
			</layout>
		</field>
		<field>
			<name value='numberProperty' />
			<layout type='log4net.Layout.RawPropertyLayout'>
				<key value='numberProperty' />
			</layout>
		</field>
		<field>
			<name value='dateProperty' />
			<layout type='log4net.Layout.RawPropertyLayout'>
				<key value='dateProperty' />
			</layout>
		</field>
		<field>
			<name value='exception' />
			<layout type='log4net.Layout.ExceptionLayout' />
		</field>
		<field>
			<name value='customProperty' />
			<layout type='log4net.Layout.RawPropertyLayout'>
				<key value='customProperty' />
			</layout>
		</field>
	</appender>
	<root>
		<level value='WARN' />
		<appender-ref ref='MongoDBAppender' />
	</root>
</log4net>
")));
            //return LogManager.GetLogger("Test");
        }




    }
}
