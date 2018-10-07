using SistemasBR.SerializadorTx2.Atributos;
using System;
using System.Linq;

namespace SistemasBR.SerializadorTx2
{
    public class SerializadorTx2
    {
        public static string Serializar(object objeto)
        {
            if (objeto == null) throw new ArgumentNullException(nameof(objeto));

            var tipo = objeto.GetType();

            var propriedades = tipo.GetProperties();

            var resposta = "";

            var cabecaclho = "";

            foreach (var atributoClasse in tipo
                .CustomAttributes
                .Where(a => a.AttributeType == typeof(Tx2CabecalhoAttribute)))
            {
                foreach (var argumentosConstrutor in atributoClasse.ConstructorArguments)
                {
                    cabecaclho = argumentosConstrutor.Value.ToString();
                    break;
                }
            }

            foreach (var propriedade in propriedades)
            {
                if (propriedade.CustomAttributes.All(a => a.AttributeType != typeof(Tx2CampoAttribute)))
                    continue;

                foreach (var atributo in propriedade
                    .CustomAttributes
                    .Where(a => a.AttributeType == typeof(Tx2CampoAttribute)))
                {
                    foreach (var argumentosConstrutor in atributo.ConstructorArguments)
                    {
                        resposta += $"{argumentosConstrutor.Value}={propriedade.GetValue(objeto)}\n";
                        break;
                    }
                }
            }

            return $"INCLUIR{cabecaclho}\n{resposta}SALVAR{cabecaclho}";
        }
    }
}
