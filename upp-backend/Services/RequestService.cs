using AutoMapper;
using Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using upp.Dtos.Article;
using upp.Dtos.Recipe;
using upp.Dtos.Request;
using upp.Dtos.Training;
using upp.Entities;
using upp.Mapper;
using upp.Migrations;

namespace upp.Services
{
    public class RequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly ArticleService _articleService;
        private readonly RecipeService _recipeService;
        private readonly TrainingService _trainingService;

        private readonly IMapper _mapper;

        public RequestService(ApplicationDbContext context, 
            IMapper mapper,
            ArticleService articleService, 
            RecipeService recipeService, 
            TrainingService trainingService)
        {
            _context = context;
            _mapper = mapper;
            _articleService = articleService;
            _recipeService = recipeService;
            _trainingService = trainingService;
        }

        public async Task<int> CreateRequest(CreateRequestDto dto, CancellationToken token)
        {
            if(dto.Article == null && dto.Recipe == null && dto.Training == null)
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
                    request.RequestType = RequestType.Article;
                }
                    

                if (dto.Recipe != null)
                {
                    var id = await _recipeService.CreateRecipe(dto.Recipe, token);
                    request.EntityId = id;
                    request.RequestType = RequestType.Recipe;
                }
                    

                if (dto.Training != null)
                {
                    var id = await _trainingService.CreateTraining(dto.Training, token);
                    request.EntityId = id;
                    request.RequestType = RequestType.Training;
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
            var query = GetQueryRequests(dto.RequestType, token);

            return await query.ToPaginateListAsync<RequestDto, RequestDto>(_mapper, dto.Page, dto.Size, token);
        }

        private IQueryable<RequestDto> GetQueryRequests(RequestType type, CancellationToken token)
        {
            IQueryable<Request> query = _context.Requests
                .Include(p => p.Operator)
                .ThenInclude(o => o.Info)
                .Include(x => x.StatusType);


            if (type == RequestType.Article)
            {
                return query.Join(_context.Articles, x => x.EntityId, c => c.Id, (x, c) => new RequestDto {
                    Id = x.Id,
                    EntityId = c.Id,
                    RequestType = RequestType.Article,
                    Name = c.Name, 
                    Comment = x.Comment,
                    StatusTypeId = x.StatusTypeId,
                    StatusTypeName = x.StatusType.Name,
                    OperatorId = x.OperatorId,
                    OperatorName = x.Operator.Info.Lastname + x.Operator.Info.Name,
                    Created = (DateTime)x.Created
                });
            }

            if (type == RequestType.Training)
            {
                return query.Join(_context.Trainings, x => x.EntityId, c => c.Id, (x, c) => new RequestDto {
                    Id = x.Id,
                    EntityId = c.Id,
                    Name = c.Name, 
                    RequestType = RequestType.Training,
                    Comment = x.Comment,
                    StatusTypeId = x.StatusTypeId,
                    StatusTypeName = x.StatusType.Name,
                    OperatorId = x.OperatorId,
                    OperatorName = x.Operator.Info.Lastname + x.Operator.Info.Name,
                    Created = (DateTime)x.Created
                });
            }

            if (type == RequestType.Recipe)
            {
                return query.Join(_context.Recipes, x => x.EntityId, c => c.Id, (x, c) => new RequestDto {
                    Id = x.Id,
                    EntityId = c.Id,
                    Name = c.Name, 
                    RequestType = RequestType.Recipe,
                    Comment = x.Comment,
                    StatusTypeId = x.StatusTypeId,
                    StatusTypeName = x.StatusType.Name,
                    OperatorId = x.OperatorId,
                    OperatorName = x.Operator.Info.Lastname + x.Operator.Info.Name,
                    Created = (DateTime)x.Created
                });
            }

           throw new Exception("asdasd");
        }


        public async Task<int> EditRequest(RequestDto dto, CancellationToken token)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(x => x.Id == dto.Id, token);

            request = _mapper.Map(dto, request);

            _context.Requests.Update(request);

            await _context.SaveChangesAsync(token);

            return request.Id;
        }

        public async Task<CreateRequestDto> GetRequest(int id, CancellationToken token)
        {
            var request = await _context.Requests.FirstOrDefaultAsync(x => x.Id == id, token);

            if (request == null)
            {
                throw new Exception("Request is null");
            }

            var requestDto = new CreateRequestDto() {
                Article = null,
                Recipe = null,
                Training = null
            };

            if(request.RequestType == RequestType.Article) {
                requestDto.Article = _mapper.Map<Article, ArticleDto>(_context.Articles.Include(x => x.ArticleBlocks).ThenInclude(x => x.Block).FirstOrDefault(x => x.Id == request.EntityId));
            }

            if(request.RequestType == RequestType.Recipe) {
                requestDto.Recipe = _mapper.Map<Recipe, RecipeDto>(_context.Recipes.Include(x => x.RecipeBlocks).ThenInclude(x => x.Block).FirstOrDefault(x => x.Id == request.EntityId));
            }

            if(request.RequestType == RequestType.Training) {
                requestDto.Training = _mapper.Map<Training, TrainingDto>(_context.Trainings.FirstOrDefault(x => x.Id == request.EntityId));
            }

            return requestDto;
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var request = await _context.Requests
                .FirstOrDefaultAsync(g => g.Id == id, token);

            if (request == null) throw new Exception("Request is null");

            _context.Requests.Remove(request);

            await _context.SaveChangesAsync(token);
        }

        public async Task ChangeRequestStatus(ChangeRequestDto dto, CancellationToken token) {
            var request = await _context.Requests
                .FirstOrDefaultAsync(g => g.Id == dto.Id, token);

            if (request == null) throw new Exception("Request is null");

            request.StatusTypeId = dto.StatusId;
            request.Comment = dto.Comment;

            if(request.RequestType == RequestType.Article) {
                var article = await _context.Articles.FirstOrDefaultAsync(x => x.Id == request.EntityId);
                article.StatusTypeId = dto.StatusId;
                _context.Articles.Update(article);
            }

            if(request.RequestType == RequestType.Recipe) {
                var recipe = await _context.Recipes.FirstOrDefaultAsync(x => x.Id == request.EntityId);
                recipe.StatusTypeId = dto.StatusId;
                _context.Recipes.Update(recipe);
            }

            if(request.RequestType == RequestType.Training) {
                var training = await _context.Trainings.FirstOrDefaultAsync(x => x.Id == request.EntityId);
                training.StatusTypeId = dto.StatusId;
                _context.Trainings.Update(training);
            }

            _context.Requests.Update(request);
            await _context.SaveChangesAsync(token);
        }
    }
}
