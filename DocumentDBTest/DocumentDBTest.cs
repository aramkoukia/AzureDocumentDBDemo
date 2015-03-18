using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Threading.Tasks;
using DocumentDBDemo;

namespace DocumentDBTest
{
    [TestClass]
    public class DocumentDBTest
    {
        [TestMethod]
        public async Task CreateDatabaseAndDocumentCollection_Test()
        {
            
            var sut = new DocumentDBDemo.DocumentDBProvider();
            var result = await sut.CreateDatabaseAndDocumentCollection();
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public async Task AddCanidateToCollection_Test()
        {

            var sut = new DocumentDBDemo.DocumentDBProvider();
            var candidate = new Candidate()
            {
                CandidateFirstName = "Aram",
                CandidateLastName = "Koukia",
                CandidateId = 1,
                Status = new CandidateStatus() { 
                     CandidateStatusId= 1,
                     CandidateStatusName = "New"
                }
            };
            var result = await sut.AddCanidateToCollection(candidate);
            Assert.IsNotNull(result);

        }

        [TestMethod]
        public void GetCandidatesCollection_Test()
        {
            var sut = new DocumentDBDemo.DocumentDBProvider();
            var result = sut.GetCandidatesCollection();
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetCandidateById_Test()
        {
            var sut = new DocumentDBDemo.DocumentDBProvider();
            var result = sut.GetCandidateById(1);
            Assert.IsNotNull(result);
        }

    }
}
