using System.ServiceModel;
using System.ServiceModel.Web;

namespace WebWCF
{
    // OBSERVAÇÃO: Você pode usar o comando "Renomear" no menu "Refatorar" para alterar o nome da interface "ISampleService" no arquivo de código e configuração ao mesmo tempo.
    [ServiceContract]
    public interface ISampleService
    {
        [OperationContract]
        [WebGet]
        string SampleMethod(string msg);
    }
}
