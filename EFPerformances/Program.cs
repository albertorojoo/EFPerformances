using EFPerformances.Models;
using EFPerformances.Services;
using System;

using var context = new ApplicationDbContext();
//Just for first run
//ApplicationDbContext.SeedData(context);

var queryService = new QueryService(context);

queryService.EagerLoading();
queryService.LazyLoading();
queryService.ExplicitLoading();