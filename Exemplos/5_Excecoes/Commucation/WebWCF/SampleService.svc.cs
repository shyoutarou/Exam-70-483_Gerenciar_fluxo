using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WebWCF
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da classe "SampleService" no arquivo de código, svc e configuração ao mesmo tempo.
    // OBSERVAÇÃO: Para iniciar o cliente de teste do WCF para testar esse serviço, selecione SampleService.svc ou SampleService.svc.cs no Gerenciador de Soluções e inicie a depuração.
    public class SampleService : ISampleService
    {
        public string SampleMethod(string msg)
        {
            return "The service greets you: " + msg;
        }
    }
}
