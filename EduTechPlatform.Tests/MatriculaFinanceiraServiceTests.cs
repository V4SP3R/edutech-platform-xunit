using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduTechPlatform.App;

namespace EduTechPlatform.Tests;

public class MatriculaFinanceiraServiceTests
{
    [Theory]
    [InlineData(1000.00, 10, true, 500.00)]
    [InlineData(1000.00, 4, false, 900.00)]
    [InlineData(1000.00, 10, false, 1000.00)]
    public void TestarMensalidade(
        double valorBase,
        int diaPagamento,
        bool bolsista,
        double esperado)
    {
        var service = new MatriculaFinanceiraService();

        var resultado = service.CalcularValorMensalidade(
            (decimal)valorBase,
            diaPagamento,
            bolsista
        );

        Assert.Equal((decimal)esperado, resultado);
    }

    [Theory]
    [InlineData(false, "APROVADO", true)]
    [InlineData(true, "APROVADO", false)]
    [InlineData(false, "REPROVADO", false)]
    public void TestarRematricula(
        bool possuiPendencia,
        string situacao,
        bool esperado)
    {
        var service = new MatriculaFinanceiraService();

        var resultado = service.PodeRealizarRematricula(
            possuiPendencia,
            situacao
        );

        Assert.Equal(esperado, resultado);
    }
}