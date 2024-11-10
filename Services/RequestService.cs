using AutoMapper;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using upp.Dtos.Article;
using upp.Dtos.Request;
using upp.Entities;
using upp.Mapper;

namespace upp.Services
{
    public class RequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly ArticleService _articleService;
        private readonly RecipeService _recipeService;
        private readonly TrainingService _trainingService;

        private readonly IMapper _mapper;

        public RequestService(ApplicationDbContext context, IMapper mapper, ArticleService articleService, RecipeService recipeService, TrainingService trainingService)
        {
            _context = context;
            _mapper = mapper;
            _articleService = articleService;
            _recipeService = recipeService;
            _trainingService = trainingService;
        }

        public async Task<int> CreateRequest(CreateRequestDto dto, CancellationToken token)
        {
            if(dto.Article == null || dto.Recipe == null || dto.Training == null)
            {
                throw new Exception("Everything is null");
            }

            var request = new Request();

            try
            {
                if (dto.Article != null)
                {
                    var id = await _articleService.CreateArticle(dto.Article, token);
                    request.EntityId = id;
                }
                    

                if (dto.Recipe != null)
                {
                    var id = await _recipeService.CreateRecipe(dto.Recipe, token);
                    request.EntityId = id;
                }
                    

                if (dto.Training != null)
                {
                    var id = await _trainingService.CreateTraining(dto.Training, token);
                    request.EntityId = id;
                }

                request.OperatorId = 1;
                request.StatusTypeId = 2;

                _context.Requests.Add(request);
                await _context.SaveChangesAsync(token);

                return request.Id;
            }
            catch(Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaginatedList<RequestDto>> GetRequests(FindRequestsDto dto, CancellationToken token)
        {
            var query = GetQueryRequests(dto.RequestType);

            return await query.ToPaginateListAsync<Request, RequestDto>(_mapper, dto.Page, dto.Size, token);
        }

        private IQueryable<Request> GetQueryRequests(RequestType type)
        {
            IQueryable<Request> query = _context.Requests
                .Include(p => p.Operator)
                .ThenInclude(o => o.Info)
                .Include(x => x.StatusType);


            if (type == RequestType.Article)
            {
                query.Join(_context.Articles, x => x.EntityId, c => c.Id, (x, c) => new {
                    Id = x.Id,
                    EntityId = c.Id,
                    RequestType = RequestType.Article,
                    Comment = x.Comment,
                    StatusTypeId = x.StatusTypeId,
                    StatusTypeName = x.StatusType.Name,
                    OpertorId = x.OperatorId,
                    OperatorName = x.Operator.Info.Lastname + x.Operator.Info.Name
                });
            }

            if (type == RequestType.Training)
            {
                query.Join(_context.Articles, x => x.EntityId, c => c.Id, (x, c) => new {
                    Id = x.Id,
                    EntityId = c.Id,
                    RequestType = RequestType.Training,
                    Comment = x.Comment,
                    StatusTypeId = x.StatusTypeId,
                    StatusTypeName = x.StatusType.Name,
                    OpertorId = x.OperatorId,
                    OperatorName = x.Operator.Info.Lastname + x.Operator.Info.Name
                });
            }

            if (type == RequestType.Recipe)
            {
                query.Join(_context.Articles, x => x.EntityId, c => c.Id, (x, c) => new {
                    Id = x.Id,
                    EntityId = c.Id,
                    RequestType = RequestType.Recipe,
                    Comment = x.Comment,
                    StatusTypeId = x.StatusTypeId,
                    StatusTypeName = x.StatusType.Name,
                    OpertorId = x.OperatorId,
                    OperatorName = x.Operator.Info.Lastname + x.Operator.Info.Name
                });
            }

            return query;
        }


        public async Task<int> EditRequest(RequestDto dto, CancellationToken token)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(x => x.Id == dto.Id, token);

            request = _mapper.Map(dto, request);

            _context.Requests.Update(request);

            await _context.SaveChangesAsync(token);

            return request.Id;
        }

        public async Task<RequestDto> GetRequest(int id, CancellationToken token)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(x => x.Id == id, token);

            if (request == null)
            {
                throw new Exception("Request is null");
            }

            return _mapper.Map<Request, RequestDto>(request);
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var request = await _context.Requests
                .FirstOrDefaultAsync(g => g.Id == id, token);

            if (request == null) throw new Exception("Request is null");

            _context.Requests.Remove(request);

            await _context.SaveChangesAsync(token);
        }
    }
}
