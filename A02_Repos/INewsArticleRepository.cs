using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A02_BOs;

namespace A02_Repos
{
    public interface INewsArticleRepository
    {
        public NewsArticle getNewsById(string newsId);

        public void AddNews(NewsArticle newsArticle);

        public void RemoveNews(string newsId);

        public void UpdateNews(NewsArticle news);

        public List<NewsArticle> GetNewsArticles();

        public List<NewsArticle> GetNewsByDateRange(DateTime startDate, DateTime endDate);

        public bool isInUsed(short cateId);

        public void UpdateNewsArticleTags(string newsArticleId, List<int> tagIds);


    }
}
