using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using PuzzleCircuit.DAL;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext> {
    public AppDbContext CreateDbContext(string[] args) {
        DbContextOptionsBuilder<AppDbContext> optionsBuilder = new();

        optionsBuilder.UseSqlServer(
            "Server=LIBTRON;Database=PuzzleCircuit;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true");

        return new AppDbContext(optionsBuilder.Options);
    }
}
