using MinimalApi.Models;

namespace MinimalApi.Services
{
    public interface IUserService
    {
        ServiceResult<UserInfo> Add(UserRequest request);
        bool Delete(Guid id);
        UserInfo[] GetAll();
        UserInfo? GetById(Guid id);
        ServiceResult<UserInfo> Update(Guid id, UserRequest request);
    }
}