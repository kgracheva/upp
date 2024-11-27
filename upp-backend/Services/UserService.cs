using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Services;
using System.Linq;
using System.Runtime.CompilerServices;
using upp.Dtos.Article;
using upp.Dtos.Recipe;
using upp.Dtos.Request;
using upp.Dtos.Training;
using upp.Dtos.User;
using upp.Entities;
using upp.Mapper;
using upp.Migrations;

namespace upp.Services
{
    public class UserService
    {
        private readonly ApplicationDbContext _context;
        

        private readonly IMapper _mapper;

        public UserService(ApplicationDbContext context, 
            IMapper mapper
         )
        {
            _context = context;
            _mapper = mapper;
         
        }

        public async Task<PaginatedList<PhyschologistDto>> Get(FindUserDto dto, CancellationToken token)
        {
            var query = _context.Users.Include(x => x.Info).Where(x => x.Roles.Any(l => l.Role.Name == Roles.Psychologist));

            return await query.ToPaginateListAsync<User, PhyschologistDto>(_mapper, dto.Page, dto.Size, token);
        }

        public async Task ChangeWorkStatus(int id, CancellationToken token) {
            
           var info = _context.AdditionalInfo.FirstOrDefault(x => x.Id == id);

           info.IsAvailableToWork = !info.IsAvailableToWork;
           _context.AdditionalInfo.Update(info);

           await _context.SaveChangesAsync(token);
        }
    }
}
