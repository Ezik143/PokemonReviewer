using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PokemonReviewApp.Migrations
{
    /// <inheritdoc />
    public partial class RenamePokemonOwnersTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_pokemonOwners_Owners_OwnerId",
                table: "pokemonOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_pokemonOwners_Pokemons_PokemonId",
                table: "pokemonOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_pokemonOwners",
                table: "pokemonOwners");

            migrationBuilder.RenameTable(
                name: "pokemonOwners",
                newName: "PokemonOwners");

            migrationBuilder.RenameIndex(
                name: "IX_pokemonOwners_OwnerId",
                table: "PokemonOwners",
                newName: "IX_PokemonOwners_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PokemonOwners",
                table: "PokemonOwners",
                columns: new[] { "PokemonId", "OwnerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwners_Owners_OwnerId",
                table: "PokemonOwners",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PokemonOwners_Pokemons_PokemonId",
                table: "PokemonOwners",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwners_Owners_OwnerId",
                table: "PokemonOwners");

            migrationBuilder.DropForeignKey(
                name: "FK_PokemonOwners_Pokemons_PokemonId",
                table: "PokemonOwners");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PokemonOwners",
                table: "PokemonOwners");

            migrationBuilder.RenameTable(
                name: "PokemonOwners",
                newName: "pokemonOwners");

            migrationBuilder.RenameIndex(
                name: "IX_PokemonOwners_OwnerId",
                table: "pokemonOwners",
                newName: "IX_pokemonOwners_OwnerId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_pokemonOwners",
                table: "pokemonOwners",
                columns: new[] { "PokemonId", "OwnerId" });

            migrationBuilder.AddForeignKey(
                name: "FK_pokemonOwners_Owners_OwnerId",
                table: "pokemonOwners",
                column: "OwnerId",
                principalTable: "Owners",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_pokemonOwners_Pokemons_PokemonId",
                table: "pokemonOwners",
                column: "PokemonId",
                principalTable: "Pokemons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
