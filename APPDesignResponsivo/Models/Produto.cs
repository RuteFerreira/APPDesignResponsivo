using System;

namespace APPDB.Models {
    public class Produto {

        public enum TipoEstado { Inativo, Ativo, Apagado };

        private Guid _id;
        private string _designacao;
        private decimal _pUnitario;
        private decimal _stkAtual;
        private TipoEstado _estado;

        public Guid Id {
            get { return _id; }
            set { 
                //Só atualizo o id se este estiver VAZIO
                if(_id == Guid.Empty) _id = value; 
            }
        }

        public string Designacao {
            get { return _designacao; }
            set { 
                _designacao = value;
                if (_designacao.Length == 0) _designacao = "A definir";
            }
        }

        public Decimal PUnitario {
            get { return _pUnitario; }
            set {
                _pUnitario = value;
                if (_pUnitario < 0) _pUnitario = 0;
            }   
        }

        public Decimal StkAtual {
            get { return _stkAtual; }
            set {
                _stkAtual = value;
                if (_stkAtual < 0) _stkAtual = 0;
            }
        }

        public TipoEstado Estado { 
            get { return _estado; }
            set { _estado = value; }
        }

        public Produto () {
            Id = Guid.Empty;        //EmptyID = modo inserção
            Designacao = "";
            PUnitario = 0;
            StkAtual = 0;
            Estado = TipoEstado.Ativo;
        }
    }
}
