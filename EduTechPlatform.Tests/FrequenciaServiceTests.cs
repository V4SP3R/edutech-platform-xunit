using EduTechPlatform.App;

namespace EduTechPlatform.Tests;

public class FrequenciaServiceTests
{
    [Theory]
    [InlineData(80, 20, 75.0)]
    [InlineData(100, 10, 90.0)]
    public void TestarPorcentagem(int aulas, int faltas, double esperado)
    {
        var service = new FrequenciaService();

        var resultado = service.CalcularPorcetagemPresenca(aulas, faltas);

        Assert.Equal(esperado, resultado);
    }

    [Theory]
    [InlineData(80, 20, true)]
    [InlineData(80, 21, false)]
    public void TestarAprovacao(int aulas, int faltas, bool esperado)
    {
        var service = new FrequenciaService();

        var resultado = service.AprovadoPorFrequencia(aulas, faltas);

        Assert.Equal(esperado, resultado);
    }
}