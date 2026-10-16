using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EduTechPlatform.App
{
    public class FrequenciaService
    {
        public double CalcularPorcetagemPresenca(int aulasTotais, int faltas)
        {
            return (aulasTotais - faltas) / (double)aulasTotais * 100;
        }
        public Boolean AprovadoPorFrequencia(int aulasTotais, int faltas)
        {
            if (aulasTotais <= 0 || faltas < 0 || faltas > aulasTotais)
            { return false; }
            double presenca = (double)(aulasTotais - faltas) / aulasTotais * 100;
            return presenca >= 75;
        }
    }
}