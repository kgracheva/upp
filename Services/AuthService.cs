using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Entities;
using upp.Dtos;


namespace Services
{
    internal class AuthService 
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AuthService(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        // public async Task<string> Login(UserLoginDto dto)
        // {
        //     var user = await _userManager.FindByNameAsync(dto.UserName);

        //     if (user == null)
        //     {
        //         throw new Exception("User is null");
        //     }

        //     if(await _userManager.CheckPasswordAsync(user,  dto.Password))
        //     {
        //         var authClaims = await GenerateAuthClaims(user);

        //         var jwtToken = GetJwtToken(authClaims);

        //         return jwtToken;
        //     }
        //     else
        //     {
        //         throw new Exception("Password invalid");
        //     }
        // }


        // public async Task<int> CreateModerator(AddUserDto dto)
        // {
        //     var moderator = await _userManager.FindByNameAsync(dto.UserName);

        //     if(moderator != null)
        //     {
        //         throw new Exception("User already exists!");
        //     }

        //     try
        //     {
        //         return await CreateUser(dto, Roles.Moderator);
        //     }
        //     catch(Exception ex)
        //     {
        //         throw new Exception(ex.Message);
        //     }
        // }

        // public async Task<int> CreateOperator(AddUserDto dto)
        // {
        //     var operatorObj = await _userManager.FindByNameAsync(dto.UserName);

        //     if (operatorObj != null)
        //     {
        //         throw new Exception("User already exists!");
        //     }

        //     try
        //     {
        //         return await CreateUser(dto, Roles.Operator);
        //     }
        //     catch (Exception ex)
        //     {
        //         throw new Exception(ex.Message);
        //     }
        // }

        // public async Task<string> Impersonate(ImpersonateRequestDto dto)
        // {
        //     var user = await _userManager.FindByNameAsync(dto.UserName);

        //     if(user == null)
        //     {
        //         throw new Exception("User is null");
        //     }

        //     var authClaims = await GenerateAuthClaims(user);

        //     var jwtToken = GetJwtToken(authClaims);

        //     return jwtToken;
        // }

        // private async Task<List<Claim>> GenerateAuthClaims(ApplicationUser user)
        // {
        //     var userRoles = await _userManager.GetRolesAsync(user);

        //     var authClaims = new List<Claim>
        //         {
        //             new Claim(ClaimTypes.Name, user.UserName),
        //             new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        //         };

        //     foreach (var role in userRoles)
        //     {
        //         authClaims.Add(new Claim(ClaimTypes.Role, role));
        //     }

        //     return authClaims;
        // }

        public async Task<int> CreatePsychologist(AddUserDto dto)
        {
            var psy = await _userManager.FindByNameAsync(dto.Email);

            if (psy != null)
            {
                throw new Exception("User already exists!");
            }

            try
            {
                return await CreateUser(dto, Roles.Operator);
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


    public enum Roles
    {

    }
}
