﻿using webapi.BLL.Repos.Implementations;
using webapi.BLL.Repos.Interfaces;
using webapi.DAL.Entities.Main;

namespace webapi.BLL.Helpers
{
    public class RecommendationSystem
    {
        private readonly IUserRepo _userRepo;

        public RecommendationSystem(IUserRepo userRepo)
        {
            _userRepo = userRepo;
        }

        public RecommendationSystem()
        {
        }

        public async Task<List<Product>> GetRecommendations(string userEmail)
        {
            var targetUser = await _userRepo.GetRegisteredUser(userEmail);
            if (targetUser == null)
            {
                throw new ArgumentException("User not found");
            }
            var similarUsers = await CalculateSimilarUsersAsync(targetUser);
            var neighborhood = SelectNeighborhood(similarUsers);
            var recommendations = GenerateRecommendations(targetUser, neighborhood, 7);

            return recommendations;
        }

        private async Task<Dictionary<User, double>> CalculateSimilarUsersAsync(User targetUser)
        {
            var allUsers = await _userRepo.GetAllRegisteredUsers();
            var similarityScores = new Dictionary<User, double>();

            foreach (var user in allUsers)
            {
                if (user.Email != targetUser.Email)
                {
                    double similarity = CalculateSimilarity(targetUser, user);
                    similarityScores.Add(user, similarity);
                }
            }

            return similarityScores;
        }

        private double CalculateSimilarity(User targetUser, User otherUser)
        {
            var targetSet = targetUser.Favourites?.Select(fav => fav.ProductId).ToList() ?? new List<int>();
            var otherSet = otherUser.Favourites?.Select(fav => fav.ProductId).ToList() ?? new List<int>();

            var intersection = targetSet.Intersect(otherSet).Count();
            var union = targetSet.Union(otherSet).Count();

            if (union == 0)
            {
                return 0; 
            }

            return (double)intersection / union;
        }


        private List<User> SelectNeighborhood(Dictionary<User, double> similarityScores)
        {
            var neighborhood = similarityScores.OrderByDescending(pair => pair.Value).Take(5).Select(pair => pair.Key).ToList();
            return neighborhood;
        }

        private List<Product> GenerateRecommendations(User targetUser, List<User> neighborhood, int maxRecommendations)
{
    var recommendations = new List<Product>();

    // Collect all products liked by users in the neighborhood
    foreach (var user in neighborhood)
    {
        if (user.Favourites != null)
        {
            var userFavorites = user.Favourites.Select(fav => fav.Product).ToList();
            recommendations.AddRange(userFavorites);
            Console.WriteLine($"Added products liked by user {user.Email} to recommendations:");
            foreach(var fav in userFavorites)
            {
                Console.WriteLine($"\tProduct ID: {fav.Id}, Name: {fav.Name}");
            }
        }
    }

    // Filter out products that the target user has already liked
    var targetUserFavoriteIds = targetUser.Favourites?.Select(fav => fav.ProductId).ToList() ?? new List<int>();
    Console.WriteLine("Target user's liked product IDs:");
    if (targetUserFavoriteIds != null)
    {
        foreach(var id in targetUserFavoriteIds)
        {
            Console.WriteLine($"\t{ id }");
        }
    }
    else
    {
        Console.WriteLine("\tNo liked products found for the target user.");
    }
    recommendations = recommendations.Where(prod => !targetUserFavoriteIds.Contains(prod.Id)).ToList();

    // Shuffle the recommendations
    var random = new Random();
    recommendations = recommendations.OrderBy(x => random.Next()).ToList();

    // Take only the maximum number of recommendations
    recommendations = recommendations.Take(maxRecommendations).ToList();

    Console.WriteLine("Final recommendations:");
    foreach(var rec in recommendations)
    {
        Console.WriteLine($"\tProduct ID: {rec.Id}, Name: {rec.Name}");
    }

    return recommendations;
}






    }

}
