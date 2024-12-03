using Flexi5S.Model;
using MongoDB.Driver;

namespace Flexi5S.Services
{
    public class MongoDBServices
    {
        private readonly IMongoCollection<AuditFormSubmission> _auditCollection;

        public MongoDBServices(IMongoClient client)
        {
            var database = client.GetDatabase("AuditFormDb");
            _auditCollection = database.GetCollection<AuditFormSubmission>("AuditForms");
        }

        public async Task CreateAuditFormAsync(AuditFormSubmission submission)
        {
            await _auditCollection.InsertOneAsync(submission);
        }
    }
}


