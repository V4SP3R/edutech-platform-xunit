# EduTechPlatform — Testes unitários com xUnit

Projeto acadêmico em **C# e .NET 10**, desenvolvido para aplicar testes unitários às regras de negócio de uma plataforma educacional com **xUnit**.

Atividade de **Garantia da Qualidade de Software / Gestão e Qualidade de Software**, sob orientação do professor **Daniel Henrique Matos de Paiva**.

## Visão geral

A EduTechPlatform reúne serviços para calcular médias, determinar a situação acadêmica, verificar aprovação por frequência, calcular mensalidades e avaliar a possibilidade de rematrícula. O foco desta implementação é a validação automatizada dessas regras.

O projeto de aplicação é do tipo console. Atualmente, seu ponto de entrada exibe apenas `Hello, World!`; os serviços são exercitados pelos testes unitários.

## Requisitos e tecnologias

- **.NET 10 SDK**, necessário para os projetos que utilizam `net10.0`.
- **Git**, para clonar e versionar o repositório.
- **C#**, linguagem de implementação.
- **xUnit 2.9.3**, framework de testes.
- **Microsoft.NET.Test.Sdk** e **xunit.runner.visualstudio**, para descoberta e execução dos testes.
- **coverlet.collector**, para coleta opcional de cobertura de código.

Verifique a instalação do SDK:

```bash
dotnet --version
```

A versão deve ser `10.0.x`. A restauração inicial requer acesso ao NuGet para baixar as dependências. Um editor ou IDE com suporte ao .NET 10 é opcional.

## Organização da solução

| Caminho | Responsabilidade |
| --- | --- |
| `EduTechPlatform.slnx` | Solução que reúne aplicação e testes. |
| `EduTechPlatform.App/` | Aplicação console e classes de serviço. |
| `EduTechPlatform.Tests/` | Testes unitários dos serviços. |
| `.gitignore` | Exclusão de arquivos locais e artefatos de compilação do versionamento. |
| `LICENSE` | Licença MIT. |

## Classes de serviço

### NotaDesempenhoService

Responsável pelo desempenho acadêmico:

- `CalcularMediaPonderada(nota1, nota2, notaProjeto)`: calcula `(nota1 × 3 + nota2 × 3 + notaProjeto × 4) / 10`.
- `ObterSituacaoAcademica(mediaFinal)`: retorna `APROVADO` para média maior ou igual a 7; `RECUPERACAO` para média maior ou igual a 5 e menor que 7; e `REPROVADO` para média menor que 5.

### FrequenciaService

Responsável pelo cálculo de presença e pela aprovação por frequência:

- `CalcularPorcetagemPresenca(aulasTotais, faltas)`: calcula `(aulasTotais − faltas) / aulasTotais × 100`. O nome reproduz a grafia existente no código.
- `AprovadoPorFrequencia(aulasTotais, faltas)`: exige presença mínima de **75%**. Retorna `false` quando o total de aulas é menor ou igual a zero, as faltas são negativas ou superam o total de aulas.

A validação de entradas inválidas está no método de aprovação; o método de cálculo de porcentagem não realiza essa validação.

### MatriculaFinanceiraService

Responsável pelas regras financeiras de matrícula:

- `CalcularValorMensalidade(valorBase, diaPagamento, eBolsista)`: aplica desconto de **50% para bolsistas**. Para não bolsistas, aplica **10% de desconto até o dia 5**; após esse dia, mantém o valor integral. Os descontos não são cumulativos, pois a bolsa tem prioridade.
- `PodeRealizarRematricula(possuiPendenciaFinanceira, situacaoAcademica)`: permite a rematrícula quando não há pendência financeira e a situação acadêmica é diferente de `REPROVADO`.

## Como executar

Clone o repositório e entre na pasta:

```bash
git clone https://github.com/V4SP3R/edutech-platform-xunit.git
cd edutech-platform-xunit
```

Execute os próximos comandos na raiz do repositório, na ordem indicada.

### 1. Restaurar as dependências

```bash
dotnet restore EduTechPlatform.slnx
```

### 2. Compilar a solução

```bash
dotnet build EduTechPlatform.slnx --no-restore
```

### 3. Executar os testes

```bash
dotnet test EduTechPlatform.slnx --no-build --verbosity normal
```

O resultado dos testes será apresentado no terminal. A opção `--no-build` pressupõe que a compilação anterior foi concluída com sucesso.

Também é possível testar diretamente o projeto, com restauração e compilação automáticas:

```bash
dotnet test EduTechPlatform.Tests/EduTechPlatform.Tests.csproj
```

Para iniciar a aplicação console:

```bash
dotnet run --project EduTechPlatform.App/EduTechPlatform.App.csproj
```

## Testes parametrizados e cenários cobertos

O atributo **`[Theory]`** identifica um teste parametrizado. Cada **`[InlineData]`** fornece um conjunto de entradas e o resultado esperado, permitindo executar a mesma lógica para diferentes cenários. A comparação entre o resultado obtido e o esperado é realizada com **`Assert.Equal`**.

Exemplo presente em `FrequenciaServiceTests`:

```csharp
[Theory]
[InlineData(80, 20, true)]
[InlineData(80, 21, false)]
public void TestarAprovacao(int aulas, int faltas, bool esperado)
{
    var service = new FrequenciaService();

    var resultado = service.AprovadoPorFrequencia(aulas, faltas);

    Assert.Equal(esperado, resultado);
}
```

Com 20 faltas em 80 aulas, a presença é de 75%, permitindo aprovação. Com 21 faltas, a presença fica abaixo do mínimo, e o resultado esperado é `false`.

A suíte atual possui **6 métodos parametrizados e 15 cenários**:

| Classe | Cenários verificados | Quantidade |
| --- | --- | --- |
| `NotaDesempenhoServiceTests` | Duas combinações de média ponderada e classificação em aprovado, recuperação ou reprovado. | 5 |
| `FrequenciaServiceTests` | Dois cálculos de presença e aprovação no limite de 75% ou reprovação abaixo desse limite. | 4 |
| `MatriculaFinanceiraServiceTests` | Bolsa, desconto por pagamento antecipado, mensalidade integral e três condições de rematrícula. | 6 |

Essa relação descreve os cenários presentes no código; não representa uma medição percentual de cobertura. Ainda não há testes específicos para entradas inválidas de frequência, médias exatamente iguais a 5 e 7, pagamento exatamente no dia 5 ou prioridade da bolsa em pagamento antecipado.

### Coleta opcional de cobertura

O projeto já inclui `coverlet.collector`. Para coletar a cobertura durante os testes:

```bash
dotnet test EduTechPlatform.Tests/EduTechPlatform.Tests.csproj --collect:"XPlat Code Coverage"
```

O arquivo `coverage.cobertura.xml` será gerado em uma subpasta de `EduTechPlatform.Tests/TestResults/`.

## Versionamento e contribuidores

Repositório público: [V4SP3R/edutech-platform-xunit](https://github.com/V4SP3R/edutech-platform-xunit).

Contribuidores identificados no repositório:

- [Geovane Santos — V4SP3R](https://github.com/V4SP3R)
- [Leo Cavalcante — cavalcante-leo](https://github.com/cavalcante-leo)
- [KdAndrade](https://github.com/KdAndrade)

O histórico pode ser consultado na página de [Contributors](https://github.com/V4SP3R/edutech-platform-xunit/graphs/contributors). A atribuição das contribuições no GitHub depende dos commits associados às contas dos participantes.

## Licença

Este projeto utiliza a **Licença MIT**. Consulte [LICENSE](LICENSE) para conhecer os termos.
