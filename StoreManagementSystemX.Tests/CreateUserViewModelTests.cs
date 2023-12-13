//using NSubstitute;
//using StoreManagementSystemX.Database.DAL.Interfaces;
//using StoreManagementSystemX.Database.Models;
//using StoreManagementSystemX.Services;
//using StoreManagementSystemX.ViewModels.Users;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;

//namespace StoreManagementSystemX.Tests
//{
//    public class CreateUserViewModelTests
//    {

//        private AuthContext GetAuthContext()
//            => new AuthContext(new User());

//        [Fact]
//        public void Returns_null_on_cancel()
//        {
//            // arrange
//            var authContext = GetAuthContext();
//            Guid? newUserId = null;

//            var onSubmit = (Guid userId) => { newUserId = userId; };

//            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
//            var unitOfWork = Substitute.For<IUnitOfWork>();

//            var createUserViewModel = new CreateUserViewModel(
//                authContext,
//                unitOfWorkFactory,
//                onSubmit,
//                () => { }
//            );

//            //act
//            createUserViewModel.CancelCommand.Execute(null);

//            //assert
//            Assert.Null(newUserId);
//        }

//        [Fact]
//        public void Returns_new_user_id_on_submit()
//        {
//            // arrange
//            var authContext = GetAuthContext();
//            Guid? newUserId = null;

//            var onSubmit = (Guid userId) => { newUserId = userId; };

//            var unitOfWorkFactory = Substitute.For<IUnitOfWorkFactory>();
//            var unitOfWork = Substitute.For<IUnitOfWork>();

//            var createUserViewModel = new CreateUserViewModel(
//                authContext,
//                unitOfWorkFactory,
//                onSubmit,
//                () => { }
//            );

//            //act
//            createUserViewModel.SubmitCommand.Execute("password");

//            //assert
//            Assert.NotNull(newUserId);
//        }
//    }
//}
