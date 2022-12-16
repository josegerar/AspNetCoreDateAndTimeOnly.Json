// See https://aka.ms/new-console-template for more information
using AspNetCoreDateAndTimeOnly.Json;

var data = new
{
    Fecha = DateTime.Now,
    Guid = Guid.NewGuid()
};

Console.WriteLine(DateOnly.FromDateTime(DateTime.Now).ToJSON());
Console.WriteLine(data.Guid.ToJSON());
Console.WriteLine(data.ToJSON());
