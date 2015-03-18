using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DocumentDBDemo
{
    public class DocumentDBProvider
    {
        private static string EndpointUrl = "https://aramdocumentdb.documents.azure.com:443/";
        private static string AuthorizationKey = "Get this from Azure portal for your subscription";
        private static string DatabaseName = "InterviewDB";
        private static string DocumentCollectionName = "InterviewCollection";

        public async Task<DocumentCollection> CreateDatabaseAndDocumentCollection()
        {
           
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);


            Database database = await client.CreateDatabaseAsync(new Database { Id = DatabaseName });

            DocumentCollection documentCollection = await client.CreateDocumentCollectionAsync(database.CollectionsLink,
                                                                                                new DocumentCollection { Id = DocumentCollectionName }
                                                                                               );
            return documentCollection;
        }

        public async Task<ResourceResponse<Document>> AddCanidateToCollection(Candidate candidate)
        {
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == DatabaseName).AsEnumerable().FirstOrDefault();
            DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(database.CollectionsLink).Where(db => db.Id == DocumentCollectionName).AsEnumerable().FirstOrDefault();

            return await client.CreateDocumentAsync(documentCollection.DocumentsLink, candidate);
        }

        public List<Candidate> GetCandidatesCollection()
        {
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == DatabaseName).AsEnumerable().FirstOrDefault();
            DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(database.CollectionsLink).Where(db => db.Id == DocumentCollectionName).AsEnumerable().FirstOrDefault();

            return client.CreateDocumentQuery<Candidate>(documentCollection.DocumentsLink).ToList();
        }

        public List<Candidate> GetCandidateById(int candidateId)
        {
            var client = new DocumentClient(new Uri(EndpointUrl), AuthorizationKey);
            Database database = client.CreateDatabaseQuery().Where(db => db.Id == DatabaseName).AsEnumerable().FirstOrDefault();
            DocumentCollection documentCollection = client.CreateDocumentCollectionQuery(database.CollectionsLink).Where(db => db.Id == DocumentCollectionName).AsEnumerable().FirstOrDefault();

            return client.CreateDocumentQuery<Candidate>(documentCollection.DocumentsLink).Where(m => m.CandidateId == candidateId).Select(m => m).ToList();
        }

    }
}
