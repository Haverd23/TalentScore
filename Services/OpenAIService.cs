#pragma warning disable OPENAI001
using OpenAI.Responses;
using System.Text.Json;
using TalentScore.DTOs;
using TalentScore.Services.Interfaces;
namespace TalentScore.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly ResponsesClient _client;

        public OpenAIService(ResponsesClient client)
        {
            _client = client;
        }

        public async Task<ResumeAnalysisDTO?> AnalyzeFileAsync(byte[] bytes, string contentType, string fileName)
        {
            var contentPart = CreateContentPart(bytes, contentType, fileName);
            var options = CreateResponseOptions(contentPart);
            var response = await _client.CreateResponseAsync(options);
            return DeserializeResponse(
                response.Value.GetOutputText()
            );


        }

        private ResponseContentPart CreateContentPart(byte[] bytes, string contentType, string fileName)
        {
            if (contentType.StartsWith("image/"))
            {
                var imageData = new BinaryData(bytes,contentType
                );

                return ResponseContentPart.CreateInputImagePart(imageData);

            }
            var fileData = BinaryData.FromBytes(bytes);

            return ResponseContentPart.CreateInputFilePart(fileData,contentType,fileName
            );

        }
        private CreateResponseOptions CreateResponseOptions(
            ResponseContentPart contentPart)
        {
            var textPart =
                ResponseContentPart.CreateInputTextPart(
                    GetCurriculumPrompt()
                );

            var userMessage =
                ResponseItem.CreateUserMessageItem(
                    new[]
                    {
                        textPart,
                        contentPart
                    }
                );

            var options = new CreateResponseOptions
            {
                Model = "gpt-5.2",

                Instructions =
                    """
                    Você é um assistente especializado em análise de currículos.

                    Sua função é analisar currículos e extrair informações
                    estruturadas de maneira precisa.

                    Nunca invente informações que não estejam presentes
                    no documento.
                    """
            };

            options.InputItems.Add(userMessage);

            return options;
        }
        private ResumeAnalysisDTO? DeserializeResponse(
          string json)
        {
            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<ResumeAnalysisDTO>(
                json,
                options
            );
        }

        private string GetCurriculumPrompt()
        {
            return """
                Analise cuidadosamente o currículo enviado.

                Extraia as seguintes informações:

                - Nome completo
                - Email
                - Telefone
                - Quantidade de experiências profissionais
                - Quantidade de formações acadêmicas
                - Quantidade de habilidades, competências, certificações
                  e proficiências linguísticas

                Regras:

                - ExperiencesCount deve ser um número inteiro.
                - EducationsCount deve ser um número inteiro.
                - SkillsCount deve ser um número inteiro.

                - Conte cada experiência profissional separadamente.
                - Conte cada formação acadêmica separadamente.
                - Conte cada habilidade, competência, certificação ou
                  proficiência linguística separadamente.

                - Apenas contabilize itens explicitamente presentes no currículo.

                - Caso não encontre nome, email ou telefone, retorne null.

                - Caso não existam experiências, formações ou habilidades,
                  retorne 0.

                - Não invente informações.
                - Utilize apenas informações realmente presentes no currículo.

                Retorne SOMENTE um JSON válido exatamente neste formato:

                {
                  "name": "nome da pessoa",
                  "email": "email da pessoa",
                  "phone": "telefone da pessoa",
                  "experiencesCount": 0,
                  "educationsCount": 0,
                  "skillsCount": 0
                }

                Não utilize Markdown.
                Não escreva ```json.
                Não escreva explicações antes ou depois do JSON.
                """;
        }
    }
}
