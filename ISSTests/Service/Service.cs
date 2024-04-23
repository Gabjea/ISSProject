using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using App.Service;
using App.Domain;
using App.Repository;
using System.Collections.ObjectModel;

namespace App.Service
{
    public class Service
    {
        private static string connectionString = "Server=172.30.242.1;Database=Music;User Id=SA;Password=Password123;TrustServerCertificate=true";
        private readonly SongRepository _songRepo;
        private readonly ClientRepository _clientRepo;
        private readonly UserRepository _userRepo;
        private readonly SongService _songService;
        private readonly Admin _admin;
        private readonly UserService _userService;

        private Account activeUser;

        private readonly ClientService _clientService;

        public Service(SongRepository songRepo, ClientRepository clientRepo, UserRepository userRepo, SongService songService, ClientService clientService, Admin admin, UserService userService)
        {
            _songRepo = songRepo;
            _clientRepo = clientRepo;
            _userRepo = userRepo;
            _songService = songService;
            _clientService = clientService;
            _admin = admin;
            _userService = userService;
            _clientService = clientService;
        }

        public void CreateAccount(string email, string username, string password, string confirmPassword, bool isClient)
        {
            if (isClient)
            {

            }
            else
            {

            }
        }

        public bool Login(string username, string password)
        {
            var user = _userRepo.getByUsername(username);
            var client = _clientRepo.getByUsername(username);

            if (user == null && client == null)
            {

                return false;
            }
            else if (user != null)
            {
                if (_userService.IsValidLogin(username, password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                if (_clientService.IsValidLogin(username, password))
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }

        }

        public bool CreateAccount(string email, string username, string password, string confirmPassword, bool isClient, string location = "", int age = 0, string artistName = "")
        {
            if (isClient)
            {
                // Call the CreateAccount method of ClientService
                return _clientService.CreateAccount(email, username, password, confirmPassword, artistName);
            }
            else
            {
                // Call the CreateAccount method of UserService
                _userService.CreateAccount(email, username, password, confirmPassword, location, age);
                return true;
            }
        }

        public ObservableCollection<string> GetSongs()
        {
            return _songService.getSongs();
        }

        public ObservableCollection<string> GetClients()
        {
            return _clientService.GetAllClients();
        }

    }
}