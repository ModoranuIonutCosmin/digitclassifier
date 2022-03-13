using DataAcces.Entities;
using DataAcces.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.UnitTests
{
    public class DatabaseInitializer
    {
        public static void Initialize(DatabaseContext context)
        {
            if (context.Users.Any())
            {
                return;
            }
            Seed(context);
        }

        private static void Seed(DatabaseContext context)
        {
            var users = new[]
            {
                new User
                {
                    Id = Guid.Parse("c74faa1b-3f57-41f3-806c-1d7710faa89c"),
                    UserName = "jhnny101",
                    FirstName = "Sefanu",
                    LastName = "Johnny",
                    Password = "parola",
                    Email = "johnny@boss.com",
                    HistoryEntries = new List<History>()
                    {
                        new History()
                        {
                            Id = Guid.Parse("e398c9ba-fadd-42b7-bb88-c227e645d2e0"),
                            DateTime = DateTime.Now,
                            Image = "yH5BAEAAAAALAAAAAABAAEAAAIBRAA7",
                            PredictedDigit = 7,
                            PredictionProbability = 0.62,

                        },
                         new History()
                        {
                            Id = Guid.Parse("ff98c9ba-fadd-42b7-bb88-c227e645d2e0"),
                            DateTime = DateTime.Now,
                            Image = "yH5BAEAAAAALAAAAAABAAEAAAIBfAA7",
                            PredictedDigit = 6,
                            PredictionProbability = 0.72,
                        },
                          new History()
                        {
                            DateTime = DateTime.Now,
                            Image = "yH5BAEAAAAALAAAAAABAAEAaAiBRAA7",
                            PredictedDigit = 5,
                            PredictionProbability = 0.52,
                        }
                    }
                },
                new User
                {
                    Id = Guid.Parse("05d173fa-e47e-4ab6-84e0-2cd577109d63"),
                    UserName = "Giani199",
                    FirstName = "Giani",
                    LastName = "M.",
                    Password = "parola",
                    Email = "giani69@sefu.com",
                    HistoryEntries = new List<History>()
                    {
                        new History()
                        {
                            DateTime = DateTime.Now.AddHours(-1),
                            Image = "yH5BAEAAAAALAAAAAABAAEAAAIBRAA8",
                            IsFavorite = true
                        },
                        new History()
                        {
                            DateTime = DateTime.Now.AddHours(-1),
                            Image = "iVBORw0KGgoAAAANSUhEUgAAAMgAAADI",
                            IsFavorite = false
                        },
                         new History()
                        {
                            DateTime = DateTime.Now,
                            Image = "yH5BAEAAAAALAAAAAABAAEAAAIBRAA7",
                            PredictedDigit = 9,
                            PredictionProbability = 0.32,

                        },
                          new History()
                        {
                            Id = Guid.Parse("e398c9ba-fadd-42b7-bb88-c227e645d2ef"),
                            DateTime = DateTime.Now,
                            Image = "yH5BAEAAAAALAAAsaEAAAIBRAA7",
                            PredictedDigit = 2,
                            PredictionProbability = 0.42,
                        },
                    }
                }
            };


            context.Users.AddRange(users);

            var ratings = new[]
            {
                new PredictionRating()
                {
                    UserId = users[0].Id,
                    Prediction = new List<History>(users[0].HistoryEntries)[0],
                    Stars = 5,
                },
                new PredictionRating()
                {
                    UserId = users[1].Id,
                    Prediction = new List<History>(users[0].HistoryEntries)[0],
                    Stars = 3,
                },
            };

            context.Ratings.AddRange(ratings);
            context.SaveChanges();
        }
    }
}
