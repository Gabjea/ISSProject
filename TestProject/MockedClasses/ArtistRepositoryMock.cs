using App.Domain;
using App.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;


namespace TestProject.MockedClasses
{
    internal class ArtistRepositoryMock : ArtistRepository
    {
        private int itemCount = 10;
        public ArtistRepositoryMock() : base("")
        {

        }

        public int getItemCount()
        {
            return itemCount;
        }

        public Client createMockArtist()
        {
            return new Client(1, "username", "password", "email", "salt", "artistName");
        }

        public override List<Client> getAll()
        {
            List<Client> mockedListOfArtists = new List<Client>();
            for (int i = 0; i < itemCount; i++)
            {
                mockedListOfArtists.Add(createMockArtist());
            }
            return mockedListOfArtists;
        }
    }
}
