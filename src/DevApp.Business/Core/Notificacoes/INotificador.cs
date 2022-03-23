using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevApp.Business.Core.Notificacoes
{
   public interface INotificador
    {
        bool TemNotificacao();

        List<Notificacao> ObterNotificacao();

        void Handle(Notificacao notificacao);  //Manipular quando uma notificacao for lançada
    }
}
