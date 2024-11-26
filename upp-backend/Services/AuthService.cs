using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Entities;
using upp.Dtos.User;
using System.Linq;
using Microsoft.EntityFrameworkCore;


namespace Services
{
    public class AuthService 
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;

        public AuthService(ApplicationDbContext context, UserManager<User> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<AuthUserDto> Login(UserLoginDto dto, CancellationToken token)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);

            if (user == null)
            {
                throw new Exception("User is null");
            }

            if (await _userManager.CheckPasswordAsync(user, dto.Password))
            {
                return await GiveAccess(user);
            }
            else
            {
                throw new Exception("Password invalid");
            }
        }

        public async Task<AuthUserDto> GiveAccess(User user)
        {
            var authClaims = await GenerateAuthClaims(user);

            var jwtToken = GetJwtToken(authClaims);


            var authDto = new AuthUserDto()
            {
                Key = jwtToken,
                UserId = user.Id,
                Roles = (List<string>)await _userManager.GetRolesAsync(user)
            };

            return authDto;
        }


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

        private async Task<List<Claim>> GenerateAuthClaims(User user)
        {
            var userRoles = await _userManager.GetRolesAsync(user);

            var authClaims = new List<Claim>
                 {
                     new Claim(ClaimTypes.Name, user.UserName),
                     new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                     new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                 };

            foreach (var role in userRoles)
            {
                authClaims.Add(new Claim(ClaimTypes.Role, role));
            }

            return authClaims;
        }

        //public async Task<int> CreatePsychologist(AddUserDto dto)
        //{
        //    var psy = await _userManager.FindByNameAsync(dto.Email);

        //    if (psy != null)
        //    {
        //        throw new Exception("User already exists!");
        //    }

        //    try
        //    {
        //        return await CreateUser(dto, Roles.Psychologist);
        //    }
        //    catch (Exception ex)
        //    {
        //        throw new Exception(ex.Message);
        //    }
        //}

        public async Task<AuthUserDto> CreateClient(AddUserDto dto)
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

        public async Task<AuthUserDto> CreateUser(AddUserDto dto, string role)
        {
            var user = new User() { Email = dto.Email, UserName = dto.Email };

            var result = await _userManager.CreateAsync(user, dto.Password); //create user

            if (result.Succeeded)
            {
                await _userManager.AddToRoleAsync(user, role); //add role

                if (dto.Name != "")
                {
                    var additionalInfo = new AdditionalInfo() { Id = user.Id, Name = dto.Name, Lastname = dto.Lastname, BirthDay = DateTime.Now }; //add fio

                    _context.AdditionalInfo.Add(additionalInfo);

                    await _context.SaveChangesAsync();
                }

                return await GiveAccess(user);
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


    public static class Roles
    {
        public const string Admin = "Admin";
        public const string Psychologist = "Psychologist";
        public const string Client = "Client";
        public static readonly string[] AvailableRoles = {
                Admin,
                Psychologist,
                Client
            };
    }
}
