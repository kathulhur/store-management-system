//using StoreManagementSystemX.Domain.Aggregates.Roots.Users;
//using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
//using StoreManagementSystemX.Domain.Factories.Users;
//using StoreManagementSystemX.Domain.Factories.Users.Interfaces;
//using StoreManagementSystemX.Domain.Repositories.Interfaces;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace StoreManagementSystemX.Tests
//{
//    public class UserRepositoryTests
//    {
//        private static readonly UserFactory _userFactory = new UserFactory();

//        private IRepository<IUser> CreateRepositoryWithSingleUser()
//        {
//            // assemble
//            var user = _userFactory.Create(new CreateUserArgs { Username = "hello", Password = "world" });

//            IRepository<IUser> repository = new UserRepository(_userFactory);

//            // Verify empty
//            Assert.False(repository.GetAll().Any());

//            // act
//            repository.Add(user);

//            // assert
//            Assert.True(repository.GetAll().Any());

//            return repository;
//        }

//        private IRepository<IUser> CreateRepositoryWithSingleUserHavingId()
//        {
//            // assemble
//            var user = _userFactory.Create(new CreateUserArgs { Username = "hello", Password = "world" });

//            IRepository<IUser> repository = new UserRepository(_userFactory);

//            // Verify empty
//            Assert.False(repository.GetAll().Any());

//            // act
//            repository.Add(user);

//            // assert
//            Assert.True(repository.GetAll().Any());

//            return repository;
//        }


//        [Fact]
//        public void User_gets_added_and_has_correct_username_value()
//        {
//            // assemble
//            IRepository<IUser> repository = new UserRepository(_userFactory);

//            var newUser = new CreateUserArgs { Username = "hello", Password = "world" };

//            Assert.False(repository.GetAll().Any());

//            // act
//            repository.Add(_userFactory.Create(newUser));

//            // assert
//            Assert.True(repository.GetAll().Any());
//            var storedUser = repository.GetAll().First();
//            Assert.True(storedUser.Username == newUser.Username);
//        }

//        [Fact]
//        public void User_gets_removed_on_remove()
//        {
//            // assemble
//            var userRepository = CreateRepositoryWithSingleUser();
//            var userToDelete = userRepository.GetAll().First();


//            // act
//            userRepository.Remove(userToDelete.Id);

//            // Assert
//            Assert.True(!userRepository.GetAll().Any());
//        }


//        private class CreateUserArgs : ICreateUserArgs
//        {
//            public Guid CreatorId { get; set; }

//            public string Username { get; set; }

//            public string Password { get; set; }
//        }
//    }
//}
