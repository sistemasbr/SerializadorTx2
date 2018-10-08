using SistemasBR.SerializadorTx2.Atributos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace SistemasBR.SerializadorTx2
{
    public class SerializadorTx2
    {
        public static string Serializar(object objeto)
        {
            if (objeto == null) throw new ArgumentNullException(nameof(objeto));

            var tipo = objeto.GetType();

            var resposta = "";

            var cabecalho = "";

            var atributosClasse = tipo.CustomAttributes as IList<CustomAttributeData> ?? tipo.CustomAttributes.ToList();

            if (atributosClasse.All(a => a.AttributeType != typeof(Tx2CabecalhoAttribute)))
                throw new CustomAttributeFormatException(
                    $"A classe deve conter o atributo \"{nameof(Tx2CabecalhoAttribute)}\" (Type: {typeof(Tx2CabecalhoAttribute)})");

            var atributoCabecalhoClasse = atributosClasse.First();

            foreach (var argumentosConstrutor in atributoCabecalhoClasse.ConstructorArguments)
                cabecalho = argumentosConstrutor.Value.ToString();




            var propriedades = tipo.GetProperties();

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

            return $"INCLUIR{cabecalho}\n{resposta}SALVAR{cabecalho}";
        }
    }
}
