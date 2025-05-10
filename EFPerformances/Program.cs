using EFPerformances.Models;
using System;

using var context = new ApplicationDbContext();
ApplicationDbContext.SeedData(context);
Console.WriteLine("Hello, World!");
