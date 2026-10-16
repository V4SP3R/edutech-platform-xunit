using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTechPlatform.App
{
    public class NotaDesempenhoService
    {
        private decimal CalcularMediaPonderada(decimal nota1, decimal nota2, decimal notaProjeto)
        {
            decimal mediaPonderada = ((nota1 * 3) + (nota2 * 3) + (notaProjeto * 4)) / 10;
            return mediaPonderada;
        }
        private string ObterSituacaoAcademica(decimal mediaFinal)
        {
            if (mediaFinal >= 7.0m)
            { return "APROVADO"; }
            else if (mediaFinal < 5.0m)
            { return "REPROVADO"; }
            else 
            { return "RECUPERACAO"; }
        }
    }
}