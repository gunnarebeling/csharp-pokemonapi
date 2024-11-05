namespace PokemonAPI.Modules.DTOs;
public class PokemonDetailsDTO
    {
        public int Id {get; set;}

        public string Name { get; set; }
        
        public decimal Price { get; set; }

        public int Rarity { get; set; }

        public  int? Condition {get; set;}

    }