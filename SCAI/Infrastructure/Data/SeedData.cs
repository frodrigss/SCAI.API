using Microsoft.EntityFrameworkCore;
using SCAI.Models;

namespace SCAI.Infrastructure
{
    public static class SeedData
    {
        public static void SeedItems(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>().HasData(
                // * Itens de nível Trooper (MinimalRoleLevel = 3) - Todos podem acessar
                new Item { Id = 1, Name = "Rifle blaster E-11", Description = "Rifle blaster padrão para Stormtroopers", Quantity = 500, MinimalRoleLevel = 3 },
                new Item { Id = 2, Name = "Capacete de stormtrooper", Description = "Capacete branco padrão com comlink integrado", Quantity = 300, MinimalRoleLevel = 3 },
                new Item { Id = 3, Name = "Armadura de stormtrooper", Description = "Armadura composta de plastoide branco", Quantity = 300, MinimalRoleLevel = 3 },
                new Item { Id = 4, Name = "Detonador térmico", Description = "Dispositivo explosivo térmico Classe-A", Quantity = 150, MinimalRoleLevel = 3 },
                new Item { Id = 5, Name = "Blaster de repetição leve SE-14r", Description = "Pistola blaster compacta para combate a curta distância", Quantity = 200, MinimalRoleLevel = 3 },
                new Item { Id = 6, Name = "Gancho de escalada", Description = "Equipamento utilitário para escalar superfícies", Quantity = 100, MinimalRoleLevel = 3 },
                new Item { Id = 7, Name = "Eletrobinóculos", Description = "Dispositivo de visão aprimorada com telêmetro", Quantity = 80, MinimalRoleLevel = 3 },
                new Item { Id = 22, Name = "Granada de concussão", Description = "Granada de impacto não-letais para dispersão", Quantity = 120, MinimalRoleLevel = 3 },
                new Item { Id = 23, Name = "Comlink de campo", Description = "Comunicador de curto alcance para patrulhas", Quantity = 250, MinimalRoleLevel = 3 },
                new Item { Id = 24, Name = "Kit de manutenção de blaster", Description = "Ferramentas e solventes para limpeza e ajuste", Quantity = 180, MinimalRoleLevel = 3 },
                new Item { Id = 25, Name = "Pack de energia reserva", Description = "Célula de energia sobressalente para armas e equipamentos", Quantity = 400, MinimalRoleLevel = 3 },
                new Item { Id = 26, Name = "Bastão de choque", Description = "Arma de contenção com descarga elétrica", Quantity = 90, MinimalRoleLevel = 3 },
                new Item { Id = 27, Name = "Ração de campo Imperial", Description = "Suprimentos compactos para missões prolongadas", Quantity = 600, MinimalRoleLevel = 3 },
                new Item { Id = 28, Name = "Filtro de respiração", Description = "Filtro substituível para ambientes com fumaça e poeira", Quantity = 220, MinimalRoleLevel = 3 },
                new Item { Id = 29, Name = "Lanterna tática", Description = "Iluminação portátil com modos estroboscópicos", Quantity = 160, MinimalRoleLevel = 3 },
                new Item { Id = 30, Name = "Sensor de movimento", Description = "Detector portátil de proximidade e deslocamento", Quantity = 70, MinimalRoleLevel = 3 },
                new Item { Id = 31, Name = "Algemas magnéticas", Description = "Dispositivo de restrição com travas magnetizadas", Quantity = 140, MinimalRoleLevel = 3 },
                new Item { Id = 32, Name = "Medkit de campo", Description = "Kit de primeiros socorros com selantes e estimulantes", Quantity = 110, MinimalRoleLevel = 3 },
                new Item { Id = 33, Name = "Placas de reposição de armadura", Description = "Conjunto de placas para reparo rápido de danos", Quantity = 75, MinimalRoleLevel = 3 },
                new Item { Id = 34, Name = "Munição de treinamento", Description = "Cargas de baixa potência para exercícios", Quantity = 1000, MinimalRoleLevel = 3 },

                // * Itens de nível Commander (MinimalRoleLevel = 2) - Commander e Sith podem acessar
                new Item { Id = 8, Name = "Rifle blaster pesado DLT-19", Description = "Blaster pesado para suporte de fogo sustentado", Quantity = 50, MinimalRoleLevel = 2 },
                new Item { Id = 9, Name = "Ombreira de comando de oficial", Description = "Ombreira de insígnia de patente para comandantes", Quantity = 25, MinimalRoleLevel = 2 },
                new Item { Id = 10, Name = "Cilindro de código Imperial", Description = "Dispositivo de autorização de segurança para áreas restritas", Quantity = 30, MinimalRoleLevel = 2 },
                new Item { Id = 11, Name = "Holoprojetor", Description = "Dispositivo de comunicação holográfica portátil", Quantity = 20, MinimalRoleLevel = 2 },
                new Item { Id = 12, Name = "Blaster de repetição leve T-21", Description = "Blaster de repetição poderoso para assalto pesado", Quantity = 40, MinimalRoleLevel = 2 },
                new Item { Id = 13, Name = "Datapad Imperial", Description = "Dispositivo de armazenamento de dados criptografados e comunicação", Quantity = 35, MinimalRoleLevel = 2 },
                new Item { Id = 14, Name = "Moto speeder de scout trooper", Description = "Moto speeder 74-Z para reconhecimento", Quantity = 15, MinimalRoleLevel = 2 },
                new Item { Id = 35, Name = "Binóculos de comando", Description = "Óptica avançada com telemetria e marcação de alvos", Quantity = 18, MinimalRoleLevel = 2 },
                new Item { Id = 36, Name = "Uniforme de oficial Imperial", Description = "Uniforme de serviço para operações e inspeções", Quantity = 22, MinimalRoleLevel = 2 },
                new Item { Id = 37, Name = "Mapa estelar atualizado", Description = "Cartografia tática com rotas e pontos de interesse", Quantity = 12, MinimalRoleLevel = 2 },
                new Item { Id = 38, Name = "Kit de criptografia de comunicações", Description = "Módulos para cifrar transmissões em campo", Quantity = 14, MinimalRoleLevel = 2 },
                new Item { Id = 39, Name = "Drone de reconhecimento", Description = "Unidade aérea compacta para varredura e vigilância", Quantity = 10, MinimalRoleLevel = 2 },
                new Item { Id = 40, Name = "Mira macrobinocular", Description = "Acessório de mira para tiros de precisão a longa distância", Quantity = 16, MinimalRoleLevel = 2 },
                new Item { Id = 41, Name = "Cartão de acesso nível-2", Description = "Credencial para áreas de segurança intermediária", Quantity = 50, MinimalRoleLevel = 2 },
                new Item { Id = 42, Name = "Console portátil de estratégia", Description = "Terminal compacto para coordenação de unidades", Quantity = 9, MinimalRoleLevel = 2 },
                new Item { Id = 43, Name = "Módulo de rastreamento", Description = "Transponder para acompanhamento de ativos e patrulhas", Quantity = 20, MinimalRoleLevel = 2 },
                new Item { Id = 44, Name = "Pistola blaster RK-3", Description = "Arma lateral padronizada para oficiais", Quantity = 60, MinimalRoleLevel = 2 },
                new Item { Id = 45, Name = "Selo de autorização Imperial", Description = "Insígnia oficial para validação de ordens", Quantity = 30, MinimalRoleLevel = 2 },
                new Item { Id = 46, Name = "Cofre de dados portátil", Description = "Armazenamento endurecido para documentos sigilosos", Quantity = 11, MinimalRoleLevel = 2 },
                new Item { Id = 47, Name = "Kit de interrogatório", Description = "Ferramentas de contenção e coleta de informações", Quantity = 6, MinimalRoleLevel = 2 },

                // * Itens de nível Sith (MinimalRoleLevel = 1) - Apenas Sith podem acessar
                new Item { Id = 15, Name = "Cristal kyber (vermelho)", Description = "Cristal kyber sintético para sabres de luz Sith", Quantity = 5, MinimalRoleLevel = 1 },
                new Item { Id = 16, Name = "Sabre de luz Sith", Description = "Arma elegante para uma era mais...incivilizada", Quantity = 3, MinimalRoleLevel = 1 },
                new Item { Id = 17, Name = "Holocron Sith", Description = "Repositório antigo de conhecimento do lado sombrio", Quantity = 2, MinimalRoleLevel = 1 },
                new Item { Id = 18, Name = "Câmara de meditação do Darth Vader", Description = "Câmara médica hiperbárica para o Lorde das Trevas", Quantity = 1, MinimalRoleLevel = 1 },
                new Item { Id = 19, Name = "Manoplas de relâmpago da Força", Description = "Amplificadores para canalizar energia do lado sombrio", Quantity = 2, MinimalRoleLevel = 1 },
                new Item { Id = 20, Name = "Armadura da guarda real do Imperador", Description = "Armadura carmesim dos guardas de elite do Imperador", Quantity = 8, MinimalRoleLevel = 1 },
                new Item { Id = 21, Name = "Capa do lorde Sith", Description = "Capa preta tradicional usada por Lordes Sith", Quantity = 4, MinimalRoleLevel = 1 },
                new Item { Id = 48, Name = "Máscara de respiração Sith", Description = "Máscara ritual com filtros para ambientes hostis", Quantity = 2, MinimalRoleLevel = 1 },
                new Item { Id = 49, Name = "Anel sigiloso Sith", Description = "Anel com sigilo antigo usado em cerimônias", Quantity = 3, MinimalRoleLevel = 1 },
                new Item { Id = 50, Name = "Tomo de alquimia Sith", Description = "Compêndio de rituais e fórmulas do lado sombrio", Quantity = 1, MinimalRoleLevel = 1 },
                new Item { Id = 51, Name = "Cápsula de bacta personalizada", Description = "Suporte de recuperação com parâmetros ajustados", Quantity = 1, MinimalRoleLevel = 1 },
                new Item { Id = 52, Name = "Cristal kyber (negro)", Description = "Cristal raro para experimentos de canalização", Quantity = 1, MinimalRoleLevel = 1 },
                new Item { Id = 53, Name = "Emissor de campo de dor", Description = "Dispositivo para amplificar efeitos de tortura", Quantity = 1, MinimalRoleLevel = 1 },
                new Item { Id = 54, Name = "Amuleto de proteção sombria", Description = "Relíquia com gravuras protetoras e maldições", Quantity = 2, MinimalRoleLevel = 1 },
                new Item { Id = 55, Name = "Lâmina vibro encantada", Description = "Arma vibro com foco ritual para combate", Quantity = 2, MinimalRoleLevel = 1 },
                new Item { Id = 56, Name = "Relíquia de Exegol", Description = "Artefato resgatado de santuários ocultos", Quantity = 1, MinimalRoleLevel = 1 },
                new Item { Id = 57, Name = "Droid sonda corrompido", Description = "Unidade de observação adaptada a ordens Sith", Quantity = 1, MinimalRoleLevel = 1 },
                new Item { Id = 58, Name = "Foco de meditação sombria", Description = "Objeto de concentração para rituais e treinamento", Quantity = 2, MinimalRoleLevel = 1 },
                new Item { Id = 59, Name = "Capa cerimonial Sith", Description = "Vestuário cerimonial para conclaves do lado sombrio", Quantity = 1, MinimalRoleLevel = 1 },
                new Item { Id = 60, Name = "Chave holocrônica", Description = "Dispositivo de abertura e proteção de holocrons", Quantity = 1, MinimalRoleLevel = 1 }
            );
        }
    }
}
