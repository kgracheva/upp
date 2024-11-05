using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Entities;
using upp.Dtos;


namespace Controllers
{
    internal class RoleController 
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public RoleController(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

   
     
        public async Task<int> CreatePsychologist(AddUserDto dto)
        {
            var psy = await _userManager.FindByNameAsync(dto.Email);

            if (psy != null)
            {
                throw new Exception("User already exists!");
            }

            try
            {
                return await CreateUser(dto, Roles.Psychologist);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<int> CreateClient(AddUserDto dto)
        {
            var client = await _userManager.FindByNameAsync(dto.Email);

            if (client != null)
            {
                throw new Exception("User already exists!");
            }

            try
            {
                return await CreateUser(dto, Roles.Client);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private async Task<int> CreateUser(AddUserDto dto, string role)
        {
            var user = new User() { Email = dto.Email };

            var result = await _userManager.CreateAsync(user, dto.Password); //create user

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role); //add role

                if (dto.Name != null)
                {
                    var additionalInfo = new AdditionalInfo() { Id = user.Id, Name = dto.Name, Lastname = dto.Lastname }; //add fio

                    _context.AdditionalInfo.Add(additionalInfo);

                    await _context.SaveChangesAsync();
                }

                return user.Id;
            }
            else
            {
                throw new Exception("User creating error");
            }
        }


        private string GetJwtToken(List<Claim> claims)
        {
            var jwt = new JwtSecurityToken(
                issuer: AuthOptions.ISSUER,
                audience: AuthOptions.AUDIENCE,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromDays(1)),
                signingCredentials: new SigningCredentials(AuthOptions.GetSymmetricSecurityKey(), SecurityAlgorithms.HmacSha256)
            );

            var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

            return encodedJwt;
        }

    }
}
