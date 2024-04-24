using App.Domain;
using App.Repository;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestProject
{
    public class UnitTestContractRepository : IDisposable
    {

        ContractRepository _contractRepositoryRepository;
        string connectionString;

        public UnitTestContractRepository()
        {
            connectionString = "data source=DESKTOP-MS42PFQ;initial catalog=ISS;trusted_connection=true;Integrated Security=true;TrustServerCertificate=True;";
            _contractRepositoryRepository = new ContractRepository(connectionString);
            ClearTestData();
        }

        public void Dispose()
        {
            ClearTestData();
        }


        private void ClearTestData()
        {
            using (var conn = new SqlConnection(connectionString))
            {
                conn.Open();
                var cmd = conn.CreateCommand();
                cmd.CommandText = "DELETE FROM Contract;";
                cmd.ExecuteNonQuery();
            }
        }
        public Song createMockSong(int id)
        {
            return new Song(id, "mockTitle", "mockArtist", "mockAlbum", new List<String>(), 0, 0, 0, 0, 0);
        }
        public Contract CreateMockContract()
        {
            Random random = new Random();
            int randomId = random.Next(50, 100001);
            createMockSong(1);
            return new Contract(randomId, 1,2,1);
        }

        [Fact]
        public void Add_ShouldAddContractSuccessfully_GetById_ShouldReturnContract()
        {
            // Arrange
            var expectedContract = CreateMockContract();
            _contractRepositoryRepository.Add(expectedContract);

            var newContraId = expectedContract.id; // Assuming Add updates the Id property with the database generated Id
            Assert.True(newContraId > 0); // Ensure ID was set indicating insertion was successful

            // Act
            Contract actualContract = _contractRepositoryRepository.getById(newContraId);



            // Assert
            Assert.Equal(expectedContract.song, actualContract.song);

            Dispose();
        }


        [Fact]
        public void Update_ShouldUpdateContractSong()
        {
            // Arrange
            var contract = CreateMockContract();
            _contractRepositoryRepository.Add(contract);

            contract.song = 3;
            _contractRepositoryRepository.Update(contract);

            // Act
            var actualContract= _contractRepositoryRepository.getById(contract.id);

            // Assert
            Assert.Equal(3, actualContract.song);

            // Clean-up in Dispose
            Dispose();
        }


        [Fact]
        public void Delete_ShouldRemoveContract()
        {
            // Arrange
            var contract = CreateMockContract();
            _contractRepositoryRepository.Add(contract);

            // Act
            var deleteResult = _contractRepositoryRepository.Delete(contract);

            // Assert
            Assert.True(deleteResult);
            Assert.Null(_contractRepositoryRepository.getById(contract.id)); // Assuming getById returns null if not found

            // Clean-up in Dispose is not required here as it has been deleted
        }

        [Fact]
        public void GetAll_ShouldReturnTwoContracts()
        {
            // Arrange
            var contract1 = CreateMockContract();
            _contractRepositoryRepository.Add(contract1);

            var contract2= CreateMockContract();
            contract2.song = 3;
            _contractRepositoryRepository.Add(contract2);

            // Act
            var contracts= _contractRepositoryRepository.getAll();

            // Assert
            Assert.True(contracts.Count == 2);

            // Clean-up in Dispose
            Dispose();
        }
    }
}
