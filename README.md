# Solução de Auditoria Automática de Dados Pessoais (LGPD) - Desafio Participa DF

![.NET](https://img.shields.io/badge/.NET-9.0-512BD4?style=flat&logo=dotnet)
![Docker](https://img.shields.io/badge/Docker-Enabled-2496ED?style=flat&logo=docker)
![Status](https://img.shields.io/badge/Status-Concluído-brightgreen)

## 📄 Resumo do Projeto

**Categoria:** Desafio 1 - Acesso à Informação

Esta solução consiste em uma API RESTful de alta performance construída em **.NET 9** e containerizada com **Docker**, projetada para auditar arquivos de pedidos públicos (`.xlsx`) e identificar automaticamente dados pessoais sensíveis, garantindo a conformidade com a LGPD.

### Funcionalidades

O sistema analisa o conteúdo textual linha a linha e detecta cinco vetores críticos de privacidade:

- **CPF**
- **RG**
- **Telefone**
- **E-mail**
- **Nomes de Pessoas** (via Análise Heurística/NER)

A API retorna um relatório estruturado (JSON) indicando a presença de dados, os tipos identificados e o grau de confiança da análise.

### Diferenciais e Inovação

1. **Arquitetura Híbrida Inteligente:** Unimos a precisão matemática de Expressões Regulares (Regex) para dados estruturados com um motor proprietário de "IA Simbólica" (NER Baseado em Regras e Heurística) para detecção de nomes. Isso garante alta Sensibilidade (*Recall*) sem os custos e lentidão de IAs generativas.

2. **Privacidade por Design:** Todo o processamento é **100% local e offline**. Nenhum dado é enviado para APIs externas, garantindo segurança total da informação.

3. **Eficiência Extrema:** A arquitetura foi otimizada para tarefas *CPU-Bound*, utilizando processamento síncrono para eliminar overhead de threads, resultando em uma aplicação leve, rápida e de fácil implantação.

---

## 🛠️ Stack Tecnológica

O projeto foi desenvolvido utilizando as tecnologias mais modernas do ecossistema Microsoft e práticas de DevOps:

- **Linguagem:** C# (.NET 9)
- **Framework Web:** ASP.NET Core Web API
- **Containerização:** Docker & Docker Compose
- **Processamento de Excel:** EPPlus (v7+)
- **Documentação:** Swagger (Swashbuckle)
- **Arquitetura:** Clean Architecture (Services, Controllers, Models, Patterns)

---

## 🤖 Documentação do Uso de IA (Item 13.9 do Edital)

Em conformidade com o edital, declaramos a estratégia de Inteligência Artificial utilizada:

- **Modelo:** IA Simbólica / Sistema Especialista Baseado em Regras (Rule-Based System).
- **Implementação:** Motor proprietário desenvolvido em C# (`DetectorNomeUtils.cs`).
- **Funcionamento:** O sistema utiliza Reconhecimento de Entidades Nomeadas (NER) através de heurísticas gramaticais, análise de contexto (palavras-gatilho) e dicionários de exclusão (*stop-words* administrativas) para identificar nomes de pessoas em textos não estruturados.
- **Justificativa:** Esta abordagem foi escolhida em detrimento de LLMs (como GPT) para garantir:
    1. Execução em ambiente restrito sem GPU.
    2. Auditabilidade total do código (Caixa Branca).
    3. Baixo consumo energético (*Green IT*).

---

## 🚀 Como Executar o Projeto

A aplicação foi totalmente containerizada. **Você não precisa ter o .NET instalado**, apenas o Docker.

### Pré-requisitos

- [Docker](https://www.docker.com/products/docker-desktop) instalado e rodando.

### Passo a Passo

**1. Clone o repositório**

Escolha uma das opções abaixo para clonar:

**Opção 1: HTTPS**
```bash
git clone https://github.com/fabriciosuhet/ParticipaDFHackaton.git
```

**Opção 2: SSH**
```bash
git clone git@github.com:fabriciosuhet/ParticipaDFHackaton.git
```

**2. Acesse a pasta do projeto**
```bash
cd SolucaoParticipaDF
```

**2. Execute via Docker Compose:**

Certifique-se de estar na raiz do projeto (mesma pasta do arquivo `docker-compose.yml`) e execute no terminal:

```bash
docker-compose up --build
```

**3. Aguarde a Inicialização:**

O Docker irá baixar as imagens e compilar o projeto. Aguarde até ver a seguinte mensagem no terminal:

```
Now listening on: http://[::]:8080
Application started. Press Ctrl+C to shut down.
```

**4. Acesse a Aplicação:**

Como o container roda isolado, o navegador não abrirá automaticamente. Você deve clicar no link abaixo ou copiá-lo para o seu navegador:

👉 **http://localhost:8080/swagger**

---

## 🧪 Como Testar (Via Swagger)

1. No Swagger, localize e clique na rota **`POST /api/Deteccao/xlsx`**.
2. Clique no botão **`Try it out`** (canto superior direito da rota).
3. No campo **`arquivo`**, faça o upload de um arquivo `.xlsx` de teste (utilize o arquivo `AMOSTRA_e-SIC.xlsx` fornecido no pacote do desafio).
4. Clique no botão azul **`Execute`**.
5. O resultado será exibido abaixo em formato JSON.

### Exemplo de Retorno JSON:

```json
[
  {
    "contemDadosPessoais": true,
    "tiposDadosIdentificados": [
      "CPF",
      "Nome"
    ],
    "motivo": "Múltiplos dados identificados (Regex + Padrão).",
    "confianca": 1
  },
  {
    "contemDadosPessoais": false,
    "tiposDadosIdentificados": [],
    "motivo": "Nenhum dado pessoal identificado.",
    "confianca": 0
  }
]
```

---

## 📂 Estrutura do Projeto

```
SolucaoParticipaDF
├── docker-compose.yml          # Orquestração dos containers (Ponto de entrada)
├── SolucaoParticipaDF.sln      # Solução .NET
└── SolucaoParticipaDF.API      # Projeto da API
    ├── Controllers             # Endpoints (DeteccaoController)
    ├── Services                # Lógica de Negócio (Regex, IA, Leitura Excel)
    ├── Models                  # Objetos de Transferência (DTOs)
    ├── Patterns                # Definições de Regex
    └── Dockerfile              # Configuração da Imagem Docker
```

---

## 📋 Detalhamento Técnico

### Arquitetura da Solução

A aplicação segue os princípios de **Clean Architecture**, separando responsabilidades em camadas distintas:

- **Controllers:** Camada de apresentação, responsável por receber requisições HTTP e retornar respostas.
- **Services:** Camada de lógica de negócio, onde residem os algoritmos de detecção.
- **Models:** Objetos de transferência de dados (DTOs) que estruturam as respostas da API.
- **Patterns:** Definições de padrões Regex utilizados para identificação de dados estruturados.

### Algoritmos de Detecção

#### 1. Dados Estruturados (Regex)

Para CPF, RG, Telefone e E-mail, utilizamos expressões regulares otimizadas:

- **CPF:** Valida formatação (XXX.XXX.XXX-XX) e dígitos verificadores.
- **RG:** Detecta padrões comuns de documentos de identidade brasileiros.
- **Telefone:** Reconhece formatos (XX) XXXXX-XXXX e variações.
- **E-mail:** Valida estrutura RFC 5322 simplificada.

#### 2. Nomes de Pessoas (IA Simbólica)

O motor de NER implementado em `DetectorNomeUtils.cs` utiliza:

- **Análise de Capitalização:** Identifica palavras capitalizadas que podem ser nomes próprios.
- **Palavras-Gatilho:** Contexto linguístico (ex: "Sr.", "Sra.", "Dr.") aumenta a confiança.
- **Stop-Words Administrativas:** Filtra termos comuns em documentos públicos que não são nomes (ex: "Secretaria", "Governo").
- **Validação de Estrutura:** Verifica se a sequência de palavras forma um nome válido (2-4 palavras capitalizadas).

### Performance e Otimizações

- **Processamento Síncrono:** Para operações CPU-bound, evitamos async/await para reduzir overhead de context switching.
- **Leitura Eficiente:** EPPlus carrega apenas células com conteúdo, economizando memória.
- **Docker Multi-Stage Build:** Reduz o tamanho da imagem final (runtime-only).

---

## 🔒 Conformidade com LGPD

A solução foi desenvolvida com foco em **Privacy by Design**, seguindo os princípios da Lei Geral de Proteção de Dados:

1. **Minimização de Dados:** Apenas o arquivo enviado é processado, sem armazenamento.
2. **Segurança:** Processamento 100% local, sem comunicação com serviços externos.
3. **Transparência:** Relatório detalhado indica exatamente quais dados foram identificados e com qual confiança.
4. **Auditabilidade:** Todo o código é open-source e auditável.

---

## 🎯 Casos de Uso

### Para Órgãos Públicos

- **Auditoria Pré-Publicação:** Verificar se arquivos de transparência contêm dados pessoais antes da publicação.
- **Conformidade:** Garantir que pedidos de acesso à informação não exponham dados sensíveis.
- **Treinamento:** Identificar padrões de erro e treinar servidores sobre boas práticas de anonimização.


## ⚖️ Licença

Este projeto foi desenvolvido exclusivamente para o **1º Hackathon em Controle Social do Distrito Federal** e está disponível sob a licença MIT.

---

## 🏆 Agradecimentos

Agradeço à organização do **Participa DF** pela oportunidade de contribuir com soluções tecnológicas para o fortalecimento da transparência pública e da proteção de dados pessoais no Distrito Federal.

---
