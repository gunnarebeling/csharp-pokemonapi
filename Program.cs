using System.Xml.Serialization;
using PokemonAPI.Modules;
using PokemonAPI.Modules.DTOs;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
List<Pokemon> pokemonList = new List<Pokemon>
        {
            new Pokemon { Id = 1, Name = "Pikachu", Price = 12.99m, Rarity = 5, Condition= 3 },
            new Pokemon { Id = 2, Name = "Charizard", Price = 24.50m, Rarity = 8 },
            new Pokemon { Id = 3, Name = "Bulbasaur", Price = 9.99m, Rarity = 4 },
            new Pokemon { Id = 4, Name = "Squirtle", Price = 10.50m, Rarity = 3 },
            new Pokemon { Id = 5, Name = "Jigglypuff", Price = 8.75m, Rarity = 2 },
            new Pokemon { Id = 6, Name = "Meowth", Price = 5.99m, Rarity = 3 },
            new Pokemon { Id = 7, Name = "Gengar", Price = 18.99m, Rarity = 7 },
            new Pokemon { Id = 8, Name = "Snorlax", Price = 15.99m, Rarity = 6 },
            new Pokemon { Id = 9, Name = "Mewtwo", Price = 30.00m, Rarity = 9 },
            new Pokemon { Id = 10, Name = "Eevee", Price = 11.99m, Rarity = 4 }
        };

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.MapGet("/api/pokemon", () => 
{
    return pokemonList.Select(pokemon => new PokemonDTO
    {
        Id = pokemon.Id,
        Name = pokemon.Name,
        Price = pokemon.Price,
        Rarity = pokemon.Rarity
    });
});

app.MapGet("/api/pokemon/{id}", (int id) => 
{
    Pokemon pokemon = pokemonList.FirstOrDefault(p => p.Id == id);
    if (pokemon != null)
    {
        return Results.Ok( new PokemonDetailsDTO 
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Price = pokemon.Price,
            Rarity = pokemon.Rarity,
            Condition = pokemon.Condition
        });
    }else
    {
        return Results.NotFound();
    }
});

app.MapPost("/api/pokemon", (Pokemon pokemon) =>

{
    pokemon.Id = pokemonList.Any()? pokemonList.Max(pokemon => pokemon.Id) + 1 : 1;
    pokemonList.Add(pokemon);
    
        return Results.Ok (new PokemonDTO
        {
            Id = pokemon.Id,
            Name = pokemon.Name,
            Price = pokemon.Price,
            Rarity = pokemon.Rarity

        });
        
   
});

app.MapDelete("/api/pokemon/{id}", (int id) =>
{
    var pokemon = pokemonList.FirstOrDefault(pokemon => pokemon.Id == id);

    if (pokemon != null)
    {
        pokemonList.Remove(pokemon);
        return Results.Ok();
    }
    else
    {
        return Results.NotFound();
    }
});

app.MapPut("/api/pokemon/{id}", (int id, Pokemon pokemon) => 
{
    Pokemon selectedPokemon = pokemonList.FirstOrDefault(p => p.Id == id);
    if (selectedPokemon != null)
    {
        selectedPokemon.Id = pokemon.Id;
        selectedPokemon.Name = pokemon.Name;
        selectedPokemon.Price = pokemon.Price;
        selectedPokemon.Rarity = pokemon.Rarity;
        return Results.Ok(new PokemonDTO
        {
            Id = selectedPokemon.Id,
            Name = selectedPokemon.Name,
            Price = selectedPokemon.Price,
            Rarity = selectedPokemon.Rarity
        });
    }else
    {
        return Results.BadRequest();
    }
});

app.Run();

