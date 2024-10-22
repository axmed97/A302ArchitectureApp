using Business.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results.Abstract;
using Core.Utilities.Results.Concrete.SuccessResults;
using Microsoft.AspNetCore.Identity;

namespace Business.Concrete
{
    public class RoleManager : IRoleService
    {
        private readonly RoleManager<AppRole> _roleManager;

        public RoleManager(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task<IResult> CreateRoleAsync(string role)
        {
            AppRole appRole = new()
            {
                Id = Guid.NewGuid().ToString(),
                Name = role,
            };

            await _roleManager.CreateAsync(appRole);
            return new SuccessResult(System.Net.HttpStatusCode.OK);
        }
    }
}
