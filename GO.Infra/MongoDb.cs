using MongoDB.Driver;
using System;

namespace GO.Infra.MongoDb
{
    public abstract class MongoDb
    {
        public MongoDb(ref MongoServer mongoServer, ref MongoDatabase mDb)
        {
            try
            {
                mongoServer = new MongoClient().GetServer();

                mongoServer.Connect();

                mDb = mongoServer.GetDatabase("dbGO");
            }

            catch (Exception ex) { throw ex; }
        }
    }
}
