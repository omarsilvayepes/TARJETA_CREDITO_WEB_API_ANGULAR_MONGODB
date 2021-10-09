using BackendTarjetaCredito.Model;
using MongoDB.Bson;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BackendTarjetaCredito.Services
{
    public class TarjetaService
    {
        private readonly IMongoCollection<TarjetaCredito> mongoCollection;

        public TarjetaService(IDatabaseSettings settings)//conexionBD accede a DataBaseSetting y a su vez este a appsetting.json
        {
            var client = new MongoClient(settings.ConnectionString);
            var database = client.GetDatabase(settings.DatabaseName);

            mongoCollection = database.GetCollection<TarjetaCredito>(settings.CollectionName);
        }


        public async Task<List<TarjetaCredito>> getAllTarjetas()
        {
            return await mongoCollection.FindAsync(new BsonDocument()).Result.ToListAsync();
        }
        public async Task AddTarjeta(TarjetaCredito tarjetaCredito)
        {
             await mongoCollection.InsertOneAsync(tarjetaCredito);
        }
        public async Task updateTarjeta(TarjetaCredito tarjetaCredito)
        {

            var tarjetaDB = Builders<TarjetaCredito>.Filter.Eq(resultado => resultado.Id, tarjetaCredito.Id);
            await mongoCollection.ReplaceOneAsync(tarjetaDB, tarjetaCredito);

        }

        public async Task deleteTarjetaById(string id)
        {
            var tarjeta = Builders<TarjetaCredito>.Filter.Eq(r => r.Id, id);
            await mongoCollection.DeleteOneAsync(tarjeta);
        }

        public async Task<TarjetaCredito> getTarjetaById(string id)
        {
            return await mongoCollection.FindAsync(new BsonDocument { { "_id", new ObjectId(id) } })
            .Result.FirstOrDefaultAsync();

        }
    }
}
