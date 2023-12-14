using CommunityToolkit.Mvvm.Input;
using StoreManagementSystemX.Database.DAL.Interfaces;
using StoreManagementSystemX.Database.Models;
using StoreManagementSystemX.Domain.Aggregates.Roots.Users.Interfaces;
using StoreManagementSystemX.Domain.Repositories.Users.Interfaces;
using StoreManagementSystemX.Services;
using StoreManagementSystemX.Services.Interfaces;
using StoreManagementSystemX.ViewModels.Transactions.Interfaces;
using StoreManagementSystemX.ViewModels.Users.Interfaces;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace StoreManagementSystemX.ViewModels.Users
{
    public class UserListViewModel : BaseViewModel, IUserListViewModel
    {

        public UserListViewModel(
            AuthContext authContext, 
            Domain.Repositories.Users.Interfaces.IUserRepository userRepository, 
            IDialogService dialogService, 
            IUserCreationService userCreationService)
        {
            _userRepository = userRepository;
            _authContext = authContext;

            Users = new ObservableCollection<IUserRowViewModel>();

            _dialogService = dialogService;
            _userCreationService = userCreationService;

            foreach (var user in _userRepository.GetAll())
            {
                AddUserToCollection(user);
            }

            NewUserCommand = new RelayCommand(NewUserCommandHandler);
        }
        private IUserCreationService _userCreationService;

        private Domain.Repositories.Users.Interfaces.IUserRepository _userRepository;

        private AuthContext _authContext;

        private IDialogService _dialogService;

        public ObservableCollection<IUserRowViewModel> Users { get; }

        public ICommand NewUserCommand { get; }

        private void NewUserCommandHandler()
        {
            var newUserId = _userCreationService.CreateNewUser(_authContext);
            if (newUserId != null && newUserId.HasValue)
            {
                var newUser = _userRepository.GetById((Guid)newUserId);
                if (newUser != null)
                {
                    AddUserToCollection(newUser);
                }
            }
        }

        private void AddUserToCollection(IUser user)
        {
            var userRow = new UserRowViewModel(this, user);
            SubscribeToUserRow(userRow);
            Users.Add(userRow);
        }

        private void SubscribeToUserRow(IUserRowViewModel userRow)
        {
            userRow.UserDeleted += HandleRemoveUser;
        }

        private void UnsubscribeToUserRow(IUserRowViewModel userRow)
        {
            userRow.UserDeleted -= HandleRemoveUser;
        }

        private void HandleRemoveUser(object? sender, EventArgs<IUserRowViewModel> e)
        {
            var userToRemove = Users.FirstOrDefault(u => u.Id == e.Item.Id);

            if (userToRemove != null)
                RemoveUser(userToRemove);

        }

        private void RemoveUser(IUserRowViewModel userRow)
        {
            Users.Remove(userRow);
            UnsubscribeToUserRow(userRow);
        }

        class UserRowViewModel : IUserRowViewModel
        {
            private readonly UserListViewModel _parent;
            public UserRowViewModel(UserListViewModel parent, IUser user)
            {
                _user = user;
                _parent = parent;
                DeleteCommand = new RelayCommand(DeleteCommandHandler, () => _user.Username != "admin");
                UpdateCommand = new RelayCommand(UpdateCommandHandler, () => _user.Username != "admin");
            }

            private readonly IUser _user;

            public Guid Id => _user.Id;

            public string Username => _user.Username;

            public ICommand UpdateCommand { get; }
            public event EventHandler<EventArgs<IUserRowViewModel>> UserUpdated = null!;

            private void UpdateCommandHandler()
            {

            }

            public ICommand DeleteCommand { get; }
            public event EventHandler<EventArgs<IUserRowViewModel>> UserDeleted = null!;
            private void DeleteCommandHandler()
            {
                if (_parent._dialogService.ShowConfirmationDialog("Confirm Delete", "Do you really want to delete this user?"))
                {
                    _parent._userRepository.Remove(_user.Id);
                    OnUserDeleted(new EventArgs<IUserRowViewModel>(this));
                }
            }

            protected virtual void OnUserDeleted(EventArgs<IUserRowViewModel> e)
            {
                UserDeleted?.Invoke(this, e);
            }
        }


    }
}
