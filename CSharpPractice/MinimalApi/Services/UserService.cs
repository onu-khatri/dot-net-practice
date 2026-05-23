using MinimalApi.Models;
using System.Collections.Concurrent;

namespace MinimalApi.Services;

internal sealed class UserService
{
    private readonly ConcurrentDictionary<Guid, UserInfo> _users = new();

    public UserInfo[] GetAll()
    {
        return _users.Values.OrderBy(static user => user.Name).ToArray();
    }

    public UserInfo? GetById(Guid id)
    {
        return _users.TryGetValue(id, out var user) ? user : null;
    }

    public ServiceResult<UserInfo> Add(UserRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email))
        {
            return ServiceResult<UserInfo>.ValidationError("Name and email are required.");
        }

        var alreadyExists = _users.Values.Any(user => string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase));
        if (alreadyExists)
        {
            return ServiceResult<UserInfo>.Conflict($"User with email '{request.Email}' already exists.");
        }

        var user = new UserInfo(Guid.NewGuid(), request.Name.Trim(), request.Email.Trim());
        _users[user.Id] = user;

        return ServiceResult<UserInfo>.Success(user);
    }

    public ServiceResult<UserInfo> Update(Guid id, UserRequest request)
    {
        ArgumentNullException.ThrowIfNull(request);

        if (!_users.TryGetValue(id, out _))
        {
            return ServiceResult<UserInfo>.NotFound($"User with id '{id}' was not found.");
        }

        if (string.IsNullOrWhiteSpace(request.Name) || string.IsNullOrWhiteSpace(request.Email))
        {
            return ServiceResult<UserInfo>.ValidationError("Name and email are required.");
        }

        var emailInUse = _users.Values.Any(user => user.Id != id && string.Equals(user.Email, request.Email, StringComparison.OrdinalIgnoreCase));
        if (emailInUse)
        {
            return ServiceResult<UserInfo>.Conflict($"User with email '{request.Email}' already exists.");
        }

        var updatedUser = new UserInfo(id, request.Name.Trim(), request.Email.Trim());
        _users[id] = updatedUser;

        return ServiceResult<UserInfo>.Success(updatedUser);
    }

    public bool Delete(Guid id)
    {
        return _users.TryRemove(id, out _);
    }
}
