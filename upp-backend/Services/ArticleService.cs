using AutoMapper;
using Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using upp.Dtos.Article;
using upp.Dtos.Product;
using upp.Entities;
using upp.Mapper;

namespace upp.Services
{
    public class ArticleService
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public ArticleService(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<int> CreateArticle(ArticleDto dto, CancellationToken token)
        {
            if (dto.Id != 0)
                throw new Exception("product Id is not null!");


            var article = _mapper.Map<Article>(dto);

            //foreach(var block in dto.Blocks)
            //{
            //    _context.Blocks.Add(_mapper.Map<Block>(block));
            //    _context.ArticleBlocks.Add(new ArticleBlock() { ArticleId = article.Id, BlockId = block.Id }); 
            //}
            _context.Articles.Add(article);
            await _context.SaveChangesAsync(token);
            return article.Id;
        }

        public async Task<PaginatedList<ArticleDto>> GetArticles(FindAtriclesDto dto, CancellationToken token)
        {
            IQueryable<Article> query = _context.Articles
                .Include(p => p.ArticleBlocks)
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

            return await query.ToPaginateListAsync<Article, ArticleDto>(_mapper, dto.Page, dto.Size, token);
        }


        public async Task<int> EditArticle(ArticleDto dto, CancellationToken token)
        {
            if (dto.Id == 0)
                throw new Exception("article Id is null!");

            var article = _context.Articles.FirstOrDefault(x => x.Id == dto.Id);

            if (article == null)
                throw new Exception("article Id is null!");

            article = _mapper.Map(dto, article);

            _context.Articles.Update(article);

            await _context.SaveChangesAsync(token);

            return article.Id;
        }

        public async Task<ArticleDto> GetArticle(int id, CancellationToken token)
        {
            var article = await _context.Articles.FirstOrDefaultAsync(x => x.Id == id, token);

            if (article == null)
            {
                throw new Exception("Article is null");
            }

            return _mapper.Map<Article, ArticleDto>(article);
        }

        public async Task Delete(int id, CancellationToken token)
        {
            var article = await _context.Articles
                .FirstOrDefaultAsync(g => g.Id == id, token);

            if (article == null) throw new Exception("Article is null");

            article.IsDeleted = true;
            _context.Articles.Update(article);

            await _context.SaveChangesAsync(token);
        }
    }
}
