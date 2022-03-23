using System.Collections.Generic;
using System.Linq;

namespace DevApp.Business.Core.Notificacoes
{
    public class Notificador : INotificador
    {
        private List<Notificacao> _notificacoes;

        public Notificador()
        {
            _notificacoes = new List<Notificacao>(); //Recebe uma nova instancia (lista vazia) de notificacao serão preenchidas conforme os erros forem aparecendo.
        }

        public void Handle(Notificacao notificacao)
        {
            _notificacoes.Add(notificacao);
        }

        public List<Notificacao> ObterNotificacao()
        {
            return _notificacoes;
        }

        public bool TemNotificacao()
        {
            return _notificacoes.Any();
        }
    }
}
