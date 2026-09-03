# TalentScore

TalentScore é uma API desenvolvida em ASP.NET Core para automatizar a análise de currículos utilizando Inteligência Artificial.

A aplicação recebe um currículo, realiza a leitura e a extração das informações por meio da OpenAI e calcula uma pontuação (**Score**) para o candidato.

Quando o candidato atinge a pontuação mínima, apenas suas informações extraídas são armazenadas no banco de dados.

> O arquivo original do currículo não é armazenado. Ele é utilizado somente durante o processo de análise.

## Funcionalidades

- Recebimento de currículos
- Validação do arquivo
- Integração com a OpenAI
- Extração dos dados do candidato
- Cálculo do Score
- Validação da pontuação mínima
- Persistência das informações do candidato

## Informações extraídas

- Nome
- E-mail
- Telefone
- Experiências profissionais
- Formações
- Habilidades e competências

## Fluxo da aplicação

```text
Currículo
    ↓
Validação do arquivo
    ↓
Análise pela OpenAI
    ↓
Extração das informações
    ↓
ResumeAnalysisDTO
    ↓
Cálculo do Score
    ↓
Validação da pontuação
    ↓
Persistência dos dados do candidato
```

## Tecnologias

- C#
- ASP.NET Core Web API
- OpenAI API
- Entity Framework Core
- SQL Server

## Configuração da OpenAI

### Criando uma API key

1. Acesse [OpenAI API Keys](https://platform.openai.com/api-keys).
2. Faça login na sua conta.
3. Clique em **Create new secret key**.
4. Informe um nome para identificar a chave.
5. Copie e guarde a chave criada.

### Configurando no Windows

Abra o PowerShell e execute:

```powershell
setx OPENAI_API_KEY "sua-chave-aqui"
```

Depois, feche e abra novamente o Visual Studio ou o terminal.

A aplicação obtém a chave desta forma:

```csharp
var apiKey = Environment.GetEnvironmentVariable(
    "OPENAI_API_KEY"
);
```

## Configuração do banco de dados

Configure a conexão com o SQL Server no arquivo `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=TalentScore;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Crie a migration:

```bash
dotnet ef migrations add InitialCreate
```

Atualize o banco de dados:

```bash
dotnet ef database update
```

## Executando o projeto

Restaure as dependências:

```bash
dotnet restore
```

Execute a aplicação:

```bash
dotnet run
```

A aplicação será iniciada em:

```text
http://localhost:5070
```

## Endpoint

```http
POST http://localhost:5070/TalentScore
```

O endpoint recebe o currículo, extrai as informações do candidato, calcula o Score e verifica se os dados devem ser armazenados.

A API não retorna as informações extraídas no corpo da resposta.

## Testando com o Postman

1. Abra o Postman.
2. Crie uma nova requisição.
3. Selecione o método `POST`.
4. Informe o endereço:

```text
http://localhost:5070/TalentScore
```

5. Abra a seção **Body**.
6. Selecione a opção **form-data**.
7. Adicione um campo com o nome:

```text
File
```

8. Altere o tipo do campo de **Text** para **File**.
9. Selecione o arquivo do currículo.
10. Clique em **Send**.

O Postman configurará automaticamente a requisição como `multipart/form-data`.

Após o envio, a aplicação:

1. Valida o arquivo.
2. Envia o currículo para a OpenAI.
3. Extrai as informações do candidato.
4. Calcula o Score.
5. Verifica a pontuação mínima.
6. Armazena as informações no banco de dados caso a pontuação mínima seja atingida.
