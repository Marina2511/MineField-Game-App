// See https://aka.ms/new-console-template for more information
using MinefieldGame.Entities;
using MinefieldGame.Interfaces;
using MinefieldGame.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MinefieldGame.Options;
using Microsoft.Extensions.Configuration;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

// Set up configuration
IConfiguration config = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.AddSingleton<IMineService, MineService>();
builder.Services.AddScoped<IPlayerService, PlayerService>();
builder.Services.AddScoped<GameService, GameService>();
builder.Services.AddScoped<IConsoleService, ConsoleService>();

builder.Services.Configure<GameOptions>(config.GetSection(GameOptions.GameConfigOptions));

var app = builder.Build();

var scope = app.Services.CreateScope();

var chessboard = new Chessboard(4);
var player = new Player(int.Parse(config["GameConfig:PlayerLives"]));
var game = new Game(player, chessboard);

var gameService = scope.ServiceProvider.GetService<GameService>();

gameService.PlayGame(game);
