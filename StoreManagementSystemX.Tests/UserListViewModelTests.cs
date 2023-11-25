using NSubstitute;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Users;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreManagementSystemX.Tests
{
    public class UserListViewModelTests
    {
        
        

        private AuthContext GetAuthContext()
            => new AuthContext(new User());

        [Fact]
        public void Records_are_loaded()
        {

            // arrange
            var users = new List<User>()
            {
                new User(),
                new User(),
                new User(),
                new User(),
            };


            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            
            var unitOfWork = Substitute.For<IUnitOfWork>();

            unitOfWork.UserRepository.GetAll().Returns(users);

            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);
            var dialogService = Substitute.For<IDialogService>();

            var authContext = GetAuthContext();
            var userCreationService = Substitute.For<IUserCreationService>();

            // act
            var userListViewModel = new UserListViewModel(
                authContext,
                unitOfWorkFactory,
                dialogService,
                userCreationService
            );


            // assert
            Assert.Equal(4, userListViewModel.Users.Count);
        }

        [Fact]
        public void New_user_gets_added_to_the_list_after_its_creation()
        {

            // arrange
            var newUserId = Guid.NewGuid();
            var newUser = new User { Id = newUserId };

            var users = new List<User>()
            {
                new User(),
                new User(),
                new User(),
                new User(),
            };


            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
            var unitOfWork = Substitute.For<IUnitOfWork>();
            unitOfWork.UserRepository.GetAll().Returns(users);
            unitOfWork.UserRepository.GetById(newUserId).Returns(newUser);

            unitOfWorkFactory.CreateUnitOfWork().Returns(unitOfWork);
            var dialogService = Substitute.For<IDialogService>();

            var authContext = GetAuthContext();
            var userCreationService = Substitute.For<IUserCreationService>();
            userCreationService.CreateNewUser(authContext).Returns(newUserId);

            // act
            var userListViewModel = new UserListViewModel(
                authContext,
                unitOfWorkFactory,
                dialogService,
                userCreationService
            );
            // assert
            Assert.Equal(4, userListViewModel.Users.Count);
            Assert.Null(userListViewModel.Users.FirstOrDefault(u => u.Id == newUserId));


            // act
            userListViewModel.NewUserCommand.Execute(null);

            // assert
            Assert.Equal(5, userListViewModel.Users.Count);
            Assert.NotNull(userListViewModel.Users.FirstOrDefault(u => u.Id == newUserId));
        }


    }
}
