using System;
using DocumentosBrasileiros.Enums;
using DocumentosBrasileiros.Helpers;
using DocumentosBrasileiros.Interfaces;

namespace DocumentosBrasileiros.IE
{
    public class DistritoFederal : IInscricaoEstadual
    {
        public UfEnum UfEnum => UfEnum.DF;

        private readonly int[] peso = { 5, 4, 3, 2, 9, 8, 7, 6, 5, 4, 3, 2 };
        public bool Validar(string inscricaoEstadual)
        {
            if (inscricaoEstadual.Length != 13) return false;
            //if (!inscricaoEstadual.StartsWith("07")) return false;

            string inscricaoSemDigito = inscricaoEstadual.Substring(0, 11);
            string digito1 = DigitoVerificador.ObterDigitoMod11("0" + inscricaoSemDigito, peso).ToString();
            string digito2 = DigitoVerificador.ObterDigitoMod11(inscricaoSemDigito + digito1, peso).ToString();

            return inscricaoSemDigito + digito1 + digito2 == inscricaoEstadual;
        }

        public string GerarFake()
        {
            string inscricaoSemDigito = "07";
            Random rnd = new Random();
            for (int i = 0; i < 9; i++)
            {
                inscricaoSemDigito += rnd.Next(0, 9).ToString();
            }

            string d1 = DigitoVerificador.ObterDigitoMod11("0" + inscricaoSemDigito, peso).ToString();
            string d2 = DigitoVerificador.ObterDigitoMod11(inscricaoSemDigito + d1, peso).ToString();
            return inscricaoSemDigito + d1 + d2;
        }
    }
}
