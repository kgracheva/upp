using AutoMapper;
using Entities;
using Microsoft.EntityFrameworkCore;
using upp.Dtos.Article;
using upp.Dtos.Training;
using upp.Entities;
using upp.Mapper;

namespace upp.Services
{
    public class TrainingService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TrainingService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateTraining(TrainingDto dto, CancellationToken token)
        {
            if (dto.Id != 0)
                throw new Exception("training Id is not null!");


            var training = _mapper.Map<Training>(dto);

            //foreach(var block in dto.Blocks)
            //{
            //    _context.Blocks.Add(_mapper.Map<Block>(block));
            //    _context.ArticleBlocks.Add(new ArticleBlock() { ArticleId = article.Id, BlockId = block.Id }); 
            //}
            _context.Trainings.Add(training);
            await _context.SaveChangesAsync(token);
            return training.Id;
        }

        public async Task<PaginatedList<TrainingDto>> GetTrainings(FindTrainingsDto dto, CancellationToken token)
        {
            IQueryable<Training> query = _context.Trainings
                .Include(p => p.TrainingBlocks)
                .ThenInclude(p => p.Block)
                .Include(p => p.Creator)
                .ThenInclude(c => c.Info);

            if (dto.Name != "")
            {
                query = query.Where(p => p.Name.ToUpper().Contains(dto.Name.ToUpper()));
            }

            if (dto.CreatorId != 0)
            {
                query = query.Where(p => dto.CreatorId == p.CreatorId);
            }

            query = query.OrderBy(p => p.Name);

            return await query.ToPaginateListAsync<Training, TrainingDto>(_mapper, dto.Page, dto.Size, token);
        }


        public async Task<int> EditTraining(TrainingDto dto, CancellationToken token)
        {
            if (dto.Id == 0)
                throw new Exception("article Id is null!");

            var training = _context.Trainings.FirstOrDefault(x => x.Id == dto.Id);

            if (training == null)
                throw new Exception("training Id is null!");

            training = _mapper.Map(dto, training);

            _context.Trainings.Update(training);

            await _context.SaveChangesAsync(token);

            return training.Id;
        }

        public async Task<TrainingDto> GetTraining(int id, CancellationToken token)
        {
            var training = await _context.Trainings.FirstOrDefaultAsync(x => x.Id == id, token);

            if (training == null)
            {
                throw new Exception("training is null");
            }

            return _mapper.Map<Training, TrainingDto>(training);
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var training = await _context.Trainings
                .FirstOrDefaultAsync(g => g.Id == id, token);

            if (training == null) throw new Exception("training is null");

            training.IsDeleted = true;
            _context.Trainings.Update(training);

            await _context.SaveChangesAsync(token);
        }
    }
}
