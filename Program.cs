using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<SongDb>(options => 
    options.UseSqlite("Data Source = SongDb.db")
);

var app = builder.Build();

app.MapGet("/" , ()  => "Välkommen till musik API!");

app.Run();


//klass för låt, med id, namn, artist, längd, genre.
public class Song {
    public int Id  {get; set; }
    public string? Titel  {get; set; }
    public string? Artist  {get; set; }
    public int Length  {get; set; }
    public string? Category  {get; set; }
}


//DbContext
public class SongDb : DbContext {
    public SongDb(DbContextOptions<SongDb> options)
    : base(options) { }

    public DbSet<Song> Songs => Set<Song>();
}