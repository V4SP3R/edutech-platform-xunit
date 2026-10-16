using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EduTechPlatform.App;

namespace EduTechPlatform.Tests;

public class NotaDesempenhoServiceTests
{
    [Theory]
    [InlineData(7.0, 7.0, 7.0, 7.0)]
    [InlineData(10.0, 10.0, 5.0, 8.0)]
    public void TestarMedia(double n1, double n2, double projeto, double esperado)
    {
        var service = new NotaDesempenhoService();

        var resultado = service.CalcularMediaPonderada(
            (decimal)n1,
            (decimal)n2,
            (decimal)projeto
        );

        Assert.Equal((decimal)esperado, resultado);
    }

    [Theory]
    [InlineData(8.5, "APROVADO")]
    [InlineData(6.0, "RECUPERACAO")]
    [InlineData(4.2, "REPROVADO")]
    public void TestarSituacao(double media, string esperado)
    {
        var service = new NotaDesempenhoService();

        var resultado = service.ObterSituacaoAcademica((decimal)media);

        Assert.Equal(esperado, resultado);
    }
}