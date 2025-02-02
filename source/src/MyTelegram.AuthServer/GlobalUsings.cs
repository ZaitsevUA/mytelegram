﻿global using System.Buffers;
global using System.Numerics;
global using System.Security.Cryptography;
global using Microsoft.Extensions.Configuration;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using MyTelegram;
global using MyTelegram.AuthServer;
global using MyTelegram.AuthServer.BackgroundServices;
global using MyTelegram.AuthServer.EventHandlers;
global using MyTelegram.AuthServer.Extensions;
global using MyTelegram.AuthServer.Services;
global using MyTelegram.Caching.Redis;
global using MyTelegram.Core;
global using MyTelegram.EventBus;
global using MyTelegram.EventBus.Rebus;
global using MyTelegram.Schema;
global using MyTelegram.Schema.Extensions;
global using MyTelegram.Services.Extensions;
global using MyTelegram.Services.NativeAot;
global using MyTelegram.Services.Services;
global using Serilog;
global using Serilog.Sinks.SystemConsole.Themes;