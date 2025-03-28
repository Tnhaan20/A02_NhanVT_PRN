﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using A02_BOs;
using A02_DAOs;

namespace A02_Repos
{
    public class NewsArticleRepository : INewsArticleRepository
    {
        public List<NewsArticle> GetNewsArticles() => NewsArticleDAO.Instance.GetNewsArticles();

        public List<NewsArticle> GetNewsByDateRange(DateTime startDate, DateTime endDate) =>
            NewsArticleDAO.Instance.getNewsByDateRange(startDate, endDate);
        public void AddNews(NewsArticle newsArticle) => NewsArticleDAO.Instance.AddNews(newsArticle);



        public NewsArticle getNewsById(string newsId) => NewsArticleDAO.Instance.GetNewsId(newsId);

        public void RemoveNews(string newsId) => NewsArticleDAO.Instance.Delete(newsId);

        public void UpdateNews(NewsArticle news) => NewsArticleDAO.Instance.Update(news);

        public bool isInUsed(short cateId) => NewsArticleDAO.Instance.isInUsed(cateId);

        public void UpdateNewsArticleTags(string newsArticleId, List<int> tagIds) => NewsArticleDAO.Instance.UpdateArticleTags(newsArticleId, tagIds);

    }
}
