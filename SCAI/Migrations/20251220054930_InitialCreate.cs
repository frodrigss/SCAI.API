using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace SCAI.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    MinimalRoleLevel = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Role = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Items",
                columns: new[] { "Id", "Description", "MinimalRoleLevel", "Name", "Quantity" },
                values: new object[,]
                {
                    { 1, "Rifle blaster padrão para Stormtroopers", 3, "Rifle blaster E-11", 500 },
                    { 2, "Capacete branco padrão com comlink integrado", 3, "Capacete de stormtrooper", 300 },
                    { 3, "Armadura composta de plastoide branco", 3, "Armadura de stormtrooper", 300 },
                    { 4, "Dispositivo explosivo térmico Classe-A", 3, "Detonador térmico", 150 },
                    { 5, "Pistola blaster compacta para combate a curta distância", 3, "Blaster de repetição leve SE-14r", 200 },
                    { 6, "Equipamento utilitário para escalar superfícies", 3, "Gancho de escalada", 100 },
                    { 7, "Dispositivo de visão aprimorada com telêmetro", 3, "Eletrobinóculos", 80 },
                    { 8, "Blaster pesado para suporte de fogo sustentado", 2, "Rifle blaster pesado DLT-19", 50 },
                    { 9, "Ombreira de insígnia de patente para comandantes", 2, "Ombreira de comando de oficial", 25 },
                    { 10, "Dispositivo de autorização de segurança para áreas restritas", 2, "Cilindro de código Imperial", 30 },
                    { 11, "Dispositivo de comunicação holográfica portátil", 2, "Holoprojetor", 20 },
                    { 12, "Blaster de repetição poderoso para assalto pesado", 2, "Blaster de repetição leve T-21", 40 },
                    { 13, "Dispositivo de armazenamento de dados criptografados e comunicação", 2, "Datapad Imperial", 35 },
                    { 14, "Moto speeder 74-Z para reconhecimento", 2, "Moto speeder de scout trooper", 15 },
                    { 15, "Cristal kyber sintético para sabres de luz Sith", 1, "Cristal kyber (vermelho)", 5 },
                    { 16, "Arma elegante para uma era mais...incivilizada", 1, "Sabre de luz Sith", 3 },
                    { 17, "Repositório antigo de conhecimento do lado sombrio", 1, "Holocron Sith", 2 },
                    { 18, "Câmara médica hiperbárica para o Lorde das Trevas", 1, "Câmara de meditação do Darth Vader", 1 },
                    { 19, "Amplificadores para canalizar energia do lado sombrio", 1, "Manoplas de relâmpago da Força", 2 },
                    { 20, "Armadura carmesim dos guardas de elite do Imperador", 1, "Armadura da guarda real do Imperador", 8 },
                    { 21, "Capa preta tradicional usada por Lordes Sith", 1, "Capa do lorde Sith", 4 },
                    { 22, "Granada de impacto não-letais para dispersão", 3, "Granada de concussão", 120 },
                    { 23, "Comunicador de curto alcance para patrulhas", 3, "Comlink de campo", 250 },
                    { 24, "Ferramentas e solventes para limpeza e ajuste", 3, "Kit de manutenção de blaster", 180 },
                    { 25, "Célula de energia sobressalente para armas e equipamentos", 3, "Pack de energia reserva", 400 },
                    { 26, "Arma de contenção com descarga elétrica", 3, "Bastão de choque", 90 },
                    { 27, "Suprimentos compactos para missões prolongadas", 3, "Ração de campo Imperial", 600 },
                    { 28, "Filtro substituível para ambientes com fumaça e poeira", 3, "Filtro de respiração", 220 },
                    { 29, "Iluminação portátil com modos estroboscópicos", 3, "Lanterna tática", 160 },
                    { 30, "Detector portátil de proximidade e deslocamento", 3, "Sensor de movimento", 70 },
                    { 31, "Dispositivo de restrição com travas magnetizadas", 3, "Algemas magnéticas", 140 },
                    { 32, "Kit de primeiros socorros com selantes e estimulantes", 3, "Medkit de campo", 110 },
                    { 33, "Conjunto de placas para reparo rápido de danos", 3, "Placas de reposição de armadura", 75 },
                    { 34, "Cargas de baixa potência para exercícios", 3, "Munição de treinamento", 1000 },
                    { 35, "Óptica avançada com telemetria e marcação de alvos", 2, "Binóculos de comando", 18 },
                    { 36, "Uniforme de serviço para operações e inspeções", 2, "Uniforme de oficial Imperial", 22 },
                    { 37, "Cartografia tática com rotas e pontos de interesse", 2, "Mapa estelar atualizado", 12 },
                    { 38, "Módulos para cifrar transmissões em campo", 2, "Kit de criptografia de comunicações", 14 },
                    { 39, "Unidade aérea compacta para varredura e vigilância", 2, "Drone de reconhecimento", 10 },
                    { 40, "Acessório de mira para tiros de precisão a longa distância", 2, "Mira macrobinocular", 16 },
                    { 41, "Credencial para áreas de segurança intermediária", 2, "Cartão de acesso nível-2", 50 },
                    { 42, "Terminal compacto para coordenação de unidades", 2, "Console portátil de estratégia", 9 },
                    { 43, "Transponder para acompanhamento de ativos e patrulhas", 2, "Módulo de rastreamento", 20 },
                    { 44, "Arma lateral padronizada para oficiais", 2, "Pistola blaster RK-3", 60 },
                    { 45, "Insígnia oficial para validação de ordens", 2, "Selo de autorização Imperial", 30 },
                    { 46, "Armazenamento endurecido para documentos sigilosos", 2, "Cofre de dados portátil", 11 },
                    { 47, "Ferramentas de contenção e coleta de informações", 2, "Kit de interrogatório", 6 },
                    { 48, "Máscara ritual com filtros para ambientes hostis", 1, "Máscara de respiração Sith", 2 },
                    { 49, "Anel com sigilo antigo usado em cerimônias", 1, "Anel sigiloso Sith", 3 },
                    { 50, "Compêndio de rituais e fórmulas do lado sombrio", 1, "Tomo de alquimia Sith", 1 },
                    { 51, "Suporte de recuperação com parâmetros ajustados", 1, "Cápsula de bacta personalizada", 1 },
                    { 52, "Cristal raro para experimentos de canalização", 1, "Cristal kyber (negro)", 1 },
                    { 53, "Dispositivo para amplificar efeitos de tortura", 1, "Emissor de campo de dor", 1 },
                    { 54, "Relíquia com gravuras protetoras e maldições", 1, "Amuleto de proteção sombria", 2 },
                    { 55, "Arma vibro com foco ritual para combate", 1, "Lâmina vibro encantada", 2 },
                    { 56, "Artefato resgatado de santuários ocultos", 1, "Relíquia de Exegol", 1 },
                    { 57, "Unidade de observação adaptada a ordens Sith", 1, "Droid sonda corrompido", 1 },
                    { 58, "Objeto de concentração para rituais e treinamento", 1, "Foco de meditação sombria", 2 },
                    { 59, "Vestuário cerimonial para conclaves do lado sombrio", 1, "Capa cerimonial Sith", 1 },
                    { 60, "Dispositivo de abertura e proteção de holocrons", 1, "Chave holocrônica", 1 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
