using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;
using MongoDB.Driver.Builders;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GO.Infra.MongoDb
{
    public abstract class DAD<T> : MongoDb, ICrud<T>
    {
        static MongoServer mongoServer = default(MongoServer);
        static MongoDatabase mDB = default(MongoDatabase);

        MongoCollection<T> mCollection = null;

        public DAD()
            : base(ref mongoServer, ref mDB)
        {
            try
            {
                mCollection = mDB.GetCollection<T>(string.Format("tb_{0}", typeof(T).Name.ToLower()));
            }

            catch (Exception ex) { throw ex; }
        }

        public virtual T Get(string id)
        {
            try
            {
                return mCollection.FindOneById(ObjectId.Parse(id));
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual IEnumerable<T> Search(T t)
        {
            try
            {
                var colecao = mCollection.FindAllAs<T>();

                return colecao.ToList();
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual T Add(T t)
        {
            try
            {
                mCollection.Insert(t);

                return t;
            }
            catch (Exception ex) { throw ex; }
        }

        /// <summary>
        /// Adicionar em lote(List)
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public virtual List<T> Add(List<T> t)
        {
            try
            {
                BsonClassMap.RegisterClassMap<T>();

                mCollection.InsertBatch(t);

                return t;
            }
            catch (Exception ex) { throw ex; }
        }

        public virtual bool Update(T t)
        {
            var atualizado = mCollection.Save(t);

            return atualizado.DocumentsAffected > 0;
        }

        public virtual bool Delete(string id)
        {
            var excluido = mCollection.Remove(Query.EQ("_id", ObjectId.Parse(id)));

            return excluido.DocumentsAffected > 0;
        }
    }
}
